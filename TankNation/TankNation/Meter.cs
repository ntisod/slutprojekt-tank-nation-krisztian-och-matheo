using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankNation
{
    //Klass meter för att visa 
    class Meter : GameObject
    {
        //Klass meter
        public Meter()
        {
            Value = 0;
        }

        //Bestäma variabeln Value
        public float Value
        {
            get;
            set;
        }


        public override void Draw(SpriteBatch spriteBatch, Vector2 DrawOffset)
        {
            spriteBatch.Draw(gfx, new Rectangle((int)base.Position.X, (int)base.Position.Y, (int)this.Value, gfx.Height), Color.White);
        }
    }
}
