package main

import (
	"bufio"
	"fmt"
	"net" //client-server; https://gist.github.com/adeekshith/34c20eb45bebe41f5247
	"os"
)

const roomName = "home"

var userName string

func main() {

	fmt.Println("Client started")

	fmt.Println("\nEnter name: ")
	userName = readUserCommand()

	if userName == "" {
		//empty string
		userName = "anonym"
	}

	fmt.Println("Connecting to server...")
	connectToServer("127.0.0.1", "60000")

	fmt.Println("Client exits")
}

func connectToServer(host string, port string) {
	conn, err := net.Dial("tcp", host+":"+port)
	if err != nil {
		fmt.Println("EXCEPTION: ", "Start client error", err)
		return
	}

	fmt.Println("User ", userName, "connected to server")

	defer conn.Close()

	sentServiceDataToServer(conn)
}

//send service info (user + room). only 1 time
func sentServiceDataToServer(conn net.Conn) {

	//connect to who??
	servInfo := "srv.user=" + userName + "|room=" + roomName
	userRecieved, err := sendAndReceive(conn, servInfo)

	if err != nil {
		fmt.Println("Error: ", err, "\nGood by!")
		return
	} else {

		if userRecieved != "ok" {
			fmt.Println("Server error: ", userRecieved, "\nGood by!")
			return
		}
	}

	//debug
	fmt.Println("Service: ", userName, roomName, "data:", userRecieved)

	fmt.Println("\n======================== CONNECTED ========================\n")

	process(conn)
}

//dialog with server
func process(conn net.Conn) {

	for {

		//read cmd
		fmt.Println("Enter message:")
		var message = readUserCommand()

		if message == "" {
			//empty string
			continue
		}

		resp, err := sendAndReceive(conn, message)
		if err != nil {
			fmt.Println("EXCEPTION: ", "sendAndReceive error", err)
		} else {
			fmt.Println("received mesage:\n", resp)
		}

	}

	fmt.Println("User ", userName, "disconnected from server :(")

}

//send and receive
func sendAndReceive(conn net.Conn, message string) (string, error) {

	//1. Send
	err := sendMessage(conn, message)

	if err == nil {
		//2. Receive
		resp, err := receiveMessage(conn)
		if err != nil {
			return "", err
		} else {
			return resp, nil
		}
	} else {
		return "", err
	}
}

//send message to room "home" //use id???
func sendMessage(conn net.Conn, msg string) error {

	if n, err := conn.Write([]byte(msg)); n == 0 || err != nil {
		return err
	}

	return nil
}

//receive message from server
func receiveMessage(conn net.Conn) (string, error) {
	buff := make([]byte, 1024)
	n, err := conn.Read(buff)

	if err != nil {
		return "", err
	}

	//responce received
	msg := string(buff[0:n])
	return msg, nil
}

//read cmd/message from keyboard
func readUserCommand() string {
	scanner := bufio.NewScanner(os.Stdin)
	scanner.Scan()
	cmd := scanner.Text()
	return cmd
}
