#!/bin/bash

proxy=$1
timeout=20
url1=http://icanhazip.com
url=https://www.google.com/
retry=4


for i in {1..4}
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
