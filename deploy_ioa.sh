#!/bin/bash

if [ "$EUID" -ne 0 ]
    then echo "Please run as root"
    exit
fi

sudo systemctl stop ioa
sudo systemctl disable ioa

sudo rm /etc/systemd/system/ioa.service
sudo rm -rf /var/www/ioa

sudo mkdir /var/www/ioa
sudo cp ./IOA/Assets/ioa.service /etc/systemd/system/ioa.service

sudo cp ./Keys/ipz.key /var/www/ioa/ipz.key

sudo cp ./IOA/* /var/www/ioa/
sudo cp ./IOA/src/* /var/www/ioa

if [ ! $(sudo cat /etc/passwd | grep "ioa" ) ]; then
    sudo useradd -m ioa
fi

sudo chown -R ioa /var/www/ioa

sudo systemctl enable ioa
sudo systemctl start ioa