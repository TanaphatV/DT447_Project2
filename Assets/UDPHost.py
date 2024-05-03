from socket import *
serverPort = 12000
serverSocket = socket(AF_INET, SOCK_DGRAM)
serverSocket.bind(('', serverPort))
print("The server is ready to receive")
clientAddressList = []
while True:
    message, clientAddress = serverSocket.recvfrom(2048)
    alreadyGotClient = False
    for x in clientAddressList:
        if x == clientAddress:
            alreadyGotClient = True
    if alreadyGotClient == False:
        clientAddressList.append(clientAddress)
    print(message.decode())
    for x in clientAddressList:
        serverSocket.sendto(message, x)