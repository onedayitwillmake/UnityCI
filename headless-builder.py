#!/usr/bin/env python

import StringIO
import os
import subprocess
import re
import shutil
import sys
import glob
import zipfile

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
	pbxfile = open(cwd+"/_builds/"+xcode_project_name+'/Unity-iPhone.xcodeproj/project.pbxproj', 'r+')
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

# def zipdir(path, zip):
# 	print(path);
#     for root, dirs, files in os.walk(path):
#         for file in files:
#             zip.write(os.path.join(root, file))

# def make_ipa:
# 	zipf = zipfile.ZipFile(xcode_project_name+'.ipa', 'w')
#     zipdir(cwd+"/_builds/"+xcode_project_name+'/bin/UnityCI.app', zipf)
#     zipf.close()

def upload_to_testflight:
	subprocess.call(['curl', 
		'http://testflightapp.com/api/builds.json',
		'-F', 'file=@UnityCI.ipa',
		'-F', "api_token='58fcc2ee02546d6fe43389734d83d406_MTI0OTA2MDIwMTMtMDgtMjAgMTA6Mzk6MzEuODg1ODcy'",
		'-F', "team_token='2f2b8d10daec740a15818a57edc26e28_MjYxODc4MjAxMy0wOC0yMCAxMDo0MToyNC43NjExNDk'",
		'-F', "notes='This build was uploaded via the upload API'",
		'-F', "notify=False"])

# Launch unity builder
if( sys.argv[1] == "-upload"):
	subprocess.call(['/Applications/Unity/Unity.app/Contents/MacOS/Unity', 
		'-quit',
		'-batchmode',
		'-executeMethod', 'BuilderScript.BuildDevAndProduction', 
		'-projectPath', cwd])
	ios_fix_facebook_paths()
else:
	# make_ipa()
	upload_to_testflight()