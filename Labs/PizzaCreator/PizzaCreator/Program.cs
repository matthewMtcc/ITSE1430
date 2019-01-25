/*
 * Lab 1 
 * Matthew McNatt
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCreator
{
    class Program
    {
        static void Main( string[] args )
        {
            DisplayMenu();
        }

        private static void DisplayMenu()
        {
            string usersChoice; //declares string containing input, int to hold validated choice, and bool to signify valid input
            int numberEntered;
            bool succesfulCheck;

            //displays menu
            Console.WriteLine("PIZZA CREATOR\n");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("New Order(1)\n");
            Console.WriteLine("Modify Order(2)\n");
            Console.WriteLine("Display Order(3)\n");
            Console.WriteLine("Quit (4)\n");

            do
            {
                Console.WriteLine("Enter a numbers 1-4 to select an option");
                usersChoice = Console.ReadLine();
                succesfulCheck = ValidateInput(usersChoice, out numberEntered);

                while (succesfulCheck == false) //loop that will prompt user till valid input is entered
                {
                    Console.WriteLine("Error: you must enter a numbers between 1 and 4");
                    usersChoice = Console.ReadLine();
                    succesfulCheck = ValidateInput(usersChoice, out numberEntered);
                }


                if (numberEntered == 1)
                    Console.WriteLine("execute new order function\n");
                else if (numberEntered == 2)
                    Console.WriteLine("execute new order function\n");
                else if (numberEntered == 3)
                    Console.WriteLine("execute new order function\n");
                else
                    return;
            } while (numberEntered != 4);
        }

        //funtion that returns true if valid input was entered and an out parameter for parsed input
        private static bool ValidateInput(string userResponse, out int result)
        {
            //parses and checks if number is between 1-4
            if (Int32.TryParse(userResponse, out result) && result < 5 && result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
