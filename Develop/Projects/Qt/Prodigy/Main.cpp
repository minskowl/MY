#include <QApplication>
#include <MainMenu.h>
#include <MainWindow.h>
#include <QDir>
 int main(int argc, char *argv[])
 {
  QApplication app(argc, argv);

  QCoreApplication::setOrganizationName("Savchin.inc");
  QCoreApplication::setApplicationName("Prodigy");


 //QString str=QDir::currentPath();
 //QDir::setCurrent(QCoreApplication::applicationDirPath());
 //str=QDir::currentPath();
 MainWindow *m= new MainWindow();

 m->show();
 return app.exec();
}
