#include "BH1750.h"
#include "BMP280.h"

int main (int argc, char *argv[])
{
	int fd;
	
	/*BH1750*/		
    fd=wiringPiI2CSetup(BH1750_ADDR);
	
	wiringPiI2CWrite(fd,0x10);
	printf("[{\"type\": \"Intensity\",\"value\": %d, \"unit\":\"Lux\"},",read_lux(fd));
	
	wiringPiSetup();


	/*BMP280*/
	fd = wiringPiI2CSetup(BMP280_I2C_ADDR);

	load_calibration(fd);
	wiringPiI2CWriteReg8(fd, BMP280_CONTROL, 0x3F);


	printf("{\"type\": \"Temperature\",\"value\": %5.2f, \"unit\":\"C\"},",read_temperature(fd));
	printf("{\"type\": \"Pressure\",\"value\": %5.2f, \"unit\":\"hPa\"}]",read_pressure(fd)/100);
	
	return 0;
}
