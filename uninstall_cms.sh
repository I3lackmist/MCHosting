#!/bin/bash
if [ "$EUID" -ne 0 ]
    then echo "Please run as root"
    exit
fi

#Stop apache from listening to port 20001
# sudo cat /etc/apache2/ports.conf | grep -v "Listen 20001" | sudo dd of=/etc/apache2/ports.conf
#Stop apache from listening to port 20002
# sudo cat /etc/apache2/ports.conf | grep -v "Listen 20002" | sudo dd of=/etc/apache2/ports.conf

#Stop mccms service and remove config file
sudo systemctl stop mccms
sudo systemctl disable mccms
sudo rm /etc/systemd/system/mccms.service

#Remove mccms from apache and remove the source files
sudo a2dissite mccms
sudo systemctl reload apache2
sudo rm /etc/apache2/sites-available/mccms.conf
sudo rm -rf /var/www/mccms

#Remove the system user
sudo userdel mccms