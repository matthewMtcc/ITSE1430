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
        public const decimal MEAT_BACON_PRICE= 0.75m;
        private static bool meatHam = false;
        public const decimal MEAT_HAM_PRICE = 0.75m;
        private static bool meatPepperoni = false;
        public const decimal MEAT_PEPPERONI_PRICE = 0.75m;
        private static bool meatSausage = false;
        public const decimal MEAT_SAUSAGE_PRICE = 0.75m;

        private static bool vegetablesBlackOlives = false; //falg variables for vegetables with const price decimals
        public const decimal VEGETABLES_BLACK_OLIVES_PRICE = 0.50m;
        private static bool vegetablesMushrooms = false;
        public const decimal VEGETABLES_MUSHROOMS_PRICE = 0.50m;
        private static bool vegetablesOnions = false;
        public const decimal VEGETABLES_ONION_PRICE = 0.50m;
        private static bool vegetablesPeppers = false;
        public const decimal VEGETABLES_PEPPERS_PRICE = 0.50m;

        private static bool sauceTraditional = false; //flag variables for sauce with const price decimals
        public const decimal SAUCE_TRADITIONAL_PRICE = 0.0m;
        private static bool sauceOregano = false;
        public const decimal SAUCE_OREGANO_PRICE = 1.00m;
        private static bool sauceGarlic= false;
        public const decimal SAUCE_GARLIC_PRICE = 1.00m;

        private static bool sizeSmall = false; //flag variables for sauce with const price decimals
        public const decimal SIZE_SMALL_PRICE = 5.00m;
        private static bool sizeLarge = false;
        public const decimal SIZE_LARGE_PRICE = 8.75m;
        private static bool sizeMedium = false;
        public const decimal SIZE_MEDIUM_PRICE = 6.25m;

        private static bool cheeseRegular = false; //flag variables for cheese with const price decimals
        public const decimal CHEESE_REGULAR_PRICE = 0.00m;
        private static bool cheeseExtra = false;
        public const decimal CHEESE_EXTRA_PRICE = 1.25m;

        private static bool PizzaDelivery = false; //flag variabls for delivery and takout with const price decimals
        public const decimal PIZZA_DELIVERY_PRICE = 2.50m;
        private static bool PizzaTakeOut = false;
        public const decimal PIZZA_TAKE_OUT_PRICE = 0.00m;

        private static bool PizzaAlreadyCreated = false;



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

                    NewOrder(); //calls new order

                else if (numberEntered == 2)

                    Console.WriteLine("execute Modify order function\n"); //TODO

                else if (numberEntered == 3)
                    DisplayOrder();


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

            GetSizeOption(); //calls each get function which sets the flags to options user wants
            Makeline();      //if there can be only one chosen, the functions declare all options false before user picks
            GetMeatsOption(); 
            Makeline();
            GetVegetablesOption();
            Makeline();
            GetSauceOption();
            Makeline();
            GetCheeseOption();
            Makeline();
            GetDeliveryOption();

            PizzaAlreadyCreated = true; //sets flag for use with creating another new pizza or modifying a non existant pizza
            Console.WriteLine("\nYOUR PIZZA IS COMPLETE!");
            DisplayOrder();
            





        }

        private static void GetSizeOption()
        {
            string usersChoice; //variables to hold users choice
            int numberEntered;

            Console.WriteLine("Select a size for your pizza. You must select 1: ");
            Console.WriteLine($"(1) Small({SIZE_SMALL_PRICE:C})");
            Console.WriteLine($"(2) Medium({SIZE_MEDIUM_PRICE:C})");
            Console.WriteLine($"(3) Larg({SIZE_LARGE_PRICE:C})");
            Console.WriteLine($"Your total is: {CalculatePrice():C}");

            do
            {
                sizeSmall = false; //declares size variables false on every call so only one size is ever selected.
                sizeMedium = false;
                sizeLarge = false;

                usersChoice = Console.ReadLine();

                if (ValidateInput(usersChoice, 3, out numberEntered) == false)
                    Console.WriteLine("Please select a valid option");

                if (numberEntered == 1) //sets appropriate flag according to user choic
                    sizeSmall = true;
                if (numberEntered == 2)
                    sizeMedium = true;
                if (numberEntered == 3)
                    sizeLarge = true;
            } while (sizeLarge == false && sizeSmall == false && sizeMedium == false); //one mube chosen ad this cant be left blank

            return;
        }

        private static void GetMeatsOption()
        {
            string usersChoice;
            int numberEntered;


            Console.WriteLine("Select Meats for your pizza."); //displays meats menu
            Console.WriteLine("Select an option again to deselect it.");
            Console.WriteLine($"(1) Bacon({MEAT_BACON_PRICE:C})");
            Console.WriteLine($"(2) Ham({MEAT_HAM_PRICE:C})");
            Console.WriteLine($"(3) Pepperoni({MEAT_PEPPERONI_PRICE:C})");
            Console.WriteLine($"(4) Sausage({MEAT_SAUSAGE_PRICE:C})");
            Console.WriteLine("(5) CONTINUE");
            Makeline();

            do
            {
                Console.WriteLine($"Your total is: {CalculatePrice():C}"); //shows price
                DislaySelectedMeats();
                Console.Write("\n");
                usersChoice = Console.ReadLine();

                if (ValidateInput(usersChoice, 5, out numberEntered) == false)
                    Console.WriteLine("Please select a valid option");

                if (numberEntered == 1 && meatBacon == false) //for each option changes flag to true or false depending on if its already selected
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

            } while (numberEntered != 5); //user can tinker with meat options until continue is pushed

            return;
        }


        private static void GetVegetablesOption()
        {
            string usersChoice;
            int numberEntered;

            Console.WriteLine("Select Vegetables for your pizza."); //displays vegi menu
            Console.WriteLine("Select an option again to deselect it.");
            Console.WriteLine($"(1) Black olives({VEGETABLES_BLACK_OLIVES_PRICE:C})");
            Console.WriteLine($"(2) Mushrooms({VEGETABLES_MUSHROOMS_PRICE:C})");
            Console.WriteLine($"(3) Onion({VEGETABLES_ONION_PRICE:C})");
            Console.WriteLine($"(4) Peppers({VEGETABLES_PEPPERS_PRICE:C})");
            Console.WriteLine("(5) CONTINUE");
            Makeline();

            do
            {
                Console.WriteLine($"Your total is: {CalculatePrice():C}"); //displays price and lets user select and unselect options
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

            Console.WriteLine("Select a sauce for your pizza. You must select one: "); //displays sauce options with price
            Console.WriteLine($"(1) Traditional({SAUCE_TRADITIONAL_PRICE:C})");
            Console.WriteLine($"(2) Garlic({SAUCE_GARLIC_PRICE:C})");
            Console.WriteLine($"(3) Oregano({SAUCE_OREGANO_PRICE:C})");
            Console.WriteLine($"Your total is: {CalculatePrice():C}"); 

            do
            {
                sauceTraditional = false; //sets sauce flags to false before selection to ensure only one is ever selected
                sauceGarlic = false;
                sauceOregano = false;

                usersChoice = Console.ReadLine();

                if (ValidateInput(usersChoice, 3, out numberEntered) == false)
                    Console.WriteLine("Please select a valid option");

                if (numberEntered == 1)
                    sauceTraditional = true;
                if (numberEntered == 2)
                    sauceGarlic = true;
                if (numberEntered == 3)
                    sauceOregano = true;
            } while (sauceTraditional == false && sauceGarlic == false && sauceOregano == false); //ensures one is selected

            return;
        }

        private static void GetCheeseOption()
        {
            string usersChoice;
            int numberEntered;

            Console.WriteLine("Select a cheese for your pizza. You must select one: ");
            Console.WriteLine($"(1) Regular({CHEESE_REGULAR_PRICE:C})");
            Console.WriteLine($"(2) Extra({CHEESE_EXTRA_PRICE:C})");
            Console.WriteLine($"Your total is: {CalculatePrice():C}");

            do
            {
                cheeseRegular = false; //sets cheese flags to false so only one is ever selected 
                cheeseExtra = false;

                usersChoice = Console.ReadLine();

                if (ValidateInput(usersChoice, 3, out numberEntered) == false)
                    Console.WriteLine("Please select a valid option");

                if (numberEntered == 1)
                    cheeseRegular = true;
                if (numberEntered == 2)
                    cheeseExtra = true;
            } while (cheeseRegular == false && cheeseExtra == false); //ensures one is selected

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
                    PizzaTakeOut = true;
                if (numberEntered == 2)
                    PizzaDelivery = true;
            } while (cheeseRegular == false && cheeseExtra == false); //ensures one is selected

            return;
        }
        private static decimal CalculatePrice()
        {
            decimal CurrentPrice = 0;

            if (sizeSmall == true) //adds cost of size
                CurrentPrice = CurrentPrice + SIZE_SMALL_PRICE;
            if (sizeMedium == true)
                CurrentPrice = CurrentPrice + SIZE_MEDIUM_PRICE;
            if (sizeLarge == true)
                CurrentPrice = CurrentPrice + SIZE_LARGE_PRICE;

            if (meatBacon == true) //adds cost of meats
                CurrentPrice = CurrentPrice + MEAT_BACON_PRICE;
            if (meatHam == true)
                CurrentPrice = CurrentPrice + MEAT_HAM_PRICE;
            if (meatPepperoni == true)
                CurrentPrice = CurrentPrice + MEAT_PEPPERONI_PRICE;
            if (meatSausage == true)
                CurrentPrice = CurrentPrice + MEAT_SAUSAGE_PRICE;

            if (vegetablesBlackOlives == true) //adds cost of Vegetables
                CurrentPrice = CurrentPrice + VEGETABLES_BLACK_OLIVES_PRICE;
            if (vegetablesMushrooms == true)
                CurrentPrice = CurrentPrice + VEGETABLES_MUSHROOMS_PRICE;
            if (vegetablesOnions == true)
                CurrentPrice = CurrentPrice + VEGETABLES_ONION_PRICE;
            if (vegetablesPeppers == true)
                CurrentPrice = CurrentPrice + VEGETABLES_PEPPERS_PRICE;

            if (sauceTraditional == true) //adds cost of Sauce
                CurrentPrice = CurrentPrice + SAUCE_TRADITIONAL_PRICE;
            if (sauceGarlic == true)
                CurrentPrice = CurrentPrice + SAUCE_GARLIC_PRICE;
            if (sauceOregano == true)
                CurrentPrice = CurrentPrice + SAUCE_OREGANO_PRICE;


            if (cheeseRegular == true) //adds cost of cheese
                CurrentPrice = CurrentPrice + CHEESE_REGULAR_PRICE;
            if (cheeseExtra == true)
                CurrentPrice = CurrentPrice + CHEESE_EXTRA_PRICE;

            if (PizzaDelivery == true) //adds cost of transport
                CurrentPrice = CurrentPrice + PIZZA_DELIVERY_PRICE;
            if (PizzaTakeOut == true)
                CurrentPrice = CurrentPrice + PIZZA_TAKE_OUT_PRICE;


            return CurrentPrice;
        }

        private static void DislaySelectedMeats()
        {
            Console.Write("Your currently selected meats are: "); //displays each meat with a space for future entries

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
            Console.Write("Your currently selected Vegetables are: "); //displays selected vegetables with space for future entries

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

        private static void DislaySelectedSize() //for modify an order only
        {
            Console.Write("Your currently selected size is: ");

            if (sizeSmall == true)
                Console.Write("Small");

            if (sizeMedium== true)
                Console.Write("Medium");

            if (sizeLarge == true)
                Console.Write("Large");

        }

        private static void DislaySelectedSauce() //for modify an order only
        {
            Console.Write("Your currently selected sauce is: ");

            if (sauceTraditional == true)
                Console.Write("Traditional");

            if (sauceGarlic == true)
                Console.Write("Garlic");

            if (sauceOregano == true)
                Console.Write("Oregano");

        }

        private static void DislaySelectedTransprt() //for modif order only
        {
            Console.Write("You are currently ordering ");

            if (PizzaDelivery == true)
                Console.Write("Delivery");

            if (PizzaTakeOut == true)
                Console.Write("Take Out");
        }

        private static void DislaySelectedCheese() //for modify order only
        {
            Console.Write("Your currently selected cheese is: ");

            if (cheeseRegular == true)
                Console.Write("Regular");

            if (cheeseExtra == true)
                Console.Write("Extra");
        }

        private static void Makeline() //makes a line of dashes
        {
            Console.WriteLine("----------------------------------------");
        }

        private static void DisplayOrder()
        {
            Console.WriteLine("\nORDER");
            Makeline();
            if (sizeSmall == true)
                Console.WriteLine($"Small Pizza        {SIZE_SMALL_PRICE:C}"); //displays size with price
            if (sizeMedium == true)
                Console.WriteLine($"Medium Pizza       {SIZE_SMALL_PRICE:C}");
            if (sizeLarge == true)
                Console.WriteLine($"Large Pizza        {SIZE_SMALL_PRICE:C}");

            if (PizzaDelivery == true)
                Console.WriteLine($"Delivery           {PIZZA_DELIVERY_PRICE}"); //displays transport with price
            if (PizzaTakeOut == true)
                Console.WriteLine($"Take Out           {PIZZA_DELIVERY_PRICE}");

            Console.WriteLine("Meats");
            if ((meatBacon == false && meatHam == false) && (meatPepperoni == false && meatSausage == false)) //displays meat header and all meets selected
                Console.WriteLine("   none");
            if (meatBacon == true)
                Console.WriteLine($"   bacon           {MEAT_BACON_PRICE:C}");
            if (meatHam == true)
                Console.WriteLine($"   Ham             {MEAT_HAM_PRICE:C}");
            if (meatPepperoni == true)
                Console.WriteLine($"   Pepperoni       {MEAT_PEPPERONI_PRICE:C}");
            if (meatSausage == true)
                Console.WriteLine($"   Sausage         {MEAT_BACON_PRICE:C}");

            Console.WriteLine("Vegetables");
            if ((vegetablesBlackOlives == false && vegetablesMushrooms == false) && (vegetablesOnions == false && vegetablesPeppers == false)) //displays vegi header and vegis selected.
                Console.WriteLine("   none");
            if (vegetablesBlackOlives == true)
                Console.WriteLine($"   Black Olives    {VEGETABLES_BLACK_OLIVES_PRICE:C}");
            if (vegetablesMushrooms == true)
                Console.WriteLine($"   Mushrooms       {VEGETABLES_MUSHROOMS_PRICE:C}");
            if (vegetablesOnions == true)
                Console.WriteLine($"   Onions          {VEGETABLES_ONION_PRICE:C}");
            if (vegetablesPeppers == true)
                Console.WriteLine($"   Peppers         {VEGETABLES_PEPPERS_PRICE:C}");

            Console.WriteLine("Sauce");
            if (sauceTraditional == true)
                Console.WriteLine($"  Traditional      {SAUCE_TRADITIONAL_PRICE:C}"); //displays sauce and price
            if (sauceGarlic == true)
                Console.WriteLine($"  Garlic           {SAUCE_GARLIC_PRICE:C}");
            if (sauceOregano == true)
                Console.WriteLine($"  Oregano          {SAUCE_TRADITIONAL_PRICE:C}");

            Makeline();
            Console.WriteLine($"Price              {CalculatePrice():C}");


        }

    }
}
