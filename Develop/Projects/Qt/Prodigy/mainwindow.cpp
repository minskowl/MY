#include "mainwindow.h"
#include "ui_mainwindow.h"
#include "pagewords.h"
#include "mainmenu.h"

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    MainMenu *m=new MainMenu();
    QObject::connect(m,SIGNAL(showForm(Page)),SLOT(showPage(Page)));
    addWidget(m);
}

int MainWindow::addWidget(QWidget *w)
{
    return ui->stackedWidget->addWidget(w);
}

void MainWindow::showWords()
{


}

void  MainWindow::showPage(Page page)
{
    switch(page)
    {
    case Words:
        int index= this->addWidget(new PageWords());
        this->ui->stackedWidget->setCurrentIndex(index);
        break;
    }
}

MainWindow::~MainWindow()
{
    delete ui;
}
