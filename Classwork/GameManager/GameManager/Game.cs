﻿using System;

namespace GameManager
{
    /// <summary>Represents a game.</summary>
    public class Game
    {
        /// <summary>Gets or sets the name of the game.</summary>
        public string Name
        {
            get { return _name ?? ""; }
            set { _name = value ?? ""; }
        }

        public int Id { get; set; }

        /// <summary>Gets or sets the description.</summary>
        public string Description
        {
            get { return _description ?? ""; }
            set { _description = value; }
        }

        /// <summary>Gets or sets the price.</summary>
        public decimal Price { get; set; }

        /// <summary>Determines if the game is owned.</summary>
        public bool Owned { get; set; } = true;

        /// <summary>Determines if the game is completed.</summary>
        public bool Completed { get; set; }

        /// <summary>Converts the object to a string.</summary>
        /// <returns>The string equivalent.</returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>Validates the object.</summary>
        /// <returns>true if valid or false otherwise.</returns>
        public bool Validate( /* Game this */ )
        {
            //Redundant use of this
            //var str = this.Name;

            //Name is required
            if (String.IsNullOrEmpty(Name))
                return false;

            //Price >= 0
            if (Price < 0)
                return false;

            //Only if you need to pass the instance to somebody else
            //MyType.Foo(this);

            return true;
        }

        #region Private Members

        private string _name = "";
        private string _description = "";

        #endregion

        #region Demo Code Only

        #region Constructors

        //Default, no return type
        // 1) Cannot be called directly
        // 2) Errors are very bad
        // 3) Should behave no different than doing it manually        
        public Game()
        {
            //Complex init
            var x = 1 + 2;
        }

        //Constructor chaining
        public Game( string name ) : this(name, 0)
        {
            //Name = name;
        }

        //As soon as you define a ctor, no default ctor anymore
        public Game( string name, decimal price )// : this()
        {
            Name = name;
            Price = price;
        }
        #endregion

        //Calculated property
        /*public bool IsCoolGame
        {
            get { return Publisher != "EA"; }
        }*/

        //Mixed accessibility
        //public double Rate { get; internal set; }

        //public void Foo()
        //{
        //    //NOT DETERMINISTIC - should have been a method
        //    var now = DateTime.Now;
        //}


        //Setter only
        //public string Password
        //{
        //    set { }
        //}

        //Auto property equivalent
        //public decimal Price
        //{
        //    get { return _price; }
        //    set { _price = value; }
        //}
        //private decimal _price;

        //Can init the data as well
        //public string[] Genres { get; set; }

        // Don't use array properties because they require cloning
        // and are inefficient
        //public string[] Genres
        //{
        //    get 
        //    {
        //        var temp = new string[_genres.Length];
        //        Array.Copy(_genres, temp, _genres.Length);
        //        return temp;
        //    }
        //}
        //private string[] _genres;

        //public string[] genres = new string[10];
        //private decimal realPrice = Price;
        #endregion
    }
}

