#!/bin/bash

lns=$(wget -qO- https://raw.githubusercontent.com/GiDRaPiTaRdA/DDoSRipper/master/automation/target.txt)

IFS=$'\n' read -d '' -r -a lines <<< $lns

server=${lines[0]}
port=${lines[1]}

server="${server:0:${#server}-1}"

echo "Server ${server}"
echo "Port ${port}"

echo "All $server : $port"
