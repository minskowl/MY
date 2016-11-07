#ifndef WORDLIST_H
#define WORDLIST_H
#include <TestList.h>
#include <QPair.h>

class WordList : public TestList
{
public:
    WordList();
    virtual ~WordList();

    QPair<int,int> *SyllablesCount;
    void applySettings();
protected:
    //virtual
    void appendTest(const QString &text);

};

#endif // WORDLIST_H
