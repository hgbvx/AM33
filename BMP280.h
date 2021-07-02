#ifndef BMP250_h
#define BMP250_h

#include <stdio.h>
#include <errno.h>
#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <stdint.h>

//WiringPi
#include "wiringPi.h"
#include "wiringPiI2C.h"

/* Adresy BMP280 */
#define BMP280_I2C_ADDR 0x76 // Adres z i2cdetect
#define BMP280_CHIP_ID 0xD0

/* Kalibracja BMP280 */
//Temperatura
#define   BMP280_DIG_T1 0x88  /*    Unsigned Calibration data (16 bits) */
#define   BMP280_DIG_T2 0x8A  /*    Signed Calibration data (16 bits) */
#define   BMP280_DIG_T3 0x8C  /*    Signed Calibration data (16 bits) */
//Cisnienie
#define   BMP280_DIG_P1 0x8E  /*    Unsigned Calibration data (16 bits) */
#define   BMP280_DIG_P2 0x90  /*    Signed Calibration data (16 bits) */
#define   BMP280_DIG_P3 0x92  /*    Signed Calibration data (16 bits) */
#define   BMP280_DIG_P4 0x94  /*    Signed Calibration data (16 bits) */
#define   BMP280_DIG_P5 0x96  /*    Signed Calibration data (16 bits) */
#define   BMP280_DIG_P6 0x98  /*    Signed Calibration data (16 bits) */
#define   BMP280_DIG_P7 0x9A  /*    Signed Calibration data (16 bits) */
#define   BMP280_DIG_P8 0x9C  /*    Signed Calibration data (16 bits) */
#define   BMP280_DIG_P9 0x9E  /*    Signed Calibration data (16 bits) */

/* Rejestry*/
#define   BMP280_CONTROL      0xF4
#define   BMP280_RESET        0xE0
#define   BMP280_CONFIG       0xF5
#define   BMP280_PRESSUREDATA 0xF7
#define   BMP280_TEMPDATA     0xFA

#define SWAP_2BYTES(x) (((x & 0xFFFF) >> 8) | ((x & 0xFF) << 8))

/* Przypisanie wartosci i typow do zmiennych z karty katalogowej */
extern unsigned short int dig_T1;
extern short int dig_T2;
extern short int dig_T3;

extern unsigned short int dig_P1;
extern short int dig_P2;
extern short int dig_P3;
extern short int dig_P4;
extern short int dig_P5;
extern short int dig_P6;
extern short int dig_P7;
extern short int dig_P8;
extern short int dig_P9;

/*Functions*/
void load_calibration(int fd);
int read_raw(int fd, int reg);
signed long int compensate_temp(int adc_T);
float read_temperature(int fd);
double read_pressure(int fd);


#endif
