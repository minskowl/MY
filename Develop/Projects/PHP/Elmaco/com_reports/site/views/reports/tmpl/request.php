<?php // no direct access
defined('_JEXEC') or die('Restricted access');
include('header.php');

$urlFile=path2url(str_replace('\\','/',$this->_basePath));

$doc =& JFactory::getDocument();
$doc->addScript( $urlFile."/Tooltip/Tooltip.js" );
$doc->addStyleSheet( $urlFile."/Tooltip/Tooltip.css" );

$d=$this->info[0];
$url=path2url(str_replace('\\','/',$this->_basePath."/file.php")).'?id'.$d['reuestId'].'&year'.$d['year'];

foreach($this->info as $i)
{
    $work[]=$i['work'];
}

//showDump($this->files);
//showDump($d);

$links='<div style="display:none;"><div id="demo0_tip">';

foreach($this->files as $data)
{
    $code=substr($data['name_file'],0,2);
    if($code=='Ak')
        $text=' Акт выполненных услуг ';
    if($code=='Ch')
        $text=' Счет ';
    if($code=='Sf')
        $text=' Счет-фактура ';

    $links.=buildLink($urlFile,$data,$text.$data['id'] )."<br/>";
}

$links.="</div></div>";

function buildLink($url,$d,$text)
{
    return '<a href="' .$url .'/file.php?id=' .$d['id'] . '&year='.$d['year'].'&f='.$d['name_file'].'" target="_blank">'.$text .'</a>';
}

?>

<table cellspacing="0" cellpadding="0" border="0">
    <tbody>
    <tr>
        <td width="713px">
            <table cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td colspan="2"><h1 class="h1_gray">заявка №&nbsp;<?=$d['requestId']?></h1></td>
                </tr>

                <tr>
                    <td class="mehanik">Оборудование:</td>
                    <td> <?=$d['liftName']?></td>
                </tr>
                <tr>
                    <td class="mehanik">Рег. номер:</td>
                    <td> <?=$d['reg_number']?></td>
                </tr>
                <tr>
                    <td class="mehanik">Площадка:</td>
                    <td> <?=$d['plName']?> / <?=$d['plAddress']?></td>
                </tr>
                <tr>
                    <td class="mehanik">Механик:</td>
                    <td> <?=$d['engineer_id']?></td>
                </tr>
                <tr>
                    <td class="mehanik">Дата выполнения:</td>
                    <td><?=$d['date_work']?></td>
                </tr>
 <?if(count($this->files)){?>
                <tr>
                    <td class="mehanik">Рабочий ордер:</td>
                    <td class="personal_down"><?
                        echo('<a href="javascript:void(0)" onclick="this.overlay=true; this.position=4;tooltip.add(this, \'demo0_tip\', true); return false;">
                        Скачать файл</a>');
                        ?></td>
                </tr>
    <?}?>
            </table>
            <table cellspacing="0" cellpadding="0" border="0" class="pers_marg">
                <tr>
                    <td class="pers_description">
                        <r>Описание неисправности</r>
                    </td>
                </tr>
            </table>
            <table cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td class="pers_problemdisc"><?=html_entity_decode(nl2br( $d['description_problem']))?></td>
                </tr>
            </table>
            <table cellspacing="0" cellpadding="0" border="0" class="pers_marg">
                <tr>
                    <td class="pers_description">
                        <r>Выполненые работы</r>
                    </td>
                </tr>
            </table>
            <table cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td class="pers_problemdisc"><?= html_entity_decode(nl2br(implode("<br/>", $work)))?></td>
                </tr>
            </table>
            <table cellspacing="0" cellpadding="0" border="0" class="pers_marg">
                <tr>
                    <td class="pers_description">
                        <r>Рекомендации</r>
                    </td>
                </tr>
            </table>
            <table cellspacing="0" cellpadding="0" border="0" class="pers_info_bottom">
                <tr>
                    <td class="pers_problemdisc"><?=html_entity_decode(nl2br($d['advice']))?></td>
                </tr>
            </table>
            <table cellspacing="0" cellpadding="0" border="0" class="pers_marg">
                <tr>
                    <td><input class="submit_send" type="submit" name="continue" style="cursor: pointer;"
                               value="отправить отзыв об обслуживании"
                              onclick="window.open('http://www.insecom.ru/remarks-offers.html');">
                </tr>
            </table>
        </td>
        <td width="232px" class="pers_right">
        </td>
    </tr>
</table>
<?php
echo($links);
include('footer.php');
?>













































































