from PyQt5 import *
from PyQt5.QtWidgets import *

class MyButton(QPushButton):
    def __init__(self, parent=None):
        super().__init__(parent)

        # Установить текст на кнопке
        self.setText("Нажмите меня")

        # Установить цвет фона кнопки
        self.setStyleSheet("background-color: blue")

        # Установить действие, которое выполняется при нажатии кнопки
        self.clicked.connect(self.on_click)

    def on_click(self):
        # Выполнить действие при нажатии кнопки
        print("На кнопку была нажата!")

class MyForm(QWidget):
    def __init__(self):
        super().__init__()

        # Создать макет формы
        layout = QHBoxLayout()

        # Добавить пользовательскую кнопку в макет формы
        button = MyButton()
        layout.addWidget(button)
        fff = MyButton()
        layout.addWidget(fff)

        # Установить макет формы на форму
        self.setLayout(layout)

    def show(self):
        super().show()

app = QApplication([])
form = MyForm()
form.show()
app.exec_()
