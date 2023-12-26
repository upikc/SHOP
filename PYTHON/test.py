import socket
import threading

def start_server():
    HOST = '0.0.0.0'
    PORT = 8888

    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as server_socket:
        server_socket.bind((HOST, PORT))
        server_socket.listen(10)
        print(f"Server is listening on port {PORT}")

        while True:
            conn, addr = server_socket.accept()
            print(f"Connected to {addr}")
            conn.send("Оператор подключен".encode())

            receiver_thread = threading.Thread(target=receive_data, args=(conn,))
            receiver_thread.daemon = True
            receiver_thread.start()

            sender_thread = threading.Thread(target=send_data, args=(conn,))
            sender_thread.daemon = True
            sender_thread.start()

def receive_data(connection):
    while True:
        try:
            data = connection.recv(1024).decode("utf-8")
            if not data:
                break
            print(f"\nПользователь отправил сообщение: {data}")
        except Exception as e:
            print(f"Ошибка при получении данных: {e}")
            break

def send_data(connection):
    while True:
        try:
            message = input("Сообщение для отправки: ")
            connection.send(message.encode())
        except Exception as e:
            print(f"Ошибка при отправке данных: {e}")
            break

if __name__ == "__main__":
    start_server()

