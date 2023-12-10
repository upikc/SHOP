import dataclasses
import json
import socket


class DataContex():
    @staticmethod
    def deserializeProd(prodLoads):
        if isinstance(prodLoads, list):
            p = []
            for i in prodLoads:
                p.append(Product_class(**i))
            return p
        elif isinstance(prodLoads, dict):
            p = Product_class(**prodLoads)
            return p

    @staticmethod
    def tupleOfProdAndUser(jsonString):
        prodList = jsonString.split("{%Yay$}")[0]
        prodList = json.loads(prodList)
        user = jsonString.split("{%Yay$}")[1]
        user = json.loads(user)
        return (prodList , user)

    @staticmethod
    def socketRECV():
        while True:
            server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            server.bind(("localhost", 11000))
            server.listen(1)
            client_socket, client_address = server.accept()
            #print(f"Connected to {client_address}")
            text = client_socket.recv(1024).decode('utf-8')
            server.close()
            return text

@dataclasses.dataclass
class Product_class:
    Name: str
    Description: str
    Category_ID: str
    Creator_ID: str
    Count: str
    Prise: str
    Images: str
    Sold: str

@dataclasses.dataclass
class User_class:
    login: str
    password: str
    role: int