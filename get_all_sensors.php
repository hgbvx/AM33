<?php
	header("Content-Type: application/json");
	


	$cmd = 'sudo python3 /home/pi/Documents/Projekt/both_json.py';

	$res = shell_exec($cmd);
	
	echo $res;
?>
