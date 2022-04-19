#!/bin/bash

YELLOW='\033[0;33m'
NC='\033[0m' # No Color
GREEN='\033[1;32m'
BLUE='\033[1;34m'

repo="https://raw.githubusercontent.com/GiDRaPiTaRdA/DDoSRipper/master/"

automation_dir="automation"
config_dir="$automation_dir/.config"

download () {
  filename=$1
  reporoot=$2
  targetdir=$3
  isexecutable=$4

  mkdir -p $targetdir

  echo -e "${YELLOW}git pull $filename${NC}"
  wget -N -P $targetdir "${repo}${reporoot}/$filename"

  if [ $isexecutable = true ]; then
    chmod +x "$targetdir/$filename"
  fi

}

download "gettarget.sh" $automation_dir $automation_dir true
download "updateproxies.sh" $automation_dir $automation_dir true
download "loopconnect.sh" $automation_dir $automation_dir true
download "startauto.sh" $automation_dir $automation_dir true

download "turbo.cfg" $config_dir $config_dir false
download "timeout.cfg" $config_dir $config_dir false

echo -e "ls $automation_dir$GREEN"
ls $automation_dir
echo -e "${NC}"

echo -e "ls $config_dir$BLUE"
ls $config_dir
echo -e "${NC}"


