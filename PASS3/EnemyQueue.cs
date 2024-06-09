//Author:Noah Segal
//File Name:EnemyQueue.cs
//Project Name:PASS3
//Creation Date:2024-05-07
//Modification Date:2024-06-09
//Description:A queue that contains the enemies 
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
    class EnemyQueue
    {
        //Declare the enemy list 
        List<Enemy> enemyList = new List<Enemy>();
        
        //Declare the size of the queue 
        int size = 0;

        /// <summary>
        /// Creates an enemy queue
        /// </summary>
        public EnemyQueue()
        {

        }

        /// <summary>
        /// Adds an enemy to the queue
        /// </summary>
        /// <param name="enemy">Is the enemy</param>
        public void Enqueue(Enemy enemy)
        {
            //Adds enemy to the list
            enemyList.Add(enemy);

            //Increase size of queue
            size++;
        }

        /// <summary>
        /// Removes the enemy from the queue
        /// </summary>
        /// <returns>Returns an enemy</returns>
        public Enemy Dequeue()
        {
            //Set the result to null
            Enemy result = null;

            //If the queue isnt empty
            if (!IsEmpty())
            {
                //Returning the front of the queue
                result = enemyList[0];

                //Loop and move all items one element forward
                for (int i = 1; i < size; i++)
                {
                    enemyList[i - 1] = enemyList[i];
                }

                //Item has been removed, reduce size
                size--;
            }

            //Return the result
            return result;

        }

        /// <summary>
        /// Checks if the queue is empty
        /// </summary>
        /// <returns>Returns a bool if the size is 0 or not</returns>
        public bool IsEmpty()
        {
            //returns the result of the comparison between size and 0
            return size == 0;
        }

    }
}
