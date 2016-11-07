<?php
defined('_JEXEC') or die('Restricted access');
jimport('joomla.application.component.controller');

/**
 * Hello World Component Controller
 *
 * @package		HelloWorld
 */
class ReportsController extends JController
{

	function __construct()
	{
		parent::__construct();

		// Register Extra tasks
		$this->registerTask( 'showlift','ShowLift' );
		$this->registerTask( 'showrequest','ShowRequest' );
        $this->registerTask( 'showpaymentlist','ShowPaymentList' );
        $this->registerTask( 'archiveDocuments','ShowArchiveDocuments' );
        $this->registerTask( 'unpaidBills','ShowUnpaidBills' );
        $this->registerTask( 'requestlist','ShowRequestList' );
        $this->registerTask( 'showto','ShowTo' );
        $this->registerTask( 'download','DownloadFile' );
	}

	/**
	 * Method to display the view
	 *
	 * @access	public
	 */
	function display()
	{
       $this->Process();
	}

    function DownloadFile()
    {
       // ob_clean();
       // ob_end_clean();
         $model=$this->getModel();

        $f=$model->getFileName($_GET['file']);

        if($f)
        {
            $file=JPATH_BASE.'/data/reports/payments/'.$f['file_name'];
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
                return;
            }
        }


            header($_SERVER["SERVER_PROTOCOL"]." 404 Not Found");


    }
    function ShowTo()
    {
        JRequest::setVar( 'layout', 'tolist'  );
        $this->Process();
    }

	function ShowLift()
	{
       JRequest::setVar( 'layout', 'lift'  );
       $this->Process();
	}

	function ShowRequest()
	{
       JRequest::setVar( 'layout', 'request'  );
       $this->Process();
	}

    function ShowRequestList()
    {
        JRequest::setVar( 'layout', 'requestlist'  );
        $this->Process();
    }

    function ShowPaymentList()
    {
        JRequest::setVar( 'layout', 'paymentList'  );
        $this->Process();
    }

    function ShowArchiveDocuments()
    {
        JRequest::setVar( 'layout', 'archivedocuments'  );
        $this->Process();
    }

    function ShowUnpaidBills()
    {
        JRequest::setVar( 'layout', 'archivedocuments'  );
        $this->Process();
    }

    function Process()
    {
	    $user= & JFactory::getUser();
	    if($user->id>0)
	    {
            try
            {
                parent::display();
                return;
            }
	        catch(Exception $ex)
            {
                $this->setRedirect('index.php?option=com_reports',null);
                return;
            }
	    }

		$this->setRedirect('login.html', "доступно только авторизованным пользователям");

    }
}
?>
