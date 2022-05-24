#!/bin/bash

#Deploys or redeploys mccms
if [ "$EUID" -ne 0 ]
    then echo "Please run as root"
    exit
fi

#Make apache listen to port 20001
if [ $(sudo cat /etc/apache2/ports.conf | grep "Listen 20001" | wc -l) -eq 0 ]; then
    sudo bash -c "echo 'Listen 20001' >> /etc/apache2/ports.conf"
fi

#Make apache listen to port 20002
if [ $(sudo cat /etc/apache2/ports.conf | grep "Listen 20002" | wc -l) -eq 0 ]; then
    sudo bash -c "echo 'Listen 20002' >> /etc/apache2/ports.conf"
fi

#Disable mccms service
sudo systemctl stop mccms
sudo systemctl disable mccms

#Remove mccms service file
sudo rm /etc/systemd/system/mccms.service

#Check if mccms is enabled in apache and disable if so
if [ $(sudo ls /etc/apache2/sites-enabled -l | grep "mccms.conf" | wc -l) -ne 0 ]; then
    sudo a2dissite mccms
    sudo systemctl reload apache2
fi

#Remove mccms.conf from apache2 sites
if [ $(sudo ls /etc/apache2/sites-available -l | grep "mccms.conf" | wc -l) -ne 0 ]; then
    sudo rm /etc/apache2/sites-available/mccms.conf
fi

#Remove previous installation
sudo rm -rf /var/www/mccms

#Add mccms.conf to apache2 sites
sudo cp ./CMS/Assets/mccms.conf /etc/apache2/sites-available/mccms.conf

#Add mccms service config file
sudo cp ./CMS/Assets/mccms.service /etc/systemd/system/mccms.service

#Deploy
sudo mkdir /var/www/mccms
sudo dotnet publish ./CMS/CMS.Application/CMS.Application.csproj -c Release -o /var/www/mccms
sudo touch /var/www/mccms/log

#Create DB file
sudo sqlite3 /var/www/mccms/mc.db < ./DB/recreate_db.sql

#Copy cert and key files
sudo cp ./Keys/ipz.crt /var/www/mccms/ipz.crt
sudo cp ./Keys/ipz.key /var/www/mccms/ipz.key

#Check if mccms sysuser exists and create if no such user exists
if [ ! $(sudo cat /etc/passwd | grep "mccms" ) ]; then
    sudo useradd --system --no-create-home mccms
fi

#Set file ownership to service account
sudo chown -R mccms /var/www/mccms

#enable apache modules
sudo a2enmod rewrite
sudo a2enmod proxy
sudo a2enmod proxy_http
sudo a2enmod headers
sudo a2enmod ssl
sudo systemctl reload apache2

#Enable apache2 site and relaod
sudo a2ensite mccms
sudo systemctl reload apache2

#Enable mccms service
sudo systemctl enable mccms
sudo systemctl start mccms