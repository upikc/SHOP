from PyQt5.QtWidgets import *
from DataContex import DataContex, Product_class


class ContentWindow(QWidget):
    def __init__(self, prodListAndUser: tuple):
        super().__init__()
        self.SortCBox = QComboBox()
        self.SortCBox.currentTextChanged.connect(lambda: self.dataWrite(self.SortCBox.currentIndex() != 0))
        self.table = QTableWidget(0, 8)
        self.prodList, self.User = DataContex.tupleOfProdAndUser(prodListAndUser)
        self.initUI()

    def initUI(self):
        vbox = QVBoxLayout()
        HToolBox = QHBoxLayout()

        self.table.setEditTriggers(QAbstractItemView.EditTrigger.NoEditTriggers)
        self.table.setHorizontalHeaderLabels(
            ["Имя", "Описание", "ID_Cat", "ID_Creat", "Count_in_stock", "prise", "image",
             "Count_purchase"])
        self.SortCBox.addItems(["сортировать по возрастанию", "сортировать по убыванию"])

        vbox.addWidget(self.table)
        vbox.addLayout(HToolBox)
        HToolBox.addWidget(self.SortCBox)
        HToolBox.addWidget(QLabel("сумма заказа :" + str(DataContex.purchaseAmount(self.prodList))))
        self.setLayout(vbox)
        self.setGeometry(300, 300, 920, 450)

    def writeRows(self, Product: Product_class, row):
        self.table.removeRow(row)
        self.table.insertRow(row)
        self.table.setItem(row, 0, QTableWidgetItem(str(Product.Name)))
        self.table.setItem(row, 1, QTableWidgetItem(str(Product.Description)))
        self.table.setItem(row, 2, QTableWidgetItem(str(Product.Category_ID)))
        self.table.setItem(row, 3, QTableWidgetItem(str(Product.Creator_ID)))
        self.table.setItem(row, 4, QTableWidgetItem(str(Product.Count)))
        self.table.setItem(row, 5, QTableWidgetItem(str(Product.Prise)))
        self.table.setItem(row, 6, QTableWidgetItem(str(Product.Images)))
        self.table.setItem(row, 7, QTableWidgetItem(str(Product.Sold)))
        self.table.resizeColumnsToContents()

    def dataWrite(self, reverse: bool):
        self.prodList.sort(key=lambda x: int(x["Prise"]), reverse=reverse)
        for j, i in enumerate(self.prodList):
            self.writeRows(DataContex.deserializeProd(i), j)
