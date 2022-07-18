<?php

$name = $_POST['name'];
$email = $_POST['email'];
$subject = $_POST['subject'];
$message = $_POST['message'];

$response = mail($email,$subject . '- from : ' . $name , $message);

if($response){
    $json = array(
        'type'=>'success',
        'message'=>'Contact form has bin sent.',
    );
    echo json_encode($json);
}else{
    $json = array(
        'type'=>'warning',
        'message'=>'Can not send contact form.',
    );
    echo json_encode($json);
}