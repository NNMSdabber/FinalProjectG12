//Author:Noah Segal
//File Name: Chest.cs
//Project Name:PASS3
//Creation Date:2024-05-07
//Modification Date:2024-06-09
//Description:A chest class that heals the player
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
    class Chest
    {
        //Declare the chest image and rectangle
        private Texture2D chestImg;
        private Rectangle chestRec;
        
        //Declare if the player took hp from chest
        private bool isTakenHp = false;

        //Declare the hp heal value
        private int hpHealValue = 30;

        /// <summary>
        /// Create an instance of the chest
        /// </summary>
        /// <param name="chestImg">The chests image</param>
        /// <param name="chestRec">The rectangle for the chest</param>
        public Chest(Texture2D chestImg, Rectangle chestRec)
        {
            //Set the chest image and rectangle
            this.chestImg = chestImg;
            this.chestRec = chestRec;
        }

        /// <summary>
        /// Gets the chest rectangles
        /// </summary>
        /// <returns>Returns chest rectangles</returns>
        public Rectangle GetRectangle()
        {
            //return the chests rectangle
            return chestRec;
        }

        /// <summary>
        /// Sets the chests hp taken
        /// </summary>
        public void SetHp()
        {
            //Set hp taken to true
            isTakenHp = true;
        }

        /// <summary>
        /// Gets the chests hp value
        /// </summary>
        /// <returns>Returns the hp heal value</returns>
        public int GetHp()
        {
            //if the hp taken is false
            if(isTakenHp == false)
            {
                //Return the heal value
                return hpHealValue;
            }

            //Return the heal value
            return 0;
        }

        /// <summary>
        /// Draws the chest
        /// </summary>
        /// <param name="spriteBatch">Draws the image</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            //If hp isnt taken yet
            if(isTakenHp == false)
            {
                //Draw chest red
                spriteBatch.Draw(chestImg,chestRec,Color.Red);
            }
            else
            {
                //Draw chest white
                spriteBatch.Draw(chestImg, chestRec, Color.White);
            }

        }
    }
}
