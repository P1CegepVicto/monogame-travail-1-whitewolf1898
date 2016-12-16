using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTab
{
    class GameObject
    {
        public bool isAlive;
        public Texture2D sprite;
        public Vector2 position;
        public Vector2 direction;
        public Vector2 vitesse;
        public Rectangle rectCollision = new Rectangle();

        public Rectangle getRect()
        {
            rectCollision.X = (int)this.position.X;
            rectCollision.Y = (int)this.position.Y;
            rectCollision.Width = this.sprite.Width;
            rectCollision.Height = this.sprite.Height;

            return rectCollision;
        }
        
    }
}
