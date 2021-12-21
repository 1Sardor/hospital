#include "groupshowqueue.h"
#include "ui_groupshowqueue.h"

GroupShowQueue::GroupShowQueue(QWidget *parent) :
    QWidget(parent),
    ui(new Ui::GroupShowQueue)
{
    ui->setupUi(this);
}

GroupShowQueue::~GroupShowQueue()
{
    delete ui;
}

void GroupShowQueue::setX(int x)
{
    this->setGeometry(x, this->y(),
                      this->width(), this->height());
}

void GroupShowQueue::setY(int y)
{
    this->setGeometry(this->x(), y,
                      this->width(), this->height());
}
