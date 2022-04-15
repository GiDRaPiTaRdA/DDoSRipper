#!/bin/bash

IFS=$'\n' read -d '' -r -a lines < target.txt

server=${lines[0]}
port=${lines[1]}

echo "Server ${server}"
echo "Port ${port}"
