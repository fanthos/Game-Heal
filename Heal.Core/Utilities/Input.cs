using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Heal.Core.Utilities
{
    public static class Input
    {
        private static KeyboardState m_keyboardState;
        private static GamePadState m_gamePadState;

        private static float m_radian;
        private static float m_speed;

        /// <summary>
        /// Gets or sets the speed.
        /// </summary>
        /// <value>The speed.</value>
        //public static float Speed;

        /// <summary>
        /// Gets or sets the radian.
        /// </summary>
        /// <value>The radian.</value>
        //public static float Radian;

        public static Speed Speed;

        /// <summary>
        /// Show which key need to input.
        /// </summary>
        public enum KeyInput
        {
            Status,
            Pause,
            Transform,
            SpeedUp,
            Confirm,
            Up, //
            Down,//
            Left, //
            Right,//
            Action,
            Attack
        } ;

        private static Keys[] m_inputKeys =
            {
                Keys.Tab,
                Keys.Escape,
                Keys.LeftControl,
                Keys.LeftShift,
                Keys.Enter,
                Keys.Up,
                Keys.Down,
                Keys.Left,
                Keys.Right,
                Keys.X,
                Keys.Z,
            };

        private static Buttons[] m_inputButtons =
            {
                Buttons.X,
                Buttons.Back,
                Buttons.LeftShoulder,
                Buttons.RightShoulder,
                Buttons.Start,
                Buttons.DPadUp,
                Buttons.DPadDown,
                Buttons.DPadLeft,
                Buttons.DPadRight,
                Buttons.A,
                Buttons.B,
            };

        private static readonly float[] Direction = new float[]
                                                        {
                                                            float.NaN,
                                                            MathHelper.PiOver2,
                                                            MathHelper.PiOver2 * ( -1 ),
                                                            float.NaN,
                                                            MathHelper.Pi,
                                                            MathHelper.PiOver4 * 3,
                                                            MathHelper.PiOver4 * ( -3 ),
                                                            MathHelper.Pi,
                                                            0,
                                                            MathHelper.PiOver4,
                                                            MathHelper.PiOver4 * ( -1 ),
                                                            0,
                                                            float.NaN,
                                                            MathHelper.PiOver2,
                                                            MathHelper.PiOver2 * ( -1 ),
                                                            float.NaN
                                                        };

        /// <summary>
        /// Make KeyInput enum to the int32.
        /// </summary>
        /// <returns>value</returns>
        private static int ToInt32( this KeyInput a )
        {
            return (int)a;
        }

        public static void Init()
        {
           
        }

        /// <summary>
        /// Determines whether start key down.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if start key down; otherwise, <c>false</c>.
        /// </returns>
        
        public static bool IsStatusKeyDown()
        {
            return IsKeyDown(KeyInput.Status);
        }

        public static bool IsPauseKeyDown()
        {
            return IsKeyDown(KeyInput.Pause);
        }
        
        public static bool IsActionKeyDown()
        {
            return IsKeyDown( KeyInput.Action );
        }

        public static bool IsConfirmKeyDown()
        {
            return IsKeyDown(KeyInput.Confirm);
        }

        public static bool IsSpeedupKeyDown()
        {
            return IsKeyDown(KeyInput.SpeedUp);
        }

        public static bool IsAttackKeyDown()
        {
            return IsKeyDown(KeyInput.Attack);
        }

        public static bool IsTransformKeyDown()
        {
            return IsKeyDown(KeyInput.Transform);
        }

        /// <summary>
        /// Determines whether [is up key press].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [is up key press]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsUpKeyDown()
        {
            if (Speed.Length > 0.5 && (Speed.Radian > (-0.25) * MathHelper.PiOver4 && Speed.Radian < 0.25 * MathHelper.PiOver4))
                return true;
            return false;
        }

        /// <summary>
        /// Determines whether [is down key press].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [is down key press]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDownKeyDown()
        {
            if (Speed.Length > 0.5 && (Speed.Radian > MathHelper.Pi + (-0.25) * MathHelper.PiOver4 && Speed.Radian < MathHelper.Pi + (0.25) * MathHelper.PiOver4))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Determines whether [is left key press].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [is left key press]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsLeftKeyDown()
        {
            if (Speed.Length > 0.5 && (Speed.Radian > (-1) * MathHelper.PiOver2 + (-0.25) * MathHelper.PiOver4 && Speed.Radian < (-1) * MathHelper.PiOver2 + (0.25) * MathHelper.PiOver4))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Determines whether [is right key press].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [is right key press]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsRightKeyDown()
        {
            if (Speed.Length > 0.5 && (Speed.Radian > MathHelper.PiOver2 + (-0.25) * MathHelper.PiOver4 && Speed.Radian < MathHelper.PiOver2 + (0.25) * MathHelper.PiOver4))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Determines whether the specified key down.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// 	<c>true</c> if the specified key is down; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsKeyDown( KeyInput key )
        {
            return ( m_keyboardState[m_inputKeys[key.ToInt32()]] == KeyState.Down ) ||
                   ( m_gamePadState.IsButtonDown( m_inputButtons[key.ToInt32()] ) );
        }

        /// <summary>
        /// Updates the keyboard or gamepad info.
        /// </summary>
        public static void Update(GameTime gameTime)
        {
            m_keyboardState = Keyboard.GetState();
            if( GamePad.GetCapabilities( PlayerIndex.One ).GamePadType == GamePadType.GamePad )
            {
                m_gamePadState = GamePad.GetState( PlayerIndex.One );
            }

            int i = 0;
            i = ( IsKeyDown( KeyInput.Up ) ? 0x08 : 0 ) | ( IsKeyDown( KeyInput.Down ) ? 0x04 : 0 ) |
                ( IsKeyDown( KeyInput.Left ) ? 0x02 : 0 ) | ( IsKeyDown( KeyInput.Right ) ? 0x01 : 0 );
            float a = Direction[i];
            if( !float.IsNaN( a ) )
            {
                Speed.Radian = a;
                Speed.Length = 1;
            }
            else
            {
                Speed.Length = 0;
            }
        }
    }
}


