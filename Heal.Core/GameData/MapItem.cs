using System.Text;
using Microsoft.Xna.Framework.Content;

namespace Heal.Core.GameData
{
    public class MapItem : DataList<MapItem.TextureInfo>
    {
        public class TextureInfo
        {
            public TextureInfo()
            {
                
            }

            public int Layer;
            public string Texture;
            public override string ToString()
            {
                return "<Layer>" + Layer + "</Layer>\r\n<Texture>" + Texture + "</Texture>\r\n";
            }
        }

        public int X;//{ get { return m_locate.X; } set { m_locate.X = value; } }}
        public int Y;//{ get { return m_locate.Y; } set { m_locate.Y = value; } }}

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(base.ToString());
            sb.AppendLine("<X>" + X + "</X>");
            sb.AppendLine("<Y>" + Y + "</Y>");
            return sb.ToString();
        }
    }
}


