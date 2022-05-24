if [ "$EUID" -ne 0 ]
    then echo "Please run as root"
    exit
fi

sudo systemctl stop ioa
sudo systemctl disable ioa

sudo rm /etc/systemd/system/ioa.service
sudo rm -rf /var/www/ioa