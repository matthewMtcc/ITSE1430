using System.Collections.Generic;

namespace ContactManager.BL
{
    public interface IContactDatabase
    {
        Contact Add( Contact contact );
        Contact Get( int id );
        IEnumerable<Contact> GetAll();
        void Remove( int id );
        Contact Update( int id, Contact contact );
    }
}