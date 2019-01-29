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
        private static bool meatBacon = false; //flag variables for meats
        private static bool meatHam = false;
        private static bool meatPepperoni = false;
        private static bool meatSausage = false;

        private static bool vegetablesBlackOlives = false; //falg variables for vegetables
        private static bool vegetablesMushrooms = false;
        private static bool vegetablesUnions = false;
        private static bool vegetablesPeppers = false;

        private static bool sauceTraditional = false; //flag variables for sauce
        private static bool sauceOregano = false;
        private static bool sauceGarlic= false;

        private static bool sizeSmall = false; //flag variables for sauce
        private static bool sizeLarge = false;
        private static bool sizeMedium = false;

        private static bool cheeseRegular = false; //flag variables for cheese
        private static bool cheeseExtra = false;

        private static bool PizzaAlreadyCreated = false; 
        private static bool PizzaDelivery = false;




        static void Main( string[] args )
        {
            string usersChoice; //declares string containing input, int to hold validated choice, and bool to signify valid input
            int numberEntered;
            bool succesfulCheck;

            do
            {
                DisplayMenu();
                Console.WriteLine("Enter numbers 1-4 to select an option");
                usersChoice = Console.ReadLine();
                succesfulCheck = ValidateInput(usersChoice, 4,  out numberEntered);

                while (succesfulCheck == false) //loop that will prompt user till valid input is entered
                {
                    Console.WriteLine("Error: you must enter a number between 1 and 4");
                    usersChoice = Console.ReadLine();
                    succesfulCheck = ValidateInput(usersChoice, 4, out numberEntered);
                }


                if (numberEntered == 1)

                    NewOrder();

                else if (numberEntered == 2)
                    Console.WriteLine("execute new order function\n");
                else if (numberEntered == 3)
                    Console.WriteLine("execute new order function\n");
                //else for check
            } while (numberEntered != 4);
        }

        private static void DisplayMenu()
        {
            //displays menu
            Console.WriteLine("PIZZA CREATOR\n");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("New Order(1)\n");
            Console.WriteLine("Modify Order(2)\n");
            Console.WriteLine("Display Order(3)\n");
            Console.WriteLine("Quit (4)\n");
            //TODO: add calculate price method

        }

        //funtion that returns true if valid input was entered and an out parameter for parsed input
        private static bool ValidateInput(string userResponse, int maxOption,  out int result)
        {
            //parses and checks if number is between 1 andthe maxOption parameter
            if (Int32.TryParse(userResponse, out result) && result < (maxOption + 1) && result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static void NewOrder()
        {
            string usersChoice; 
            int numberEntered;

            if (PizzaAlreadyCreated == true) //a catch for if a user has already created a pizza.
            {
                Console.WriteLine("You have already created a pizza. Would you like to create a new one? ENTER: \"1\" for yes or \"2\" for no");
                usersChoice = Console.ReadLine();
                while (ValidateInput(usersChoice, 2, out numberEntered) == false)
                {
                    Console.WriteLine("please enter a valid number");
                    usersChoice = Console.ReadLine();
                }
                if (numberEntered == 2)
                    return;
            }
            GetSizeOption();
            Makeline();
            GetMeatsOption();





        }

        private static void GetSizeOption()
        {
            string usersChoice;
            int numberEntered;

            Console.WriteLine("Select a size for your pizza. You must select 1: ");
            Console.WriteLine("(1) Small: $5");
            Console.WriteLine("(2) Medium: $6.25");
            Console.WriteLine("(3) Lmall: $8.75");
            Console.WriteLine($"Your total is: {CalculatePrice():C}");

            do
            {
                usersChoice = Console.ReadLine();

                if (ValidateInput(usersChoice, 3, out numberEntered) == false)
                    Console.WriteLine("Please select a valid option");

                if (numberEntered == 1)
                    sizeSmall = true;
                if (numberEntered == 2)
                    sizeMedium = true;
                if (numberEntered == 3)
                    sizeLarge = true;
            } while (sizeLarge == false && sizeSmall == false && sizeMedium == false);

            return;
        }

        private static void GetMeatsOption()
        {
            string usersChoice;
            int numberEntered;

            Console.WriteLine("Select Meats for your pizza. Each option is an extra $0.75.");
            Console.WriteLine("Select an option again to deselect it.");
            Console.WriteLine("(1) Bacon");
            Console.WriteLine("(2) Ham");
            Console.WriteLine("(3) Pepperoni");
            Console.WriteLine("(4) Sausage");
            Console.WriteLine("(5) CONTINUE");

            do
            {
                Console.WriteLine($"Your total is: {CalculatePrice():C}");
                DislaySelectedMeats();
                Console.Write("\n");
                usersChoice = Console.ReadLine();

                if (ValidateInput(usersChoice, 5, out numberEntered) == false)
                    Console.WriteLine("Please select a valid option");

                if (numberEntered == 1 && meatBacon == false)
                    meatBacon = true;
                else if (numberEntered == 1 && meatBacon == true)
                    meatBacon = false;


                if (numberEntered == 2 && meatHam == false)
                    meatHam = true;
                else if (numberEntered == 2 && meatHam == true)
                    meatHam = false;

                if (numberEntered == 3 && meatPepperoni == false)
                    meatPepperoni = true;
                else if (numberEntered == 3 && meatPepperoni == true)
                    meatPepperoni = false;

                if (numberEntered == 4 && meatSausage == false)
                    meatSausage = true;
                else if (numberEntered == 4 && meatSausage == true)
                    meatSausage = false;

            } while (numberEntered != 5);

            return;
        }

        private static decimal CalculatePrice()
        {
            decimal CurrentPrice = 0;

            if (sizeSmall == true)
                CurrentPrice = CurrentPrice + 5.0m;
            if (sizeMedium == true)
                CurrentPrice = CurrentPrice + 6.25m;
            if (sizeLarge == true)
                CurrentPrice = CurrentPrice + 5.0m;

            return CurrentPrice;
        }

        private static void DislaySelectedMeats()
        {
            Console.Write("Your currently selected meats are: ");

            if (meatBacon == true)
            {
                Console.Write("Bacon ");
            }

            if (meatHam == true)
            {
                Console.Write("Ham ");
            }

            if (meatPepperoni == true)
                Console.Write("Pepperoni ");

            if (meatSausage == true)
                Console.Write("Sausage ");
        }

        private static void Makeline()
        {
            Console.WriteLine("----------------------------------------");
        }
    }
}
