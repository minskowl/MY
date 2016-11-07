#ifndef TESTLIST_H
#define TESTLIST_H
#include <QVector>
class TestList
{
public:
    explicit TestList(const char *fileName);
    explicit TestList(const QString &fileName);
    virtual ~TestList();
    QString getTest();
protected:
    virtual void appendTest(const QString &text);

private:
     QVector<QString> *m_words;

      void readWords(QString fileName);
      int getRandom(int min, int max);

};

#endif // TESTLIST_H
