package main

import "fmt"

var plainText = "aabbbcccd"

func main() {

	fmt.Println("Compressing\nPlain text is:", plainText)
	compress(plainText)
}

func compress(input string) {
	var model = parseStringToModel(input)

	fmt.Println(model)
}
