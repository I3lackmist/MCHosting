#!/bin/bash

if [ $(sudo cat /etc/apache2/ports.conf | grep "Listen 20000" | wc -l) -eq 0 ]; then
    sudo bash -c "echo 'Listen 20000' >> /etc/apache2/ports.conf"
fi

sudo a2dissite mccmw
sudo systemctl reload apache2

sudo rm /etc/apache2/sites-available/mccmw.conf
sudo cp ./CMW/Assets/mccmw.conf /etc/apache2/sites-available/mccmw.conf

sudo rm -rf /var/www/mccmw/
cd ./CMW
npm i
npx ng build

cd ..
sudo cp -r ./CMW/dist/cmw/ /var/www/mccmw/
sudo cp ./CMW/Assets/.htaccess /var/www/mccmw/.htaccess
sudo cp ./Keys/ipz.crt /var/www/mccmw/ipz.crt
sudo cp ./Keys/ipz.key /var/www/mccmw/ipz.key
sudo a2ensite mccmw
sudo systemctl reload apache2
