#!/bin/bash

YELLOW='\033[0;33m'
RED='\033[0;31m'
GREEN='\033[0;32m'
GRAY='\033[0;30m'
NC='\033[0m' # No Color

repo=https://raw.githubusercontent.com/GiDRaPiTaRdA/DDoSRipper/master
repoproxy=${repo}/automation/proxy/data/proxychains.conf
tmp=/tmp/proxychains.conf
proxy=/etc/proxychains.conf

echo -e "${YELLOW}Fetch proxychains.conf${NC}"
sudo wget $repoproxy -O $tmp

if [ -s $tmp ]
then
    echo -e "${GREEN}copy from ${NC}$tmp${GREEN} to ${NC}${proxy}"
    sudo cp $tmp $proxy
else
    echo -e "${tmp} ${RED}does not exist or empty${NC}"
fi

if [ -e $tmp ]
then
    echo -e "${RED}remove${NC} $tmp"
    sudo rm $tmp
fi
