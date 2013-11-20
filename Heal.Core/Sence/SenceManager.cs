using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Entity = Heal.Core.Entities.Entity;

namespace Heal.Core.Sence
{
    public class SenceManager : GameClassManager, IUpdateable
    {
        #region Instance
        public SenceManager()
        {
            m_instance = this;
        }
        private static SenceManager m_instance;
        public static SenceManager GetInstance()
        {
            return m_instance;
        }
        #endregion

        private Texture2D m_texture;
        private SencePart[,] m_sences;
        private Point m_size;
        public int PartSize;
        public Vector2 CameraLocate;
        public Vector2 CameraFollow;

        public SencePart this[int x, int y]
        {
            get { return (m_sences[x, y]); }
            set { m_sences[x, y] = value; }
        }
        public SencePart this[Point a]
        {
            get { return m_sences[a.X, a.Y]; }
            set { m_sences[a.X, a.Y] = value; }
        }

        public SencePart CreatePart(int x, int y)
        {
            SencePart part = new SencePart( x, y );
            m_sences[x, y] = part;
            return part;
        }

        public void Initialize(int x, int y, int partSize)
        {
            m_sences = new SencePart[x,y];
            m_size.X = x;
            m_size.Y = y;
            PartSize = partSize;
        }

        public void Update( GameTime gameTime )
        {
            
        }

        public void ItemCommand(string command)
        {
            GameCommands.Enqueue( command );
        }

        public Vector2 MoveTest(Vector2 locate, float radian, float length)
        {
            return MoveTest(locate, radian, length, 18);
        }

        public Vector2 MoveTest(Vector2 locate, float radian, float length, int size)
        {
            Vector2 vector2 = locate + Utilities.CoreUtilities.GetVector(length, radian);
            if (!IntersectPixels(vector2, 18))
            {
                return vector2;
            }
            else
            {
                for (float i = 0.06f; i < 0.5f; i += 0.07f)
                {
                    vector2 = locate + Utilities.CoreUtilities.GetVector(length, radian - MathHelper.Pi * i);
                    if (!IntersectPixels(vector2, size))
                    {
                        return vector2;
                    }
                    else
                    {
                        vector2 = locate +
                                  Utilities.CoreUtilities.GetVector(length, radian + MathHelper.Pi * i);
                        if (!IntersectPixels(vector2, size))
                        {
                            return vector2;
                        }
                    }
                }
            }
            return locate;

        }
        public Vector2 MoveTest(Vector2 locate, float radian, float speed, float time)
        {
            return MoveTest(locate, radian, speed * time);
        }
        public Vector2 MoveTest(Vector2 locate, float radian, float speed, float time, int size)
        {
            return MoveTest(locate, radian, speed * time, size);
        }

        public Vector2 MoveTest(Vector2 locate, Speed speed, float time)
        {
            return MoveTest(locate, speed.Radian, speed.Length, time);
        }
        public Vector2 MoveTest(Vector2 locate, Speed speed, float time, int size)
        {
            return MoveTest(locate, speed.Radian, speed.Length, time, size);
        }

        /// <summary>
        /// Intersects the collusion by pixels.
        /// </summary>
        /// <param name="data">Image data.</param>
        /// <param name="rect">Entity location.</param>
        /// <returns></returns>
        public bool IntersectPixels(Vector2 locate, int size)
        {
            //Matrix matrix;
            //int value = 0;
            Vector2 a = new Vector2( size , 0 );
            double k = MathHelper.TwoPi / ( size * 4 );
            Vector2 b = new Vector2((float) Math.Cos( k ), (float) Math.Sin( k ));
            int x,y;
            int i = 0;
            for(; i < size * 4; i++ )
            {
                a.X = a.X * b.X + a.Y * b.Y;
                a.Y = -a.X * b.Y + a.Y * b.X;
                x = (int) ( a.X + locate.X );
                y = (int) ( a.Y + locate.Y );
                try
                {
                    if(
                        ( m_sences[x / PartSize, y / PartSize].CollusionTexture[
                                                                                   ( x % PartSize +
                                                                                     ( y % PartSize ) * PartSize ) >> 3] &
                          ( 1 << ( x & 0x7 ) ) ) != 0 )
                    {
                        return true;
                        //value++;
                    }
                }
                catch
                {
                    return true;
                }
            }
            //Console.WriteLine(value + "  " + i + "  " + locate);
            return false;
            /*
            for( int i = rect.X / PartSize; i < rect.Right / PartSize; i++ )
            {
                for( int j = rect.Y / PartSize; j < rect.Bottom / PartSize; j++ )
                {
                    if (IntersectPartPixels(m_sences[i, j], data, rect)) return true;
                }
            }
            return false;
            */
        }

        private bool IntersectPartPixels(SencePart map, byte []itemInfo, Rectangle rect)
        {
            int xa = map.X * PartSize;
            int ya = map.Y * PartSize;
            int x0 = rect.Left - xa;
            int y0 = rect.Top - ya;
            int i = Math.Max(0, x0);
            int x2 = Math.Min( rect.Right - xa, PartSize );
            int j = Math.Max(0, y0);
            int y2 = Math.Min( rect.Bottom - ya, PartSize );
            byte[] mapInfo = map.CollusionTexture;
            int xn = rect.Width;
            for( ; i < x2; i++ )
            {
                for (; j < y2; j++)
                {
                    if( ( mapInfo[( i + j * PartSize ) >> 3] & ( 1 << ( i & 0x7 ) ) ) != 0 &&
                        ( itemInfo[( ( i + x0 ) + ( j + y0 ) * xn ) >> 3] & ( 1 << ( i & 0x7 ) ) ) != 0 ) return true;
                }
            }
            return false;
        }

        public void RefreshState(int x,int y,bool isLoad)
        {
            
        }
        public bool Enabled
        {
            get { return true; }
        }

        public int UpdateOrder
        {
            get { return 0; }
        }

        public event EventHandler EnabledChanged;
        public event EventHandler UpdateOrderChanged;

        #region Overrides of GameClassManager

        public override void Initialize()
        {
            ;
        }

        #endregion
    }
}
