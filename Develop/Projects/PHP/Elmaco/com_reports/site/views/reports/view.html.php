<?php

jimport( 'joomla.application.component.view');


class ReportsViewReports extends JView
{
	function display($tpl = null)
	{
        $task=$this->getModel()->getState()->task;
        $doc =& JFactory::getDocument();

		if($task=="showlift")
		{
			$this->assignRef( 'rows',$this->get("showlift"));
            $info=$this->get( 'LiftInfo' );
			$this->assignRef( 'info',$info);

            $doc->setTitle($info['type'].' '.$info['liftName'].' '.$info['reg_number']);
		}
		else if($task=="showrequest")
		{
            $info=$this->get( 'RequestInfo' );
			$this->assignRef( 'info',$info);
            $this->getInfo('files','RequestFiles');
            $doc->setTitle('заявка № '.$info[0]['requestId']);
		}
        else if($task=="showpaymentlist")
        {
            $this->assignRef( 'rows',$this->get( 'PaymentList' ));
        }
        else if($task=="archiveDocuments")
        {
            $this->assignRef( 'rows',$this->get( 'ArchiveDocuments' ));
            $this->assign( 'title',"Архив документов");
            $doc->setTitle("Архив документов");
        }
        else if($task=="showto")
        {
            $this->assignRef( 'rows',$this->get( 'ToList' ));
            $info=$this->get( 'LiftInfo' );
            $this->assignRef( 'info',$info);
            $doc->setTitle("Cписок проведенных ТО");
        }
        else if($task=="unpaidBills")
        {
            $this->assignRef( 'rows',$this->get( 'UnpaidBills' ));
            $this->assign( 'title',"Список неоплаченных счетов");
            $doc->setTitle("Список неоплаченных счетов");
        }
        else if($task=="requestlist")
        {
            $this->assignRef( 'rows',$this->get( 'RequestList' ));
            $doc->setTitle("Список заявок");
        }
		else
		{
			$this->assignRef( 'rows',$this->get("EquipmentList"));
            $doc->setTitle("Список оборудования");
		}

		$this->assignRef( 'company',$this->get( 'CompanyInfo' ));

		parent::display($tpl);
	}
    function getInfo($key, $method)
    {
        $this->assignRef( $key,$this->get($method));
    }
}
?>
