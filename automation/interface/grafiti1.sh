#!/bin/bash

YELLOW='\033[0;33m'
NC='\033[0m' # No Color
GREEN='\033[0;32m'
BLUE='\033[0;34m'
RED='\033[0;31m'
CYAN='\033[0;36m'
ORANGE='\033[0;33m'

REDL='\033[1;31m'
YELLOWL='\033[1;33m'
BLUEL='\033[1;34m'
GREENL='\033[0;32m'
CYANL='\033[1;36m'
ORANGEL='\033[1;33m'

grafiti="$(wget -qO- https://raw.githubusercontent.com/GiDRaPiTaRdA/DDoSRipper/master/grafiti.txt)"

function print() {

  color=$1
  color1=$2

echo -e "\n"
echo -e "----------------------------------------------------------------------------------------------------------"
echo -e "${color}"

echo " (     (           (    (                                  ("
echo " )\ )  )\ )        )\ ) )\ )                               )\ )                             )"
echo -e "(()/( (()/(       (()/((()/( (                   (   (    (()/(     )    (               ( /(    (   ("
echo " /(_)) /(_))   (   /(_))/(_)))\  \`  )   \`  )    ))\  )(    /(_)) ( /(   ))\   (      (   )\())  ))\  )("
echo -e "(${color1}_${color}))${color1}_${color} (${color1}_${color}))${color1}_${color}    )\ (${color1}_${color})) (${color1}_${color})) ((${color1}_${color}) /(/(   /(/(   /((_)(()\  (${color1}_${color}))   )(_)) /((_)  )\ )   )\ ((${color1}_${color})\  /((_)(()\ "
echo -e "${color1} |   \ |   \ ${color} ((${color1}_${color})${color1}/ __|| _ \ (_)${color}((${color1}_${color})${color1}_${color}\ ((${color1}_${color})${color1}_${color}\ (${color1}_${color}))   ((${color1}_${color}) ${color1}| | ${color}  ((${color1}_${color})${color1}_${color} (${color1}_${color}))(  ${color1}_${color}(${color1}_${color}/(  ((${color1}_${color})${color1}| |${color}(${color1}_${color})(${color1}_${color}))   ((${color1}_${color})${color1}"
echo -n " | |) || |) |/ _ \\\\__ \|   / | || '_ \)| '_ \)/ -_) | '_| | |__ / _\` || || || ' \\"
echo -e "${color}))${color1}/ _| | ' \ / -_) | '_|"
echo -e " |___/ |___/ \___/|___/|_|_\ |_|| .__/ | .__/ \___| |_|   |____|\__,_| \_,_||_||_| \__| |_||_|\___| |_|"
echo -e "                                |_|    |_|"

echo -e "${NC}"
echo -e "----------------------------------------------------------------------------------------------------------"
echo -e "${BLUEL}												Kali Linux${NC}"
}

#print $BLUEL $YELLOW

#print $GREENL $BLUEL

print $RED $YELLOW

#print $GREENL $YELLOW
