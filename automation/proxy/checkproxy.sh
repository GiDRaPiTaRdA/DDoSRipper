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

proxy=$1
timeout=20
url1=http://icanhazip.com
url=https://www.google.com/
retry=4

sourcefile="automation/proxy/data/socks5.csv"
targetfile="automation/proxy/data/parcedproxies.txt"

ipregex='[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}:[0-9]{2,5}'

echo -e "${BLUEL}Parse proxies...${NC}"
proxies=($(grep -Eo $ipregex $sourcefile))
echo -e "${GREENL}Found ${#proxies[@]} entries in $sourcefile ${NC}"


## now loop through the above array
for i in "${proxies[@]}"
do
   echo "$i"
   # or do whatever with individual element of the array
done
exit 0

for i in {1..$retry}
do
   # curl -I -s -m 10 --socks5 5.161.100.145:1080 http://icanhazip.com | head -n 1
   response=$(curl -I -s -m $timeout --socks5 $proxy $url | head -n 1)

   if [ ! -z "$response" ]
   then
       echo "Connected" 
       break      	   #Abandon the loop.
   fi

   echo "retry $i"
done
