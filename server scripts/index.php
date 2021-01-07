<?php
  $url = parse_url(getenv("CLEARDB_DATABASE_URL"));

  $server = $url["host"];
  $username = $url["user"];
  $password = $url["pass"];
  $db = substr($url["path"], 1);

  $conn = new mysqli($server, $username, $password, $db); 
// sql to create table
  // $sql = "CREATE TABLE messages ( 
  //   id INT(10) PRIMARY KEY, 
  //   message VARCHAR(280) NOT NULL
  //   )";

  // echo $sql;

  // if ($conn->query($sql) === TRUE) {
  //   echo "Table messages created successfully";
  // } else {
  //   echo "Error creating table: " . $conn->error;
  // }

?>