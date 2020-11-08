#include <stdlib.h>
#include <stdio.h>
#include <string.h>

void TestModuleOperationSucceed();                 //test logic
void Encrypt();                                    // encrypt raw text
void Decrypt(char decodedText[10]);                // decrypt encrypted text
char GetCharByCode(char collection[10], int code); //get char by it's code in alfabet
int GetKeyCode(char collection[10], char ch);      //get code of char {ch} in alfabet

char alfabet[10] = {'a','b','c','d','e','f','g','h','i','j'}; 
char notepad[10] = {'a','g','a','f','e','b','a','c','j','h'}; 

int main()
{
    printf("Encrypt/Decrypt \n\n");

    Encrypt();

   // test
   // TestModuleOperationSucceed();

    return 0;
}

void Encrypt()
{
    //read msg
    printf("Enter input text [10 symbols]:\n");

    char line[10];
    char enc_line[10];
    fgets(line, sizeof(line), stdin);

    //encrypt
    for (int i = 0; i < strlen(line); i++)
    {
        int alf_code = GetKeyCode(alfabet, line[i]);
        int key_code = GetKeyCode(alfabet, notepad[i] );

        if (line[i] != '\n')
        {
            int encrypted_char = (alf_code + key_code) % 10;

            char enc_char = GetCharByCode(alfabet, encrypted_char);
            enc_line[i] = enc_char;

            printf("E: [%d + %d mod 10 => %d | %c] \n", alf_code, key_code, encrypted_char, enc_char);
        }
    }

    printf("=========================================== \n");

    Decrypt(enc_line);
}


void Decrypt(char encodedText[10])
{
    int length = strlen(encodedText);
    int decryptedChars[length]; 

    for (int i = 0; i < length; i++)
    {
        if (encodedText[i] == '\n')
            continue;

        int alf_code = GetKeyCode(alfabet, encodedText[i]);
        int key_code = GetKeyCode(alfabet, notepad[i] );

        int min = alf_code - key_code;

        int decrypted_char = min % 10;
        decryptedChars[i] = decrypted_char;

        char dec_char = GetCharByCode(alfabet, decrypted_char);

        if (alf_code != 999 && key_code != 999)
            printf("D: [%d - %d mod 10 => %d | %c] \n", alf_code, key_code, decrypted_char, dec_char);
    }
}


char GetCharByCode(char collection[10], int code)
{
    for (int i = 0; i < strlen(collection); i++)
    {
        if (i == code)
            return collection[i];   
    }

    return 'z'; // key not found
}

int GetKeyCode(char collection[10], char ch)
{
    int length = strlen(collection);

    for (int i = 0; i < length; i++)
    {
        if (collection[i] == ch)
            return i;   
    }

    return 999; // key not found
}

//OK
void TestModuleOperationSucceed()
{
    int messageLength = 10;

    int raw = 200;
    int key = 111;

    //encrypt - сложение по модулю
    int char1 = (2 + 1) % messageLength;
    int char2 = (0 + 1) % messageLength;
    int char3 = (0 + 1) % messageLength;
    
    //decrypt - вычитание по модулю
    int raw1 = (char1 - 1) % messageLength; 
    int raw2 = (char2 - 1) % messageLength;
    int raw3 = (char3 - 1) % messageLength;

    printf("char1: %d \n",char1);
    printf("char2: %d \n",char2);
    printf("char3: %d \n",char3);
    printf("raw1:  %d \n",raw1);
    printf("raw2:  %d \n",raw2);
    printf("raw3:  %d \n",raw3);

    if (raw1 !=2)
        printf("raw1 is WRONG");
    if (raw2 !=0)
        printf("raw2 is WRONG");
    if (raw3 !=0)
        printf("raw3 is WRONG");
}