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

# loading animation
#cd interface/
./automation/bin/interface/showsplash.sh
#cd ..

echo -e "${YELLOW}git pull update.sh${NC}"
#wget -N -P "automation" "https://raw.githubusercontent.com/GiDRaPiTaRdA/DDoSRipper/master/update.sh"
chmod +x automation/update.sh

sleep 1

./automation/update.sh

turbo=($(cat "$config_dir/turbo.cfg"))
timeout=($(cat "$config_dir/timeout.cfg"))

echo -e "${BLUEL}READ $config_dir/turbo.cfg	${RED}TURBO	: $turbo${NC}"
echo -e "${BLUEL}READ $config_dir/timeout.cfg	${RED}TIMEOUT	: $timeout${NC}\n" 

#cd "$automation_dir/"

./automation/bin/startauto.sh -t $turbo -o $timeout
