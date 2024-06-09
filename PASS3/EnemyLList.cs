//Author:Noah Segal
//File Name:EnemyLList.cs
//Project Name:PASS3
//Creation Date:2024-05-07
//Modification Date:2024-06-09
//Description:A enemy linked list. It maintains only a reference to the head of the list, no other Node
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
    class EnemyLList
    {
        //Store the head of the list only which may be null if the list is empty
        private ENode head;

        //Maintain the total number of Nodes in the list
        private int count;

        /// <summary>
        /// Initializes a new instance of the EnemyLList
        /// </summary>
        public EnemyLList()
        {
            //Set count to 0 
            count = 0;
        }

        /// <summary>
        /// Retrienve the current head of the Linked List
        /// </summary>
        /// <returns>The linked list head</returns>
        public ENode GetHead()
        {
            return head;
        }


        /// <summary>
        /// Adds a new node to the tail of the linked list.
        /// </summary>
        /// <param name="newNode">The new node to add.</param>
        public void AddToTail(ENode newNode)
        {
            // If the list is empty, set the new node as the head
            if (count == 0)
            {
                head = newNode;
            }
            else
            {
                // Start from the head node
                ENode curNode = head;

                // Traverse to the last node in the list
                while (curNode.GetNext() != null)
                {
                    curNode = curNode.GetNext();
                }

                // Set the next node of the last node to the new node
                curNode.SetNext(newNode);
            }

            // Increment the count of nodes in the list
            count++;
        }

        /// <summary>
        /// Deletes the head node of the linked list.
        /// </summary>
        public void DeleteHead()
        {
            head = head.GetNext();
            count--;
        }

        /// <summary>
        /// Deletes a node at a specified position in the linked list.
        /// </summary>
        /// <param name="position">The position of the node to delete.</param>
        public void Delete(int position)
        {
            // If the position is 0, delete the head node
            if (position == 0)
            {
                DeleteHead();
            }
            // If the position is within the bounds of the list
            else if (position < count)
            {
                // Start from the head node
                ENode curNode = head;

                // Traverse to the node just before the specified position
                for (int i = 0; i < position - 1; i++)
                {
                    curNode = curNode.GetNext();
                }

                // If the position is the last node, set the current node's next to null
                if (position == count - 1)
                {
                    curNode.SetNext(null);
                }
                // Otherwise, link the current node to the node after the next node
                else
                {
                    // Decrease the count of nodes in the list
                    curNode.SetNext(curNode.GetNext().GetNext());
                }

                count--;
            }
        }

        /// <summary>
        /// Draws all enemies in the linked list.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch used for drawing.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            //Set the curnode to head
            ENode curNode = head;

            //Draws the curent node
            while (curNode != null)
            {
                curNode.Draw(spriteBatch);
                curNode = curNode.GetNext();
            }
        }

        /// <summary>
        /// Updates all enemies in the linked list.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void UpdateList(GameTime gameTime)
        {
            //Set the curnode to head
            ENode curNode = head;

            //Updates the current node
            while (curNode != null)
            {
                curNode.Update(gameTime);
                curNode = curNode.GetNext();
            }
        }

        /// <summary>
        /// Sets whether a specific enemy in the list can see the player.
        /// </summary>
        /// <param name="position">The position of the enemy.</param>
        /// <param name="val">True if the enemy can see the player; otherwise, false.</param>
        public void SetSeePlayer(int position,bool val)
        {
            // Initialize a counter to track the position in the list
            int countForPlayer = 0;

            // Start from the head node
            ENode tempNode = head;

            // Traverse to the specified position in the list
            while (countForPlayer < position)
            {
                tempNode = tempNode.GetNext();
                countForPlayer++;
            }

            // Set whether the enemy can see the player
            tempNode.SetEnemySeen(val);
        }

        /// <summary>
        /// Sets the health of a specific enemy in the list.
        /// </summary>
        /// <param name="position">The position of the enemy.</param>
        /// <param name="val">The amount to set the enemy's health.</param>
        public void SetHealth(int position, int val)
        {
            // Initialize a counter to track the position in the list
            int countForHealth = 0;

            // Start from the head node
            ENode tempNode = head;

            // Traverse to the specified position in the list
            while (countForHealth < position)
            {
                tempNode = tempNode.GetNext();
                countForHealth++;
            }

            // Set the health of the enemy at the specified position
            tempNode.SetHealth(val);
        }
    }
}
