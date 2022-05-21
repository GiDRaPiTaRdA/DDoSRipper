#!/bin/bash

YELLOW='\033[0;33m'
NC='\033[0m' # No Color

while getopts t: flag
do
    case "${flag}" in
        t) delay=${OPTARG};;
    esac
done

if [ -z "$delay" ]; then
    delay="300"
fi

while :
do
    ./automation/proxy/checkproxy.sh
    ./automation/publish.sh
    echo -e "${YELLOW}Wait $delay${NC}"
    read -t $delay
done
