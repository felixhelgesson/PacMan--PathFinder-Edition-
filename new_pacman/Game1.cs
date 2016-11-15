using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace new_pacman
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    /// 
    enum Gamestate
    {
        Start,
        Playing,
        Gameover,
        Highscore
    }

    public enum Direction
    {
        StandingStill,
        Up,
        Down,
        Left,
        Right,
    }

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D tile_tex;
        Texture2D pac_tex;
        Texture2D food_tex;
        Texture2D ghost_tex;
        Texture2D start_background;
        Texture2D whiteBackground;
        Rectangle pac_rec;
        Rectangle tile_rec;
        Rectangle food_rec;
        Rectangle ghost_rec;
        Rectangle food_hitrec;
        Rectangle tile_hitrec;
        Rectangle pac_hitrec;
        Rectangle ghost_hitrec;
        Vector2 position;

        SpriteFont font;
        Food_placement food_p;
        List<string> map = new List<string>();
        List<Game_objects> game_objects = new List<Game_objects>();
        pacman pac;
        Ghost ghost;
        AStarProgram AStar;
        Tiles tiles;
        public bool dead;

        public static int dots = 0;
        public static int score = 0;
        public static int lives = 3;
        int height;
        int width;
        Gamestate cG = Gamestate.Start;
        HashTable hashTable;
        HighScoreText highScoreText;



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            List<string> map = new List<string>();



            start_background = Content.Load<Texture2D>("pacman_arcade.png");
            whiteBackground = Content.Load<Texture2D>("wB");

            font = Content.Load<SpriteFont>("lives");
            //AStar = new AStarProgram();
            food_p = new Food_placement(food_tex, position, food_rec, food_hitrec, false);
            tiles = new Tiles(tile_tex, tile_rec, tile_hitrec, position);
            hashTable = new HashTable(10);
            StreamReader sr = new StreamReader(@"MyMap.txt");
            highScoreText = new HighScoreText();
            while (!sr.EndOfStream)
            {
                map.Add(sr.ReadLine());
            }

            sr.Close();

            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (map[i][j] == 'p')
                    {
                        pac = new pacman(pac_tex, pac_rec, new Rectangle(16 * j, 16 * i, 16, 16), new Vector2(16 * j, 16 * i), tiles, food_p, dead);
                    }
                }
            }
            
           
            

            //for (int i = 0; i < map.Count; i++)
            //{
            //    for (int j = 0; j < map[i].Length; j++)
            //    {
            //        if (map[i][j] == 'w')
            //        {
                        
            //            AStar.AddWalls(i,j);
            //        }   
            //    }
            //}


            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (map[i][j] == 'g')
                    {
                        ghost = new Ghost(ghost_tex, ghost_rec, new Rectangle(16 * j, 16 * i, 16, 16), new Vector2(16 * j, 16 * i), tiles, pac);

                    }
                }
            }




            height = graphics.PreferredBackBufferHeight = 150;
            width = graphics.PreferredBackBufferWidth = 304;
            graphics.ApplyChanges();



            base.Initialize();
        }


        protected override void LoadContent()
        {
            food_p.Load(Content);
            tiles.Load(Content);
            
            pac.Load(Content);
            ghost.Load(Content);


            spriteBatch = new SpriteBatch(GraphicsDevice);


        }


        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            switch (cG)
            {
                case Gamestate.Start:
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        cG = Gamestate.Playing;
                    }
                    break;
                case Gamestate.Playing:

                    
                    //AStar.Run();
                    pac.Update(gameTime);
                    ghost.Update(gameTime);
                    pac.Checkghost(ghost.GetPos());


                    if (pac.dead == true)
                    {
                        dead = false;
                        --lives;
                    }

                    if (lives <= 0 || dots == 84)
                    {
                        cG = Gamestate.Gameover;
                    }


                    break;
                case Gamestate.Gameover:
                    highScoreText.TextUpdate(gameTime);
                    
                    break;
                default:
                    break;
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            if (cG == Gamestate.Start)
            {
                spriteBatch.Draw(start_background, new Rectangle(0, 0, width, height), Color.White);
                spriteBatch.DrawString(font, "press Enter to start", new Vector2(10, 0), Color.Black);
            }

            if (cG == Gamestate.Playing)
            {

                tiles.Draw(spriteBatch);
                pac.Draw(spriteBatch);
                ghost.Draw(spriteBatch);
                food_p.Draw(spriteBatch);

                spriteBatch.DrawString(font, "lives;" + lives.ToString(), new Vector2(5, height - 15), Color.Black);
                spriteBatch.DrawString(font, "score;" + score.ToString(), new Vector2(80, height - 15), Color.Black);
            }
            if (cG == Gamestate.Gameover)
            {
                spriteBatch.Draw(whiteBackground, new Rectangle(0, 0, width, height), Color.White);
                spriteBatch.DrawString(font,"Your Score:" + score.ToString(), new Vector2(10, height - 50), Color.Black);
                hashTable.Put(highScoreText.name, score);
                spriteBatch.DrawString(font, highScoreText.name, new Vector2(10, height - 25), Color.Black);
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    cG = Gamestate.Highscore;
                }
            }

            if (cG == Gamestate.Highscore)
            {
                spriteBatch.Draw(whiteBackground, new Rectangle(0, 0, width, height), Color.White);
                spriteBatch.DrawString(font, highScoreText.name + hashTable.Get(highScoreText.name), new Vector2(10, height - 50), Color.Black);
                Console.WriteLine(hashTable.Get(highScoreText.name));
                 
            }

            spriteBatch.End();

            // TODO: Add your drawing code here
            
            base.Draw(gameTime);
        }

    }
}
