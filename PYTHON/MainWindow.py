import sys
from PyQt5.QtCore import QObject, pyqtSignal, QThread
from PyQt5.QtWidgets import *
from DataContex import DataContex
from BASKET import ContentWindow


class MyButton(QPushButton):
    def __init__(self, prodListAndUser: tuple):
        super().__init__()
        self.setText("Открыть ")
        self.setStyleSheet("background-color: grey")
        self.clicked.connect(lambda: self.on_click(prodListAndUser))

    def on_click(self, prodListAndUser: tuple):
        self.ContenW = ContentWindow(prodListAndUser)
        self.ContenW.show()


class PurchaseLay(QWidget):
    def __init__(self, RECV=None):
        super().__init__()

        self.RECV = RECV
        self.prodList, self.userData = DataContex.tupleOfProdAndUser(RECV)
        #print(self.prodList)
        self.Summ = DataContex.purchaseAmount(self.prodList)
        self.lay = QGridLayout()
        self.lay.addWidget(QLabel("Логин: " + self.userData['login']), 0, 0)
        self.lay.addWidget(QLabel("Сумма покупки: " + str(self.Summ)), 0, 1)
        self.lay.addWidget(QLabel("Дата: " + DataContex.timeFromJsonString(RECV)), 0, 2)
        self.lay.addWidget(MyButton(RECV), 0, 3)

        self.setLayout(self.lay)


class mainWindow(QWidget):
    def __init__(self):
        super().__init__()
        self.mainLay = QVBoxLayout()
        self.lay = QVBoxLayout()
        self.mainLay.addWidget(QLabel("Поиск по пользователю"))
        self.UserSearchTextEdit = QTextEdit()
        self.mainLay.addWidget(self.UserSearchTextEdit)
        self.QSortingBox = QComboBox()
        self.QSortingBox.addItems(["по возростанию", "По убыванию"])
        self.QSortingBox.currentTextChanged.connect(self.updateTable)
        self.UserSearchTextEdit.textChanged.connect(self.updateTable)
        self.mainLay.addWidget(self.QSortingBox)

        self.mainLay.addLayout(self.lay)
        self.LayWriter()

        self.worker = Worker()
        thread = QThread(self)
        self.worker.moveToThread(thread)
        self.worker.dataChanged.connect(self.updateTable)
        thread.started.connect(self.worker.task)
        thread.start()

        self.setLayout(self.mainLay)

    def WidgetAdd(self, item: PurchaseLay):
        self.lay.addWidget(item)

    def LayWriter(self):
        revers = self.QSortingBox.currentText() == "По убыванию"
        NameUserSort = self.UserSearchTextEdit.toPlainText()
        listLay = []
        for i in DataContex.jsonReadLines():
            listLay.append(PurchaseLay(i))  #создаем лаяуты и делаем лист
        listLay.sort(key=lambda x: x.Summ, reverse=revers)
        for i in listLay:
            if NameUserSort in str(i.userData["login"]):
                self.WidgetAdd(i)

    def updateTable(self):
        for i in reversed(range(self.lay.count())):
            self.lay.itemAt(i).widget().setParent(None)
        self.LayWriter()


class MainScrollArea(QScrollArea):
    def __init__(self):
        super().__init__()
        self.setMinimumSize(600, 300)
        self.setWidget(mainWindow())
        self.setWidgetResizable(True)
        self.show()


class Worker(QObject):
    dataChanged = pyqtSignal(str)

    def task(self):
        while True:
            RECV = DataContex.socketRECV()
            DataContex.jsonWriter(RECV)
            print("Воркер сработал")
            self.dataChanged.emit(RECV)


app = QApplication(sys.argv)
win = MainScrollArea()
sys.exit(app.exec_())
