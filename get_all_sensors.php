<?php
	header("Content-Type: application/json");
	

chdir("/home/pi/Documents/Projekt");
$myfile = fopen("data.txt", "r") or die("Unable to open file!");
echo fread($myfile,filesize("data.txt"));
fclose($myfile);

	
?>
