#!/usr/bin/python
import json
import time
from bh1750 import readLight
from bmp280 import readBME280All

while True:

	temperature,pressure,humidity = readBME280All()
	lux = readLight()

	temp = {
		"type" : "Temperature",
		"value": temperature,
		"unit" : "C"
	}
	press = {
		"type" : "Pressure",
		"value": pressure,
		"unit" : "hPa"
	}
	luxx = {
		"type" : "Intensity",
		"value": lux,
		"unit" : "Lux"
	}

	temp_string = json.dumps(temp)
	press_string = json.dumps(press)
	luxx_string = json.dumps(luxx)


	f = open("data.txt", "w")
	f.write("[" + temp_string + "," + press_string + ',' + luxx_string + "]")
	f.close()
	time.sleep(0.2)
