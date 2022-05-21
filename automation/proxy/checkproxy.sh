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

#proxy=$1
timeout=20
url1=http://icanhazip.com
url=https://www.google.com/
retry=2

protocol="socks5"
sourcefile="automation/proxy/data/socks5.csv"
targetfile="automation/proxy/data/parcedproxies.txt"
proxychainsrawconfig="automation/proxy/data/proxychains-raw.conf"
proxychainsconfig="automation/proxy/data/proxychainstest.conf"

ipregex='[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}:[0-9]{2,5}'

echo -e "${BLUEL}Parse proxies...${NC}"
proxies=($(grep -Eo $ipregex $sourcefile))
echo -e "${GREENL}Found ${#proxies[@]} entries${NC} $sourcefile"

proxychekedfile=/tmp/proxychecked.txt
touch $proxychekedfile
echo -n "" > $proxychekedfile

function check(){
    proxy=$1
    for ((i = 1 ; i <= $retry ; i++));
    do
       # curl -I -s -m 10 --socks5 5.161.100.145:1080 http://icanhazip.com | head -n 1
       response=$(curl -I -s -m $timeout --socks5 $proxy $url | head -n 1)

       if [ ! -z "$response" ]
       then
           echo -e "${GREEN}Connected${YELLOW} $(echo $response | awk '{print $2}') $(echo $response | awk '{print $1}')${NC} $proxy"
           echo $proxy >> $proxychekedfile
           break
       fi

       #echo "retry $i"
    done
}

echo -e "\\n${RED}Check proxies${NC} $url"
# now loop through the above array
for proxy in "${proxies[@]}"
do
     check $proxy &
done

wait

#readarray -t proxiesverified < $proxychekedfile
proxiesverified=($(cat $proxychekedfile))
rm $proxychekedfile

echo -e "${NC}Verified ${#proxiesverified[@]}"



# Export proxychains.conf
cp $proxychainsrawconfig $proxychainsconfig
echo "" >> $proxychainsconfig
for proxyv in "${proxiesverified[@]}"
do
     # format proxy
     printf "socks5 %s\n" "$(echo $proxyv | tr ":" " ")" >> $proxychainsconfig
done

echo "Exported to $proxychainsconfig"


# Publish
./automation/publish.sh
