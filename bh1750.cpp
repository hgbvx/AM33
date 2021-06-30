extern "C" {
	#include <wiringPiI2C.h>
}
#include <stdio.h>
#define BH1750ADDR 0x23

int main (void) {
   int handle = wiringPiI2CSetup(BH1750ADDR) ; //Numer pinu (jeśli podłączymy ADDR do 3,3V trzeba zmienić na 0x5c)
   wiringPiI2CWrite(handle,0x10);
   int word=wiringPiI2CReadReg16(handle,0x00); //odczyt
   int lux=((word & 0xff00)>>8) | ((word & 0x00ff)<<8); //notacja odwrotna, zmieniamy bajty
   printf("Natężenie światła w jednostkach LUX:%d \n",lux); //wynik w jednostkach LUX
   return 0;
 }
