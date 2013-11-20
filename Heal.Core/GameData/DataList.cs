using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heal.Core.GameData
{
    public class DataList<T>
    {
        private List<T> m_list = new List<T>();
        public List<T> List
        {
            get { return m_list; }
            set { m_list = value; }
        }

        public void Add(T item)
        {
            m_list.Add(item);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<List>");
            foreach (var item in List)
            {
                sb.AppendLine("<Item>");
                sb.Append(item.ToString());
                sb.AppendLine("</Item>");
            }
            sb.AppendLine("</List>");
            return sb.ToString();
        }

        [Microsoft.Xna.Framework.Content.ContentSerializerIgnore]
        public T this[int a]
        {
            get
            {
                return m_list[a];
            }
        }
    }
}
