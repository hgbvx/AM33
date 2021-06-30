#!/usr/bin/python
import json
from bh1750 import readLight
from bmp280 import readBME280All


temperature,pressure,humidity = readBME280All()
lux = readLight()
print(pressure)
