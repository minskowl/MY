using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlipIt.Core;
using Microsoft.Xna.Framework;

namespace FlipIt
{

    class Tile : AnimatedSprite
    {
        private Rectangle _bounds;
        /// <summary>
        /// Gets or sets a value indicating whether this instance is wright.
        /// </summary>
        /// <value><c>true</c> if this instance is wright; otherwise, <c>false</c>.</value>
        public bool IsWright
        {
            get { return Frame == 0; }
            set { Frame = value ? 0 : 1; }
        }

        public Tile()
        {
            FramesCount = 2;
        }
        public override void Initialize()
        {
            _bounds = new Rectangle((int)Position.X, (int)Position.Y, Texture.Height, Texture.Height);
            base.Initialize();
        }
        /// <summary>
        /// Toggles this instance.
        /// </summary>
        public void Toggle()
        {
            IsWright = !IsWright;
        }

        /// <summary>
        /// Determines whether [is in bound] [the specified vector].
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>
        /// 	<c>true</c> if [is in bound] [the specified vector]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsInBound(int x,int y)
        {
            return _bounds.Contains(x,y);
        }
    }
}
