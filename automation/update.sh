#!/bin/bash

YELLOW='\033[0;33m'
NC='\033[0m' # No Color
GREEN='\033[1;32m'
BLUE='\033[1;34m'

repo="https://raw.githubusercontent.com/GiDRaPiTaRdA/DDoSRipper/master/"

# const dirs
subdir="automation/bin"
automation_dir="automation"
config_dir=".config"
interface_dir="interface"

echo -e "${YELLOW}\nUpdating scripts . . .\n${NC}"

download () {
  filename=$1
  reporoot=$2
  targetdir=$3
  isexecutable=$4

  mkdir -p $targetdir

  # local file pass
  filepass="$targetdir/$filename"

  echo -e "${YELLOW}git pull $filepass${NC}"
  wget -N -P $targetdir "${repo}${reporoot}/$filename"

  if [ $isexecutable = true ]; then
    chmod +x $filepass
  fi

}

# automation
remoteroot="$automation_dir"
localroot="${subdir}"
download "gettarget.sh" $remoteroot $localroot true
download "updateproxies.sh" $remoteroot $localroot true
download "loopconnect.sh" $remoteroot $localroot true
download "startauto.sh" $remoteroot $localroot true
download "kill.sh" $remoteroot $localroot true

# automatiom/.config
remoteconfig="$automation_dir/$config_dir"
localconfig="${subdir}/$config_dir"

test -f "${localconfig}/turbo.cfg" && download "turbo.cfg"  $remoteconfig $localconfig false
test -f "${localconfig}/timeout.cfg" && download "timeout.cfg" $remoteconfig $localconfig false

# automation/interface
#remoteinterface="$automation_dir/$interface_dir"
#localinterface="${subdir}/$interface_dir"
#download "grafiticolored.sh" $remoteinterface $localinterface true
#download "showsplash.sh" $remoteinterface $localinterface true

echo -e "\n${YELLOW}Updated files:${NC}"


# automation
echo -e -n "$localroot/$GREEN\t"
echo -e -n $(ls $localroot)
echo -e $NC

# automation/.config
echo -e -n "$localconfig/$BLUE\t"
echo -n $(ls $localconfig)
echo -e $NC

# automation/interface
#echo -e "ls $localinterface/$GREEN"
#ls $localinterface
#echo -e $NC

