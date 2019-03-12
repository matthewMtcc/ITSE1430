using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CharacterCreator.Winforms
{
    /// <summary>Form for adding or creating a character.</summary>
    public partial class CharacterForm : Form
    {
        public CharacterForm()
        {
            InitializeComponent();
        }

        /// <summary>Property holding the character gnerated in the form</summary>
        public Character CurrentCharacter { get; set; }

        //simple cancel even handler
        private void OnCancel( object sender, EventArgs e )
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        //loads a character into the form for editing 
        private void LoadData(Character character)
        {
            _txtName.Text = character.Name;
            _txtDescription.Text = character.Description;
            _cbRace.SelectedItem = character.Race;
            _cbProfession.SelectedItem = character.Profession;

            _txtStrength.Text = character.Strength.ToString();
            _txtAgility.Text = character.Agility.ToString();
            _txtConstitution.Text = character.Constitution.ToString();
            _txtIntelligence.Text = character.Intellignece.ToString();
            _txtCharisma.Text = character.Charisma.ToString();
        }

        //saves character data in controls to character property
        private Character SaveData()
        {
            var character = new Character();
            character.Name = _txtName.Text;
            character.Description = _txtDescription.Text;
            character.Race = _cbRace.SelectedItem.ToString();
            character.Profession = _cbProfession.SelectedItem.ToString();

            character.Strength = ReadInt(_txtStrength);
            character.Agility = ReadInt(_txtAgility);
            character.Constitution = ReadInt(_txtConstitution);
            character.Intellignece = ReadInt(_txtIntelligence);
            character.Charisma = ReadInt(_txtCharisma);

            return character;
        }

        //helper method that converts string to int 
        //returns -1 if invalid
        private int ReadInt( TextBox control)
        {
            if (control.Text.Length == 0)
                return 0;

            if (Int32.TryParse(control.Text, out var value))
                return value;

            return -1;
        }

        //validaton for name, simply requires that something is there
        private void OnValidateName( object sender, CancelEventArgs e )
        {
            var tb = sender as TextBox;

            if (tb.Text.Length == 0)
            {
                _errors.SetError(tb, "name is required.");
                e.Cancel = true;
            } else
                _errors.SetError(tb, "");
        }

        //validation for attributes. Requires a number between 1 and 100
        private void OnValidateStat( object sender, System.ComponentModel.CancelEventArgs e )
        {
            var tb = sender as TextBox;
            var stat = ReadInt(tb);

            if (stat < 0 || stat > 100)
            {
                _errors.SetError(tb, "stat must be a number in between 50 and 100.");
                e.Cancel = true;
            } else
                _errors.SetError(tb, "");

        }

        //OnSave Event handler. Validates at UI and business level.
        private void OnSave( object sender, EventArgs e )
        {
            if (!ValidateChildren())
                return;

            var character = SaveData();

            if (!character.Validate())
            {
                MessageBox.Show(this, "Character is not valid.", "Error", MessageBoxButtons.OK);
                return;
            };

            CurrentCharacter = character;
            DialogResult = DialogResult.OK;
            Close();
        }

        //Validation for race combo box. Requires that a race is selected
        private void OnValidateRace( object sender, CancelEventArgs e )
        {
            var cb = sender as ComboBox;

            if (cb.SelectedIndex == -1 )
            {
                _errors.SetError(cb, "Race is required");
                e.Cancel = true;
            } else
                _errors.SetError(cb, "");

        }
        //Validation for Profession combo box. Requires that a profession is selected
        private void OnValidateProfession( object sender, CancelEventArgs e )
        {
            var cb = sender as ComboBox;

            if (cb.SelectedIndex == -1)
            {
                _errors.SetError(cb, "Profession is required");
                e.Cancel = true;
            } else
                _errors.SetError(cb, "");

        }

        //sets attributes intitial values to 50 then 
        //if CurrentCharacter is not assigned,
        //most likely for editing, load
        //that data into the controls
        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad(e);

            _txtStrength.Text = "50";
            _txtAgility.Text = "50";
            _txtConstitution.Text = "50";
            _txtIntelligence.Text = "50";
            _txtCharisma.Text = "50";

            if (CurrentCharacter != null)
                LoadData(CurrentCharacter);

            ValidateChildren();
        }
    }
}
