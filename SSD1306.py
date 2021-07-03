import time
import subprocess

from board import SCL, SDA
import busio
from PIL import Image, ImageDraw, ImageFont
import adafruit_ssd1306
import json

i2c = busio.I2C(SCL, SDA)

disp = adafruit_ssd1306.SSD1306_I2C(128,32,i2c)

disp.fill(0)
disp.show()

width = disp.width
height = disp.height
image = Image.new("1", (width,height))

draw = ImageDraw.Draw(image)
draw.rectangle((0, 0, width, height), outline=0, fill=0)
padding = -2
top = padding
bottom = height - padding
x = 0
font = ImageFont.load_default()

while True:

    
    draw.rectangle((0, 0, width, height), outline=0, fill=0)
    
    cmd = "hostname -I | cut -d' ' -f1"
    IP = subprocess.check_output(cmd, shell=True).decode("utf-8")

    cmd = "sudo /home/pi/Documents/Projekt/new"
    json_string = subprocess.check_output(cmd, shell=True).decode("utf-8")
    string = json.loads(json_string)

    draw.text((x, top + 0), "Temperatura: " + str(string[1]["value"]), font=font, fill=255)
    draw.text((x, top + 8), "Cisnienie: " + str(string[2]["value"]), font=font, fill=255)
    draw.text((x, top + 16), "Naswietlenie: " + str(string[0]["value"]), font=font, fill=255)
    draw.text((x, top + 25), "IP: " + IP, font=font, fill=255)

    disp.image(image)
    disp.show()
    time.sleep(1)
