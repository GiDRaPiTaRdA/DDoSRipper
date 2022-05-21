#!/bin/bash

proxy=$1
timeout=20
url1=http://icanhazip.com
url=https://www.google.com/
retry=4

sourcefile="automation/proxy/data/socks5.csv"
targetfile="automation/proxy/data/parcedproxies.txt"

ipregex='[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}:[0-9]'

echo -e "${BLUEL}Parse IPs${NC}"
grep -Eo $ipregex $sourcefile > $targetfile
echo -e "${GREENL}Found $(wc -l $targetfile)${NC}"

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
