﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <div class="contentbox">
        <bot:HeaderPage runat="server"> BotvaBot</bot:HeaderPage>
        Немного о ботах, бот - это программа, запускаемая на компьютере пользователя, которая
        управляет (эмулирует) его действиями. При этом присутствие самого пользователя совсем
        не обязательно. Настроив и запустив программу, он может спокойно заниматься своими
        делами в то время как программа будет полностью автоматизировать все «доверенные»
        ей процессы.<br />
        <bot:HeaderParagraph runat="server">Описание бота </bot:HeaderParagraph>
        <br />
        BotvaBot – программа-бот для автоматического выполнения различных действий в игре
        Botva Online (Ботва Онлайн бот). Возможности бота:
        <ul>
            <li>Автоматическое нападение на игроков.</li>
            <li>Различные списки для нападения</li>
            <li>Функция “Белый списоки”. Вы можете добавлять игроков и кланы в белый список, если
                при случайном поиске вы наткнетесь на игрока из белого списка или на игрока состоящего
                в клане из белого списка, то нападение на него производиться не будет. </li>
            <li>Походы в дозор</li>
            <li>Различные виды инвестирования ресурсов</li>
            <li>Автоматическая прокачка способностей</li>
            <li>Автоматическая покупка товаров на Сбытнице</li>
            <li>Автоматическое переодевание кулонов.</li>
            <li>Автоматическая добыча кристаллов в шахте (карьер и поляны).</li>
            <li>Автоматическое питье бутылок.</li>
            <li>Статистика по награбленному, по шахте и по затратам.</li>
            <li>Усыпление бота по расписанию.</li>
            <li>При появлении проверки на программу-бота в Ботве, бот остановиться и известит вас
                в отчете и звуковым сигналом, благодаря чему вы сразу сможете ввести код в игре.
                Никто никогда не узнает, что вы используете бота.</li>
        </ul>
    </div>
</asp:Content>
