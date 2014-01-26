#!/usr/bin/env python

import StringIO
import os
import subprocess
import re
import shutil
import sys
import glob

xcode_project_name = 'UnityCI_xcode';
cwd = os.path.dirname(os.path.realpath(__file__))  # Note we can't use getcwd() because this file will be called from places other than it's own directory

logfile = open('headless-builder.log', 'w+')
logfile.seek(0)
logfile.truncate()

def log( *args ):
	""" Wraps calls to print to logfile as well """
	print(args)
	for param in args:
		logfile.write(str(param) + "\n")

def ios_fix_facebook_paths():
	""" Fix strange issue with facebook paths - not sure what is causing this yet """
	pbxfile = open(cwd+xcode_project_name+'/Unity-iPhone.xcodeproj/project.pbxproj', 'r+')
	pbxstring = pbxfile.read()

	# Remove extra uneeded backups
	regex = re.compile(re.escape('../../../../../../Facebook/'))
	new_pbxstring = regex.sub("../../../build/Assets/Facebook/", pbxstring)

	# DEV - print matches
	log("Looking for matches", regex.pattern)
	for m in regex.finditer(pbxstring):
	    log(m.start(), m.group())

	# Overwrite the file
	pbxfile.seek(0)
	pbxfile.truncate()
	pbxfile.write(new_pbxstring)
	pbxfile.close()

# Launch unity builder
subprocess.call(['/Applications/Unity/Unity.app/Contents/MacOS/Unity', 
	'-quit',
	'-batchmode',
	'-executeMethod', 'BuilderScript.BuildDevAndProduction', 
	'-projectPath', cwd])

ios_fix_facebook_paths()