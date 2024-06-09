//Author:Noah Segal
//File Name:ProbeDroid.cs
//Project Name:PASS3
//Creation Date:2024-06-08
//Modification Date:2024-06-08
//Description:A unique enemy that when seen will run like crazy and increase speed, they drop a coin when dieing 
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
    class ProbeDroid : Enemy
    {
        //Declare the enemy rectangles
        private Rectangle enemyRec;
        private Rectangle rayCast;

        //Declare the booleans for if the enemy is going right or can see the player
        private bool isRight = true;
        private bool canSeePlayer = false;

        //Declare the distance traveled
        private float disTraveled = 0;

        //Declare the hp
        private int hp = 10;

        //Declare the probe image
        private Texture2D probeImg;
        
        //Declare the speed
        private float speed = 2f;

        //Declare the total moved
        private int totalMoved = 300;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProbeDroid"/> class.
        /// </summary>
        /// <param name="enemyRec">The rectangle representing the Probe Droid's position and size.</param>
        /// <param name="probeImg">The texture of the Probe Droid.</param>
        public ProbeDroid(Rectangle enemyRec, Texture2D probeImg) : base(enemyRec)
        {
            //Set the rectangle and image
            this.enemyRec = enemyRec;
            this.probeImg = probeImg;

            //Set the rey cast
            rayCast = new Rectangle(enemyRec.X, enemyRec.Y, 400, 200);
        }

        /// <summary>
        /// Updates the Probe Droid's state.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            // If the Probe Droid has not yet traveled the total distance and is moving to the right
            if (disTraveled < totalMoved && isRight == true)
            {
                // Move the Probe Droid to the right
                enemyRec.X += (int)speed;
                disTraveled += (int)speed;
                rayCast.X = enemyRec.X;
            }
            else
            {
                // Change direction to left
                isRight = false;
                enemyRec.X -= (int)speed;
                disTraveled -= (int)speed;
                rayCast.X = enemyRec.X - rayCast.Width;

                // If the Probe Droid has traveled the total distance to the left
                if (disTraveled < -totalMoved)
                {
                    // Change direction to right
                    isRight = true;
                    rayCast.X = enemyRec.X;
                }
            }

            // If the Probe Droid can see the player
            if (canSeePlayer == true)
            {
                // Increase the total distance the Probe Droid can move and its speed
                totalMoved += 1;
                speed += 0.05f;
            }

        }

        /// <summary>
        /// Draws the Probe Droid.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch used for drawing.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(probeImg,enemyRec,Color.White);
        }

        /// <summary>
        /// Gets the health of the Probe Droid.
        /// </summary>
        /// <returns>The health of the Probe Droid.</returns>
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
        /// Gets the rectangle representing the Probe Droid's position and size.
        /// </summary>
        /// <returns>The rectangle representing the Probe Droid's position and size.</returns>
        public override Rectangle GetRec()
        {
            return enemyRec;
        }

        /// <summary>
        /// Sets whether the Probe Droid has seen the player.
        /// </summary>
        /// <param name="canSeePlayer">A value indicating whether the Probe Droid has seen the player.</param>
        public override void SetEnemySeen(bool canSeePlayer)
        {
            this.canSeePlayer = canSeePlayer;
        }


        /// <summary>
        /// Sets the health of the Probe Droid.
        /// </summary>
        /// <param name="hp">The amount of health to be subtracted.</param>
        public override void SetHealth(int hp)
        {
            this.hp -= hp;
        }

        /// <summary>
        /// Gets the Y-coordinate for specific calculations.
        /// </summary>
        /// <returns>The Y-coordinate plus an offset.</returns>
        public int GetY()
        {
            return enemyRec.Y+30;
        }

        /// <summary>
        /// Gets the X-coordinate for specific calculations.
        /// </summary>
        /// <returns>The X-coordinate plus an offset.</returns>
        public int GetX()
        {
            return enemyRec.X+40;
        }
    }
}
