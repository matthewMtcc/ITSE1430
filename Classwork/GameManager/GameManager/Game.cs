using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManager
{
    /// <summary> Represents a game </summary>
    public class Game
    {
        /// <summary>Name of the game.</summary>
        public string Name
        {
            get { return _name ?? ""; }//null coalescing operatr, will set to left value if right is null
            set { _name = value ?? ""; }
        }

        private string _name = "";

        /// <summary>Publisher of the game.</summary>
        public string Publisher
        {
            get { return _publisher ?? ""; } //defacto string property
            set { _publisher = value; }
        }
        private string _publisher = "";

        //Calculated property
        public bool IsCoolGame //this is cool
        {
            get { return Publisher != "EA"; }
        }

        //public string Password //its there, you must have either get or set.
        //{
        //    set { }
        //}

        //Auto property
        public decimal Price { get; set; }

        //public decimal Price
        //{
        //    get { return _price; }
        //    set { _price = value; }
        //}
        //private decimal _price;

        public bool Owned { get; set; } = true; //compiler sugar, option on auto to initialize.

        public bool Completed { get; set; }

        //Mixed accessibility
        public double Rate //they can read bunt not write, not needed alot, cant mod both
        {
            get;
           internal set;
        }

        public void Foo()
        {
            //NOT DETERMINISTIC - should have been a method
            var now = DateTime.Now;
        }
        
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
        /// <summary> Validates the Object </summary>
        /// <returns></returns>
        public bool Validate(/* Game this*/)
        {
            //redndant
            //var str = this.Name;

            //Name is required
            if (String.IsNullOrEmpty(Name))
                return false;

            //Price >=0
            if (Price < 0)
                return false;
            //Only if you need to pass the instnce to somebody else
            //MyType.Foo(this);
            return true;
        }

    }
}
