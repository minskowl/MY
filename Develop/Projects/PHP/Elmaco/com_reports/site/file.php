<?php
$rr="ыввывыв";
define( '_JEXEC', 1 );
$path=str_replace("components/com_reports/file.php","",str_replace('\\','/',__FILE__));
//echo($path);

define('JPATH_BASE', $path );
define( 'DS', DIRECTORY_SEPARATOR );

require_once ( JPATH_BASE .DS.'includes'.DS.'defines.php' );
require_once ( JPATH_BASE .DS.'includes'.DS.'framework.php' );

JDEBUG ? $_PROFILER->mark( 'afterLoad' ) : null;

/**
 * CREATE THE APPLICATION
 */
$mainframe =& JFactory::getApplication('site');

/**
 * INITIALISE THE APPLICATION
 */
// set the language
$mainframe->initialise();


$params = &JComponentHelper::getParams( 'com_reports' );

$f=getFileName($params);

//showDump($f);
$code=substr($f,0,2);
if($code=='Ak')
    $text='Акт выполненных услуг ';
if($code=='Ch')
    $text='Счет ';
if($code=='Sf')
    $text='Счет-фактура ';
if($f)
{

    $file=JPATH_BASE.$paymentPath.$f.'.pdf';
    $file=  mb_convert_encoding ($file ,"UTF-8" , "Windows-1251" );
//   echo($file."<br/>".file_exists($file));
    if(file_exists($file))
    {
        header('Content-Description: File Transfer');
        header('Content-Type: application/pdf');
        header('Content-Disposition: attachment; filename='.basename($file));
        header('Content-Transfer-Encoding: binary');
        header('Expires: 0');
        header('Cache-Control: must-revalidate, post-check=0, pre-check=0');
        header('Pragma: public');
        header('Content-Length: ' . filesize($file)); //Remove

        readfile($file);

    }
    else
    {
        failSend($file);
    }
}
else
{
failSend($file);
}

function getFileName($params)
{
    mb_internal_encoding("UTF-8");
//    /echo(mb_internal_encoding());
    $dbPath= $params->get( 'dbPath' );
    $paymentPath= $params->get( 'paymentPath' );

    $user=& JFactory::getUser();

    $clientId=$user->getParam('clientId');
    $cs=JPATH_BASE.'/'.$dbPath;
//echo($cs);
    $dbh = new PDO('sqlite:'.$cs); // success
    $id=mysql_real_escape_string($_GET['id']);
    $year=intval($_GET['year']);
    $f=mysql_real_escape_string($_GET['f']);

    $code=substr($id,0,2);
    if($code=="TO" || $code=="OP")
     $sql="select [orders].name_file
FROM [document]
INNER JOIN [orders] ON [orders].[id]=[document].id and [orders].[year]=[document].[year]
INNER JOIN lifts ON document.lift_id = lifts.id
INNER JOIN platform ON lifts.platform_id = platform.id
where [client_id]='".$clientId."' AND [orders].[id]='".$id."' AND orders.[year]=".$year." AND orders.[name_file]='".$f."'";
        else
     $sql="select [orders].name_file
FROM [payments]
INNER JOIN [orders] ON [orders].[id]=[payments].id and [orders].[year]=[payments].[year]
where [client_id]='".$clientId."' AND [orders].[id]='".$id."' AND orders.[year]=".$year." AND orders.[name_file]='".$f."'";

//echo($sql);

    $q=$dbh->query($sql);
   // showDump($q);
    if($q==false)
        return false;
    $res=$q->fetch(PDO::FETCH_COLUMN);
    //showDump($res);
    return $paymentPath.$res;

}

function failSend($file)
{
    header($_SERVER["SERVER_PROTOCOL"]." 404 Not Found");
    echo(404);
    echo($file);
}

function showDump($v)
{
    echo("<pre>");
    var_dump($v);
    echo("</pre>");
}
?>

