#!/bin/bash

sha1sum proxychains.conf > proxychains.conf.sha1

git fetch
git add .
git status
git commit -m "proxy"
git push origin master
