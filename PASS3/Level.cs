//Author:Noah Segal
//File Name:Level.cs
//Project Name:PASS3
//Creation Date:2024-05-07
//Modification Date:2024-06-09
//Description: The level class that controls and creates the levels and all stage hazzards
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
    class Level
    {
        //Sets the max level and chests
        private const int MAX_LEVEL = 3;
        private const int MAX_CHESTS = 5;

        //Declare the levels background, texture and rectangles
        private byte[,] levelBg;
        private Texture2D[] levelTex;
        private Rectangle[,] levelRecs;

        //Declare the platforms
        private List<Platform>[] platforms = new List<Platform>[MAX_LEVEL];
        
        //Declare the current layer
        private int curLayer = 0;

        //Declare the chests
        private Chest[] chests = new Chest[MAX_CHESTS];

        //Declare teleporters
        Teleporter[] teleporters = new Teleporter[MAX_LEVEL];

        //Declare linked list for coins
        CoinLList coinLList = new CoinLList();

        //Declare queue and linked list for enemys
        EnemyQueue[] enemyQueue = new EnemyQueue[3];
        EnemyLList enemyLList = new EnemyLList();

        //Declare random rng
        public static Random rng = new Random();

        /// <summary>
        /// Creates an instance of the level
        /// </summary>
        /// <param name="levelTex">Is the levels texture</param>
        /// <param name="levelRecs">Is the levels rectangle</param>
        /// <param name="levelBg">Is the levels background</param>
        public Level(Texture2D[] levelTex,Rectangle[,] levelRecs, byte[,] levelBg)
        {
            //Sets the textures, rectangles and bytes of the levels
            this.levelTex = levelTex;
            this.levelRecs = levelRecs;
            this.levelBg = levelBg;

            //Sets the platforms
            for(int i = 0; i < platforms.Length; i++)
            {
                platforms[i] = new List<Platform>();
            }

            //Creates the level
            CreateLevel();
        }

        /// <summary>
        /// Creates the level
        /// </summary>
        public void CreateLevel()
        {
            //Loops through the rows and collumns 
            for(int i = 0; i < levelRecs.GetLength(0); i++)
            {

                for (int j = 0; j < levelRecs.GetLength(1); j++)
                {
                    //Based on the layer, set the image
                    if(i == 0)
                    {
                        levelBg[i, j] = 0;
                        
                    }
                    else if(i == 1)
                    {
                        levelBg[i, j] = (byte)rng.Next(1,3);
                    }
                    else
                    {
                        levelBg[i, j] = (byte)rng.Next(3, 5);
                    }

                    //Sets the rectangles
                    levelRecs[i, j] = new Rectangle(0 + (j* Game1.screenWidth),0 + (i* Game1.screenHeight), Game1.screenWidth, Game1.screenHeight);
                }

            }

            //Loops through the platforms
            for(int i = 0; i < platforms.Length; i++)
            {
                //Adds platforms 
                platforms[i].Add(new Platform(null,new Rectangle(0,Game1.screenHeight + Game1.screenHeight*i, Game1.screenWidth*10,200)));


                //Loops through platforms
                for (int j = 0; j < 24; j++)
                {
                    //Adds the platforms
                    platforms[i].Add(new Platform(Game1.platText, new Rectangle((j * Game1.screenWidth)/2 + rng.Next(-100,300), Game1.screenHeight * i + rng.Next(400, 620), 200, 100)));
                }
            }

            //Loops through the teleporters
            for(int i = 0; i < teleporters.Length;i++)
            {
                //Creats new teleporters
                int nums = i;
                teleporters[i] = new Teleporter(Game1.teleportAnim[i], new Point(200, Game1.screenHeight*i+ 1600));
            }

            //Loops through the coins 
            for(int i = 0; i < rng.Next(40,81);i++)
            {
                //Adds the coins
                int x = rng.Next(1, 11) * Game1.screenWidth - rng.Next(200, 801);
                int y = rng.Next(1, 4) * Game1.screenHeight - rng.Next(200, 401);
                coinLList.Add(new CNode(new Coin(Game1.coinText,new Rectangle(x,y, Game1.coinText.Width / 4, Game1.coinText.Height / 4))));
            }

            //Loops through the layers
            for(int i = 0; i < MAX_LEVEL; i++)
            {
                //Creates new queue
                int val = i;
                enemyQueue[i] = new EnemyQueue();

                //Loops through enemyies
                for(int j = 1; j < rng.Next(6+5*i,21+5*i); j++)
                {
                    //Calculates a number, x val and y val
                    int num = rng.Next(0,101);
                    int x = Game1.screenWidth * j / (val + 1) + rng.Next(-400, 400);
                    int y = 700 + Game1.screenHeight * i;

                    //Adds scouts or probedroid
                    if (num < 80)
                    {
                        //Adds to queue
                        Animation[] anim = new Animation[2];
                        anim[0] = new Animation(Game1.trooperImg[0], 4, 1, 4, 0, Animation.NO_IDLE, Animation.ANIMATE_FOREVER, 500, new Vector2(x, y), 4f, true);
                        anim[1] = new Animation(Game1.trooperImg[1], 4, 1, 4, 0, Animation.NO_IDLE, Animation.ANIMATE_FOREVER, 1000, new Vector2(x, y), 4f, true);
                        enemyQueue[i].Enqueue(new Scout(new Rectangle(x, y, Game1.trooperAnim[0].GetDestRec().Width, Game1.trooperAnim[0].GetDestRec().Height), anim));
                    }
                    else
                    {
                        //Adds to queue
                        enemyQueue[i].Enqueue(new ProbeDroid(new Rectangle(x,y, Game1.probeImg.Width/2, Game1.probeImg.Height/2),Game1.probeImg));
                    }
                    
                }

            }

            //Loops through the chests
            for(int i = 0; i < MAX_CHESTS; i++)
            {
                int val = i;

                //Adds chests to the array
                if(val < 3)
                {
                    chests[i] = new Chest(Game1.chestImg, new Rectangle(rng.Next(300,9*Game1.screenWidth+400), 750,Game1.chestImg.Width, Game1.chestImg.Height));
                }
                else if(val < 4 && val > 2)
                {
                    chests[i] = new Chest(Game1.chestImg, new Rectangle(rng.Next(300, 9 * Game1.screenWidth + 400), 750+Game1.screenHeight, Game1.chestImg.Width, Game1.chestImg.Height));
                }
                else
                {
                    chests[i] = new Chest(Game1.chestImg, new Rectangle(rng.Next(300, 9 * Game1.screenWidth + 400), 750 + Game1.screenHeight*2, Game1.chestImg.Width, Game1.chestImg.Height));
                }
            }

            //Adds enemys to the linked list
            AddEnemyToLList();
        }

        /// <summary>
        /// Adds coins to a linked list
        /// </summary>
        /// <param name="x">Is the x value of the coin</param>
        /// <param name="y">Is the y value of the coin</param>
        public void AddToCoinLList(int x, int y)
        {
            //Adds the coins to the list
            coinLList.Add(new CNode(new Coin(Game1.coinText, new Rectangle(x, y, Game1.coinText.Width/4, Game1.coinText.Height/4))));
        }

        /// <summary>
        /// Adds enemies to the linked lists
        /// </summary>
        public void AddEnemyToLList()
        {
            //Creates a new enemy list
            enemyLList = new EnemyLList();

            //While enemyqueue isnt empty
            while(!enemyQueue[curLayer].IsEmpty())
            {
                //Add enemy to linked list
                enemyLList.AddToTail(new ENode(enemyQueue[curLayer].Dequeue()));
            }


        }

        /// <summary>
        /// Updates the level
        /// </summary>
        /// <param name="gameTime">Game time is the time passed through the game</param>
        public void UpdateLevel(GameTime gameTime)
        {
            //Updates the teleporters
            for (int i = 0; i < teleporters.Length; i++)
            {
                teleporters[i].UpdateTele(gameTime);
            }

            //Updates the enemys
            enemyLList.UpdateList(gameTime);
        }

        /// <summary>
        /// Draws the level
        /// </summary>
        /// <param name="spriteBatch">Sprite batch used to draw the level</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draws the level background
            for (int i = 0; i < levelRecs.GetLength(0); i++)
            {

                for (int j = 0; j < levelRecs.GetLength(1); j++)
                {
                    spriteBatch.Draw(levelTex[levelBg[i,j]],levelRecs[i,j],Color.White);
                }
            }

            // Draws the platforms
            for (int i = 0; i < platforms.Length; i++)
            {

                for (int j = 0; j < platforms[i].Count; j++)
                {
                    platforms[i][j].DrawPlat(spriteBatch);
                }
            }

            // Draws the teleporters
            for (int i = 0; i < teleporters.Length; i++)
            {
                teleporters[i].DrawTele(spriteBatch);
            }

            // Draws the chests
            for (int i = 0; i < chests.Length; i++)
            {
                chests[i].Draw(spriteBatch);
            }

            // Draws the coins and enemies
            coinLList.Draw(spriteBatch);
            enemyLList.Draw(spriteBatch);

        }

        /// <summary>
        /// Gets the chests in the level
        /// </summary>
        /// <returns>Array of chests</returns>
        public Chest[] GetChests()
        {
            return chests;
        }

        /// <summary>
        /// Sets the health of a chest
        /// </summary>
        /// <param name="num">Index of the chest</param>
        public void SetChestHealth(int num)
        {
            chests[num].SetHp();
        }

        /// <summary>
        /// Gets the platforms in the current layer
        /// </summary>
        /// <returns>List of platforms</returns>
        public List<Platform> GetPlatforms()
        {
            return platforms[curLayer];
        }

        /// <summary>
        /// Gets the teleporters in the level
        /// </summary>
        /// <returns>Array of teleporters</returns>
        public Teleporter[] GetTeleporter()
        {
            return teleporters;
        }

        /// <summary>
        /// Sets the current layer of the level
        /// </summary>
        /// <param name="layer">Layer index</param>
        public void SetCurLayer(int layer)
        {
            curLayer = layer;
        }

        /// <summary>
        /// Gets the current layer of the level
        /// </summary>
        /// <returns>Index of the current layer</returns>
        public int GetCurLayer()
        {
            return curLayer;
        }

        /// <summary>
        /// Gets the head node of the coin linked list
        /// </summary>
        /// <returns>Head node of the coin linked list</returns>
        public CNode GetCoinHead()
        {
            return coinLList.GetHead();
        }

        /// <summary>
        /// Gets the head node of the enemy linked list
        /// </summary>
        /// <returns>Head node of the enemy linked list</returns>
        public ENode GetEnemyHead()
        {
            return enemyLList.GetHead();
        }

        /// <summary>
        /// Removes a coin at a specific position from the linked list
        /// </summary>
        /// <param name="position">Position of the coin</param>
        public void RemoveAt(int position)
        {
            coinLList.RemoveAt(position);
        }

        /// <summary>
        /// Removes an enemy at a specific position from the linked list
        /// </summary>
        /// <param name="position">Position of the enemy</param>
        public void RemoveAtEnemy(int position)
        {
            enemyLList.Delete(position);
        }

        /// <summary>
        /// Returns the enemy linked list
        /// </summary>
        /// <returns>Enemy linked list</returns>
        public EnemyLList ReturnEnemyLList()
        {
            return enemyLList;
        }

        /// <summary>
        /// Sets whether an enemy can see the player
        /// </summary>
        /// <param name="num">Index of the enemy</param>
        /// <param name="val">Boolean indicating if the enemy can see the player</param>
        public void CanSeePlayer(int num,bool val)
        {
            enemyLList.SetSeePlayer(num,val);
        }

        /// <summary>
        /// Sets the health of an enemy
        /// </summary>
        /// <param name="num">Index of the enemy</param>
        /// <param name="hpTaken">Health points to be taken from the enemy</param>
        public void SetHealth(int num, int hpTaken)
        {
            enemyLList.SetHealth(num,hpTaken);
        }



    }
}
