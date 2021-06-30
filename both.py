#!/usr/bin/python
import time
from bh1750 import readLight
from bmp280 import readBME280All


def main():
    while True:
        temperature,pressure,humidity = readBME280All()
        print("Temperature : ", temperature, "C")
        print("Pressure : ", pressure, "hPa")
        print("Light Level: " + str(readLight()) + " lux")
        time.sleep(0.5)

if __name__=="__main__":
    main()
