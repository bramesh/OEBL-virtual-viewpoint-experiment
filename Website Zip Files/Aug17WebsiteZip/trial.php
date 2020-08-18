<?php

	$servername = "localhost";
	$serverusername = "id14241042_bramesh"; // root
	$serverpassword = "!#l^JH6jDu9_3&)6"; // root
	$dbname = "id14241042_viewpoint_experiment"; // unity_access

	$con = mysqli_connect($servername, $serverusername, $serverpassword, $dbname);

	// check that connection happened
	if (mysqli_connect_errno())
	{
		echo "1: Connection failed."; // error code #1 - connection failed.
		exit();
	}

	$id = $_POST["id"];
	$trialNumber = $_POST["trialNumber"];
	$polarAngle = $_POST["polarAngle"];
	$azimuthalAngle = $_POST["azimuthalAngle"];
	$indexOfDifficulty = $_POST["indexOfDifficulty"];
	$targetDirection = $_POST["targetDirection"];
	$time = $_POST["time"];
	$distance = $_POST["distance"];
	$path = $_POST["path"];
	$loadTime = $_POST["loadTime"];
	$firstMovement = $_POST["firstMovement"];
	$completionTime = $_POST["completionTime"];

	// Clean spatial variable
	$id = mysqli_real_escape_string($con, $id);
	$trialNumber = mysqli_real_escape_string($con, $trialNumber);
	$polarAngle = mysqli_real_escape_string($con, $polarAngle);
	$azimuthalAngle = mysqli_real_escape_string($con, $azimuthalAngle);
	$indexOfDifficulty = mysqli_real_escape_string($con, $indexOfDifficulty);
	$targetDirection = mysqli_real_escape_string($con, $targetDirection);
	$time = mysqli_real_escape_string($con, $time);
	$distance = mysqli_real_escape_string($con, $distance);
	$path = mysqli_real_escape_string($con, $path);
	$loadTime = mysqli_real_escape_string($con, $loadTime);
	$firstMovement = mysqli_real_escape_string($con, $firstMovement);
	$completionTime = mysqli_real_escape_string($con, $completionTime);

	$insertuserquery = "INSERT INTO subjects VALUES (?,?,?,?,?,?,?,?,?,?,?,?);";

	$stmt = $con->prepare($insertuserquery);
	$stmt->bind_param("ssssssssssss",$id,$trialNumber,$distance,$time,$polarAngle,$azimuthalAngle,$indexOfDifficulty,$targetDirection,$path,$loadTime,$firstMovement,$completionTime);
	$stmt->execute();
	$stmt->close();

	echo json_encode(array("value"=>0));

	mysqli_close($con);

?>