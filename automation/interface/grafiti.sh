#!/bin/bash

YELLOW='\033[0;33m'
NC='\033[0m' # No Color
GREEN='\033[0;32m'
BLUE='\033[0;34m'
RED='\033[0;31m'
CYAN='\033[0;36m'
ORANGE='\033[0;33m'

REDL='\033[1;31m'
YELLOWL='\033[1;33m'
BLUEL='\033[1;34m'
GREENL='\033[0;32m'
CYANL='\033[1;36m'
ORANGEL='\033[1;33m'

grafiti="$(wget -qO- https://raw.githubusercontent.com/GiDRaPiTaRdA/DDoSRipper/master/grafiti.txt)"

color=${YELLOW}

echo -e "\n"
echo -e "----------------------------------------------------------------------------------------------------------"
echo -e "${color}"
echo "$grafiti"
echo -e "${NC}"
echo -e "----------------------------------------------------------------------------------------------------------"
echo -e "${BLUEL}												Kali Linux${NC}"

