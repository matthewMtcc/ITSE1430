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
using Message = ContactManager.BL.Message;

namespace ContactManager.UI
{
    public partial class SendMessageForm : Form
    {
        public SendMessageForm()
        {
            InitializeComponent();
        }

        private Message Savedata()
        {
            var message = new Message();

            message.Subject = _txtSubject.Text;
            message.Body = _txtBody.Text;
            message.MessageContact = CurrentContact.Address;           

            return message;
        }

        public Contact CurrentContact { get; set; }
        public Message CurrentMessage { get; set; }

        private void OnCancel( object sender, EventArgs e )
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad(e);

            _lblAddress.Text = CurrentContact.Address;

            ValidateChildren();
        }

        private void OnValidateSubject( object sender, CancelEventArgs e )
        {
            var tb = sender as TextBox;

            if (tb.Text.Length == 0)
            {
                _errors.SetError(tb, "Name is required.");
                e.Cancel = true;
            } else
                _errors.SetError(tb, "");
        }

        private void OnSave( object sender, EventArgs e )
        {
            if (!ValidateChildren())
                return;

            var message = Savedata();

            try
            {
                ObjectValidator.Validate(message);
            } catch (ValidationException)
            {
                MessageBox.Show(this, "Message is not valid.", "Error", MessageBoxButtons.OK);
                return;
            }
            CurrentMessage = message;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
