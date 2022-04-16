#!/bin/bash

while getopts s:p:t:o: flag
do
    case "${flag}" in
        s) server=${OPTARG};;
        p) port=${OPTARG};;
        t) turbo=${OPTARG};;
	o) timeout=${OPTARG};;
    esac
done

while :
do
    sudo ./updateproxies.sh
    sudo timeout ${timeout} ./loopconnect.sh -t $turbo -p $port -s $server
done
