#!/bin/bash

YELLOW='\033[0;33m'
NC='\033[0m' # No Color
GREEN='\033[0;32m'
BLUE='\033[0;34m'
RED='\033[0;31m'

REDL='\033[1;31m'
YELLOWL='\033[1;33m'
BLUEL='\033[1;34m'
GREENL='\033[0;32m'

automation_dir="automation"
config_dir="$automation_dir/.config"

uri="https://raw.githubusercontent.com/GiDRaPiTaRdA/DDoSRipper/master/automation/run.sh"

echo -e "${YELLOW}git pull run.sh${NC}"
wget -N -P "$automation_dir/" "$uri"
#cd $automation_dir
chmod +x automation/run.sh

./automation/run.sh
