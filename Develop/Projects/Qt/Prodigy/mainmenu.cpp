#include "mainmenu.h"
#include "PageButton.h"
#include "ui_mainmenu.h"

MainMenu::MainMenu(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::MainMenu)
{
    ui->setupUi(this);

    PageButton *b= new PageButton();
    b->setText("Words");
    b->setPage(Words);

    ui->verticalLayout->addWidget(b);

    connect(b,SIGNAL(showForm(Page)),this,SLOT(buttonShowForm(Page)));

}

MainMenu::~MainMenu()
{
    delete ui;
}

void MainMenu::buttonShowForm(Page key)
{
    emit showForm(key);
}
