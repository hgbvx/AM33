#include "BH1750.h"

short read_lux(int fd) {
	short data = wiringPiI2CReadReg16(fd,0x1b00);
	unsigned char *bytes = (unsigned char *)&data;
	data = (bytes[0] << 8) | bytes[1];
	return data;
}


       
