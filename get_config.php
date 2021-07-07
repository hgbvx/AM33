<?php
    $myfile = fopen("config.txt","r") or die ("Unable to open file!");
    $line = fread($myfile, "60");
    fclose($myfile);

    echo $line;
?>