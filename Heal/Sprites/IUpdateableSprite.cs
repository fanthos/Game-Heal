using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Heal.Sprites
{
    internal interface IUpdateableSprite : ISprite
    {
        /// <summary>
        /// Called when the game component drawing data should be updated.
        /// </summary>
        /// <param name="gameTime">gameTime: Snapshot of the game's timing state.</param>
        void UpdateDraw( GameTime gameTime );
        /// <summary>
        /// Gets a value indicating whether this <see cref="IUpdateableSprite"/> is palse.
        /// </summary>
        /// <value><c>true</c> if palse; otherwise, <c>false</c>.</value>
        bool Palse { get; }
    }
}
