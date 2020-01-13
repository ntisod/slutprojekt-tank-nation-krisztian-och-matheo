using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Design;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;


namespace TankNation
{
    class Explosion : GameObject
    {
        public int Time;
        public int Frame;
        public int AnimationSpeed;
        public bool Active;
        public Vector2 customHitbox;

        public Explosion(Vector2 Position, Texture2D Gfx) : base(Position, Gfx, 0)
        {
            Time = 0;
            Frame = 0;
            Active = true;
            AnimationSpeed = 30;
            Angle = 0;
            customHitbox = new Vector2(50, 50);
        }
        public void Update(GameTime gameTime)
        {
            Time += gameTime.ElapsedGameTime.Milliseconds;
            if (Time > AnimationSpeed)
            {
                Time = 0;
                Frame++;
                if (Frame > 15)
                {
                    Active = false;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 DrawOffset)
        {
            Rectangle tmp = new Rectangle((Frame % 4) * 64, (Frame / 4) * 64, 64, 64);

            spriteBatch.Draw(Gfx,
                Position - DrawOffset + new Vector2(400, 300),
                tmp, Color.White, base.Angle,
                new Vector2(32, 32), 1.0f, SpriteEffects.None, 0);
        }

        public virtual double GetRad()
        {
            return (26.0);
        }
        public virtual void SetRad(double value)
        {

        }
    }
}
