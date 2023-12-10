import sys
from datetime import datetime
from PyQt5.QtWidgets import *
from DataContex import DataContex
from BASKET import ContentWindow


# класс кнопки пока что пустой
class MyButton(QPushButton):
    def __init__(self, prodListAndUser: tuple):
        super().__init__()
        self.setText("кнопка ")
        self.setStyleSheet("background-color: grey")
        self.clicked.connect(lambda : self.on_click(prodListAndUser))

    def on_click(self, prodListAndUser: tuple):
        print("На кнопку была нажата!") ##тут будет открытие контента
        self.ContenW = ContentWindow(prodListAndUser)
        self.ContenW.show()

class PurchaseLay(QWidget):  ##экземпляр покупки
    def __init__(self, RECV=None, Datime=None):
        super().__init__()

        self.RECV = RECV
        self.prodList, self.userData = DataContex.tupleOfProdAndUser(RECV)
        self.lay = QGridLayout()
        self.lay.addWidget(QLabel("Логин: " + str(self.userData)), 0, 0)
        self.lay.addWidget(QLabel("Сумма покупки: "), 0, 1)
        self.lay.addWidget(QLabel("Дата: " + datetime.now().strftime("%m/%d/%Y, %H:%M:%S")), 0, 2)
        self.lay.addWidget(MyButton(RECV), 0, 3)

        self.setLayout(self.lay)


class mainWindow(QWidget):  ##это готово
    def __init__(self):
        super().__init__()
        self.lay = QVBoxLayout()


        RECV = DataContex.socketRECV() ##тут запись в файл
        with open("jsonFrom.txt", "a", encoding="UTF-8") as f:
            f.write("\r" + RECV)

        with open("jsonFrom.txt", "r", encoding="UTF-8") as f:
            for i in f.readlines():
                self.WidgetAdd(PurchaseLay(i))


        self.setLayout(self.lay)

    def WidgetAdd(self, item: QWidget):  ##это готово
        self.lay.addWidget(item)


class MainScrollArea(QScrollArea):  ##это готово
    def __init__(self):
        super().__init__()
        self.setMinimumSize(600, 300)
        self.setWidget(mainWindow())
        self.setWidgetResizable(True)
        self.show()


app = QApplication(sys.argv)
win = MainScrollArea()
sys.exit(app.exec_())
