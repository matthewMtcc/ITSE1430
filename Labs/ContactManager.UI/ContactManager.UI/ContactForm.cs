/*
 * Lab 3
 * Matthew McNatt
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ContactManager.BL;

namespace ContactManager.UI
{
    public partial class ContactForm : Form
    {
        public ContactForm()
        {
            InitializeComponent();
        }

        //on Cancel Event Handler
        private void OnCancel( object sender, EventArgs e )
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        //Loads Data into text boxes
        private void LoadData( Contact contact )
        {
            _txtName.Text = contact.Name;
            _txtAddress.Text = contact.Address;
        }

        //saves data in textboxes to a new contact
        private Contact Savedata()
        {
            var contact = new Contact();

            contact.Name = _txtName.Text;
            contact.Address = _txtAddress.Text;

            return contact;

        }

        //OnSave Event Handler, validates at business and UI level
        private void OnSave( object sender, EventArgs e )
        {
            if (!ValidateChildren())
                return;

            var contact = Savedata();

            try
            {
                new ObjectValidator().Validate(contact);
            } catch (ValidationException)
            {
                MessageBox.Show(this, "Contact not valid.", "Error", MessageBoxButtons.OK);
                return;
            }
            CurrentContact = contact;
            DialogResult = DialogResult.OK;
            Close();
        }

        //Validation for name
        private void OnValidateName( object sender, CancelEventArgs e )
        {
            var tb = sender as TextBox;

            if (tb.Text.Length == 0)
            {
                _errors.SetError(tb, "Name is required.");
                e.Cancel = true;
            } else
                _errors.SetError(tb, "");
        }

        //Validation for address, will check if it is a valid email
        private void OnValidateAddress( object sender, CancelEventArgs e )
        {
            var tb = sender as TextBox;

            if (tb.Text.Length == 0)
            {
                _errors.SetError(tb, "Name is required.");
                e.Cancel = true;
            } else if (IsValidEmail(tb.Text) == false)
            {
                _errors.SetError(tb, "Invalid email Address");
                e.Cancel = true;
            } else
                _errors.SetError(tb, "");

        }

        //helper method for checking email format
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

        //OnLoad event Handler
        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad(e);

            if (CurrentContact != null)
                LoadData(CurrentContact);

            ValidateChildren();
        }

        /// <summary> Property to hold the Forms contact</summary>
        public Contact CurrentContact { get; set; }
    }
}
