<?php
  include('templates\header.php');
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
      <td class="priva">
        <table cellspacing="0" cellpadding="0" border="0">
          <tbody>
            <tr>
              <td width="30px">
              </td>
              <td class="personal_head">
                <table width="100%" cellspacing="0" cellpadding="2px" style="border: 0000">
                  <tbody>
                    <tr>
                      <td>
                        <r>название лифта</r>
                      </td>
                      <td>
                        <r>тип</r>
                      </td>
                      <td>
                        <r>рег. номер</r>
                      </td>
                      <td>
                        <r>механик</r>
                      </td>
                      <td>
                        <r>
                          дата последнего ТО
                        </r>
                      </td>
                    </tr>

<?

 foreach($array as $elem){ ?>
 Value: <?=$elem?>
                           <tr>
                      <td style="border: 0000; background-color: #FFF; text-align: center; color: #41474d">
                        <a href="http://www.insecom.ru/personal/A03_08.pdf">Шиндлер</a>
                      </td>
                      <td style="border: 0000; background-color: #FFF; text-align: center; color: #41474d">
                        Лифт
                      </td>
                      <td style="border: 0000; background-color: #FFF; text-align: center; color: #41474d">
                        41961
                      </td>
                      <td style="border: 0000; background-color: #FFF; text-align: center; color: #41474d">
                        Кузьмин Александр
                      </td>
                      <td style="border: 0000; background-color: #FFF; text-align: center; color: #41474d">
                        13.03.2012
                      </td>
                    </tr>

<? } ?>



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
        <table cellspacing="0" cellpadding="0" border="0">
          <tbody>
            <tr>
              <td width="713px" class="private_bottom">
                <br>
                  <br>
                    <br>
                      <table cellpadding="0" cellspacing="0" class="moduletablebottom">
                        <tbody>
                          <tr>
                            <td>
                              <table width="100%" border="0" cellpadding="0" cellspacing="1">
                                <tbody>
                                  <tr>
                                    <td nowrap="nowrap">
                                      <a href="http://www.insecom.ru/" class="mainlevelbottom">Список оборудования</a>
                                      <a
                                                                                                                href="http://www.insecom.ru/" class="mainlevelbottom">Список счетов</a>
                                      <a href="http://www.insecom.ru/"
                                                                                                                    class="mainlevelbottom">Обратная связь</a>
                                      <a href="http://www.insecom.ru/" class="mainlevelbottom">
                                        Архив
                                        документов
                                      </a>
                                      <a href="http://www.insecom.ru/login.html" class="mainlevelbottom">Выход</a>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </td>
              <td width="232px" class="pers_right">
              </td>
            </tr>
          </tbody>
        </table>
      </td>
    </tr>
  </tbody>
</table>
<?php
  include('templates\footer.php');
?>