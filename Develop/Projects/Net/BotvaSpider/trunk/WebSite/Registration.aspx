<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="Registration.aspx.cs" Inherits="Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="Server">
    <bot:HeaderPage runat="server" ID="header" Text="Регистрация пользователя" />
    Чтобы управлять лицензиями на продукты Вы должны быть зарегестрированым пользователем

            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            <table class="f">
                <tr>
                    <td class="fl">
                    
                        <asp:Label runat="server" Text="Логин:" ID="labelLogin"  />
                    </td>
                    <td class="fv">
                        <bot:TextBoxEx ID="textBoxLogin" runat="server" SkinID="Full" Wrap="False" PropertyName="Login" ></bot:TextBoxEx>
                    </td>
                    <td>
                        <bot:AdvancedValidator runat="server" ID="val1" ControlToValidate="textBoxLogin" LabelID="labelLogin" />
                    </td>
                </tr>
                <tr>
                    <td class="fl">
                        Имя:
                    </td>
                    <td class="fv">
                        <bot:TextBoxEx ID="textBoxFirstName" runat="server" SkinID="Full" Wrap="False" PropertyName="FirstName"></bot:TextBoxEx>
                    </td>
                    <td>
                        <bot:AdvancedValidator runat="server" ID="AdvancedValidator2" ControlToValidate="textBoxFirstName" />
                    </td>
                </tr>
                <tr>
                    <td class="fl">
                        Фамилия:
                    </td>
                    <td class="fv">
                        <bot:TextBoxEx ID="textBoxLastName" runat="server" SkinID="Full" Wrap="False" PropertyName="LastName"></bot:TextBoxEx>
                    </td>
                    <td>
                        <bot:AdvancedValidator runat="server" ID="AdvancedValidator3" ControlToValidate="textBoxLastName" />
                    </td>
                </tr>
                <tr>
                    <td class="fl">
                        Email:
                    </td>
                    <td class="fv">
                        <bot:TextBoxEx ID="textBoxEmail" runat="server" SkinID="Full" Wrap="False" PropertyName="Email" />
                    </td>
                    <td>
                        <bot:AdvancedValidator runat="server" ID="AdvancedValidator1" ControlToValidate="textBoxEmail" />
                    </td>
                </tr>
                <tr>
                    <td class="fl">
                        Пароль:
                    </td>
                    <td class="fv">
                        <bot:TextBoxEx ID="textBoxPassword" runat="server" SkinID="Full" Wrap="False" TextMode="Password" />
                    </td>
                    <td>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Please confirm password valid."
                            Text="*" ControlToValidate="textBoxPassword" ControlToCompare="textBoxConfirmPassword" />
                        <bot:ProxyValidator runat="server" ID="valProxy" PropertyName="Password" />
                    </td>
                </tr>
                <tr>
                    <td class="fl">
                        Подтверждение пароля:
                    </td>
                    <td class="fv">
                        <bot:TextBoxEx ID="textBoxConfirmPassword" runat="server" SkinID="Full" Wrap="False"
                            TextMode="Password" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="fl">
                        Секретный вопрос:<bot:HelpTip runat="server">Данный вопрос будет Вам задаваться при восстановлении забытого пароля.</bot:HelpTip>
                    </td>
                    <td class="fv">
                        <bot:TextBoxEx ID="textBoxSecurityQuestion" runat="server" SkinID="Full" Wrap="False"
                            PropertyName="SecurityQuestion" />
                    </td>
                    <td>
                        <bot:AdvancedValidator runat="server" ID="AdvancedValidator4" ControlToValidate="textBoxSecurityQuestion" />
                    </td>
                </tr>
                <tr>
                    <td class="fl">
                        Ответ на секретный вопрос:
                    </td>
                    <td class="fv">
                        <bot:TextBoxEx ID="textBoxSecurityAnswer" runat="server" SkinID="Full" Wrap="False"
                            PropertyName="SecurityAnswer" />
                    </td>
                    <td>
                        <bot:AdvancedValidator runat="server" ID="AdvancedValidator5" ControlToValidate="textBoxSecurityAnswer" />
                    </td>
                </tr>
            </table>
            <bot:ButtonEx ID="buttonSave" runat="server" OnClick="buttonSave_Click" Text="Зарегестрироваться" />
            <bot:CancelButton ID="buttonCancel" runat="server" OnClick="buttonCancel_Click" />

</asp:Content>
