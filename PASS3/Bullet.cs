//Author:Noah Segal
//File Name:Bullet.cs
//Project Name:PASS3
//Creation Date:2024-05-07
//Modification Date:2024-06-09
//Description:A projectile that is shot by the scout
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
    class Bullet
    {
        //Declare the bulletspeed
        private const int BULLETSPEED = 10;

        //Declare the bullet image and rec
        Texture2D bulletImg;
        Rectangle bulletRec;
        
        //Declare the bullet direction
        int direction = 0;

        /// <summary>
        /// Creates an instance of the bullet
        /// </summary>
        /// <param name="bulletImg">Is the bullet image</param>
        /// <param name="bulletRec">Is the bullet rectangle</param>
        /// <param name="direction">Is the direction the bullet will travel</param>
        public Bullet(Texture2D bulletImg,Rectangle bulletRec, int direction)
        {
            //Sets the bullet image and rectangle
            this.bulletImg = bulletImg;
            this.bulletRec = bulletRec;

            //Sets the direction
            this.direction = direction;
        }

        /// <summary>
        /// Updates the bullet
        /// </summary>
        /// <param name="gameTime">Game time is the time passed in the game</param>
        public void Update(GameTime gameTime)
        {
            //Moves the bullet
            bulletRec.X += BULLETSPEED * direction;
        }

        /// <summary>
        /// Draws the bullet
        /// </summary>
        /// <param name="spriteBatch">draws the image</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            //Draws the bullet
            spriteBatch.Draw(bulletImg, bulletRec, Color.White);
        }

        /// <summary>
        /// Gets the bullet x position
        /// </summary>
        /// <returns>Returns bullet x position</returns>
        public int GetXPos()
        {
            //Returns the bullet x pos
            return bulletRec.X;
        }

        /// <summary>
        /// Gets the bullet rectangle 
        /// </summary>
        /// <returns>Returns the bullet rectangle</returns>
        public Rectangle GetRectangle()
        {
            //Returns the bullet rectangle
            return bulletRec;
        }



    }
}
