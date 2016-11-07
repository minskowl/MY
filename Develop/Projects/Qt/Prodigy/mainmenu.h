#ifndef MAINMENU_H
#define MAINMENU_H

#include <QDialog>
enum Page {
    Words = 1
};

namespace Ui {
    class MainMenu;
}

class MainMenu : public QDialog
{
    Q_OBJECT

public:
    explicit MainMenu(QWidget *parent = 0);
    ~MainMenu();
signals:
  void showForm(Page key);
public slots:
      void buttonShowForm(Page key);

private:
    Ui::MainMenu *ui;
};

#endif // MAINMENU_H
