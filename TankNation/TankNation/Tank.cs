using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Design;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace TankNation
{
    public class Tank : MovingObject
    {
        //det här är spelare och deras live speed kills.
        public bool Enemy;
        public float MaxSpeed;
        public float ShotPower;
        public int WeaponType;
        public bool ShotFired;
        public float Life;
        public int Kills;
        protected KeyboardState prevKs;

        public Tank(bool Enemy, Vector2 Direction, Vector2 Position, Texture2D Gfx) : base(Direction, 0, Position, Gfx, -(float)(Math.PI / 2))
        {
            //alla mot alla
            this.Enemy = Enemy;
            MaxSpeed = 2.5F;
            ShotPower = 0;
            prevKs = Keyboard.GetState();
            Life = 5F;
            Kills = 0;
        }
        public void Respawn()
        {
            Life = 100F;
            Random randomerare = new Random();
            Position = new Vector2(randomerare.Next(500), randomerare.Next(500));
            Angle = 0;

        }
        public override void Update(GameTime gameTime)
        {

            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.W))
            {
                if (Speed < 0) Speed = 0;
                if (Speed < MaxSpeed) Speed = Speed * 1.005F + 0.01F;
                else Speed = MaxSpeed;
            }
            if (ks.IsKeyDown(Keys.S))
            {
                if (Speed > -1.0F) Speed -= 0.04F;
                else Speed = -1.0F;
            }
            if (ks.IsKeyUp(Keys.S) && ks.IsKeyUp(Keys.W) && Speed > 0)
            {
                Speed -= 0.01F;
                if (Speed <= 0) Speed = 0;
            }
            if (ks.IsKeyUp(Keys.S) && ks.IsKeyUp(Keys.W) && Speed < 0)
            {
                Speed += 0.01F;
                if (Speed >= 0) Speed = 0;
            }

            if (ks.IsKeyUp(Keys.A))
            {
                Angle += 0.02F;
            }
            if (ks.IsKeyUp(Keys.D))
            {
                Angle -= 0.02F;
            }
            if (ks.IsKeyDown(Keys.Space))
            {
                if (ShotPower < 100)
                    ShotPower += 0.5F;
                else
                    ShotPower = 100;
            }

            if (ks.IsKeyUp(Keys.Space) && prevKs.IsKeyDown(Keys.Space))
            {
                //ShotPower = 0;
                ShotFired = true;
            }

            prevKs = ks;
            Direction = new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle));

            base.Update(gameTime);
        }

        public static String[] GetUpdate(string updateString)
        {
            String[] values = updateString.Split(':');


            // "1:76:50:16"
            // "ID:HP:POS.X:POS.Y" etc.
    
            return values;
        }
    }
}
