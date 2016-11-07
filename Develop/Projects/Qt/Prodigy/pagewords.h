#ifndef PAGEWORDS_H
#define PAGEWORDS_H

#include <QWidget>
#include <WordList.h>

namespace Ui {
    class PageWords;
}

class PageWords : public QWidget
{
    Q_OBJECT

public:
    explicit PageWords(QWidget *parent = 0);
    ~PageWords();

public slots:
      void buttonTestClicked();
      void buttonApplySettingsClicked();

private:
    Ui::PageWords *ui;
    WordList *m_list;

    void updateTest();
};

#endif // PAGEWORDS_H
