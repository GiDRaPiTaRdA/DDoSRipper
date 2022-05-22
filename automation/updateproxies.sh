#!/bin/bash

YELLOW='\033[0;33m'
RED='\033[0;31m'
NC='\033[0m' # No Color

repo=https://raw.githubusercontent.com/GiDRaPiTaRdA/DDoSRipper/master
repoproxy=${repo}/automation/proxy/data/proxychains.conf
tmp=/tmp/proxychains.conf
proxy=/etc/proxychains.conf

echo -e "${YELLOW}Fetch proxychains.conf${NC}"
sudo wget $repoproxy -O $tmp

if [ -e x.txt ]
then
    sudo cp $tmp $proxy
    sudo rm $tmp
else
    echo -e "${RED}${tmp} does not exist${NC}"
fi

