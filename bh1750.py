#!/usr/bin/python
import smbus
import time

DEVICE = 0x23
ONE_TIME_HIGH_RES_MODE = 0x20

bus = smbus.SMBus(1)

def convertToNumber(data):
    return ((data[1] + (256 * data[0])) / 1.2)

def readLight(addr=DEVICE):
    data = bus.read_i2c_block_data(addr,ONE_TIME_HIGH_RES_MODE)
    return convertToNumber(data)
