package main

import "fmt"
import "encoding/json"
//multiines in string : https://stackoverflow.com/questions/7933460/how-do-you-write-multiline-strings-in-go

type LoginStr struct{
	Login string     `json:"login"`
	Password string  `json:"password"`
}

var jsonBody = `{
	"login": "aaaaabbbccc",
	"password": "463648234"
}`

func main(){	
	fmt.Println("Json to struct deserialization")

	var r LoginStr
	err := json.Unmarshal([]byte(jsonBody), &r)
	if err != nil {
		fmt.Printf("Error converting json to struct: ", err)
	}

	fmt.Println(r)
}

