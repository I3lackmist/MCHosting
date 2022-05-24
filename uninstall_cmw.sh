sudo cat /etc/apache2/ports.conf | grep -v "Listen 20000" | sudo dd of=/etc/apache2/ports.conf
sudo a2dissite mccmw
sudo rm /etc/apache2/sites-available/mccmw.conf
sudo rm -rf /var/www/mccmw/
sudo systemctl reload apache2
