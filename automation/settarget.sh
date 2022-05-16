#!/bin/bash

YELLOW='\033[0;33m'
CYAN='\033[0;36m'
RED='\033[0;31m'
NC='\033[0m' # No Color


echo -e "${CYAN}Target $1:$2${NC}"
echo $1 $2 > automation/target.txt

sudo ./automation/publish.sh

