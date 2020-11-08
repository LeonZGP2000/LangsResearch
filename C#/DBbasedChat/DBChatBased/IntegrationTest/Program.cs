using IntegrationTest.Tests;
using System;

namespace IntegrationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            new UserTest().Start();
            new CreateChatTest().Start();
            new MessagesTest().Start();
            new DialogTest().Start();

            Console.WriteLine("Everything if OK");
            Console.ReadKey();
        }
    }
}
