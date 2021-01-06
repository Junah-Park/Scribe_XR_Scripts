<?php

	$con = mysqli_connect('localhost', 'root', 'root', 'unityaccess');

	//validate connection
	if(mysqli_connect_errno())
	{
		echo "1: Connection failed"; //error code 1: connection failed
		exit();
	}

	$username = mysqli_real_escape_string($con, $_POST["name"]);
	//SQL injection prevention
	$usernameclean = filter_var($username, FILTER_SANITIZE_STRING, FILTER_FLAG_STRIP_LOW | FILTER_FLAG_STRIP_HIGH);
	$password = $_POST["password"];

	//validate name
	$namecheckquery = "SELECT id, username, salt, hash, score FROM users WHERE username='" . $usernameclean . "';";

	$namecheck = mysqli_query($con, $namecheckquery) or die("2: Username query failed"); //error code 2: name query fails

	if(mysqli_num_rows($namecheck)!=1)
	{
		echo "5: More or less than a single username"; //error code 5: more or less than 1 username
		exit();
	}

	//get login info
	$existinginfo = mysqli_fetch_assoc($namecheck);
	$salt = $existinginfo["salt"];
	$hash = $existinginfo["hash"];

	$loginhash = crypt($password, $salt);
	if ($hash != $loginhash)
	{
		echo "6: Incorrect password"; //error code 6: password does not hash to match table
		exit();
	}

	echo "0\t" . $existinginfo["score"] . "\t" . $existinginfo["id"];

?>