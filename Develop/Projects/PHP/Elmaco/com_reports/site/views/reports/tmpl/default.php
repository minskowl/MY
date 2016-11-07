<?php // no direct access
defined('_JEXEC') or die('Restricted access');
include('header.php');
?>

<table cellspacing="0" cellpadding="0" border="0">
    <tbody>
    <tr>

        <td width="713px">

            <h1 class="h1_gray">
                список оборудования
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

        <td width="653px">
            <?
            $id = '';
            $firstRow = true;

            foreach ($this->rows as $elem) {
                if ($id != $elem['plId']) {
                    $id = $elem['plId'];
                    if ($firstRow == false) {
                        echo('</tbody></table><br/><br/>');
                    }
                    ?>

                    <h5><?=$elem['plName']?> / <?=$elem['plAddress']?></h5>

                <table width="100%" cellspacing="0" cellpadding="2px" style="border: 0000">
                  <tbody>
                    <tr>
                        <td class="personal_head">
                            <r>оборудование</r>
                        </td>
                        <td class="personal_head">
                            <r>рег. номер</r>
                        </td>
                        <td class="personal_head">
                            <r>тип</r>
                        </td>
                        <td class="personal_head">
                            <r>механик</r>
                        </td>

                        <td class="personal_head">
                            <r>последнее ТО</r>
                        </td>

                    </tr>
                    <?
                }
                ?>
            <tr style="text-align: center;">

                <td class="personal_liftname">
                    <a href="?option=com_reports&task=showlift&id=<?=$elem['liftId']?>"><?=$elem['liftName']?></a>
                </td>
                <td class="personal_tablist">
                    <?=$elem['reg_number']?>
                </td>
                <td class="personal_tablist">
                    <?=$elem['type']?>
                </td>
                <td class="personal_tablist">
                    <?=$elem['engineer_id']?>
                </td>

                <td class="personal_tablist">

                    <?=$elem['date']?>

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
<?php include('footer.php'); ?>

