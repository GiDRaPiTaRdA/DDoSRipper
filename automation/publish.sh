#!/bin/bash

cd automation
sha1sum proxychains.conf > proxychains.conf.sha1
cd ..

git fetch
git add .
git status
git commit -m "proxy"
git push origin master
