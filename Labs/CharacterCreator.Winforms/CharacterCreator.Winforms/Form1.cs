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
            var DOOM = new Character();
            Close();
        }

        private void OnAboutClicked( object sender, EventArgs e )
        {
            var form = new AboutBox1();
            form.ShowDialog();
        }
    }
}
