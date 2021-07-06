#!/usr/bin/python
import sys
import getopt

tflag = 0
pflag = 0
lflag = 0
iflag = 0

sysarg = sys.argv[1:]

try:
	opts, args = getopt.getopt(sysarg, ':tpli')
except getopt.GetoptError as err:
	print (err)
	sys.exit(1)


for opt, arg in opts:
	if opt in '-t':
		tflag = 1
	elif opt in '-p':
		pflag = 1
	elif opt in '-l':
		lflag = 1
	elif opt in '-i':
		iflag = 1


f = open("display.txt", "w")
	
if tflag:
	f.write("t")
	print("t")
if pflag:
	f.write("p")
	print("p")
if lflag:
	f.write("l")
	print("l")
if iflag:
	f.write("i")
	
f.close()


