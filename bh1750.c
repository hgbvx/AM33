#include <stdio.h>
#include <wiringPi.h>
#include <wiringPiI2C.h>
#include <unistd.h>

#define bh1750 0x23

int main (int argc, char *argv[])
{
        int fd;
	
		short data1=wiringPiI2CReadReg16(fd,0x1b00);
		unsigned char *bytes = (unsigned char *)&data1;
		data1 = (bytes[0] << 8) | bytes[1];
		
		
        wiringPiSetup () ;
        fd=wiringPiI2CSetup (bh1750) ;  /*Use i2cdetect command to find your respective device address*/
        if(fd==-1)
        {
                printf("Can't setup the I2C device\n");
                return -1;
        }
        else
        {
                while (1)
                {
			sleep(1);
                        
				data1=wiringPiI2CReadReg16(fd,0x1b00);
                               //data2=wiringPiI2CRead(fd);  //this part returns 0 down know why
                       if(data1==-1)
                        {
                                printf("No data\n");
                                return -1;
                        }
                        else
                        {      
			printf("data=%f\n", data1);
			}
                }
        }
        return 0;
}
