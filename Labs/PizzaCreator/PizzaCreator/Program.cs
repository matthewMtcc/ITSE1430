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
        private static bool meatBacon = false; //flag variables for meats with const price decimals
        private static bool meatHam = false;
        private static bool meatPepperoni = false;
        private static bool meatSausage = false;

        private static bool vegetablesBlackOlives = false; //falg variables for vegetables with const price decimals
        private static bool vegetablesMushrooms = false;
        private static bool vegetablesOnions = false;
        private static bool vegetablesPeppers = false;

        private static bool sauceTraditional = false; //flag variables for sauce with const price decimals
        private static bool sauceOregano = false;
        private static bool sauceGarlic= false;

        private static bool sizeSmall = false; //flag variables for sauce with const price decimals
        private static bool sizeLarge = false;
        private static bool sizeMedium = false;

        private static bool cheeseRegular = false; //flag variables for cheese with const price decimals
        private static bool cheeseExtra = false;

        private static bool PizzaAlreadyCreated = false; 
        private static bool PizzaDelivery = false;
        private static bool PizzaTakeout = false;



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

                Makeline();


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
            Makeline();
            GetVegetablesOption();
            Makeline();
            GetSauceOption();
            Makeline();
            GetCheeseOption();
            Makeline();
            GetDeliveryOption();
            





        }

        private static void GetSizeOption()
        {
            string usersChoice;
            int numberEntered;

            Console.WriteLine("Select a size for your pizza. You must select 1: ");
            Console.WriteLine("(1) Small: $5");
            Console.WriteLine("(2) Medium: $6.25");
            Console.WriteLine("(3) Large: $8.75");
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
            Makeline();

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


        private static void GetVegetablesOption()
        {
            string usersChoice;
            int numberEntered;

            Console.WriteLine("Select Vegetables for your pizza. Each option is an extra $0.50.");
            Console.WriteLine("Select an option again to deselect it.");
            Console.WriteLine("(1) Black olives");
            Console.WriteLine("(2) Mushrooms");
            Console.WriteLine("(3) Onion");
            Console.WriteLine("(4) Peppers");
            Console.WriteLine("(5) CONTINUE");
            Makeline();

            do
            {
                Console.WriteLine($"Your total is: {CalculatePrice():C}");
                DislaySelectedVegetables();
                Console.Write("\n");
                usersChoice = Console.ReadLine();

                if (ValidateInput(usersChoice, 5, out numberEntered) == false)
                    Console.WriteLine("Please select a valid option");

                if (numberEntered == 1 && vegetablesBlackOlives == false)
                    vegetablesBlackOlives = true;
                else if (numberEntered == 1 && vegetablesBlackOlives == true)
                    vegetablesBlackOlives = false;


                if (numberEntered == 2 && vegetablesMushrooms == false)
                    vegetablesMushrooms = true;
                else if (numberEntered == 2 && vegetablesMushrooms == true)
                    vegetablesMushrooms = false;

                if (numberEntered == 3 && vegetablesOnions == false)
                    vegetablesOnions = true;
                else if (numberEntered == 3 && vegetablesOnions == true)
                    vegetablesOnions = false;

                if (numberEntered == 4 && vegetablesPeppers == false)
                    vegetablesPeppers = true;
                else if (numberEntered == 4 && vegetablesPeppers == true)
                    vegetablesPeppers = false;

            } while (numberEntered != 5);

            return;
        }
        private static void GetSauceOption()
        {
            string usersChoice;
            int numberEntered;

            Console.WriteLine("Select a sauce for your pizza. You must select one: ");
            Console.WriteLine("(1) Traditional: $0");
            Console.WriteLine("(2) Garlic: $1");
            Console.WriteLine("(3) Oregano: $1");
            Console.WriteLine($"Your total is: {CalculatePrice():C}");

            do
            {
                usersChoice = Console.ReadLine();

                if (ValidateInput(usersChoice, 3, out numberEntered) == false)
                    Console.WriteLine("Please select a valid option");

                if (numberEntered == 1)
                    sauceTraditional = true;
                if (numberEntered == 2)
                    sauceGarlic = true;
                if (numberEntered == 3)
                    sauceOregano = true;
            } while (sauceTraditional == false && sauceGarlic == false && sauceOregano == false);

            return;
        }

        private static void GetCheeseOption()
        {
            string usersChoice;
            int numberEntered;

            Console.WriteLine("Select a cheese for your pizza. You must select one: ");
            Console.WriteLine("(1) Regular: $0");
            Console.WriteLine("(2) Extra: $1");
            Console.WriteLine($"Your total is: {CalculatePrice():C}");

            do
            {
                usersChoice = Console.ReadLine();

                if (ValidateInput(usersChoice, 3, out numberEntered) == false)
                    Console.WriteLine("Please select a valid option");

                if (numberEntered == 1)
                    cheeseRegular = true;
                if (numberEntered == 2)
                    cheeseExtra = true;
            } while (cheeseRegular == false && cheeseExtra == false);

            return;
        }

        private static void GetDeliveryOption()
        {
            string usersChoice;
            int numberEntered;

            Console.WriteLine("Do You want your pizza to be take out or delivery? You must select one: ");
            Console.WriteLine("(1) Take Out: $0");
            Console.WriteLine("(2) Delivery: $2.50");
            Console.WriteLine($"Your total is: {CalculatePrice():C}");

            do
            {
                usersChoice = Console.ReadLine();

                if (ValidateInput(usersChoice, 3, out numberEntered) == false)
                    Console.WriteLine("Please select a valid option");

                if (numberEntered == 1)
                    PizzaTakeout = true;
                if (numberEntered == 2)
                    PizzaDelivery = true;
            } while (cheeseRegular == false && cheeseExtra == false);

            return;
        }
        private static decimal CalculatePrice()
        {
            decimal CurrentPrice = 0;

            if (sizeSmall == true) //adds cost of size
                CurrentPrice = CurrentPrice + 5.0m;
            if (sizeMedium == true)
                CurrentPrice = CurrentPrice + 6.25m;
            if (sizeLarge == true)
                CurrentPrice = CurrentPrice + 5.0m;

            if (meatBacon == true) //adds cost of meats
                CurrentPrice = CurrentPrice + 0.75m;
            if (meatHam == true)
                CurrentPrice = CurrentPrice + 0.75m;
            if (meatPepperoni == true)
                CurrentPrice = CurrentPrice + 0.75m;
            if (meatSausage == true)
                CurrentPrice = CurrentPrice + 0.75m;

            if (vegetablesBlackOlives == true) //adds cost of Vegetables
                CurrentPrice = CurrentPrice + 0.50m;
            if (vegetablesMushrooms == true)
                CurrentPrice = CurrentPrice + 0.50m;
            if (vegetablesOnions == true)
                CurrentPrice = CurrentPrice + 0.50m;
            if (vegetablesPeppers == true)
                CurrentPrice = CurrentPrice + 0.50m;

            if (sauceTraditional == true) //adds cost of Sauce
                CurrentPrice = CurrentPrice + 0.00m;
            if (sauceGarlic == true)
                CurrentPrice = CurrentPrice + 1.00m;
            if (sauceOregano == true)
                CurrentPrice = CurrentPrice + 1.00m;


            if (cheeseRegular == true) //adds cost of cheese
                CurrentPrice = CurrentPrice + 0.00m;
            if (cheeseExtra == true)
                CurrentPrice = CurrentPrice + 1.00m;


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

        private static void DislaySelectedVegetables()
        {
            Console.Write("Your currently selected Vegetables are: ");

            if (vegetablesBlackOlives == true)
            {
                Console.Write("Black Olives ");
            }

            if (vegetablesMushrooms == true)
            {
                Console.Write("Mushrooms ");
            }

            if (vegetablesOnions == true)
                Console.Write("Onions ");

            if (vegetablesPeppers == true)
                Console.Write("Peppers ");
        }

        private static void Makeline()
        {
            Console.WriteLine("----------------------------------------");
        }
    }
}
