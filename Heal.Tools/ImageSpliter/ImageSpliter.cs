using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Xml.Serialization;
using Heal.Core.GameData;

namespace Heal.Tools.ImageSpliter
{
    public partial class ImageSpliter : Form
    {
        private Dictionary<string, Image> m_images = new Dictionary<string, Image>();
        private Bitmap m_bitmap;

        public ImageSpliter()
        {
            InitializeComponent();
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog
                          {
                              CheckFileExists = true,
                              Multiselect = false,
                              Filter = "png|*.png|gif|*.gif|jpg|*.jpg|images|*.png;*.jpg;*.gif",
                              DefaultExt = "images|*.png;*.jpg;*.gif"
                          };
            dlg.ShowDialog();
            Point size = new Point();
            Image img = Image.FromStream( dlg.OpenFile() );
            size.X = ( img.Width + 511 ) / 512;
            size.Y = ( img.Height + 511 ) / 512;
            Bitmap output = new Bitmap( 512, 512 );
            Graphics g = Graphics.FromImage( output );
            var list = new MapInfo();
            MapItem item;
            var dlg1 = new FolderBrowserDialog();
            dlg1.ShowDialog();
            string path = dlg1.SelectedPath;
            for( int i = 0; i < size.X; i++ )
            {
                for( int j = 0; j < size.Y; j++ )
                {
                    item = new MapItem();// i, j, txtDir.Text + "\\" + txtPrefix.Text + "_" + j + "_" + i );
                    list.Add( item );
                    g.Clear( Color.Transparent );
                    g.DrawImage(img, new Rectangle(-512 * i, -512 * j, img.Width, img.Height));
                    //output.Save( path + "\\" + txtPrefix.Text + "_" + j + "_" + i + ".png", ImageFormat.Png );
                }
            }
            var xmlSerializer = new XmlSerializer( typeof( MapInfo ) );
            Stream stream = new FileStream( path + "\\" + ".xml", FileMode.Create );
            xmlSerializer.Serialize( stream, list );
            stream.Close();
        }

        private void lstFiles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog
                          {
                              CheckFileExists = true,
                              Multiselect = true,
                              Filter = "png|*.png|gif|*.gif|jpg|*.jpg|images|*.png;*.jpg;*.gif",
                              DefaultExt = "images|*.png;*.jpg;*.gif"
                          };
            dlg.ShowDialog();
            foreach (string fileName in dlg.FileNames)
            {
                lstFiles.Items.Add(fileName);
                m_images.Add(fileName, Image.FromFile(fileName));
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (lstFiles.Items.Count < 1) return;
            Image img = m_images[lstFiles.Items[0].ToString()];
            m_bitmap = new Bitmap( img.Width, img.Height );
            Graphics g = Graphics.FromImage( m_bitmap );
            Rectangle rect = new Rectangle(0, 0, img.Width, img.Height);
            foreach( var fileName in lstFiles.Items )
            {
                img = m_images[fileName.ToString()];
                g.DrawImage( img, rect, rect, GraphicsUnit.Pixel );
            }
            pictureBox1.Image = m_bitmap;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            m_images.Clear();
            m_bitmap = null;
            lstFiles.Items.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int i = lstFiles.SelectedIndex;
                object o = lstFiles.Items[i];
                lstFiles.Items[i] = lstFiles.Items[i - 1];
                lstFiles.Items[i - 1] = o;
                lstFiles.SelectedIndex--;
            }
            finally
            {
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int i = lstFiles.SelectedIndex;
                object o = lstFiles.Items[i];
                lstFiles.Items[i] = lstFiles.Items[i + 1];
                lstFiles.Items[i + 1] = o;
                lstFiles.SelectedIndex++;
            }
            finally
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Image> list = new List<Image>();
            Image img;
            foreach( var fileName in lstFiles.Items )
            {
                img = m_images[fileName.ToString()];
                list.Add( img );
            }
            SplitImage( textBox1.Text, list, new MapInfo() );
        }

        private void SplitImage(string filePrefix, IList<Image> data, MapInfo list)
        {
            Image img = data[0];
            list.X = (img.Width + 511) / 512;
            list.Y = (img.Height + 511) / 512;
            Bitmap output = new Bitmap(512, 512);
            Graphics g = Graphics.FromImage(output);
            MapItem item;
            Image image;
            var dlg1 = new FolderBrowserDialog();
            dlg1.ShowDialog();
            string path = dlg1.SelectedPath;
            list.LayerCount = data.Count;
            for (int i = 0; i < list.X; i++)
            {
                for (int j = 0; j < list.Y; j++)
                {
                    item = new MapItem() {X = i, Y = j};
                    for (int k = 0; k < list.LayerCount; k++)
                    {
                        image = data[k];
                        g.Clear( Color.Transparent );
                        g.DrawImage( image, new Rectangle( -512 * i, -512 * j, img.Width, img.Height ) );
                        output.Save( path + "\\" + filePrefix + "_" + k + "_" + j + "_" + i + ".png", ImageFormat.Png );
                        item.List.Add( new MapItem.TextureInfo()
                                           {Layer = k, Texture = filePrefix + "_" + k + "_" + j + "_" + i} );
                    }
                    list.Add(item);
                }
            }
            list.CollusionLayer = Convert.ToInt32( textBox2.Text );
            FileStream fs = new FileStream( path + "\\" + filePrefix + ".xml", FileMode.Create );
            StreamWriter writer = new StreamWriter( fs );
            writer.Write( list.ToString() );
            writer.Close();
            fs.Close();
        }
    }
}
