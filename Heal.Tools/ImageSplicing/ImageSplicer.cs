using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Heal.Tools.ImageSplicing
{
    public partial class ImageSplicer : Form
    {
        private int m_imgPage = 0;
        private Dictionary<string, Image> m_images = new Dictionary<string, Image>( );
        public ImageSplicer()
        {
            InitializeComponent();
        }

        private void ImageSplicer_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog( );
            dlg.CheckFileExists = true;
            dlg.Multiselect = true;
            dlg.Filter = "png|*.png|gif|*.gif|jpg|*.jpg|images|*.png;*.jpg;*.gif";
            dlg.DefaultExt = "images|*.png;*.jpg;*.gif";
            dlg.ShowDialog( );
            foreach( string fileName in dlg.FileNames )
            {
                lstFiles.Items.Add( fileName );
                m_images.Add( fileName, Image.FromFile( fileName ) );
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lstFiles.Items.Clear(  );
            m_images.Clear(  );
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            foreach( var item in lstFiles.Items )
            {
                list.Add( item.ToString( ));
            }
            ProcessImage( list );
        }

        private void ProcessImage(IEnumerable<string> files)
        {
            int i = 0;
            if(files.Count()<1) return;
            Image img = m_images[files.ElementAt( 0 )];
            int a = Convert.ToInt32( textBox2.Text );
            int b = Convert.ToInt32( textBox3.Text );
            Size size = new Size( a, b );
            Bitmap bmp = new Bitmap(size.Width * files.Count(), size.Height);
            Graphics graphics = Graphics.FromImage( bmp );
            foreach( string file in files )
            {
                img = Bitmap.FromFile( file );
                graphics.DrawImage( img, new Rectangle( size.Width * i++, 0, size.Width, size.Height ) );
            }
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "png|*.png";
            dlg.DefaultExt = "images|*.png;*.jpg;*.gif";
            dlg.ShowDialog();
            bmp.Save( dlg.FileName, ImageFormat.Png );
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (lstFiles.Items.Count <= m_imgPage) m_imgPage = 0;
            if (lstFiles.Items.Count == 0) return;
            pictureBox1.Image = m_images[lstFiles.Items[m_imgPage].ToString(  )];
            m_imgPage++;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int n = Convert.ToInt32( textBox1.Text );
                if (n > 30) timer1.Interval = n;
            }
            catch
            {
            }
        }
    }
}
