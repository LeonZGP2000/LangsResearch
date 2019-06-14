package main

import "fmt"

type serverRooms struct {
	rooms []mapChat
}

type mapChat struct {
	roomName    string
	connections []mapRoomUser
}

type mapRoomUser struct {
	userName string
	ip       string
}

func main() {
	fmt.Println("2")

	//var rooms := serverRooms{}

	user1 := mapRoomUser{userName: "usr1", ip: "127.0.0.1"}
	user2 := mapRoomUser{userName: "usr2", ip: "127.0.0.1"}

	usrArray := []mapRoomUser{user1, user2}
	room1 := mapChat{roomName: "Room 1", connections: usrArray}

	chatsArray := []mapChat{room1}
	server := serverRooms{rooms: chatsArray}

	fmt.Println(server)
}
