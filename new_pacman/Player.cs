using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace new_pacman
{
    class Player : GameObject
    {
        double frameTimer, frameInterval;
        float rotation = 0;
        int frame;
        Vector2 targetpos;
        Direction direction = Direction.StandingStill;
        MapManager mapManager;
        Ghost[] ghost = new Ghost[4];
        public static bool alive = true;

        public Player(Texture2D tex, Vector2 pos, Rectangle srcrec, Rectangle hitbox, MapManager mapManager, Ghost RedGhost, Ghost PurpleGhost, Ghost BlueGhost, Ghost OrangeGhost)
            : base(tex, pos, srcrec, hitbox)
        {
            frameTimer = 100;
            frameInterval = 100;
            this.pos = pos;
            targetpos = this.pos;
            this.srcrec = new Rectangle(0, 0, 16, 16);
            this.hitbox = new Rectangle(0, 0, 16, 16);
            this.mapManager = mapManager;
            this.ghost[0] = RedGhost;
            this.ghost[1] = PurpleGhost;
            this.ghost[2] = BlueGhost;
            this.ghost[3] = OrangeGhost;
            this.hitbox.X = (int)pos.X;
            this.hitbox.Y = (int)pos.Y;
        }

        public override void Update(GameTime gameTime)
        {
            PacmanControls();
            PacmanAnimation(gameTime);

            if (pos == targetpos && direction != Direction.StandingStill)
            {
                CheckWallContact();
                CheckFoodContact();
                CheckGhostContact();
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, srcrec, Color.White, rotation, new Vector2(8, 8), 1.5f, SpriteEffects.None, 1);
        }

        void PacmanControls()
        {
            if (Input.Key(Keys.Left))
            {
                direction = Direction.Left;
            }
            else if (Input.Key(Keys.Right))
            {
                direction = Direction.Right;
            }
            else if (Input.Key(Keys.Up))
            {
                direction = Direction.Up;
            }
            else if (Input.Key(Keys.Down))
            {
                direction = Direction.Down;
            }
        }
        void PacmanAnimation(GameTime gameTime)
        {
            if (frameTimer <= 0)
            {
                frameTimer = frameInterval;
                frame++;
                srcrec.X = (frame % 4) * 16;
            }
            switch (direction)
            {
                case Direction.StandingStill:
                    break;
                case Direction.Up:
                    frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
                    rotation = MathHelper.ToRadians(-90);
                    break;
                case Direction.Down:
                    frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
                    rotation = MathHelper.ToRadians(90);
                    break;
                case Direction.Left:
                    frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
                    rotation = MathHelper.ToRadians(180);
                    break;
                case Direction.Right:
                    frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
                    rotation = MathHelper.ToRadians(0);
                    break;
                default:
                    break;
            }
            if (targetpos != pos)
            {
                if (targetpos.X < pos.X)
                    pos.X -= 2f;
                else if (targetpos.X > pos.X)
                    pos.X += 2f;
                else if (targetpos.Y < pos.Y)
                    pos.Y -= 2f;
                else if (targetpos.Y > pos.Y)
                    pos.Y += 2f;
            }
        }
        void CheckWallContact()
        {
            if (mapManager.CheckWall(targetpos, direction) == true)
            {
                switch (direction)
                {
                    case Direction.Up:
                        targetpos.Y -= 30;
                        break;
                    case Direction.Down:
                        targetpos.Y += 30;
                        break;
                    case Direction.Left:
                        targetpos.X -= 30;
                        break;
                    case Direction.Right:
                        targetpos.X += 30;
                        break;
                    default:
                        break;
                }
            }
        }
        void CheckFoodContact()
        {
            if (mapManager.CheckFood(hitbox) == true)
            {
                for (int i = 0; i < mapManager.map.Count; i++)
                {
                    mapManager.eaten = true;
                }
            }
        }
        void CheckGhostContact()
        {
            for (int i = 0; i < 4; i++)
            {
                if (ghost[i].GhostPlayerContact(pos) == true)
                {
                    alive = false;
                }
            }
            if (!alive)
            {
                direction = Direction.StandingStill;
                srcrec = new Rectangle(0, 0, 16, 16);
                this.pos = new Vector2(375, 375);
                targetpos = this.pos;
                Game1.lives--;

                //ghost[0].ResetGhostPos();
                //ghost[1].ResetGhostPos();
                //ghost[2].ResetGhostPos();
                //ghost[3].ResetGhostPos();

                alive = true;
            }
        }
    }
}