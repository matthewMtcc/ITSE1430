//
//Lab 2
//Matthew McNatt
//
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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void OnFileExit( object sender, EventArgs e )
        {
            Close();
        }

        //Event Handler for about button
        private void OnAboutClicked( object sender, EventArgs e )
        {
            var form = new AboutBox1();
            form.ShowDialog();
        }

        //Event Handler for adding a character
        private void OnCharacterAdd( object sender, EventArgs e )
        {
            var form = new CharacterForm();

                if (form.ShowDialog(this) != DialogResult.OK)
                    return;

                _characters[GetNextEmptyPosition()] = form.CurrentCharacter;

                BindList();
        }

        //Helper method: Binds data to the list box
        private void BindList()
        {
            _listCharacters.Items.Clear();
            _listCharacters.DisplayMember = nameof(Character.Name);

            foreach(var character in _characters)
            {
                if (character != null)
                    _listCharacters.Items.Add(character);
            };

        }

        //ensures the user wants to close form before doing so
        protected override void OnFormClosing( FormClosingEventArgs e )
        {
            if (MessageBox.Show(this, "Are you sure?", "Close", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            };
            base.OnFormClosing(e);
        }

        //helper method to get the user's currently selected character
        //will return null if one is not selected
        private Character GetSelectedCharacter()
        {
            return _listCharacters.SelectedItem as Character;
        }

        //helper method to get next free position in _characters array
        private int GetNextEmptyPosition()
        {
            for (int i = 0; i < _characters.Length; ++i)
            {
                if (_characters[i] == null)
                    return i;
            }

            return -1;
                
        }

        //Event Handler for editing a game
        private void OnGameEdit( object sender, EventArgs e )
        {
            var form = new CharacterForm();
            form.Text = "Edit Character";

            var character = GetSelectedCharacter();
            if (character == null)
                return;
            form.CurrentCharacter = character;

            if (form.ShowDialog(this) != DialogResult.OK)
                return;

            UpdateCharacter(GetSelectedCharacter(), form.CurrentCharacter);

            BindList();
        }

        //helper method for updating an existing character
        private void UpdateCharacter( Character oldCharacter, Character newCharacter)
        {
            for(int i = 0; i < _characters.Length; i++)
            {
                if (_characters[i] == oldCharacter)
                {
                    _characters[i] = newCharacter;
                    break;
                };
            };
        }

        //event handler for delete character. Finds 
        //selected character and assigns it the value null
        private void OnCharacterDelete( object sender, EventArgs e )
        {
            var selected = GetSelectedCharacter();
            if (selected == null)
                return;

            if (MessageBox.Show(this, $"Are you sure you want to delete {selected.Name}?", "Confirm Delete?",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            for (int index = 0; index < _characters.Length; ++index)
            {
                if (_characters[index] == selected)
                {
                    _characters[index] = null;
                    break;
                };
            };
            BindList();
        }

        //arrary holding all created characters
        private Character[] _characters = new Character[100];
    }
}
