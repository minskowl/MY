<?php // no direct access

defined('_JEXEC') or die('Restricted access');

include('header.php');
?>

<table cellspacing="0" cellpadding="0" border="0">
    <tbody>
    <tr>
        <td width="713px">
            <h1 class="h1_gray">
                Cписок проведенных ТО
            </h1>
        </td>
        <td width="232px" class="pers_right">
        </td>
    </tr>
    <tr>
        <td>
            <table>
                <tr>
                    <td class="reg_number">Оборудование</td>
                    <td> <?=$this->info['liftName']?></td>
                </tr>
                <tr>
                    <td class="reg_number">Регистрационный номер:</td>
                    <td> <?=$this->info['reg_number']?></td>
                </tr>
                <tr>
                    <td class="reg_number">Площадка:</td>
                    <td> <?=$this->info['plName']?> / <?=$this->info['plAddress']?></td>
                </tr>
                <tr>
                    <td class="reg_number">Дата последнего ТО:</td>
                    <td><?=$this->info['date']?> </td>
                </tr>
            </table>
            <br/>
        </td>
        <td class="pers_right"></td>
    </tr>
    </tbody>
</table>

<table cellspacing="0" cellpadding="0" border="0">
    <tbody>
    <tr>
        <td width="30px">
        </td>
        <td width="653px">
            <table width="100%" cellspacing="0" cellpadding="2px" style="border: 0000">
                <tbody>
                <tr>
                    <td class="personal_head">
                        <r>дата ТО</r>
                    </td>
                    <td class="personal_head">
                        <r>номер ТО</r>
                    </td>
                    <td class="personal_head">
                        <r>механик</r>
                    </td>
                    <td class="personal_head">
                        <r>статус</r>
                    </td>
                </tr>
                <? foreach ($this->rows as $elem) { ?>
                <tr style="text-align: center;">
                    <td class="personal_liftname">
                        <?=$elem['date']?>
                    </td>
                    <td class="personal_liftname">
                        <?=$elem['id']?>
                    </td>
                    <td class="personal_tablist">
                        <?=$elem['engineer_id']?>
                    </td>
                    <td class="personal_tablist" style="color: #457a00;">
                        Выполнено
                    </td>
                </tr>
                    <? } ?>
                <tr>
                    <td class="personal_back"><a href="index.php?option=com_reports&task=showlift&id=<?=$this->info['id']?>">назад к списку заявок</a><br/></br></td>
                </tr>
                </tbody>
            </table>

        </td>
        <td width="30px">
        </td>
        <td width="232px" class="pers_right">
        </td>
    </tr>
    </tbody>
</table>
<?php include('footer.php');?>
