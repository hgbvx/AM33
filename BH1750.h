#ifndef BH1750_h
#define BH1750_h
#include <stdio.h>
#include <wiringPiI2C.h>
#include <unistd.h>

#define BH1750_ADDR 0x23

short read_lux(int fd);

#endif
