package main

import (
	"time"
	"fmt"
	"os"
	"strconv"
	//https://yourbasic.org/golang/convert-int-to-string/
)

import "github.com/fatih/color"

//структура, которая содержит инфу по папке/файлу
type dirItemInfo struct{
	name string
	isDir bool
	size int64
}

var folderColor =  color.New(color.FgHiMagenta)
var fileColor =   color.New(color.FgCyan).Add(color.Underline)

//уровень влдоженности папки
const folderLevelMAX = 2

//stat
var totalFoldersFound = 0
var totalFilesFound = 0

func main(){

	//predefinition
	var rootFolder = "C://"
	var folderLevel = 0

	fmt.Println("v 0.1 Directories *********** ")
	fmt.Println(time.Now())

	//work
	processDirectories(rootFolder, folderLevel)

	//farewells
	defer goodBy()
}

func processDirectories(root string, folderLevel int){

	fmt.Println("[Processing path: " + root + ". Folder level: " , folderLevel)

	//получить список файлов/папок
	fileNames, folderArray,  err := readDir(root)
	if err != nil {
        fmt.Println("Error:",  err)
	}

	fileNames = fileNames//mock

	printListOfItemsStr(folderArray, folderLevel)

	//folders...
	if (folderLevel+1 <= folderLevelMAX){

		//цикл
		for _, f := range folderArray {
			if (f.isDir){
				var newPath = root + "//" + f.name
				color.New(color.FgWhite).Println("[Processing new path for nested level", folderLevel+1, ". Path: ", newPath)
				processDirectories(newPath, folderLevel+1)
			}
		}
	}else{
		color.New(color.FgWhite).Println("[MAX level folder level reched for folder", root, "]. Level:", folderLevel )
	}
}

//work with directories
func readDir(root string) ([]string, []dirItemInfo, error) {
	var files []string
	var dirItems []dirItemInfo
    f, err := os.Open(root)
    if err != nil {
        return files,dirItems, err
    }
    fileInfo, err := f.Readdir(-1)
    f.Close()
    if err != nil {
        return files, dirItems, err
    }

    for _, file := range fileInfo {
		files = append(files, file.Name())
		
		//full info
		dirItem := dirItemInfo{isDir:file.IsDir(), name:file.Name(), size: file.Size()}
		//dirItems
		dirItems = append(dirItems, dirItem)
	}
	//sort array of structs
	dirItems = sortByDir(dirItems)

    return files, dirItems, nil
}

//print a list of files/directories
func printListOfItems(items []string){
	for _, i := range items {
       fmt.Println(i)
    }
}

//print struc
func printListOfItemsStr(items []dirItemInfo,  level int){
	
	if (len(items) == 0){
		fmt.Println("Folder empty")
	}	
	
	var (
		line = getFolderPrefixByFolderLevel(level) //top level = "" | level1 = "-->" etc.
		sizeString = ""		
		spaces = ""
	)
	
	for _, i := range items {	   
		var part1 = i.name
		if i.isDir{
			part1 = "["+part1+"]"
		}
		part1 = line + part1
		spaces = getWiteSpace(part1)
		if i.isDir== false{
			sizeString = getSizeAsString(i.size)
		}
		var FULLstr = part1 + spaces + sizeString

		if i.isDir{
			folderColor.Println(FULLstr)
			totalFoldersFound++
		}else{
			fileColor.Println(FULLstr)
			totalFilesFound++
		}
    }
}

//converts 102424... (bytes size) to Kb,  Mb etc. and return string repseresntation
func getSizeAsString(size int64)string{
	if size<1024 {
		return strconv.FormatInt(size, 10) + " b."
	}
	if (size<1024*1024){
		return strconv.FormatInt(size/1024, 10) + " Kb."
	}
	if (size<1024*1024*1024){
		return strconv.FormatInt(size/(1024*1024), 10) + " Mb."
	}else{
		return strconv.FormatInt(size/(1024*1024*1024), 10) + " Gb."
	}
}

//egt white space string
func getWiteSpace(startText string) string {
	var length = 30 - len(startText)
	var spaces = ""
	for i:=0; i<length; i++{
		spaces+= " "
	}
	return spaces
}

//get prefix to show graphically the hierrarchy (Parent --> Level1 ----> Level2 etc)
func getFolderPrefixByFolderLevel(folderLevel int ) string{
	//0 - TOP level,  no prefixes
	if (folderLevel ==0){
		return ""
	}
	if (folderLevel ==1){
		return "----> "
	}
	if (folderLevel == 2){
		return "------> "
	}else{
		return "-----------> "
	}
}

//exit
func goodBy(){
	fmt.Println("************************************************************")	
	fmt.Println("Folder processed:", totalFoldersFound)
	fmt.Println("Files processed: ", totalFilesFound)
	fmt.Println("\nGood by! Time: ",  time.Now())
}