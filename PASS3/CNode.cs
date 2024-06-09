//Author:Noah Segal
//File Name:CNode.cs
//Project Name:PASS3
//Creation Date:2024-05-07
//Modification Date:2024-06-09
//Description:A coin node class that holds reference to the next node
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
    class CNode
    {
        //Cargo data to be maintained
        private Coin coin;

        //Reference to the Next Node in the list
        private CNode nextNode;

        /// <summary>
        /// Initializes a new instance of the CNODE
        /// </summary>
        /// <param name="coin">The coin to be stored in the node.</param>
        public CNode(Coin coin)
        {
            this.coin = coin;
        }

        /// <summary>
        /// Gets the next node in the list.
        /// </summary>
        /// <returns>The next node.</returns>
        public CNode GetNext()
        {
            return nextNode;
        }

        /// <summary>
        /// Draws the coin.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch used for drawing.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
                coin.Draw(spriteBatch);
        }

        /// <summary>
        /// Gets the rectangle representing the coin's position and size.
        /// </summary>
        /// <returns>The rectangle representing the coin's position and size.</returns>
        public Rectangle GetRec()
        {
                return coin.GetRec(); 
        }

        /// <summary>
        /// Sets the next node in the list.
        /// </summary>
        /// <param name="node">The next node.</param>
        public void SetNext(CNode node)
        {
            nextNode = node;
        }
    }
}
