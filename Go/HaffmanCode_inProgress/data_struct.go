package main

import (
	"fmt"
	"sort"
)

type pq struct {
	value    string //node value
	children [2]*pq //binary tree
}

func parseStringToModel(input string) pq {

	var charsMap = make(map[string]int)

	for _, ch := range input {
		//char to string
		s := fmt.Sprintf("%c", ch)

		if val, ok := charsMap[s]; ok {
			charsMap[s]++
			val = val
		} else {
			charsMap[s] = 1
		}
	}

	//sorting
	sortMap(charsMap) //  no need to sort ???
	//testSort()
	fmt.Println(charsMap)

	var model = pq{value: ""}
	return model
}

//sort mapChars to Asc
func sortMap(m map[string]int) {
	fmt.Println("BEFORE sorting...\n", m)
	keys := make([]string, 0, len(m))
	for k := range m {
		keys = append(keys, k)
	}
	sort.Strings(keys)

	fmt.Println("sorting...\n", m)
}

func testSort() {
	m := map[string]int{"Eve": 2, "Bob": 25, "Alice": 23}

	keys := make([]string, 0, len(m))
	for k := range m {
		keys = append(keys, k)
	}
	sort.Strings(keys)

	for _, k := range keys {
		fmt.Println(k, m[k])
	}
}
