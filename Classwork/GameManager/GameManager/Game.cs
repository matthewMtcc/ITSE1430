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
       /// <summary> Name of the game </summary>
       public string Name;
       
       public string Publisher;
       public decimal Price;
       public bool Owned;
       public bool Completed;
    }
}
