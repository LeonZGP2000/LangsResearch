package main

import (
	"fmt"
	"time" //https://tecadmin.net/get-current-date-time-golang/
	//https://golang.org/pkg/time/
	//type of date-time: https://stackoverflow.com/questions/22137885/define-a-new-type-of-time-in-golang
	//https://stackoverflow.com/questions/42391869/time-since-in-days-hours-minutes-seconds-format?rq=1
	//time format: https://tecadmin.net/get-current-date-time-golang/
)

var dateFormat = "01-02-2006 15:04:05 Monday"

//time.Time
func main() {

	//server started
	serverStarted := time.Now().Format(dateFormat)

	fmt.Println("Char server v.0.0.1A. Started:", serverStarted)

	//start server
	start(dateFormat)

	defer beforeExit()
}

func beforeExit() {
	fmt.Println("Server stopped:", time.Now().Format(dateFormat))
}

//time since.. (no use)
func getTymeOnlyneSince() {
	//fmt.Println("Server stopped: %s", time.Since(serverStarted))
}
