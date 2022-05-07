#!/bin/bash

YELLOW='\033[0;33m'
CYAN='\033[0;36m'
RED='\033[0;31m'
NC='\033[0m' # No Color


function check(){

hash=$1
filename=$2
hs="$hash $filename"
echo $hs | sha1sum --quiet --status -c -
return $?
}

repo="https://raw.githubusercontent.com/GiDRaPiTaRdA/DDoSRipper/master"
url="${repo}/automation/proxy/data/proxychains.conf.sha1"

filename=/etc/proxychains.conf

echo -e "${CYAN}Start proxy hash check${NC}"
echo -e "${YELLOW}URL $url${NC}\n"

#run
while :
do

  result=$(wget -qO- $url)

  if [ -z "$result" ]
  then
    echo -e "${RED}No hash $(date)${NC}"
  else
    hash=$(echo $result | awk '{print $1;}')

    echo -e "${CYAN}$hash${NC}"
    check $hash $filename

    if [ $? -eq 0 ]
    then
      echo "Hash matches $(date)"
    else
      echo "Hash does not match $(date)"
      echo "Update..."
      sudo ./automation/bin/updateproxies.sh
    fi
  fi

  delay="5m"
  #echo "Wait $delay"
  read -t 300

done

