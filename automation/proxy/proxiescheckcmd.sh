#!/bin/bash

YELLOW='\033[0;33m'
NC='\033[0m' # No Color

get=1000
retry=4
parser=ip
protocol=socks5
nopausecall=true
ascii=false
proxyfile="automation/proxy/data/socks5.csv"

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
    ./automation/proxy/TestConcole.exe $parser $protocol $get $proxyfile $nopausecall $ascii $retry
    ./automation/publish.sh
    echo -e "${YELLOW}Wait $delay${NC}"
    read -t $delay
done
