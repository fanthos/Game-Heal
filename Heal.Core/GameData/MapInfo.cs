using System.Collections.Generic;
using System.Text;

namespace Heal.Core.GameData
{
    public class MapInfo : DataList<MapItem>
    {
        public class EffectString
        {
            public int Layer;
            public string Effect;
        }
        public DataList<EffectString> Effects = new DataList<EffectString>();
        public int CollusionLayer;
        public int LayerCount;
        public int X;
        public int Y;
        public int PX;
        public int PY;
        public string Name;
        public string FullName;
        public string Describe;
        public string PostLoading;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder( base.ToString() );
            sb.AppendLine( "<Effects>" );
            sb.Append( Effects.ToString() );
            sb.AppendLine("</Effects>");
            sb.AppendLine("<CollusionLayer>" + CollusionLayer + "</CollusionLayer>");
            sb.AppendLine("<LayerCount>" + LayerCount + "</LayerCount>");
            sb.AppendLine("<X>" + X + "</X>");
            sb.AppendLine("<Y>" + Y + "</Y>");
            sb.AppendLine("<PX>" + PX + "</PX>");
            sb.AppendLine("<PY>" + PY + "</PY>");
            sb.AppendLine("<Name>" + Name + "</Name>");
            sb.AppendLine("<FullName>" + FullName + "</FullName>");
            sb.AppendLine("<Describe>" + Describe + "</Describe>");
            sb.AppendLine("<PostLoading>" + PostLoading + "</PostLoading>");
            return sb.ToString();
        }

        //[Microsoft.Xna.Framework.Content.ContentSerializerIgnore]
        //public MapData this[int a,int b]
        //{
            //get { return this[a * X + b]; }
        //}
    }
}


