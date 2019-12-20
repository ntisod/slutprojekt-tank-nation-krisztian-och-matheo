using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace TankNation
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Tank Player1, Player2; //skapa två spelare/tanks
        GameObject Bana; //Skapa ett GameObj som ska användas som bana
        SpriteFont font; //Spritefont för utskrift
        Meter PowerMeter, Player1LifeMeter, Player2LifeMeter; //Mätare för liv och kraft

        List<Shot> allShots = new List<Shot>(); //Ny lista för alla skott(-objekt)
        Texture2D shot1Gfx; 
   
        List<Explosion> allExplosions = new List<Explosion>(); 
        Texture2D explosionGfx; 
        string displayMessage = ""; 

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D greenTankGfx = Content.Load<Texture2D>("green_tank");
            Texture2D redTankGfx = Content.Load<Texture2D>("red_tank");
            Texture2D level1Gfx = Content.Load<Texture2D>("level");
            Texture2D redMeterGfx = Content.Load<Texture2D>("meter");
            Texture2D lifeMeterGfx = Content.Load<Texture2D>("life_meter");
            shot1Gfx = Content.Load<Texture2D>("shot");
            explosionGfx = Content.Load<Texture2D>("explosion");

            Player1 = new Tank(false, new Vector2(0, -1), new Vector2(400, 300), greenTankGfx);
            Player2 = new Tank(false, new Vector2(0, -1), new Vector2(500, 400), redTankGfx);

            Bana = new GameObject(Vector2.Zero, level1Gfx, 0);

            PowerMeter = new Meter(new Vector2(350, 350), redMeterGfx, 0);
            Player1LifeMeter = new Meter(new Vector2(50, 50), lifeMeterGfx, 0);
            Player2LifeMeter = new Meter(new Vector2(250, 50), lifeMeterGfx, 0);
            
            font = Content.Load<SpriteFont>("Font");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            Player1.Update(gameTime);
            PowerMeter.Value = Player1.ShotPower;
            Player1LifeMeter.Value = Player1.Life;
            Player2LifeMeter.Value = Player2.Life;

            if (Player1.ShotFired)
            {
                Vector2 Position = Player1.Position + (Player1.Direction * (Player1.Gfx.Height) / 2) +
                                (Player1.Direction * shot1Gfx.Height / 2);
                allShots.Add(new Shot(Player1.ShotPower, Player1.Direction, 5f + Player1.Speed, Position, shot1Gfx, Player1.Angle));
                Player1.ShotPower = 0;
                Player1.ShotFired = false;
            }

            foreach(Explosion explosion in allExplosions.ToArray())
            {
                if (Player2.CheckCollision(explosion))
                {
                    Player2.Life -= 1;
                }
                if (Player1.CheckCollision(explosion))
                {
                    Player1.Life -= 1;
                }
            }

            foreach (Shot shot in allShots.ToArray())
            {
                shot.Update(gameTime);
                if (shot.Power < 0)
                {
                    allExplosions.Add(new Explosion(shot.Position, explosionGfx));
                    allShots.Remove(shot);
                }
                if (shot.CheckCollision(Player1))
                {

                }
                if (shot.CheckCollision(Player2))
                {

                }
            }

            foreach (Explosion explosion in allExplosions.ToArray())
            {
                explosion.Update(gameTime);
                if (!explosion.Active)
                    allExplosions.Remove(explosion);
            }

            if (Player1.CheckCollision(Player2))
            {
                if (Player1.Speed < 1.0 && Player1.Speed < 1.0)
                {
                    Player1.Speed = 0;
                    Player2.Speed = 0;
                    Player1.Position += Player2.Direction * Player2.Speed;
                    Player2.Position += Player1.Direction * Player1.Speed;
                }
                else
                {
                    if (Player1.Speed > Player2.Speed)
                    {
                        Player2.Life -= 5;
                        Player1.Speed = 0F;
                        Player2.Position = Player2.Position + Player1.Direction * 10F;
                    }
                    else
                    {
                        if (Player1.Speed == Player2.Speed)
                        {
                            Player2.Life -= 5;
                            Player1.Speed = 0F;
                            Player2.Position = Player2.Position + Player1.Direction * 10F;
                            Player1.Life -= 5;
                            Player2.Speed = 0F;
                            Player1.Position = Player1.Position + Player2.Direction * 10F;
                        }
                        else
                        {
                            Player1.Life -= 5;
                            Player2.Speed = 0F;
                            Player1.Position = Player1.Position + Player2.Direction * 10F;
                        }
                    }
                }
            }

            if (Player2.Life < 0)
            {
                Player1.Kills++;
                Player2.Respawn();

            }

            if(Player1.Life < 0)
            {
                Player2.Kills++;
                Player1.Respawn();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            //Ritar ut bana, spelare och mätare
            Bana.Draw(spriteBatch, Player1.Position);
            Player1.Draw(spriteBatch, Player1.Position);
            PowerMeter.Draw(spriteBatch, Player1.Position);
            Player2.Draw(spriteBatch, Player1.Position);
            Player1LifeMeter.Draw(spriteBatch, new Vector2(0, 0));
            Player2LifeMeter.Draw(spriteBatch, new Vector2(0, 0));

            //Loopar igenom alla skott och ritar ut dem
            for (int i = 0; i < allShots.Count; i++)
            {
                allShots[i].Draw(spriteBatch, Player1.Position);
            }
            //Loopar igenom alla explosioner och ritar ut dem
            for (int i = 0; i < allExplosions.Count; i++)
            {
                allExplosions[i].Draw(spriteBatch, Player1.Position);
            }
            //Skriver ut spelarnas namn och liv mm..
            string nameFormat = "{0}\nLife: {1}%\n\nKills: {2}";
            displayMessage = string.Format(nameFormat, "Player1", Player1.Life, Player1.Kills);
            spriteBatch.DrawString(font, displayMessage, new Vector2(51, 4), Color.Black);
            spriteBatch.DrawString(font, displayMessage, new Vector2(50, 5), Color.White);
            displayMessage = string.Format(nameFormat, "Player2", Player2.Life, Player2.Kills);
            spriteBatch.DrawString(font, displayMessage, new Vector2(251, 4), Color.Black);
            spriteBatch.DrawString(font, displayMessage, new Vector2(250, 5), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
