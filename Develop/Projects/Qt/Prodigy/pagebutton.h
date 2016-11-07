#ifndef PAGEBUTTON_H
#define PAGEBUTTON_H

#include <QPushButton>
#include <MainMenu.h>
#include <QtDesigner/QDesignerExportWidget>

class PageButton : public QPushButton
{
    Q_OBJECT
    Q_PROPERTY(Page page READ getPage WRITE setPage)
public:
    explicit PageButton(QWidget *parent = 0);


    void setPage(Page page)
     {
         m_page = page;
        // emit priorityChanged(priority);
     }
     Page getPage() const
     {
         return m_page;
     }
signals:
  void showForm(Page key);

public slots:
      void buttonClicked();

private :
    Page m_page;
};

#endif // PAGEBUTTON_H
