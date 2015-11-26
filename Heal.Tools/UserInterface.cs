using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Heal.Tools
{
    public partial class UserInterface : Form
    {
        public UserInterface()
        {
            InitializeComponent();
        }

        private void UserInterface_Load(object sender, EventArgs e)
        {
            ToolsManager.GetInstance( );
            foreach (ToolsManager.ToolInformation item in ToolsManager.GetInstance().Items)
            {
                lstItems.Items.Add( item );
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //try
            {
                var item = (ToolsManager.ToolInformation) lstItems.SelectedItem;
                Form form = (Form)Activator.CreateInstance( item.WindowType );
                //form.FormClosed+=delegate { this.Visible = true; };
                form.ShowDialog( );
            }
        }

        private void lstItems_DoubleClick(object sender, EventArgs e)
        {
            btnStart_Click( sender, e );
        }
    }
}
