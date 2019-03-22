using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.BL
{
    public class Message : IValidatableObject
    {
        /// <summary>Gets or sets email address the message is meant for.</summary>
        private string MessageContact
        {
            get { return _messageContact ?? ""; }
            set { _messageContact = value ?? ""; }
        }

        /// <summary>Gets or sets the Subject of the message.</summary>
        private string Subject
        {
            get { return _subject ?? ""; }
            set { _subject = value ?? ""; }
        }

        /// <summary>Gets or sets the body of the message.</summary>
        private string Body
        {
            get { return _body ?? ""; }
            set { _body = value ?? ""; }
        }

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

        //implements ValidatableObject interface
        public IEnumerable<ValidationResult> Validate( ValidationContext validationContext )
        {
            var items = new List<ValidationResult>();

            if (String.IsNullOrEmpty(Subject))
                items.Add(new ValidationResult("Subject is required", new[] { nameof(Subject) }));

            if (String.IsNullOrEmpty(Body))
                items.Add(new ValidationResult("Body is required", new[] { nameof(Body) }));

            if (IsValidEmail(MessageContact) == false)
                items.Add(new ValidationResult("Invalid email address", new[] { nameof(MessageContact) }));


            return items;

        }

        //private fields
        private string _body;
        private string _subject;
        private string _messageContact;
    }
}
