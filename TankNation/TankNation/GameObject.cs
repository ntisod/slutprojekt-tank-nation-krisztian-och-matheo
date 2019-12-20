using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace TankNation
{
    class GameObject
    {

        public Vector2 Position;
        public Texture2D Gfx;
        public float Angle;

        public GameObject(Vector2 Position, Texture2D Gfx, float Angle)
        {
            this.Position = Position;
            this.Gfx = Gfx;
            this.Angle = Angle;
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 DrawOffset)
        {
            spriteBatch.Draw(Gfx,
                Position - DrawOffset + new Vector2(400, 300), null,
                Color.White, Angle + (float)Math.PI / 2,
                new Vector2(Gfx.Width / 2, Gfx.Height / 2), 1.0f,
                SpriteEffects.None, 0);
        }
        public virtual double Rad
        {
            get
            {
                return (this.Gfx.Height / 2);
            }
            set
            {

            }
        }
        public bool CheckCollision(GameObject target)
        {
            bool collision;
            double xdiff = this.Position.X - target.Position.X;
            double ydiff = this.Position.Y - target.Position.Y; 

            if((xdiff * xdiff + ydiff * ydiff)< (this.Rad + target.Rad)*(this.Rad + target.Rad))
            {
                collision = true;
            }
            else
            {
                collision = false;
            }
            return (collision);
        }
    }
}
