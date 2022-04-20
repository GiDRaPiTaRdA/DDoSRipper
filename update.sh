#!/bin/bash

YELLOW='\033[0;33m'
NC='\033[0m' # No Color
GREEN='\033[1;32m'
BLUE='\033[1;34m'

repo="https://raw.githubusercontent.com/GiDRaPiTaRdA/DDoSRipper/master/"

# const dirs
subdir="bin"
automation_dir="automation"
config_dir=".config"
interface_dir="interface"


download () {
  filename=$1
  reporoot=$2
  targetdir=$3
  isexecutable=$4

  mkdir -p $targetdir

  echo -e "${YELLOW}git pull $targetdir$filename${NC}"
  wget -N -P $targetdir "${repo}${reporoot}/$filename"

  if [ $isexecutable = true ]; then
    chmod +x "$targetdir/$filename"
  fi

}

# automation
remoteroot="$automation_dir"
localroot="${subdir}"
download "gettarget.sh" $remoteroot $localroot true
download "updateproxies.sh" $remoteroot $localroot true
download "loopconnect.sh" $remoteroot $localroot true
download "startauto.sh" $remoteroot $localroot true

# automatiom/.config
remoteconfig="$automation_dir/$config_dir"
localconfig="${subdir}/$config_dir"
download "turbo.cfg"  $remoteconfig $localconfig false
download "timeout.cfg" $remoteconfig $localconfig false

# automation/interface
remoteinterface="$automation_dir/$interface_dir"
localinterface="${subdir}/$interface_dir"
download "grafiticolored.sh" $remoteinterface $localinterface true
download "showsplash.sh" $remoteinterface $localinterface true


# automation/.config
echo -e "ls $localconfig/$BLUE"
ls $localconfig
echo -e "${NC}"

# automation/interface
echo -e "ls $localinterface/$GREEN"
ls $localinterface
echo -e "${NC}"

# automation
echo -e "ls  $localroot/$GREEN"
ls $localroot
echo -e "${NC}"
