<?php 

	if(isset($_GET["arg1"]) && $_GET["arg1"] != NULL)
		$arg1 = ' -' . $_GET["arg1"];
	
	if(isset($_GET["arg2"]) && $_GET["arg2"] != NULL)
		$arg2 = ' -' . $_GET["arg2"];
		
	if(isset($_GET["arg3"]) && $_GET["arg3"] != NULL)
		$arg3 = ' -' . $_GET["arg3"];
		
	if(isset($_GET["arg4"]) && $_GET["arg4"] != NULL)
		$arg4 = ' -' . $_GET["arg4"];
	
	chdir('/home/pi/Documents/Projekt/');
	$cmd = 'sudo python3 display.py ' .$arg1 . $arg2 . $arg3 . $arg4;

	echo $cmd;

	$res = shell_exec($cmd);	

?>
