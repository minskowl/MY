#include "wordlist.h"
#include <QSettings>
#include <QVariant>
#include <QPair>
#include <QStringList>
#include <QMessageBox>
WordList::WordList(): TestList("Words.txt")
{
    QSettings *settings= new QSettings();
    settings->beginGroup("/WordList");


    SyllablesCount= new QPair<int,int>(
                    settings->value("SyllablesCountFrom",2).toInt() ,
                    settings->value("SyllablesCountFrom",2).toInt());

    settings->endGroup();

    delete settings;

}
 WordList::~WordList()
{
  delete SyllablesCount;
}

 void WordList::appendTest(const QString &text)
 {
     QMessageBox msgBox;
      msgBox.setText("WordList::appendTest");
      msgBox.exec();

    QStringList l= text.split("-");
    if(l.count()>=SyllablesCount->first &&
       l.count()<=SyllablesCount->second
            )
    {
        TestList::appendTest(text);
    }
 }

 void WordList::applySettings()
 {
     QSettings *settings= new QSettings();
     settings->beginGroup("/WordList");

     settings->setValue("SyllablesCountFrom",SyllablesCount->first);
     settings->setValue("SyllablesCountFrom",SyllablesCount->second);

     settings->endGroup();

     delete settings;
 }
