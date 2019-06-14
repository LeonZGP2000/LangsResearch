package main

import (
	"sort" //https://golang.org/pkg/sort/
)

// Sortin - directories go first
type isDirSortType []dirItemInfo

func (a isDirSortType) Len() int           { return len(a) }
func (a isDirSortType) Swap(i, j int)      { a[i], a[j] = a[j], a[i] }
func (a isDirSortType) Less(i, j int) bool { return a[i].isDir }

//sorting
func sortByDir( structArr []dirItemInfo) []dirItemInfo {
	sort.Sort( isDirSortType(structArr))
	//debug: fmt.Println(structArr)
	return structArr
}