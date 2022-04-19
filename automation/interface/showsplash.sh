#!/bin/bash

./grafiti1.sh


# loading animation
delay=0.02
char="- "
sleep $delay
echo -n "Loading $char"
for i in {1..48}
do
   sleep $delay
   echo -n "$char"
done
echo -e "\n"

