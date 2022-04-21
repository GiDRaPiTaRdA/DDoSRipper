#!/bin/bash

#ps -axjf -a

#sudo pkill -9 -f -c "launcher.sh"


function killprocess(){
pname=$1

id=$(pgrep -f $pname)

if [ ! -z "$id" ]
	then
	echo "Kill $pname"
        echo $id
	kill -9 $id
	#sudo pkill -TERM -P $id
fi

#processes=$(ps aux | grep "$pname")
#echo "$processes"

#ids=$(echo $processes | awk '{print $2}')

#echo $ids
#sudo kill ${ids}

}

killprocess "launcher.sh"
killprocess "run.sh"
killprocess "startauto.sh"
killprocess "loopconnect.sh"
killprocess "DRipper.py"

# ps -ef -H
# ps aux -h process view hierarchy
# pkill -TERM -P 27888
# sudo kill -9 17070
# pgrep -f "launcher.sh"
# sudo kill -9 $(pgrep -f "launcher.sh")
