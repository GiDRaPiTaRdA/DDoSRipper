#!/bin/bash

YELLOW='\033[0;33m'
NC='\033[0m' # No Color
repo=https://raw.githubusercontent.com/GiDRaPiTaRdA/DDoSRipper/master
echo -e "${YELLOW}Fetch proxychains.conf${NC}"
sudo wget ${repo}/automation/proxy/data/proxychains.conf -O /tmp/proxychains.conf
sudo cp /tmp/proxychains.conf /etc/proxychains.conf

