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

	$username = $_POST["name"];
	$password = $_POST["password"];

	//validate name
	$namecheckquery = "SELECT username FROM users WHERE username='" . $username . "';";

	$namecheck = mysqli_query($conn, $namecheckquery) or die("2: Username query failed"); 
	//error code 2: name query fails

	if(mysqli_num_rows($namecheck)>0)
	{
		echo "3: Username already exists"; //error code 3: name exists
		exit();
	}

	//add user to the table
	$salt = "\$5\$rounds=5000\$" . "steamedhams" . $username . "\$";
	$hash = crypt($password, $salt);
	$insertuserquery = "INSERT INTO users (username, hash, salt) VALUES ('" . $username . "', '" . $hash . "', '" . $salt . "');";

	$conn->query($insertuserquery) or die("4: Insert user query failed"); //error code 4: insert query failed

	echo "0";
?>