#!/bin/bash

YELLOW='\033[0;33m'
NC='\033[0m' # No Color

while getopts s:p:t:o: flag
do
    case "${flag}" in
        s) serverinpt=${OPTARG};;
        p) portinpt=${OPTARG};;
        t) turbo=${OPTARG};;
	o) timeout=${OPTARG};;
    esac
done

if [ -z $turbo ]; then
    turbo="300"
fi

if [ -z $timeout ]; then
    turbo="30m"
fi

while :
do
    sudo ./automation/bin/updateproxies.sh

    if [ -z $serverinpt ] || [ -z $portinpt ]; then
        echo -e "${YELLOW}GET target from GIT${NC}"

        declare target=($(./automation/bin/gettarget.sh))

        server=${target[0]}
        server="${server:0:${#server}-1}"

        port=${target[1]}

        #server="79.142.100.87"
        #port="80"

        echo "Server $server : $port"
    else
	server=$serverinpt
        port=$portinpt
    fi

    read -t 5 -p 'Wait 5s to startaauto'

    sudo timeout ${timeout} ./automation/bin/loopconnect.sh -t ${turbo} -p ${port} -s ${server}
done
