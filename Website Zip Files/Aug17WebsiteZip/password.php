<?php

	$servername = "localhost"; // Original: localhost
	$serverusername = "id14241042_bramesh"; //  root
	$serverpassword = "!#l^JH6jDu9_3&)6"; //  root
	$dbname = "id14241042_viewpoint_experiment"; // unity_access

	$con = mysqli_connect($servername, $serverusername, $serverpassword, $dbname);

	// check that connection happened
	if (mysqli_connect_errno())
	{
		echo "1: Connection failed."; // error code #1 - connection failed.
		exit();
	}

	$password = $_POST["password"];

	// Check if ID is already is already in database
	$passwordcheckquery = "SELECT password FROM password WHERE password='" . $password . "';";

	$passwordcheck = mysqli_query($con, $passwordcheckquery) or die("2: Password check query failed."); // error code #2 - id check query failed

	if (mysqli_num_rows($passwordcheck) == 0)
	{
		echo "3: Invalid password."; // error code #3 - ID has already been used
		exit();
	}

	echo("0"); // Everything worked properly

?>