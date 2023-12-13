import sys
from PyQt5.QtWidgets import *
from DataContex import DataContex, Product_class
import socket

class ContentWindow(QWidget):
    def __init__(self, prodListAndUser: tuple):
        super().__init__()
        self.prodList, self.User = DataContex.tupleOfProdAndUser(prodListAndUser)
        self.initUI()

    def initUI(self):
        vbox = QVBoxLayout()
        self.table = QTableWidget(0, 8)

        self.table.setEditTriggers(QAbstractItemView.EditTrigger.NoEditTriggers)
        self.table.setHorizontalHeaderLabels(
            ["Имя", "Описание", "ID_Cat", "ID_Creat", "Count_in_stock", "prise", "image",
             "Count_purchase"])

        for j, i in enumerate(self.prodList):
            self.writeRows(DataContex.deserializeProd(i), j)

        vbox.addWidget(self.table)
        vbox.addWidget(QLabel("сумма заказа :" + str(DataContex.purchaseAmount(self.prodList))))
        self.setLayout(vbox)
        self.setGeometry(300, 300, 920, 450)

    def writeRows(self, Product: Product_class, row):
        self.table.insertRow(self.table.rowCount())
        self.table.setItem(row, 0, QTableWidgetItem(str(Product.Name)))
        self.table.setItem(row, 1, QTableWidgetItem(str(Product.Description)))
        self.table.setItem(row, 2, QTableWidgetItem(str(Product.Category_ID)))
        self.table.setItem(row, 3, QTableWidgetItem(str(Product.Creator_ID)))
        self.table.setItem(row, 4, QTableWidgetItem(str(Product.Count)))
        self.table.setItem(row, 5, QTableWidgetItem(str(Product.Prise)))
        self.table.setItem(row, 6, QTableWidgetItem(str(Product.Images)))
        self.table.setItem(row, 7, QTableWidgetItem(str(Product.Sold)))

        self.table.resizeColumnsToContents()

