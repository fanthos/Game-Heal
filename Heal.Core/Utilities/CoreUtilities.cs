using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;

namespace Heal.Core.Utilities
{
    public static class CoreUtilities
    {
        public static Thread MainThread;
        public static bool Running
        {
            get
            {
                return MainThread.IsAlive;
            }
        }

        public static bool StateIsTrue(string item)
        {
            object obj = GameItemState.Get(item);
            if (obj == null) return false;
            return (bool)obj;
        }

        public static bool StateIsFalse(string item)
        {
            object obj = GameItemState.Get(item);
            if (obj == null) return false;
            return !(bool)obj;
        }

        public static bool IsTrue(string item)
        {
            object obj = Config.Get(item);
            if (obj == null) return false;
            return (bool)obj;
        }

        public static bool IsFalse(string item)
        {
            object obj = Config.Get(item);
            if (obj == null) return false;
            return (bool)obj;
        }

        public static Vector2 GetVector(float length, float radian)
        {
            return new Vector2( (float) ( length * Math.Sin( radian ) ),
                                -(float) ( length * Math.Cos( radian ) ) );
        }

        #region Functions for Vector2

        /// <summary>
        /// Get the rotation of the vector2.
        /// </summary>
        /// <returns></returns>
        public static float Radian(this Vector2 item)
        {
            float d = (float)Math.Acos(-item.Y / item.Length(  ));
            if (!(d < MathHelper.TwoPi && d > -MathHelper.Pi))
            {
                return 0;
            }

            if(item.X>0)
            {
                return d;
            }
            else return -d;
        }

        #endregion

        #region Functions for Rectangle

        /// <summary>
        /// Gets the size.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static int Size(this Rectangle item)
        {
            return item.Width * item.Height;
        }

        #endregion
    }
}
