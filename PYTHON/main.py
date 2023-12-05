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
    server.close();
