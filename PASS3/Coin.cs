//Author:Noah Segal
//File Name:Coin.cs
//Project Name:PASS3
//Creation Date:2024-05-07
//Modification Date:2024-06-09
//Description:A coin class that gives the player a coin when picked up
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
    class Coin
    {
        //Declare the coin texture and rectangle
        Texture2D coinTxt;
        Rectangle coinRec;

        /// <summary>
        /// Creates a coin
        /// </summary>
        /// <param name="coinTxt">Is the coin texture</param>
        /// <param name="coinRec">Is the coin Rectangle</param>
        public Coin(Texture2D coinTxt, Rectangle coinRec)
        {
            //Sets the coin rectangle and texture
            this.coinTxt = coinTxt;
            this.coinRec = coinRec;
        }

        /// <summary>
        /// Draws the coin
        /// </summary>
        /// <param name="spriteBatch">Draws the image</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw the coin
            spriteBatch.Draw(coinTxt,coinRec,Color.White);
        }

        /// <summary>
        /// Gets the coins rectangle
        /// </summary>
        /// <returns>Returns the coin rectangle</returns>
        public Rectangle GetRec()
        {
            //Returns the coins rectangle
            return coinRec;
        }
    }
}
