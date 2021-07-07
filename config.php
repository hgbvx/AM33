<?php 

    $port =$_GET["port"];
    $api = $_GET["api"];
    $sampling = $_GET["sampling"];
    $samples = $_GET["samples"];

    $config = array("port"=>intval($port), "api"=>floatval($api), "sampling"=>floatval($sampling), "samples"=>intval($samples));

    $json = json_encode($config);

    $myfile = fopen("config.txt","w") or die ("Unable to open file!");

    fwrite($myfile, $json);

    fclose($myfile);
	
	header("Location: http://192.168.0.102/app.html");


?>