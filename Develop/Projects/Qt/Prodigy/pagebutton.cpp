#include "pagebutton.h"

PageButton::PageButton(QWidget *parent) :
    QPushButton(parent)
{

    QObject::connect(this,SIGNAL(clicked()),SLOT(buttonClicked()));
}

void PageButton::buttonClicked()
{
      emit showForm(m_page);
}
