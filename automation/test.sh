#!/bin/bash

declare RESULT=($(wget -qO- https://raw.githubusercontent.com/GiDRaPiTaRdA/DDoSRipper/master/automation/target.txt)) 

printf '%s\n' "${RESULT[@]}"
