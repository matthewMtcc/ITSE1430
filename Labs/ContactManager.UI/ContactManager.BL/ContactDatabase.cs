using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.BL
{
    public class ContactDataBase : IContactDataBase
    {
        public Contact Add ( Contact contact )
        {
            //makes sure there is a game
            if (contact == null)
                throw new ArgumentNullException(nameof(contact));

            //validates object
            new ObjectValidator().Validate(contact);

            //checks to see if Contact exists
            var existing = GetIndex(contact.Name);
            if (existing >= 0)
                throw new Exception("Contact must be Unique");

            //adds contact to the list
            _contacts.Add(new Contact() {Name = contact.Name, Address = contact.Address, Id = ++_nextId });

            //returns contact
            return contact;
        }

        //updates an Existing Contact
        public Contact Update( int id, Contact contact )
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be > 0");
            if (contact == null)
                throw new ArgumentNullException(nameof(contact));

            new ObjectValidator().Validate(contact);

            var index = GetIndex(id);
            if (index < 0)
                throw new Exception("Contact does not exist");

            var existingIndex = GetIndex(contact.Name);
            if (existingIndex >= 0 && existingIndex != index)
                throw new Exception("Contact must be Unique");

            contact.Id = id;
            _contacts[index] = (new Contact() { Name = contact.Name, Address = contact.Address, Id = id });

            return contact;

        }

        //removes a contact
        public void Remove(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater then 0");

            var index = GetIndex(id);

            if(index >= 0)
            _contacts.RemoveAt(index);
        }

        //returns a clone of an existing Contact
        public Contact Get(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater then 0");
            var index = GetIndex(id);
            if (index >= 0)
                return Clone(_contacts[index]);

            return null;

        }

        //returns an IEnumerable list
        public IEnumerable<Contact> GetAll()
        {
            var temp = new List<Contact>();
            foreach (var contact in _contacts)
                temp.Add(Clone(contact));

            return temp.ToArray();
        }

        //Helper method: clones a contact
        private Contact Clone(Contact contact)
        {
            var newContact = new Contact()
            {Name =contact.Name, Address = contact.Address, Id = contact.Id };

            return newContact;
            
        }

        //gets index of a contact and returns -1 if no such contact exists
        private int GetIndex( string name)
        {
            for (var index = 0; index < _contacts.Count; ++index)
                if (String.Compare(_contacts[index]?.Name, name, true) == 0)
                    return index;

            return -1;
        }

        //overload that gets contact index based on its Id
        private int GetIndex( int id )
        {
            for (var index = 0; index < _contacts.Count; ++index)
                if (_contacts[index]?.Id == id)
                    return index;

            return -1;
        }

        private int _nextId = 0;
        private readonly List<Contact> _contacts = new List<Contact>();
    }
}
