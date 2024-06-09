//Author:Noah Segal
//File Name:CoinLList.cs
//Project Name:PASS3
//Creation Date:2024-05-07
//Modification Date:2024-06-09
//Description:A coin linked list. It maintains only a reference to the head of the list, no other Node
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
    class CoinLList
    {
        //Count of the list
        private int count;

        // Reference to the head node of the linked list
        CNode head;

        /// <summary>
        /// Initializes a new instance of the CoinLList class.
        /// </summary>
        public CoinLList()
        {
            count = 0;
        }

        /// <summary>
        /// Adds a new node to the linked list.
        /// </summary>
        /// <param name="newNode">The new node to add.</param>
        public void Add(CNode newNode)
        {
            // If the list is empty, set the new node as the head
            if (count == 0)
            {
                head = newNode;
            }
            else
            {
                // Start from the head node
                CNode curNode = head;

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
        /// Draws all coins in the linked list.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch to draw the coins.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Start from the head node
            CNode curNode = head;

            // Traverse the list and draw each coin
            while (curNode != null)
            {
                curNode.Draw(spriteBatch);
                curNode = curNode.GetNext();
            }
        }

        /// <summary>
        /// Deletes the head node of the linked list.
        /// </summary>
        public void DeleteHead()
        {
            // Set the head to the next node, effectively deleting the current head
            head = head.GetNext();
            count--;
        }

        /// <summary>
        /// Removes a node at a specified position in the linked list.
        /// </summary>
        /// <param name="position">The position of the node to remove.</param>
        public void RemoveAt(int position)
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
                CNode curNode = head;

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
                    curNode.SetNext(curNode.GetNext().GetNext());
                }

                // Decrease the count of nodes in the list
                count--;
            }
        }

        /// <summary>
        /// Gets the head of the coin llist
        /// </summary>
        /// <returns>the head</returns>
        public CNode GetHead()
        {
            return head;
        }

    }
}
