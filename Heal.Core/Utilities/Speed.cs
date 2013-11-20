using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Heal.Core.Utilities
{
    public struct Speed
    {
        private float m_length;
        public float Length
        {
            get
            {
                return Math.Abs( m_length );
            }
            set
            {
                m_length = value;
            }
        }

        private float m_radian;
        public float Radian
        {
            get
            {
                if (m_length > 0)
                {
                    return m_radian;
                }
                else
                {
                    return -m_radian;
                }
            }
            set
            {
                if(value != value)
                {
                    
                }
                m_radian = MathHelper.WrapAngle(value);
                if (float.IsNaN(m_radian) || float.IsNaN(m_length))
                {

                }
            }
        }

        public float X
        {
            get
            {
                return m_length * (float)Math.Sin( m_radian );
            }
            set
            {
                SetSpeedXY(value, Y, ref this);
                if (float.IsNaN(m_radian) || float.IsNaN(m_length))
                {

                }
            }
        }

        public float Y
        {
            get
            {
                return m_length * -(float) Math.Cos( m_radian );
            }
            set
            {
                SetSpeedXY(X, value, ref this);
                if (float.IsNaN(m_radian) || float.IsNaN(m_length))
                {

                }
            }
        }

        public Speed Set(float x, float y)
        {
            SetSpeedXY( x, y, ref this );

            if (float.IsNaN(m_radian) || float.IsNaN(m_length))
            {

            }
            return this;
        }

        public Speed(float length, float radian)
        {
            m_length = length;
            m_radian = radian;
        }

        public static Speed operator *(Speed item, float data)
        {
            return new Speed(item.Length * data, item.Radian);
        }

        public static Speed operator +(Speed item1, Speed item2)
        {
            return FormXY( item1.X + item2.X, item1.Y + item2.Y );
        }

        public static explicit operator Vector2(Speed speed)
        {
            return new Vector2(speed.X, speed.Y);
        }

        public static implicit operator Speed(Vector2 input)
        {
            return FormVector2(input);
        }

        private static void SetSpeedXY(float x,float y, ref Speed i)
        {
            i.m_length = (float)Math.Sqrt(x * x + y * y);
            float d = (float)Math.Acos(-y / i.m_length);
            if (!(d<MathHelper.TwoPi && d>-MathHelper.Pi))
            {
                i.m_radian = 0;
                return;
            }
            //Console.WriteLine(d);
            if (x > 0)
            {
                i.m_radian = d;
            }
            else i.m_radian = -d;
        }

        public static Speed FormXY(float x,float y)
        {
            Speed i = new Speed();
            SetSpeedXY(x, y, ref i);
            if (float.IsNaN(i.m_radian) || float.IsNaN(i.m_length))
            {

            }
            return i;
        }

        public static Speed FormVector2(Vector2 input)
        {
            Speed a = new Speed(input.Length(), input.Radian());
            if(float.IsNaN(a.m_radian) || float.IsNaN(a.m_length))
            {
                
            }
            return a;
        }

        public override string ToString()
        {
            return String.Format("rd( {0}, {1} )  xy( {2}, {3} )", m_radian, m_length, X, Y);
        }
    }
}
