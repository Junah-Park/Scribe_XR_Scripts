<?php
	$url = parse_url(getenv("CLEARDB_DATABASE_URL"));

	$server = $url["host"];
	$username = $url["user"];
	$password = $url["pass"];
	$db = substr($url["path"], 1);

	$conn = new mysqli($server, $username, $password, $db);


	//validate connection
	if(mysqli_connect_errno())
	{
		echo "1: Connection failed"; //error code 1: connection failed
		exit();
	}

	$id = $_POST["id"];
	$message = $_POST["message"];

	$insertmessagequery = "REPLACE INTO messages (id, message) VALUES (" . $id . ", '" . $message . "');";
	mysqli_query($conn, $insertmessagequery) or die("7: Replace message query failed"); //error code 7: message query failed

	echo ("0");
?>