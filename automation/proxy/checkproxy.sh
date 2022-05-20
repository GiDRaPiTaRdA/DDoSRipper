#!/bin/bash

proxy=$1
timeout=20

response=$(curl -s -m $timeout --socks5 $proxy http://icanhazip.com)

echo $response
