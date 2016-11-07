using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using log4net.Config;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("BotvaSpider")]
[assembly: AssemblyVersion("2.0.0.*")]
[assembly: AssemblyFileVersion("2.0.0.0")]
[assembly: AssemblyDescription(@"
Бот умеет:
 автоматически бодаться
 автоматически ходить в дозор межу боями по 10мин
 помогает в поиске кристалов на поляне
 помогает качать шмот
 одевать разные кулоны на разные действия и для разных противников.
 маскировка броузера и приложения.
 интелектуальная система подсказок.
В версии 1.1.0
Добавлено:
    Умеет ходить в шахту.
    Умеет ходить на малую поляну.
    Умеет качать кулоны.

В версии 1.0.11
Обновлено:
    Система логирования событий. При ошибках приложение моргает и выкидывает туул тип  у иконки в таскбаре.
    Так же система делает подсказки игроку.

В версии 1.0.10
Добавлено:
    Расписание сна бота

В версии 1.0.9
Добавлено:
    Можно указать кулон для Сна, Драки, Похода в дозор
    Экстренный слив для Ублюдков при первом нападении
    Покупка шмота в сбытице
Исправлено:
    автопереодевание кулонов. Работало только у автора.

В версии 1.0.8
Добавлено:
    В ферме можно указать кулон для атаки.

В версии 1.0.7
Исправлено:
    Разрешен слив славы

    ")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Неистовая Ботва")]
[assembly: AssemblyCopyright("Copyright ©  2009")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("b0704233-0ea6-412c-8f01-92a0d27dbb66")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      BuildWardrobeItem Number
//      Revision
//
// You can specify all the values or you can default the BuildWardrobeItem and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]

[assembly: XmlConfigurator()]