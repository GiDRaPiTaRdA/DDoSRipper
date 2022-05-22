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


sourcefile="targets/rawtargets.txt"
targetfile="targets/targets.txt"

ipregex='[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}'

echo -e "${BLUEL}Parse IPs${NC}"
grep -Eo $ipregex $sourcefile > $targetfile
echo -e "${GREENL}Found $(wc -l $targetfile)${NC}"

ports="20,21,22,25,53,80,443,110,123,143,465,631,993,995"

echo -e "${BLUEL}NMap $targetfile${NC}"
proxychains nmap -Pn -iL $targetfile --open -p $ports
