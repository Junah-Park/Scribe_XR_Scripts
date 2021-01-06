<?php
	$con = mysqli_connect('localhost', 'root', 'root', 'unityaccess');

	//validate connection
	if(mysqli_connect_errno())
	{
		echo "1: Connection failed"; //error code 1: connection failed
		exit();
	}

	$id = $_POST["id"];
	$message = $_POST["message"];

	$insertmessagequery = "REPLACE INTO messages (id, message) VALUES (" . $id . ", '" . $message . "');";
	mysqli_query($con, $insertmessagequery) or die("7: Replace message query failed"); //error code 7: message query failed

	echo ("0");
?>