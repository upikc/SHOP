import dataclasses
import sys
import json
from PyQt5.QtWidgets import *

@dataclasses.dataclass
class Product_class():
    Name : str
    Description :str
    Category_ID :str
    Creator_ID :str
    Count :str
    Prise :str
    Images :str
    Sold :str



class DataContex():
    def deserialize(prodLoads):
        if isinstance(prodLoads, list):
            for i in prodLoads:
                p = Product_class(**i)
                return p
        if isinstance(prodLoads, dict):
            p = Product_class(**prodLoads)
            return p



class MainWindow(QWidget):
    def __init__(self, prod):
        super().__init__()
        self.prodList = prod.split("{%Yay$}")[0]
        print(self.prodList)
        self.prodList = json.loads(self.prodList)
        DataContex.deserialize(self.prodList)
        self.initUI()

    def initUI(self):
        vbox = QVBoxLayout()
        self.table = QTableWidget(0, 8)
        self.table.setHorizontalHeaderLabels(
            ["Имя", "Описание", "ID_Cat", "ID_Creat", "Count_in_stock", "prise", "image",
             "Count_purchase"])

        j = 0
        for i in self.prodList:
            self.writeRows(DataContex.deserialize(i), j)
            j = +1

        vbox.addWidget(self.table)

        self.setLayout(vbox)
        self.setGeometry(300, 300, 920, 450)
        self.show()

    def writeRows(self, Product: Product_class, row):
        self.table.insertRow(self.table.rowCount())
        self.table.setItem(row, 0, QTableWidgetItem(Product.Name))
        self.table.setItem(row, 1, QTableWidgetItem(Product.Description))
        self.table.setItem(row, 2, QTableWidgetItem(str(Product.Category_ID)))
        self.table.setItem(row, 3, QTableWidgetItem(str(Product.Creator_ID)))
        self.table.setItem(row, 4, QTableWidgetItem(str(Product.Count)))
        self.table.setItem(row, 5, QTableWidgetItem(str(Product.Prise)))
        self.table.setItem(row, 6, QTableWidgetItem(str(Product.Images)))
        self.table.setItem(row, 7, QTableWidgetItem(str(Product.Sold)))


if __name__ == '__main__':
    #text = '[{"Name":"Название Два","Description":"описаниеДва","Category_ID":1,"Creator_ID":0,"Count":3,"Prise":5,"Images":"C:\\Users\u\\source\\repos\\SHOP\\bin\\Debug\\images\\AAA.png","Sold":2}]{%Yay$}{"login":"upik","pass":"321","role":1}'

    import socket

    HOST = "localhost"
    PORT = 11000

    while True:
        server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        server.bind((HOST, PORT))
        server.listen(1)
        client_socket, client_address = server.accept()
        print(f"Connected to {client_address}")
        data = client_socket.recv(1024)
        print(f"Received data from {client_address}: {data.decode('utf-8')}")
        text = data.decode('utf-8')
        app = QApplication(sys.argv)
        ex = MainWindow(text)
        sys.exit(app.exec_())
        server.close();


    #with open(r"C:\Users\u\Desktop\jsonFrom.txt" , "r" , encoding= "UTF-8") as f:
    #    text = f.read()

    app = QApplication(sys.argv)
    ex = MainWindow(text)
    sys.exit(app.exec_())
