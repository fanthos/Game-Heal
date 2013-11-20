using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Heal.Tools
{
    public class ToolsManager
    {

        #region Instance
        private static ToolsManager m_instance;
        public static ToolsManager GetInstance()
        {
            if (m_instance == null)
            {
                m_instance = new ToolsManager();
            }
            return m_instance;
        }

        private ToolsManager()
        {
            m_list = new List<ToolInformation>();
            InitTools( );
        }

        private void AddItem(string name, Type type)
        {
            m_list.Add( new ToolInformation( name, type ) );
        }

        #endregion

        private void InitTools()
        {
            AddItem("图片序列拼接", typeof(ImageSplicing.ImageSplicer));
            AddItem("图片切分", typeof(ImageSpliter.ImageSpliter));
        }

        public List<ToolInformation> Items { get { return m_list; } }

        public class ToolInformation : IEquatable<ToolInformation>
        {
            public ToolInformation(string name, Type type)
            {
                DisplayName = name;
                WindowType = type;
            }
            public string DisplayName { get; private set; }
            public Type WindowType { get; private set; }

            #region IEquatable<?> Members

            public override int GetHashCode()
            {
                return WindowType.GetHashCode( );
            }

            public bool Equals(ToolInformation other)
            {
                return WindowType == other.WindowType;
            }

            public override string ToString()
            {
                return DisplayName;
            }

            #endregion
        }

        private List<ToolInformation> m_list;
    }
}
