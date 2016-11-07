#include "pagewords.h"
#include "ui_pagewords.h"
#include <QSpinBox>

PageWords::PageWords(QWidget *parent) :
    QWidget(parent),
    ui(new Ui::PageWords)
{
    ui->setupUi(this);

    m_list= new WordList();
    QObject::connect(ui->buttonWord,SIGNAL(clicked()),SLOT(buttonTestClicked()));
    QObject::connect(ui->buttonApply,SIGNAL(clicked()),SLOT(buttonApplySettingsClicked()));

    updateTest();

    ui->spinBox_SylCntFom->setValue(m_list->SyllablesCount->first);
    ui->spinBox_SylCntTo->setValue(m_list->SyllablesCount->second);

}


PageWords::~PageWords()
{
    delete ui;
    delete m_list;
}

void PageWords::buttonApplySettingsClicked()
{
    m_list->SyllablesCount->first=ui->spinBox_SylCntFom->value();
    m_list->SyllablesCount->second=ui->spinBox_SylCntTo->value();
    m_list->applySettings();
}

void PageWords::buttonTestClicked()
{
    updateTest();
}

void  PageWords::updateTest()
{
    ui->buttonWord->setText(m_list->getTest());
}
