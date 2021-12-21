#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include <QTimer>
#include <QTime>
#include <QVector>
#include <QGroupBox>

QT_BEGIN_NAMESPACE
namespace Ui { class MainWindow; }
QT_END_NAMESPACE

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    MainWindow(QWidget *parent = nullptr);
    ~MainWindow();

private slots:
    void timerOut();

signals:


private:
    Ui::MainWindow *ui;
    QTimer* timer;
    QVector<QGroupBox*> boxes;
};
#endif // MAINWINDOW_H
