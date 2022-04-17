#!/bin/bash

YELLOW='\033[0;33m'
NC='\033[0m' # No Color

while getopts s:p:t: flag
do
    case "${flag}" in
        s) server=${OPTARG};;
        p) port=${OPTARG};;
        t) turbo=${OPTARG};;
    esac
done

echo -e "${YELLOW}Start loop connect"

echo "Server: $server";
echo "Port: $port";
echo -e "Turbo: $turbo${NC}";

if [ -z $port ]; then
    port="80"
fi

if [ -z $server ]; then
    echo "Server -p is empty"
    exit 0
fi


# go back
cd ..

# run
while :
do
  sudo proxychains python3 DRipper.py -t $turbo -p $port -s $server
done









