/*
 * Lab 3
 * Matthew McNatt
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ContactManager.BL;
using Message = ContactManager.BL.Message;

namespace ContactManager.UI
{
    public partial class MainForm : Form, IMessageService
    {
        public MainForm()
        {
            InitializeComponent();
        }

        //Exit event handler
        private void OnFileExit( object sender, EventArgs e )
        {
            Close();
        }

        //About click Event Handler
        private void OnAboutClicked( object sender, EventArgs e )
        {
            var form = new AboutBox();
            form.ShowDialog();
        }

        //OnFormClosing EventHandler
        protected override void OnFormClosing( FormClosingEventArgs e )
        {
            if (MessageBox.Show(this, "Are you sure?", "Close", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            };
            base.OnFormClosing(e);
        }

        //on Contact add Event Handler
        private void OnContactAdd( object sender, EventArgs e )
        {
            var form = new ContactForm();

            while (true)
            {
                if (form.ShowDialog(this) != DialogResult.OK)
                    return;
                try
                {
                    _contacts.Add(form.CurrentContact);
                    break;
                } catch (Exception ex)
                {
                    DisplayError(ex);
                };
            };

            BindList();
        }

        //method to bind database to the listbox
        private void BindList()
        {
            _listContacts.Items.Clear();
            _listContacts.DisplayMember = nameof(Contact.Name);
            var items = _contacts.GetAll();
            items = items.OrderBy(GetName);
            _listContacts.Items.AddRange(items.ToArray());
        }

        private string GetName(Contact contact)
        {
            return contact.Name;
        }
        //helper method to display an error message
        private void DisplayError( Exception ex )
        {
            MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // overriden OnLoad to bind list initially
        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad(e);

            BindList();
        }

        //Edit EventHandler
        private void OnContactEdit( object sender, EventArgs e )
        {
            var form = new ContactForm();
            var contact = GetSelectedContact();

            if (contact == null)
                return;

            form.CurrentContact = contact;
            form.Text = "Edit Contact";

            while(true)
            {
                if (form.ShowDialog(this) != DialogResult.OK)
                    return;

                try
                {
                    _contacts.Update(contact.Id, form.CurrentContact);
                    break;
                } catch (Exception ex)
                {
                    DisplayError(ex);
                };
            };

            BindList();
        }

        //gets the selected contact or returns null
        private Contact GetSelectedContact()
        {
            return _listContacts.SelectedItem as Contact;
        }

        //Database of contacts
        private IContactDataBase _contacts = new ContactDataBase();

        //Delete event handler
        private void OnContactDelete( object sender, EventArgs e )
        {
            var contact = GetSelectedContact();

            if (contact == null)
                return;

            if (MessageBox.Show(this, $"Are you sure you want to delete {contact.Name}?", "Confirm Delete?",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                _contacts.Remove(contact.Id);
            } catch (Exception ex)
            {
                DisplayError(ex);
            };
            BindList();

        }

        private void OnSendMessage( object sender, EventArgs e )
        {
            var contact = GetSelectedContact();
            if (contact == null)
                return;

            var form = new SendMessageForm();
            form.CurrentContact = contact;
            form.Text = $"Message to {contact.Name}";

            if (form.ShowDialog(this) != DialogResult.OK)
                return;

            Send(form.CurrentMessage);

        }

        public void Send(Message message)
        {
            _txtSentMessages.Text += $"To: {message.MessageContact}\r\nSubject: {message.Subject}\r\n{message.Body}\r\n\r\n";
            return;
        }
    }
}
