package main

import (
	"fmt"
	"net"
	"strings"
	"time"
)

//server date format
var srvDateFormat = ""

func start(dtFormat string) {
	dateFormat = dtFormat
	fmt.Println("Func callled at ", showDate(time.Now()))

	//server
	listener, err := net.Listen("tcp", ":60000")

	if err != nil {
		throwException("Strat server error", err)
		return
	}

	fmt.Println("Server listening")

	for {
		conn, err := listener.Accept()

		if err != nil {
			throwException("Server accept error", err)
			conn.Close()
			continue
		}

		go handleConnection(conn)

	}

	defer listener.Close()

}

// new connection handling
func handleConnection(conn net.Conn) {

	defer conn.Close()

	for {

		//recieving data from channel
		input := make([]byte, 1024*4)
		n, err := conn.Read(input)

		if n == 0 || err != nil {
			fmt.Println("Channel reading error:", err)
			break
		}

		//n - bytes readed from stream

		source := string(input[0:n])
		fmt.Println("Received from client:", source)

		//write responce
		//var patternResponce = "Server receiaved:[" + source + "]. thank you!"
		//conn.Write([]byte(patternResponce))

		var responce = processClientCommand(source)
		conn.Write([]byte(responce))
	}

}

func processClientCommand(cmd string) string {
	if strings.Contains(cmd, "srv.") {
		return processServiceCommand(cmd)
	} else {
		return "[Server] " + cmd
	}
}

func processServiceCommand(cmd string) string {
	var clientName = ""
	var chatRoom = ""

	var c = strings.Replace(cmd, "srv.", "", -1)
	var params = strings.Split(c, "|")

	var paramsCount = len(params)

	if len(params) != 2 {
		fmt.Println("Exceted 2 parameters in Service Message, but received ", paramsCount)
		return "400 Bad Request"
	}

	clientName = strings.Replace(params[0], "user=", "", 1)
	chatRoom = strings.Replace(params[1], "room=", "", 1)

	fmt.Println("Client data: name:", clientName, " room: ", chatRoom)

	return "ok"
}

//!!!!
func CheckRoomAndAddUser(clientName string, chatRoom string) {
	//!!!!!!!!!!!!!!!!!!!!!!!!!!!!
}

//****************************************************************************************

func showDate(t time.Time) string {
	return t.Format(dateFormat)
}

//exception
func throwException(message string, e error) {
	fmt.Println("EXCEPTION: ", message, e.Error())
}
