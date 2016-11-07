<?php
// Check to ensure this file is included in Joomla!
defined('_JEXEC') or die();
jimport( 'joomla.application.component.model' );

function showDump($v)
{
	echo("<pre>");
	var_dump($v);
	echo("</pre>");
}
function path2url($file)
{
    //echo($file."<br/>".JPATH_BASE."<br/>");
    return JURI::base().str_replace(str_replace('\\','/',JPATH_BASE)."/", '', $file);
}

class ReportsModelReports  extends JModel
{

    private  $client;
    private  $clientId;
    private  $databasePath='./data/reports/test.db';
    private  $id;

   function __construct()
    {
       parent::__construct();

       $this->id=mysql_real_escape_string($_GET['id']);

       $this->user=& JFactory::getUser();
       $this->client=$this->getClientInfo();
       $this->clientId=$this->client['clientId'];

        $params = &JComponentHelper::getParams( 'com_reports' );
        $this->databasePath= $params->get( 'dbPath' );

        $this->user->setParam("clientId",$this->clientId);
    }

    function getClientInfo()
    {
        $db =& JFactory::getDBO();
        $db->setQuery( "SELECT cb_companycontactperson,cb_companycontact,cb_companyid,cb_companyname   FROM  #__comprofiler cp WHERE user_id=".$this->user->id);
  		$tmp= $db->loadRow();


        $res=$this->readRow("SELECT [client].[adress],(SELECT SUM(payments.[sum]) FROM payments WHERE payments.[client_id]=[client].[id] AND [payments].[pay]=0)  as debt
FROM client  WHERE [client].[id]='".$tmp[2]."'");

        $res['person']=$tmp[0];
        $res['contact']=$tmp[1];
        $res['clientId']=$tmp[2];
        $res['name']=$tmp[3];

        $dateUpdate=$this->readRow("SELECT [Value] FROM [Settings] WHERE [Key]='date_refresh'");
        list($year, $month, $day) = split('[/.-]',$dateUpdate['Value']);
        $res['date_update']=$day."-".$month."-".$year;
        //showDump($res);
        return $res;
    }

	function getCompanyInfo()
	{
        return $this->client;
	}


    function getFileName($f)
    {
        return $this->readRow("SELECT [file_name] FROM [payments] where [client_id]='".$this->clientId."' AND [file_name]='".$f."'");
    }

    function getEquipmentList()
    {
        // return  $this->readCsv("./data/Unilevel_Rus_equip.csv");
        //echo("getEquipmentList");
        return $this-> readRows("SELECT
   	   platform.id AS plId,	   platform.name AS plName,  platform.adress AS plAddress,
   	   lifts.name AS liftName, lifts.reg_number, lifts.type, strftime('%d-%m-%Y',lastTo.[date]) as [date],
   	   document.[engineer_id],  lifts.id AS liftId
FROM  platform
INNER JOIN  lifts ON platform.id = lifts.platform_id
LEFT JOIN (SELECT [lift_id], MAX([date]) as [date] FROM document WHERE [type]='TO' GROUP BY [lift_id]) as lastTo ON lastTo.[lift_id]= lifts.id
LEFT JOIN document ON document.[lift_id]=lastTo.[lift_id] AND document.[date]=lastTo.[date] AND document.[type]='TO'
WHERE  platform.client_id='".$this->clientId."'");
    }

    function getLiftInfo()
    {
        //echo("getLiftInfo<br/>");
        $res= $this->readRow("SELECT
     lifts.id,lifts.name AS liftName,lifts.type,
     lifts.reg_number, platform.name AS plName ,  platform.adress AS plAddress,
     strftime('%d-%m-%Y',lastTo.[date]) as [date]
FROM          lifts
INNER JOIN    platform ON lifts.platform_id = platform.id
LEFT JOIN (SELECT [lift_id], MAX([date]) as [date] FROM document WHERE [type]='TO' GROUP BY [lift_id]) as lastTo ON lastTo.[lift_id]= lifts.id
WHERE lifts.id='".$this->id."' AND  platform.client_id='".$this->clientId."'");

        if(!$res)
            throw new AccessException();

        return $res;
    }

    function getShowLift()
    {
        //echo("getShowLift<br/>");
        $res=$this->readRows("SELECT
  strftime('%d-%m-%Y',[document].[date]) as [date] , document.[request_id],request.[year], document.[type], document.[engineer_id],
 request.status,  [request].[date_work]
FROM  document
INNER JOIN request ON document.request_id = request.id AND document.[request_year]=request.[year]
INNER JOIN lifts ON document.lift_id = lifts.id
INNER JOIN platform ON lifts.platform_id = platform.id
WHERE  document.lift_id='".$this->id."' AND  platform.client_id='".$this->clientId."'
ORDER BY  request.[year],document.[request_id]");
        //showDump($res);
        return $res;
    }

    function getRequestFiles()
    {
        //echo("getRequestFiles");
        $year=intval($_GET['year']);
        $res= $this->readRows("
  SELECT [date],[name_file],orders.[id],orders.[year]
FROM document
INNER JOIN [orders] ON [orders].[id]=[document].id and [orders].[year]=[document].[year]
WHERE document.[request_id]='".$this->id."'	AND document.[request_year]=".$year."
ORDER BY document.[date];
");
        return $res;
    }
    function getRequestInfo()
	{
	   //echo("getRequestInfo");
        $year=intval($_GET['year']);
		$res= $this->readRows("
SELECT
lifts.[name] as liftName, lifts.[id] as liftId, lifts.type, lifts.reg_number,
platform.[name] as plName,platform.adress as plAddress,
document.[id] as document_id, document.[year] as document_year, document.[engineer_id], strftime('%d-%m-%Y',document.[date]) as [date],
document.[description_problem], document.[work],document.[advice],
strftime('%d-%m-%Y',request.[date_work]) as [date_work] , request.id as requestId
 FROM request
 INNER JOIN  [document] ON request.[id] =document.[request_id]  and [request].[year]=[document].request_year
INNER JOIN lifts ON document.lift_id = lifts.id
INNER JOIN platform ON lifts.platform_id = platform.id
WHERE request.[id]='".$this->id."'	AND request.[year]=".$year." AND platform.client_id='".$this->clientId."'
ORDER BY document.[date];
");

        if(!$res)
            throw new AccessException();

        return $res;
	}


    function getUnpaidBills()
    {
        $res= $this->getCalendarInfo("  SELECT [date],[name_file],orders.[id],orders.[year]
FROM [payments]
INNER JOIN [orders] ON [orders].[id]=[payments].id and [orders].[year]=[payments].[year]
WHERE [client_id]='".$this->clientId."' and pay=0 and SUBSTR([name_file], 0, 2)<>'OP'
ORDER BY [date];");
        //showDump($res);
        return $res;
    }

    function getArchiveDocuments()
    {
        $res= $this->getCalendarInfo("
        SELECT [date],[name_file],orders.[id],orders.[year]
FROM [payments]
INNER JOIN [orders] ON [orders].[id]=[payments].id and [orders].[year]=[payments].[year]
WHERE [client_id]='".$this->clientId."' and SUBSTR([name_file], 0, 2)<>'OP'
ORDER BY [date];");

        //showDump($res);
        return $res;
    }

    function getCalendarInfo($q)
    {
        $tmp= $this->readRows($q);
        //showDump($tmp);
        $currentYear = null;
        $res=array();
        foreach ($tmp as $e)
        {
            list($year, $month, $day) = split('[/.-]', $e['date']);
            $month=intval($month);

            if ($currentYear != $year)
            {
                $currentYear = $year;
                $res[$currentYear]=array();
            }

            $res[$currentYear][$month][]=$e;

        }
        //showDump($res);
        return $res;
    }


    function getRequestList()
    {
        //echo("getRequestList");
        return $this-> readRows("
        select  distinct [platform].[id] as plId,  [platform].[name] as plName,[platform].[adress] as plAddress,
[lifts].[name] as liftName,[lifts].[reg_number],[document].request_id, [document].[request_year],
   strftime('%d-%m-%Y',[request].[date]) as [date],  request.[status],
document.[engineer_id]
from [platform]
inner join [lifts]   ON [platform].[id]=[lifts].[platform_id]
inner join [document] ON [lifts].[id]=[document].[lift_id]
inner join request ON document.request_id= [request].id and document.request_year=[request].[year]
where [platform].[client_id]='".$this->clientId."' AND [document].request_id<>''
order by [plId], request.[date]
        ");
    }

    function getToList()
    {
        return $this-> readRows("SELECT
         strftime('%d-%m-%Y',[document].[date]) as [date],[document].[id],[document].[engineer_id]
FROM [document]
INNER JOIN [lifts] ON [lifts].[id]=[document].[lift_id]
INNER JOIN [platform] ON [platform].[id]=[lifts].[platform_id]
WHERE [document].[lift_id]='".$this->id."' AND [document].[type]='TO' AND [client_id]='".$this->clientId."'
ORDER BY [document].[date]");
    }

    function readRow($sql)
    {
       //echo("<br/>".$sql);
       $dbh = new PDO('sqlite:'.$this->databasePath); // success
       $res= $dbh->query($sql);
        if($res==false)
        {
            showDump($dbh);
            showDump($res);
            echo("<br/>".$sql);
            return false;
        }
        return $res->fetch(PDO::FETCH_ASSOC);
       //showDump($res);
    }

    function readRows($sql)
    {

       $dbh = new PDO('sqlite:'.$this->databasePath); // success
       //echo("<br/><br/><br/><br/>".$sql);
       $res= $dbh->query($sql);
        if($res==false)
        {
            showDump($this->databasePath);
            showDump($res);
            echo("<br/><br/><br/>".$sql);
            return false;
        }
       return $res->fetchAll(PDO::FETCH_ASSOC);
    }

    function convertEncoding($data,$enc)
    {
  	  	foreach ($data as &$val)
	    	$val = mb_convert_encoding($val,"UTF-8",$enc);
	    return $data;
    }


}
