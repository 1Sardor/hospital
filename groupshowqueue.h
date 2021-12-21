#ifndef GROUPSHOWQUEUE_H
#define GROUPSHOWQUEUE_H

#include <QWidget>

namespace Ui {
class GroupShowQueue;
}

class GroupShowQueue : public QWidget
{
    Q_OBJECT

public:
    explicit GroupShowQueue(QWidget *parent = nullptr);
    ~GroupShowQueue();
    void setX(int x);
    void setY(int y);

private:
    Ui::GroupShowQueue *ui;
};

#endif // GROUPSHOWQUEUE_H
