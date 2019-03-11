using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterCreator
{
    public class Character
    {
        /// <summary>Gets or sets the name of the Character.</summary>
        public string Name
        {
            get { return _name ?? ""; }
            set { _name = value ?? ""; }
        }

        /// <summary>Gets or sets the profession of the Character.</summary>
        public string Profession
        {
            get { return _profession ?? ""; }
            set { _profession = value ?? ""; }
        }

        /// <summary>Gets or sets the race of the Character.</summary>
        public string Race
        {
            get { return _race ?? ""; }
            set { _race = value ?? ""; }
        }

        /// <summary>Gets or sets the description of the Character.</summary>
        public string Description
        {
            get { return _description ?? ""; }
            set { _description = value ?? ""; }
        }



        //All attributes will assign 50 to the value if an invalid number is passed
        /// <summary>Gets or sets the Strength of the Character.</summary>
        public int Strength
        {
            get { return _strength; }
            set { _strength = (value > 0 && value < 101) ? value : 50; }
        }

        public int Intellignece
        {
            get { return _intelligence; }
            set { _intelligence = (value > 0 && value < 101) ? value : 50; }
        }

        public int Agility
        {
            get { return _agility; }
            set { _agility = (value > 0 && value < 101) ? value : 50; }
        }

        public int Constitution
        {
            get { return _constitution; }
            set { _constitution = (value > 0 && value < 101) ? value : 50; }
        }

        public int Charisma
        {
            get { return _charisma; }
            set { _charisma = (value > 0 && value < 101) ? value : 50; }
        }

        //private stat fields
        private int _constitution;
        private int _agility;
        private int _intelligence;
        private int _charisma;
        private int _strength;

        //assorted fields
        private string _description;
        private string _race;
        private string _profession;
        private string _name;

    }
}
