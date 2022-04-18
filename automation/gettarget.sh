#!/bin/bash


while getopts o:u: flag
do
    case "${flag}" in
        o) output=${OPTARG};;
        u) urlout=${OPTARG};;
    esac
done

uri="https://raw.githubusercontent.com/GiDRaPiTaRdA/DDoSRipper/master/automation/target.txt"

declare target=($(wget -qO- --no-cache --no-cookies --no-check-certificate $uri))

server=${target[0]}
port=${target[1]}

#fix url
server="${server:0:${#server}-1}"
#server=$(echo $server|tr -d '\n')
#server=${server%$'\n'}

# output URL
if [ "$urlout" == "true" ]; then
    echo "URL ${uri}"
fi

# output details
if [ "$output" == "true" ]; then

    echo "Server ${server}"
    echo "Port ${port}"

    echo "All $server : $port"
fi

# output for return
if [ -z $urlout ] && [ -z $output ]; then
    printf '%s\n' "${target[@]}"
fi
