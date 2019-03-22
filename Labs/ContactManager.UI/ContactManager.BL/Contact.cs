using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.BL
{
    public class Contact : IValidatableObject
    {
        /// <summary>Gets or sets the name of the Contact.</summary>
        public string Name
        {
            get { return _name ?? ""; }
            set { _name = value ?? ""; }
        }

        /// <summary>Gets or sets the address of the Contact.</summary>
        public string Address
        {
            get { return _address ?? ""; }
            set { _address = value ?? ""; }
        }

        /// <summary>Contact Id for DataBase</summary>
        public int Id { get; set; }

        //helper method that checks for valid email address
        private bool IsValidEmail( string source )
        {
            try
            {
                new System.Net.Mail.MailAddress(source);
                return true;
            } catch
            { };

            return false;
        }

        //implements IValidatableObject interface
        public IEnumerable<ValidationResult> Validate( ValidationContext validationContext)
        {
            var items = new List<ValidationResult>();

            //name is required
            if (String.IsNullOrEmpty(Name))
                items.Add(new ValidationResult("Name is required.", new[] { nameof(Name) }));

            if(IsValidEmail(_address) == false)
                items.Add(new ValidationResult("Invalid email Address", new[] { nameof(Address) }));

            return items;
        }

       //private backing field
        private string _name;
        private string _address;
    }
}
