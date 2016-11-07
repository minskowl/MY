#include <testlist.h>
#include <QtGlobal>
#include <QFile>
#include <QTextStream>
#include <QTime>

 TestList::TestList(const char *fileName)
 {
     m_words= new  QVector<QString>();
      readWords(fileName);

      QTime midnight(0, 0, 0);
      qsrand(midnight.secsTo(QTime::currentTime()));
 }


TestList::~TestList()
{
    delete m_words;
}

 QString TestList::getTest()
 {
     return m_words->value(getRandom(0,m_words->count()));
 }

void TestList::readWords(QString fileName)
{
    m_words->clear();

    QFile file(fileName);

     if (!file.open(QIODevice::ReadOnly | QIODevice::Text))
         return;

     QTextStream in(&file);
     QString line = in.readLine();
     while (!line.isNull())
     {
         appendTest(line);
         line = in.readLine();
     }
}

void  TestList::appendTest(const QString &text)
{
    m_words->append(text);
}

int TestList::getRandom(int min, int max)
{
    return int( qrand() / (RAND_MAX + 1.0) * (max + 1 - min) + min );
}
