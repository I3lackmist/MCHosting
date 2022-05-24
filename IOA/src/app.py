from threading import Thread
from flask import Flask, request, jsonify, make_response
import openstack
from time import sleep
import ansible_runner

class SimpleResponse:
    @staticmethod
    def new(message, status_code):
        return make_response(jsonify(message = message), status_code)

app = Flask(__name__)

privateNetworkName = 'szyszbar'
publicNetworkName = 'public1'
hostsFilePath = './hosts'
serviceFilePath = './mcserver.service'

envvars = dict(
    ANSIBLE_HOST_KEY_CHECKING=False
)

keyFile = open('./ipz.key')
privateKey = keyFile.read()
keyFile.close()

def deleteServer(serverName, ip):
    with open(hostsFilePath, 'r') as hostsFile:
        hostsFileLines = hostsFile.readlines()

    with open(hostsFilePath, 'w') as hostsFile:
        for line in hostsFileLines:
            if (line.strip()!=ip):
                hostsFile.write(line)

    conn.delete_server(serverName, wait = True)

def launchServer(ip, jarDownloadUrl, javaDownloadUrl, maxRam):
    sleep(30)

    serviceFile = open(serviceFilePath, 'r')
    serviceContent = serviceFile.read()
    serviceContent = serviceContent.replace('!MAXRAM!', maxRam)

    r = ansible_runner.run(
        playbook = '/var/www/ioa/run_server.yml',
        inventory = ip,
        ssh_key = privateKey,
        extravars = dict(
            jar_download_url = jarDownloadUrl,
            max_ram = maxRam,
            java_download_url = javaDownloadUrl,
            service_content = serviceContent
        ),
        envvars = envvars
    )

# Post - Make server
# Name, Flavor, Version, jarDownloadUrl, javaDownloadUrl / success
@app.route('/server/create', methods=['POST'])
def httpCreateServer():
    serverRequest = request.get_json()

    imageName = 'debian-10'
    if (serverRequest['flavor'] == 'm1.tiny'):
        imageName = 'cirros'

    try:
        floatingIp = conn.available_floating_ip('public1')['floating_ip_address']
    except:
        return SimpleResponse.new('Server creation failed.', 500)

    try:
        conn.create_server(
            name = serverRequest['name'],
            flavor = serverRequest['flavor'],
            image = imageName,
            network = privateNetworkName,
            ips = list(floatingIp),
            ip_pool = publicNetworkName,
            wait = True,
            key_name = 'services'
        )

        hostsFile = open(hostsFilePath, 'a')
        hostsFile.write(str(floatingIp) + '\n')
        hostsFile.close()

        Thread(target = launchServer, args = (floatingIp, serverRequest['jarDownloadUrl'], serverRequest['javaDownloadUrl'], serverRequest['maxRam'],) ).start()

        return SimpleResponse.new('Server created.', 200)

    except:
        deleteServer(serverRequest['name'], floatingIp)

        return SimpleResponse.new('Server creation failed.', 500)

# Get - Return server IP
# name / success, message(ip)
@app.route('/server/ip', methods=['GET'])
def httpGetServerIp():
    serverName = request.args.get('name')

    return SimpleResponse.new(conn.get_server(serverName)['accessIPv4'], 200)

# Get - Return server state
# ServerName / ServerOffline/GameServerOffline/Online
@app.route('/server/status', methods=['GET'])
def httpGetServerStatus():
    serverName = request.args.get('name')

    status = conn.get_server(serverName)['vm_state']

    if(status == 'active'):
        return SimpleResponse.new('Online', 200)
    else:
        return SimpleResponse.new('Offline', 200)

# Delete - delete server
# name, ip / success, message
@app.route('/server/delete', methods=['DELETE'])
def httpDeleteServer():
    serverName = request.args.get('name')
    ip = request.args.get('ip')

    deleteServer(serverName, ip)

    return SimpleResponse.new('Server deleted', 200)

# Put - change server name
# name, newname / success, message
@app.route('/server/name', methods=['PUT'])
def httpChangeServerName():
    serverName = request.args.get('name')
    newServerName = request.args.get('newname')

    conn.update_server(name_or_id = serverName, name = newServerName)

    return SimpleResponse.new('Name changed', 200)

# Put - change server flavor
# name, flavor / success, message

# Put - change server version. Downgrading should delete map
# name, version / success, message

if __name__ == '__main__':
    conn = openstack.connect(cloud='local')

    app.run(port='20004')