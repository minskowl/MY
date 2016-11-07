<?php // no direct access

defined('_JEXEC') or die('Restricted access');

include('header.php');
?>

<table cellspacing="0" cellpadding="0" border="0">
    <tbody>
    <tr>
        <td width="713px">
            <h1 class="h1_gray">
                cписок заявок
            </h1>

        </td>
        <td width="232px" class="pers_right">
        </td>

    </tr>

    </tbody>

</table>

<table cellspacing="0" cellpadding="0" border="0">
    <tbody>
    <tr>

        <td width="30px">

        </td>

        <td  width="653px">
            <?
            $id = '';
            $firstRow = true;
            $request_id='';

            foreach ($this->rows as $elem) {
                if ($id != $elem['plId']) {
                    $id = $elem['plId'];
                    if ($firstRow == false) {
                        echo('</tbody></table><br/><br/>');
                    }

                    ?>
                <h5><?=$elem['plName']?> / <?=$elem['plAddress']?></h5>
                <table width="100%" cellspacing="0" cellpadding="2px" border="0">
                  <tbody>

                    <tr>

                        <td class="personal_head">
                            <r>дата</r>
                        </td>
                        <td class="personal_head">
                            <r>номер заявки</r>
                        </td>
                        <td class="personal_head">
                            <r>статус</r>
                        </td>
                        <td class="personal_head">
                            <r>оборудование</r>
                        </td>

                        <td class="personal_head">
                            <r>рег. номер</r>
                        </td>
                        <td class="personal_head">
                            <r>механик</r>
                        </td>
                    </tr>
                    <?
                }
                if($request_id==$elem['request_id']) continue;
                $request_id=$elem['request_id'];
                ?>
            <tr style="text-align: center;">
                <td class="personal_liftname">
                    <?=$elem['date']?>
                </td>
                <td class="personal_tablist">
                    <a href="?option=com_reports&task=showrequest&id=<?=$elem['request_id']?>&year=<?=$elem['request_year']?>"><?=$elem['request_id']?></a>
                </td>
                <td class="personal_tablist" style="color: <?=$elem['status']=="Выполнена"? "#457a00;":"#960000;"?>">
                    <?=$elem['status']?>
                </td>
                <td class="personal_tablist">
                    <?=$elem['liftName']?>
                </td>
                <td class="personal_tablist">
                    <?=$elem['reg_number']?>
                </td>
                <td class="personal_tablist">
                    <?=$elem['engineer_id']?>
                </td>

            </tr>
                <?
                $firstRow = false;
            } ?>
        </tbody>
        </table>
        <br/>
        </td>

        <td width="30px">

        </td>

        <td width="232px" class="pers_right">

        </td>

    </tr>

    </tbody>

</table>
<?php include('footer.php');?>


