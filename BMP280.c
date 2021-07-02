#include "BMP280.h"

unsigned short int dig_T1 = 27504;
short int dig_T2 = 26435;
short int dig_T3 = -1000;

unsigned short int dig_P1 = 36477;
short int dig_P2 = -10685;
short int dig_P3 = 3024;
short int dig_P4 = 2855;
short int dig_P5 = 140;
short int dig_P6 = -7;
short int dig_P7 = 15500;
short int dig_P8 = -14600;
short int dig_P9 = 6000;

void load_calibration(int fd)
{
	dig_T1 = wiringPiI2CReadReg16(fd, BMP280_DIG_T1);
	dig_T2 = wiringPiI2CReadReg16(fd, BMP280_DIG_T2);
	dig_T3 = wiringPiI2CReadReg16(fd, BMP280_DIG_T3);
	dig_P1 = wiringPiI2CReadReg16(fd, BMP280_DIG_P1);
	dig_P2 = wiringPiI2CReadReg16(fd, BMP280_DIG_P2);
	dig_P3 = wiringPiI2CReadReg16(fd, BMP280_DIG_P3);
	dig_P4 = wiringPiI2CReadReg16(fd, BMP280_DIG_P4);
	dig_P5 = wiringPiI2CReadReg16(fd, BMP280_DIG_P5);
	dig_P6 = wiringPiI2CReadReg16(fd, BMP280_DIG_P6);
	dig_P7 = wiringPiI2CReadReg16(fd, BMP280_DIG_P7);
	dig_P8 = wiringPiI2CReadReg16(fd, BMP280_DIG_P8);
	dig_P9 = wiringPiI2CReadReg16(fd, BMP280_DIG_P9);
}

int read_raw(int fd, int reg)
{
	int raw = SWAP_2BYTES(wiringPiI2CReadReg16(fd,reg));
	raw <<= 8;
	raw = raw | wiringPiI2CReadReg8(fd, reg + 2);
	raw >>= 4;
	return raw;
}

signed long int compensate_temp(int adc_T)
{
	int var1 = (((adc_T>>3) - (dig_T1<<1)) * (dig_T2)) >> 11;
	int var2 = (((((adc_T>>4) - (dig_T1)) * ((adc_T>>4) - (dig_T1))) >> 12) *(dig_T3)) >> 14;
	return var1+var2; 
}

float read_temperature(int fd)
{
  int raw_temp = read_raw(fd, BMP280_TEMPDATA);
  int compensated_temp = compensate_temp(raw_temp);
  return (float)((compensated_temp * 5 + 128) >> 8) / 100;
}

double read_pressure(int fd)
{
  int raw_temp = read_raw(fd, BMP280_TEMPDATA);
  signed long int compensated_temp = compensate_temp(raw_temp);
  int raw_pressure = read_raw(fd, BMP280_PRESSUREDATA);

  signed long long int p1 = compensated_temp/2 - 64000;
  signed long long int p2 = p1 * p1 * (signed long long int)dig_P6/32768;
  signed long long int buf = (p1 * (signed long long int)dig_P5*2);
  p2 += buf << 17;
  p2 += (signed long long int)dig_P4 << 35;
  p1 = ((p1 * p1 * dig_P3) >> 8) + ((p1 * dig_P2) << 12);
  p1 = (((signed long long int)1 << 47) + p1) * ((signed long long int)dig_P1) >> 33;

  // Avoid exception caused by division by zero
  if (0 == p1)
  {
    return 0;
  }

  signed long long int p = 1048576 - raw_pressure;
  p = (((p << 31) - p2) * 3125) / p1;
  p1 = ((signed long long int)dig_P9 * (p >> 13) * (p >> 13)) >> 25;
  p2 = ((signed long long int)dig_P8 * p) >> 19;
  p = ((p + p1 + p2) >> 8) + (((signed long long int)dig_P7) << 4);

  return (double)(p / 256);
}
