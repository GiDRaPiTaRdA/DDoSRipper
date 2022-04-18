#!/bin/bash

repo="https://raw.githubusercontent.com/GiDRaPiTaRdA/DDoSRipper/master/"
dir="automation"

mkdir $dir

wget -N -P $dir "${repo}automation/gettarget.sh"
wget -N -P $dir "${repo}automation/updateproxies.sh"
wget -N -P $dir "${repo}automation/loopconnect.sh"
wget -N -P $dir "${repo}automation/startauto.sh"

chmod +x "$dir/gettarget.sh"
chmod +x "$dir/updateproxies.sh"
chmod +x "$dir/loopconnect.sh"
chmod +x "$dir/startauto.sh"

ls $dir
