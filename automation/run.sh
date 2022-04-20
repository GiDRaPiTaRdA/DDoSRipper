#!/bin/bash

YELLOW='\033[0;33m'
NC='\033[0m' # No Color
GREEN='\033[0;32m'
BLUE='\033[0;34m'
RED='\033[0;31m'

REDL='\033[1;31m'
YELLOWL='\033[1;33m'
BLUEL='\033[1;34m'
GREENL='\033[0;32m'

automation_dir="automation"
config_dir="$automation_dir/bin/.config"


# banner
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
echo -e "${BLUEL}                                                                                               Kali Linux${NC}"
}

# print banner
print $RED $YELLOW

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


# pull updater
echo -e "${YELLOW}git pull update.sh${NC}"
#wget -N -P "automation" "https://raw.githubusercontent.com/GiDRaPiTaRdA/DDoSRipper/master/update.sh"
chmod +x automation/update.sh

sleep 1

# run update
./automation/update.sh

# loading animation
#./automation/bin/interface/showsplash.sh

echo -e "\n${RED}Read configs${NC}"
turbo=($(cat "$config_dir/turbo.cfg"))
timeout=($(cat "$config_dir/timeout.cfg"))

echo -e "${BLUEL}READ $config_dir/turbo.cfg	${RED}TURBO	: $turbo${NC}"
echo -e "${BLUEL}READ $config_dir/timeout.cfg	${RED}TIMEOUT	: $timeout${NC}\n" 

#cd "$automation_dir/"

./automation/bin/startauto.sh -t $turbo -o $timeout
