//Author:Noah Segal
//File Name:ENode.cs
//Project Name:PASS3
//Creation Date:2024-05-07
//Modification Date:2024-06-09
//Description:This is a class that represents a single Node to be used on a Linked List
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
    class ENode
    {
        //Cargo data to be maintained
        private Enemy enemy;

        //Reference to the Next Node in the list
        private ENode next;

        /// <summary>
        /// Initializes a new instance of ENODE
        /// </summary>
        /// <param name="enemy">The enemy to be stored in the node.</param>
        public ENode(Enemy enemy)
        {
            this.enemy = enemy;
        }

        /// <summary>
        /// Updates the enemy.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime)
        {
            enemy.Update(gameTime);
        }

        /// <summary>
        /// Sets the next node in the list.
        /// </summary>
        /// <param name="node">The next node.</param>
        public void SetNext(ENode node)
        {
            next = node;
        }

        /// <summary>
        /// Gets the next node in the list.
        /// </summary>
        /// <returns>The next node.</returns>
        public ENode GetNext()
        {
            return next;
        }

        /// <summary>
        /// Gets the rectangle used for raycasting from the enemy.
        /// </summary>
        /// <returns>The rectangle used for raycasting.</returns>
        public Rectangle GetRayCast()
        {
            return enemy.GetRayCast();
        }

        /// <summary>
        /// Sets whether the enemy has seen the player.
        /// </summary>
        /// <param name="set">A value indicating whether the enemy has seen the player.</param>
        public void SetEnemySeen(bool set)
        {
            enemy.SetEnemySeen(set);
        }

        /// <summary>
        /// Gets the rectangle representing the enemy's position and size.
        /// </summary>
        /// <returns>The rectangle representing the enemy's position and size.</returns>
        public Rectangle GetRec()
        {
            return enemy.GetRec();
        }

        /// <summary>
        /// Gets the bullet fired by the enemy.
        /// </summary>
        /// <returns>The bullet fired by the enemy, or null if no bullet is fired.</returns>
        public Bullet GetBullet()
        {
            return enemy.GetBullet();
        }

        /// <summary>
        /// Draws the enemy.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch used for drawing.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            enemy.Draw(spriteBatch);
        }

        /// <summary>
        /// Sets the health of the enemy.
        /// </summary>
        /// <param name="hp">The amount of health to be subtracted.</param>
        public void SetHealth(int hp)
        {
            enemy.SetHealth(hp);
        }

        /// <summary>
        /// Gets the health of the enemy.
        /// </summary>
        /// <returns>The health of the enemy.</returns>
        public int GetHp()
        {
            return enemy.GetHp();
        }

        /// <summary>
        /// Gets the enemy stored in the node.
        /// </summary>
        /// <returns>The enemy stored in the node.</returns>
        public Enemy GetEnemy()
        {
            return enemy;
        }

    }
}
