#!/usr/bin/python
import json
from bh1750 import readLight
from bmp280 import readBME280All


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
print("{\"sensors\":[" + temp_string + "," + press_string + ',' + luxx_string + "]}")
