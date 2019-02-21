using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameManager.Host.Winforms
{
    public partial class GameForm : Form
    {
        public GameForm()// : base()   always called, implicitly there. Its calling the parents first
        {
            InitializeComponent();
        }


        public Game Game { get; set; } //property that holds the Game this form generates
        private void OnSave( object sender, EventArgs e ) //eventHandler for save button
        {
            Game = SaveData(); //Game = all data in buttons/boxes
            DialogResult = DialogResult.OK; //sets show dialog to ok to indicate there is now a game
            Close(); //close form
        }

        private void OnCancel( object sender, EventArgs e ) //fucntion that closes but also sets dialog to show it canceled
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        private decimal ReadDecimal(TextBox control) //simple check for number
        {
            if (Decimal.TryParse(control.Text, out var value))
                return value;

            return -1;
        }



        private void LoadData(Game game)
        {
            _txtName.Text = game.Name;
            _txtPublisher.Text = game.Publisher;
            _txtPrice.Text = game.Price.ToString();
            _cbOwned.Checked = game.Owned;
            _cbCompleted.Checked = game.Completed;
        }


        private Game SaveData()
        {
            var game = new Game();
            game.Name = _txtName.Text;
            game.Publisher = _txtPublisher.Text;
            game.Price = ReadDecimal(_txtPrice);
            game.Owned = _cbOwned.Checked ;
            game.Completed = _cbCompleted.Checked;

            //Demoing ctor
            var game2 = new Game(_txtName.Text, ReadDecimal(_txtPrice));

            return game;
        }


        //Defined in type
        //Derived types may override and change it
        protected virtual void CanBeChanged() { }

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad(e);

            if (Game != null)
                LoadData(Game);
        }
    }
}
