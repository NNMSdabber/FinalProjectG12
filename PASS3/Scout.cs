//Author:Noah Segal
//File Name:Scout.cs
//Project Name:PASS3
//Creation Date:2024-05-07
//Modification Date:2024-06-09
//Description:A unique enemy that fires bullets at the player when it sees the player.
using GameUtility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PASS3
{
    class Scout:Enemy
    {
        //Declare the movement constant
        private const int MOVEMENT_SPEED = 3;

        //Declare the enemy rectangle and ray cast
        private Rectangle enemyRec;
        private Rectangle rayCast;

        //Declare the animations
        private Animation[] scoutAnims;

        //Declare wether the scout is right or left, can see player and is shooting
        private bool isRight = true;
        private bool canSeePlayer = false;
        private bool isShoot = false;

        //Declare the distance traveled
        private float disTraveled = 0;

        //Declare the bullet shoot timer
        private Timer bulletShootTime = new Timer(1000,true);
        
        //Declare the scout hp
        private int hp = 40;

        /// <summary>
        /// Creates a new instance of the scout
        /// </summary>
        /// <param name="enemyRec">The rectangle representing the Scout's position and size.</param>
        /// <param name="scoutAnims">The animations for the Scout.</param>
        public Scout(Rectangle enemyRec,Animation[] scoutAnims) : base (enemyRec)
        {
            //Define the enemy rec and anim
            this.enemyRec = enemyRec;
            this.scoutAnims = scoutAnims;

            //Translates the anims
            scoutAnims[0].TranslateTo(enemyRec.X, enemyRec.Y);
            scoutAnims[1].TranslateTo(enemyRec.X, enemyRec.Y);

            //Sets up the ray cast
            rayCast = new Rectangle(enemyRec.X,enemyRec.Y,400,200);
        }

        /// <summary>
        /// Updates the Scout's state.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            // If the Scout is shooting, check if the bullet shoot timer is finished and reset it
            if (isShoot == true)
            {
                if (bulletShootTime.IsFinished())
                {
                    bulletShootTime.ResetTimer(true);
                }
            }

            // Update the bullet shoot timer
            bulletShootTime.Update(gameTime.ElapsedGameTime.TotalMilliseconds);

            // Update the Scout's animations to match its position
            scoutAnims[0].TranslateTo(enemyRec.X, enemyRec.Y);
            scoutAnims[1].TranslateTo(enemyRec.X, enemyRec.Y);

            // Set the shooting state based on whether the Scout can see the player
            if (canSeePlayer == true)
            {
                isShoot = true;
            }
            else
            {
                isShoot = false;
            }

            // Update the Scout's animations
            scoutAnims[0].Update(gameTime);
            scoutAnims[1].Update(gameTime);

            // Move the Scout if it's not shooting
            if (disTraveled < 200 && isRight == true && isShoot != true)
            {
                //Moves the scout
                enemyRec.X += MOVEMENT_SPEED;
                disTraveled += MOVEMENT_SPEED;
                rayCast.X = enemyRec.X;
            }
            else
            {
                if(isShoot != true)
                {
                    //Moves the scout
                    isRight = false;
                    enemyRec.X -= MOVEMENT_SPEED;
                    disTraveled -= MOVEMENT_SPEED;
                    rayCast.X = enemyRec.X - rayCast.Width;

                    // Change direction if the Scout has traveled the maximum distance
                    if (disTraveled < -200)
                    {
                        isRight = true;
                        rayCast.X = enemyRec.X;
                    }
                }
            }
        }

        /// <summary>
        /// Draws the Scout.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch used for drawing.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw the appropriate animation based on the shooting and direction states
            if (isShoot == false)
            {
                //If the scout is right, flip animation horizontally
                if (isRight == true)
                {
                    scoutAnims[0].Draw(spriteBatch, Color.White, SpriteEffects.FlipHorizontally);
                }
                else
                {
                    scoutAnims[0].Draw(spriteBatch, Color.White);
                }
            }
            else
            {
                //If the scout is right, flip animation horizontally
                if (isRight == true)
                {
                    scoutAnims[1].Draw(spriteBatch, Color.White, SpriteEffects.FlipHorizontally);
                }
                else
                {
                    scoutAnims[1].Draw(spriteBatch, Color.White);
                }
            }

        }

        /// <summary>
        /// Gets the bullet fired by the Scout.
        /// </summary>
        /// <returns>The bullet fired by the Scout, or null if no bullet is fired.</returns>
        public override Bullet GetBullet()
        {
            // If the Scout is shooting and the bullet shoot timer is finished, create and return a new bullet
            if (isShoot == true && bulletShootTime.IsFinished())
            {
                int num;

                //Creates bullets
                if(isRight == true)
                {
                    num = 1;
                    return new Bullet(Game1.bulletImg, new Rectangle(enemyRec.X, enemyRec.Y+30, Game1.bulletImg.Width / 6, Game1.bulletImg.Height / 6), num);
                }
                else
                {
                    num = -1;
                    return new Bullet(Game1.bulletImgLeft, new Rectangle(enemyRec.X, enemyRec.Y+30, Game1.bulletImg.Width / 6, Game1.bulletImg.Height / 6), num);
                }  

            }

            //Return nothing
            return null; 
        }

        /// <summary>
        /// Gets the health of the Scout.
        /// </summary>
        /// <returns>The health of the Scout.</returns>
        public override int GetHp()
        {
            return hp;
        }

        /// <summary>
        /// Gets the rectangle used for raycasting.
        /// </summary>
        /// <returns>The rectangle used for raycasting.</returns>
        public override Rectangle GetRayCast()
        {
            return rayCast;
        }

        /// <summary>
        /// Gets the rectangle representing the Scout's position and size.
        /// </summary>
        /// <returns>The rectangle representing the Scout's position and size.</returns>
        public override Rectangle GetRec()
        {
            return enemyRec;
        }

        /// <summary>
        /// Sets whether the Scout has seen the player.
        /// </summary>
        /// <param name="canSeePlayer">A value indicating whether the Scout has seen the player.</param>
        public override void SetEnemySeen(bool canSeePlayer)
        {
            this.canSeePlayer = canSeePlayer;
        }

        /// <summary>
        /// Sets the health of the Scout.
        /// </summary>
        /// <param name="hp">The amount of health to be subtracted.</param>
        public override void SetHealth(int hp)
        {
            this.hp -= hp;
        }


    }
}
