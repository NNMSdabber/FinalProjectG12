//Author:Noah Segal
//File Name:Platform.cs
//Project Name:PASS3
//Creation Date:2024-05-07
//Modification Date:2024-06-09
//Description:A platform that the player can stand on
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
    class Platform
    {
        //Declare the platform texture and rectangle
        Rectangle platRec;
        Texture2D platText;

        /// <summary>
        /// Creates an instance of the platform
        /// </summary>
        /// <param name="platText">Is the platforms texture</param>
        /// <param name="platRec">Is the platforms rectangle</param>
        public Platform(Texture2D platText, Rectangle platRec)
        {
            //Sets the platform rectangle and texture
            this.platRec = platRec;
            this.platText = platText;
        }

        /// <summary>
        /// Draws the platform
        /// </summary>
        /// <param name="spriteBatch">Is used to draw the image</param>
        public void DrawPlat(SpriteBatch spriteBatch)
        {
            //Draw the platform if the texture isnt null
            if(platText != null)
            {
                spriteBatch.Draw(platText,platRec,Color.White);
            }
        }

        /// <summary>
        /// Gets the platform rectangle
        /// </summary>
        /// <returns>Returns the platform rectangle</returns>
        public Rectangle GetRectangle()
        {
            return platRec;
        }

    }
}
