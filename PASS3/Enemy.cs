//Author:Noah Segal
//File Name:Enemy.cs
//Project Name:PASS3
//Creation Date:2024-05-07
//Modification Date:2024-06-09
//Description:An enemy base class 
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
    class Enemy
    {

        /// <summary>
        /// Initializes a new instance of the enemy 
        /// </summary>
        /// <param name="enemyRec">The rectangle representing the enemy's position and size.</param>
        public Enemy(Rectangle enemyRec)
        {

        }

        /// <summary>
        /// Updates the enemy's state.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public virtual void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// Draws the enemy.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch used for drawing.</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        /// <summary>
        /// Gets the rectangle used for raycasting.
        /// </summary>
        /// <returns>A rectangle used for raycasting.</returns>
        public virtual Rectangle GetRayCast()
        {
            return new Rectangle(0, 0, 0, 0);
        }

        /// <summary>
        /// Gets the rectangle representing the enemy's position and size.
        /// </summary>
        /// <returns>The rectangle representing the enemy's position and size.</returns>
        public virtual Rectangle GetRec()
        {
            return new Rectangle(0, 0, 0, 0);
        }

        /// <summary>
        /// Sets whether the enemy has seen the player.
        /// </summary>
        /// <param name="val">A value indicating whether the enemy has seen the player.</param>
        public virtual void SetEnemySeen(bool val)
        {

        }

        /// <summary>
        /// Gets the bullet fired by the enemy.
        /// </summary>
        /// <returns>The bullet fired by the enemy, or null if no bullet is fired.</returns>
        public virtual Bullet GetBullet()
        {
            return null;
        }

        /// <summary>
        /// Sets the enemy's health.
        /// </summary>
        /// <param name="hp">The amount of health to set.</param>
        public virtual void SetHealth(int hp)
        {

        }

        /// <summary>
        /// Gets the enemy's health.
        /// </summary>
        /// <returns>The enemy's health.</returns>
        public virtual int GetHp()
        {
            return 0;
        }
    }
}
