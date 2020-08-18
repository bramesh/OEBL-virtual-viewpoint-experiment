<?php

	$servername = "localhost"; // Original: localhost
	$serverusername = "id14241042_bramesh"; // root
	$serverpassword = "!#l^JH6jDu9_3&)6"; // root
	$dbname = "id14241042_viewpoint_experiment"; //unity_access

	$con = mysqli_connect($servername, $serverusername, $serverpassword, $dbname);

	// check that connection happened
	if (mysqli_connect_errno())
	{
		echo "1: Connection failed."; // error code #1 - connection failed.
		exit();
	}

	$id = $_POST["id"];

	// Check if ID is already is already in database
	$idcheckquery = "SELECT id FROM subjects WHERE id=" . $id . ";";

	$idcheck = mysqli_query($con, $idcheckquery) or die("2: ID check query failed."); // error code #2 - id check query failed

	if (mysqli_num_rows($idcheck) > 0)
	{
		echo "3: ID already exists."; // error code #3 - ID has already been used
		exit();
	}

	// Add subject to the table
	//$insertuserquery = "INSERT INTO subjects (id) VALUES (" . $id . ");";
	//mysqli_query($con, $insertuserquery) or die("4: Insert subject query failed."); // error code #4 - inserting subject ID failed.

	echo("0"); // Everything worked properly

?>