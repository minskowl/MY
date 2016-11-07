#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include <mainmenu.h>

namespace Ui {
    class MainWindow;
}

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    explicit MainWindow(QWidget *parent = 0);
    ~MainWindow();
    int addWidget(QWidget *w);
public slots:
      void showWords();
      void showPage(Page page);

private:
    Ui::MainWindow *ui;
};

#endif // MAINWINDOW_H
