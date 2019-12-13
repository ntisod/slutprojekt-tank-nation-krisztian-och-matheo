using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Design;

namespace TankNation
{
    class Tank : MovingObject
    {
        public Tank()
        {
            MaxSpeed = 2F;
            ShotPower = 0;
            prevKs = Keyboard.GetState();
            Life = 100F;
            Kills = 0;
            angle = -(float)(Math.PI / 2);
        }
        public bool Enemy
        {
            get;
            set;
        }
        public float MaxSpeed
        {
            get;
            set;
        }
        public float ShotPower
        {
            get;
            set;
        }
        public int WeaponType
        {
            get;
            set;
        }
        public bool ShotFired
        {
            get;
            set;
        }
        public int Kills
        {
            get;
            set;
        }
        public float Life
        {
            get;
            set;
        }
        protected KeyboardState prevKs;

        public void Respawn()
        {
            Life = 100F;
            Random randomerare = new Random();
            pos = new Vector2(randomerare.Next(1000), randomerare.Next(100));
            angle = 0;
        }
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.W))
            {
                if (speed < 0) speed = 0;
                if (speed < MaxSpeed) speed = speed * 1.003F + 0.02F;
                else speed = MaxSpeed;
            }
            if (ks.IsKeyDown(Keys.S))
            {
                if (speed > -1F) speed -= 0.04F;
                else speed = -1F;
            }

        }
    }
}
