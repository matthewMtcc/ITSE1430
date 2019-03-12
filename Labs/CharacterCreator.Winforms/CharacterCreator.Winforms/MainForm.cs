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

        private void OnAboutClicked( object sender, EventArgs e )
        {
            var form = new AboutBox1();
            form.ShowDialog();
        }

        private void OnCharacterAdd( object sender, EventArgs e )
        {
            var form = new CharacterForm();

                if (form.ShowDialog(this) != DialogResult.OK)
                    return;

                _characters[GetNextEmptyPosition()] = form.CurrentCharacter;

                BindList();
        }

        //binds data to list box
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

        //KEEP WATCH: seems good
        private Character GetSelectedCharacter()
        {
            return _listCharacters.SelectedItem as Character;
        }

        private int GetNextEmptyPosition()
        {
            for (int i = 0; i < _characters.Length; ++i)
            {
                if (_characters[i] == null)
                    return i;
            }

            return -1;
                
        }

        private void OnGameEdit( object sender, EventArgs e )
        {
            var form = new CharacterForm();

            var character = GetSelectedCharacter();
            if (character == null)
                return;
            form.CurrentCharacter = character;

            if (form.ShowDialog(this) != DialogResult.OK)
                return;

            UpdateCharacter(GetSelectedCharacter(), form.CurrentCharacter);

            BindList();
        }

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

        private Character[] _characters = new Character[100];
    }
}
