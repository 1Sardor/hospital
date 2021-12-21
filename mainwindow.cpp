#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    timer = new QTimer(this);
    ui->labelTime->setText(QTime::currentTime().toString("H:mm:ss"));
    timer->setInterval(1000);
    connect(timer, SIGNAL(timeout()), this, SLOT(timerOut()));
    timer->start();
    boxes.append(ui->groupBox1);
    boxes.append(ui->groupBox2);
    boxes.append(ui->groupBox3);
    boxes.append(ui->groupBox4);
    boxes.append(ui->groupBox5);
    boxes.append(ui->groupBox6);
    boxes.append(ui->groupBox7);
    boxes.append(ui->groupBox8);
    boxes.append(ui->groupBox9);
    boxes.append(ui->groupBox10);

//    for(auto x: boxes) x->setVisible(false);
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::timerOut()
{
    ui->labelTime->setText(QTime::currentTime().toString("H:mm:ss"));
}

