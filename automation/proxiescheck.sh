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
    delay="15m"
fi

while :
do
    C:/Users/Maxim/Documents/Source/web/ProxySocket/TestConcole/bin/Debug/TestConcole.exe ip socks5 400 C:/Users/Maxim/Documents/Source/web/prox/good/socks5.csv true true
    ./publish.sh
    echo -e "${YELLOW}Wait $delay${NC}"
    sleep $delay
done
