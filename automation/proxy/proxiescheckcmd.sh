#!/bin/bash

YELLOW='\033[0;33m'
NC='\033[0m' # No Color

get=1000


while getopts t: flag
do
    case "${flag}" in
        t) delay=${OPTARG};;
    esac
done

if [ -z "$delay" ]; then
    delay="15m"
fi

while :
do
    ./automation/proxy/TestConcole.exe ip socks5 $get automation/proxy/data/socks5.csv true false
    ./automation/publish.sh
    echo -e "${YELLOW}Wait $delay${NC}"
    sleep $delay
done
