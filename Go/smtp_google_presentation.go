package main   //reusable code - namespace в .NET
import (
	"fmt"      // строки, форматирование      : http://golang-book.ru/chapter-11-packages.html
	"os"       // файловаыя система           : http://golang-book.ru/chapter-13-core-packages.html
	"bufio"    // read from keyboard          : аналог Stream в .NET : https://metanit.com/go/tutorial/8.9.php
	"strconv"  // convertations               : https://golang.org/pkg/strconv/
	"strings"  // operations with strings     : https://golang.org/pkg/strings/#Split
	"net/smtp" // var 1 - send email          : SMTP Protocol support
			   // var 2 - "github.com/jordan-wright/email" (easy)  
			   // import ( "github.com/jordan-wright/email" )
			   // project name / folders.../filename => filename.func()
	)

//google account settings
type gmailSettings struct{
	login string
	password string
}

//SMTP server statisticks
type  smtpStat struct {
	sendedEmail int
	notSent int
}

//SMTP server settings
type  smtpData struct {
	port int
	host string
	requestTimeout int
} 

//Email message
type emailMesgObject struct {
	recipientList []string
	body string
	subject string
	from string 
}

//тип присваивается по выражению после "="

//email server settings
var settings smtpData     = smtpData {port: 587, host: "smtp.gmail.com", requestTimeout:  30}
//statisticks
var  statisticks smtpStat = smtpStat{0, 0}
//gmail login
var  gmail gmailSettings  = gmailSettings{"leoniddeutschmail@gmail.com", "87648765bGd155555555555777456aA!"}

func main(){
	//load settings (SAMPLE)
	loadSmtpSettings()

	defer beforeExit()

	fmt.Println("****** Hello ******")

	var msg = emailMesgObject{ from: "default_email@gmail.com" }

	//recipients
	fmt.Println("Enter recipients (',' separator):")
	var emailRecipients = readTextFromUser()
	msg.recipientList = splitRecipientsToArray(emailRecipients)

	//body
	fmt.Println("Enter text:")
	msg.body = readTextFromUser()

	//subject
	fmt.Println("Enter subject:")
	msg.subject = readTextFromUser()

	//send email & stat
	if sendEmail(msg, gmail) {
		statisticks.sendedEmail ++
	} else {  //achtung - запись!
		statisticks.notSent ++
	}

	fmt.Println( emailRecipients)
}

//before exite - show stat
func beforeExit(){
	fmt.Println( 
		">>" + 
		strconv.Itoa(statisticks.sendedEmail) + //convert int  to string
		 "|" + 
		 strconv.Itoa(statisticks.notSent) +    //convert int  to string
		 "\nGood-by!");
}

//create file and write
func createFileAndWriteLog(msg string){
	//open dir/file - os.Open(path)
	file, err := os.Create("test.txt")
	if err != nil{  
		panic("Can't create file")  
	}
	file.WriteString(msg)
}

//Exceptions
func throwException(msg string){
	panic("Exception. Message: " + msg)
}

//read text from Keyboard
func readTextFromUser()(string){
	//only one word
	//fmt.Fscan( os.Stdin, &emailMessage)
	//read line with 
	scanner := bufio.NewScanner(os.Stdin)
	scanner.Scan()
	emailMessage := scanner.Text();
	return emailMessage
}

//convert "a1@gmail.com, a2@gmail.com" string to [a1@gmail.com, a2@gmail.com] array
func splitRecipientsToArray(input string) ([]string) {
	var recipients []string 
	var emails = strings.Split(input, ",")

	for i:=0; i<len(emails); i++{
		var mail = emails[i]
		if strings.Contains( mail, "@" ){
			recipients = append(recipients, mail)
		}
	}

	fmt.Println( "Recipients found [", len( recipients), "] : ", recipients )
    return recipients
}

//load settings from file
func loadSmtpSettings(){
	var filePath = "C:\\log.txt"
	//read from file -> JSON to struct
	fmt.Println("log path: " , filePath)
}

//send email, using smtp server
func sendEmail(email emailMesgObject, gmail gmailSettings) (bool) {

	err := smtp.SendMail( 
		settings.host + ":" + strconv.Itoa(settings.port), 
		smtp.PlainAuth("", gmail.login , gmail.password, settings.host),
		email.from,
		email.recipientList,
		[]byte(email.body))
	
		if err != nil{
			fmt.Println("SMTP error sending: &s",  err)
			return false;
		}
		return true;
}