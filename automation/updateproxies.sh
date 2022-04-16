#!/bin/bash

YELLOW='\033[0;33m'
NC='\033[0m' # No Color

echo -e "${YELLOW}Fetch proxychains.conf${NC}"
sudo wget https://raw.githubusercontent.com/GiDRaPiTaRdA/DDoSRipper/master/automation/proxychains.conf -O /etc/proxychains.conf

