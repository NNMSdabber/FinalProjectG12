//Author:Noah Segal
//File Name:Game1.cs
//Project Name:PASS3
//Creation Date:2024-05-07
//Modification Date:2024-06-09
//Description:Echoes of Revan's Legacy takes place in the Star Wars universe during the empire's reign. The player will have to navigate through different landscapes such as the icy planet of Hoth or Darth Revan's Star Forge. The player must reach the end goal in order to beat the level. 
//OOP: Used all throughout the program
//Lists and 2D arrays: 2D arrays are used in the level class for the background system, Lists are used for the bullets and inside the enemyQueue
//Queues: Queues are specifically used in the enemy queue class
//Linked Lists: Linked Lists are used for both the enemy linked list and the coin linked list
//File io: Used to store the users coins and buffs that they obtained from gambling. Code can be found in multiple methods in Game1.cs
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
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        //Declare constants for the levels
        private const int HOTH = 0;
        private const int STAR_FORGE = 1;

        //Declare constants for hoth backgrounds
        private const int HOTH_BG = 0;
        private const int CAVE_LARGE = 1;
        private const int ICE_CAVE = 2;
        private const int ICE_CAVE_SPIKE = 3;
        private const int ICE_SPIKE_DARK = 4;

        //Declare constants for gamestates
        private const int MENU = 0;
        private const int GAME_PLAY = 1;
        private const int INSTRUCTIONS = 2;
        //private const int STATS = 3;
        private const int GAME_OVER = 4;
        private const int SHOP = 5;

        //Declare constants for gambling
        private const int MYSTERY_BOX = 0;
        private const int SPEED = 1;
        private const int DAMAGE = 2;
        private const int HEALTH = 3;

        //Declare constants for luke
        public const int LUKE_RIGHT = 0;
        public const int LUKE_LEFT = 1;

        //Declare constants for player animation
        public const int RUNNING_RIGHT = 0;
        public const int JUMPING = 1;
        public const int CROUCHING = 2;
        public const int ATTACKING = 3;
        public const int RUNNING_LEFT = 4;

        //Store screenwidth and screenheight
        public static int screenHeight;
        public static int screenWidth;

        //Store the background constants
        private const int BACKGROUND = 0;
        private const int START = 1;
        private const int HOTH_STATE = 2;
        private const int FORGE_STATE = 3;
        private const int BACK = 4;
        private const int START_GAME = 6;
        private const int SHOP_BUTTON = 5;

        //Store the sound effect constants
        private const int LIGHTSABER_SWING = 0;
        private const int MONEY = 1;
        private const int CLICK = 2;
        private const int SCREAM = 3;
        private const int YAYYY = 4;
        private const int WOMP = 5;

        //Store the music constants
        private const int BATTLE = 0;
        private const int CANTINA = 1;
        private const int EASTER_EGG = 2;
        private const int GAME_OVER_MUSIC = 3;

        //Store the instruction 
        private const int INSTRUCTIONS_BUTTON = 0;
        private const int INSTRUCTIONS_IMAGE = 1;

        //Store the current level
        private bool isLevelSelect = false;

        //Store the gamestate
        private int gameState = MENU;

        //Declare the levels
        private Level[] levels = new Level[2];

        //Declare the hoth textures
        private Texture2D[] hothTextures = new Texture2D[5];

        //Declare the player texture
        private Texture2D[] lukeTxt = new Texture2D[2];
        private Texture2D[] playerAnimsImgs = new Texture2D[5];
        private Animation[] playerAnims = new Animation[5];

        //Declare the instruction image
        private Texture2D[] instructionImgs = new Texture2D[2];

        //Declare the menu images
        private Texture2D[] menuTxt = new Texture2D[6];

        //Declare the gambling images
        private Texture2D[] gambleTxt = new Texture2D[4];

        //Declare the files
        private static StreamWriter outFile;
        private static StreamReader inFile;

        //Declare the bullet image
        public static Texture2D bulletImg;
        public static Texture2D bulletImgLeft;
        
        //Declare the current player level
        private int curLevel = 0;

        //Declare the gambling state
        private int gambleState = 0;

        //Declare the platform texture
        public static Texture2D platText;

        //Declare the teleporter texture and animation
        public static Texture2D teleporterText;
        public static Animation[] teleportAnim = new Animation[3];

        //Declare the trooper texture and animation
        public static Texture2D[] trooperImg = new Texture2D[2];
        public static Animation[] trooperAnim = new Animation[2];

        //Declare the coin images
        public static Texture2D coinText;

        //Define the chest image
        public static Texture2D chestImg;

        //Define starforge background image
        private static Texture2D starForgeImg;

        //Declare the shop image
        private Texture2D shopImg;

        //Declare probe image
        public static Texture2D probeImg;

        //Declare the instruction rectangles
        private Rectangle[] instructionRecs = new Rectangle[2];

        //Keyboard state 
        public static KeyboardState kb;
        public static KeyboardState prevKb;

        //Mouse states
        public static MouseState mouse;
        public static MouseState prevMouse;

        //Declare the player
        private Player player;

        //Declare player stats
        private int playerHp;
        private int playerDmg;
        private float playerSpeed;

        //Define camera system
        Cam2D cam;

        //Define rng
        Random rng = new Random();
        
        //Define the coins
        private int coins = 0;

        //Define the coin for shop
        Coin coin;

        //Define animation for the gambling
        private Timer animTimer = new Timer(3000,false);
        private Timer fadeTime = new Timer(500, false);
        private bool fadeOut = true;
        private float fadeVal = 0f;

        //Define world rectangle
        Rectangle worldBounds;
        Rectangle playerRec;

        //Define the menu rectangles
        Rectangle[] menuRecs = new Rectangle[7];

        //Define the gambling rectangles
        Rectangle[] gambleRecs = new Rectangle[4];

        //Define the list of bullets
        private List<Bullet> bullets = new List<Bullet>();

        //Define the sound effects and the music
        public static SoundEffect[] sounds = new SoundEffect[6];
        private Song[] music = new Song[4];

        //Define the fonds
        private SpriteFont font;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            //Set windows title
            Window.Title = "Echoes of Revan's Legacy";

            //Makes the mouse visible
            IsMouseVisible = true;

            //Change background size
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 1000;

            //Set the screen width and height
            screenHeight = graphics.PreferredBackBufferHeight;
            screenWidth = graphics.PreferredBackBufferWidth;

            //For camera efficentcy
            graphics.PreferMultiSampling = true;
            graphics.SynchronizeWithVerticalRetrace = false;

            

            graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //Load the hoth images
            hothTextures[HOTH_BG] = Content.Load<Texture2D>("Sprites/Images/Background");
            hothTextures[CAVE_LARGE] = Content.Load<Texture2D>("Sprites/Images/CaveLarge");
            hothTextures[ICE_CAVE] = Content.Load<Texture2D>("Sprites/Images/IceCave");
            hothTextures[ICE_CAVE_SPIKE] = Content.Load<Texture2D>("Sprites/Images/IceCaveSpike");
            hothTextures[ICE_SPIKE_DARK] = Content.Load<Texture2D>("Sprites/Images/IceSpikeDark");

            //Load Luke Images
            lukeTxt[LUKE_RIGHT] = Content.Load<Texture2D>("Sprites/Images/StandingSkywalker");
            lukeTxt[LUKE_LEFT] = Content.Load<Texture2D>("Sprites/Images/StandingSkywalkerLeft");

            //Load luke animations
            playerAnimsImgs[RUNNING_RIGHT] = Content.Load<Texture2D>("Sprites/Images/RunningRightSpritesheet");
            playerAnimsImgs[JUMPING] = Content.Load<Texture2D>("Sprites/Images/SkywalkerJumping");
            playerAnimsImgs[CROUCHING] = Content.Load<Texture2D>("Sprites/Images/SkywalkerCrouching");
            playerAnimsImgs[ATTACKING] = Content.Load<Texture2D>("Sprites/Images/SkywalkerAttackSpritesheet");
            playerAnimsImgs[RUNNING_LEFT] = Content.Load<Texture2D>("Sprites/Images/RunningLeftSpritesheet");

            //Load Plaform textures
            platText = Content.Load<Texture2D>("Sprites/Images/IcePlatform");
            teleporterText = Content.Load<Texture2D>("Sprites/Images/PortalSpriteSheet");

            //Load background textures
            menuTxt[BACKGROUND] = Content.Load<Texture2D>("Sprites/Images/backgroundMenuImg");
            menuTxt[START] = Content.Load<Texture2D>("Sprites/Images/StartBtn");
            menuTxt[HOTH_STATE] = Content.Load<Texture2D>("Sprites/Images/Hoth");
            menuTxt[FORGE_STATE] = Content.Load<Texture2D>("Sprites/Images/Forge");
            menuTxt[BACK] = Content.Load<Texture2D>("Sprites/Images/back");
            menuTxt[SHOP_BUTTON] = Content.Load<Texture2D>("Sprites/Images/Shop");

            //Load Gambling textures
            gambleTxt[MYSTERY_BOX] = Content.Load<Texture2D>("Sprites/Images/MysteryBox");
            gambleTxt[SPEED] = Content.Load<Texture2D>("Sprites/Images/Shoe");
            gambleTxt[DAMAGE] = Content.Load<Texture2D>("Sprites/Images/Damage");
            gambleTxt[HEALTH] = Content.Load<Texture2D>("Sprites/Images/Heart");

            //Load starforge image
            starForgeImg = Content.Load<Texture2D>("Sprites/Images/StarForgePlat");

            //Load coin texture
            coinText = Content.Load<Texture2D>("Sprites/Images/gold-coin");

            //Load shop texture
            shopImg = Content.Load<Texture2D>("Sprites/Images/shopBg");

            //Load the sounds
            sounds[LIGHTSABER_SWING] = Content.Load<SoundEffect>("Audio/lightsaber");
            sounds[MONEY] = Content.Load<SoundEffect>("Audio/Money");
            sounds[CLICK] = Content.Load<SoundEffect>("Audio/mouseclick");
            sounds[SCREAM] = Content.Load<SoundEffect>("Audio/StormtrooperScream");
            sounds[YAYYY] = Content.Load<SoundEffect>("Audio/YAYYY");
            sounds[WOMP] = Content.Load<SoundEffect>("Audio/Womp");

            //Load the music 
            music[BATTLE] = Content.Load<Song>("Audio/Battle");
            music[CANTINA] = Content.Load<Song>("Audio/Cantina");
            music[EASTER_EGG] = Content.Load<Song>("Audio/EasterEgg");
            music[GAME_OVER_MUSIC] = Content.Load<Song>("Audio/GameOver");

            //Load Bullet image
            bulletImg = Content.Load<Texture2D>("Sprites/Images/Bullet");
            bulletImgLeft = Content.Load<Texture2D>("Sprites/Images/BulletLeft");

            //Load the trooper image
            trooperImg[0] = Content.Load<Texture2D>("Sprites/Images/ScoutRun");
            trooperImg[1] = Content.Load<Texture2D>("Sprites/Images/ScoutShoot");

            //Load the chest image
            chestImg = Content.Load<Texture2D>("Sprites/Images/Chest");

            //Load the font
            font = Content.Load<SpriteFont>("Fonts/Score");

            //Load the probe image
            probeImg = Content.Load<Texture2D>("Sprites/Images/ProbeDroid");

            //Load the instruction images
            instructionImgs[INSTRUCTIONS_BUTTON] = Content.Load<Texture2D>("Sprites/Images/InstructionButton");
            instructionImgs[INSTRUCTIONS_IMAGE] = Content.Load<Texture2D>("Sprites/Images/InstructionsImg");

            //Set up the instruction rectangles
            instructionRecs[INSTRUCTIONS_BUTTON] = new Rectangle(230,850, menuTxt[SHOP_BUTTON].Width, menuTxt[SHOP_BUTTON].Height);
            instructionRecs[INSTRUCTIONS_IMAGE] = new Rectangle(0, 0, screenWidth,screenHeight);

            //Set up trooper animation
            trooperAnim[0] = new Animation(trooperImg[0],4,1,4,0,Animation.NO_IDLE,Animation.ANIMATE_FOREVER,500,new Vector2(500,700),4f,true);
            trooperAnim[1] = new Animation(trooperImg[1],4,1,4,0, Animation.NO_IDLE, Animation.ANIMATE_FOREVER, 1000, new Vector2(500, 700), 4f, true);

            //Set up luke animations
            playerAnims[RUNNING_RIGHT] = new Animation(playerAnimsImgs[RUNNING_RIGHT], 4, 2, 8, 0, Animation.NO_IDLE, Animation.ANIMATE_FOREVER, 1000, new Vector2(200,400), 4.3f, true);
            playerAnims[JUMPING] = new Animation(playerAnimsImgs[JUMPING], 2, 1, 2, 0, Animation.NO_IDLE, Animation.ANIMATE_FOREVER, 9, new Vector2(200, 400), 4.3f, true);
            playerAnims[CROUCHING] = new Animation(playerAnimsImgs[CROUCHING], 6, 1, 6, 0, Animation.NO_IDLE, Animation.ANIMATE_FOREVER, 9, new Vector2(200, 400), 4.3f, true);
            playerAnims[ATTACKING] = new Animation(playerAnimsImgs[ATTACKING], 12, 1, 12, 0, Animation.NO_IDLE, Animation.ANIMATE_FOREVER, 500, new Vector2(200, 400), 4.3f, true);
            playerAnims[RUNNING_LEFT] = new Animation(playerAnimsImgs[RUNNING_RIGHT], 4,2,8,0, Animation.NO_IDLE, Animation.ANIMATE_FOREVER, 1000, new Vector2(200, 400), 4.3f, true);

            //Set up teleporter animation
            teleportAnim[0] = new Animation(teleporterText,9,1,9,0, Animation.NO_IDLE, Animation.ANIMATE_FOREVER, 1000, new Vector2(9*screenWidth+900, 300), 4.3f, true);
            teleportAnim[1] = new Animation(teleporterText, 9, 1, 9, 0, Animation.NO_IDLE, Animation.ANIMATE_FOREVER, 1000, new Vector2(9 * screenWidth + 900, 300+screenHeight), 4.3f, true);
            teleportAnim[2] = new Animation(teleporterText, 9, 1, 9, 0, Animation.NO_IDLE, Animation.ANIMATE_FOREVER, 1000, new Vector2(9 * screenWidth + 900, 300+screenHeight*2), 4.3f, true);

            //Set up background rectangles
            menuRecs[BACKGROUND] = new Rectangle(0,0,screenWidth,screenHeight);
            menuRecs[START] = new Rectangle(200,600,menuTxt[START].Width/2,menuTxt[START].Height/2);
            menuRecs[HOTH_STATE] = new Rectangle(200,200, menuTxt[HOTH_STATE].Width, menuTxt[HOTH_STATE].Height);
            menuRecs[FORGE_STATE] = new Rectangle(200, 700, menuTxt[FORGE_STATE].Width/4, menuTxt[FORGE_STATE].Height/4);
            menuRecs[BACK] = new Rectangle(1100,700, menuTxt[BACK].Width, menuTxt[BACK].Height);
            menuRecs[START_GAME] = new Rectangle(270, 500, menuTxt[START].Width / 2, menuTxt[START].Height / 2);
            menuRecs[SHOP_BUTTON] = new Rectangle(230,740, menuTxt[SHOP_BUTTON].Width, menuTxt[SHOP_BUTTON].Height);

            //Set up gambling rectangles
            gambleRecs[MYSTERY_BOX] = new Rectangle(700,450, gambleTxt[MYSTERY_BOX].Width*4, gambleTxt[MYSTERY_BOX].Height*4);
            gambleRecs[SPEED] = new Rectangle(250,650, gambleTxt[SPEED].Width, gambleTxt[SPEED].Height);
            gambleRecs[DAMAGE] = new Rectangle(500, 650, gambleTxt[DAMAGE].Width/2, gambleTxt[DAMAGE].Height/2);
            gambleRecs[HEALTH] = new Rectangle(800, 650, gambleTxt[HEALTH].Width, gambleTxt[HEALTH].Height);

            //Set up the master volume of sounds
            SoundEffect.MasterVolume = 1f;

            //Set volume for song, float from 0 → 1
            MediaPlayer.Volume = 0.8f;

            //Set the MediaPlayer to loop the song
            MediaPlayer.IsRepeating = true;

            //Reads the stats
            ReadStats();

            //Creates the player
            player = new Player(lukeTxt, playerAnims, playerHp,playerDmg,playerSpeed,0.5f);

            //Creates the coin
            coin = new Coin(coinText,new Rectangle(100,100,coinText.Width,coinText.Height));

            //Creates camera rectangle
            worldBounds = new Rectangle(0, 0, screenWidth * 10, screenHeight * 3);
            playerRec = player.GetPlayerRec();

            //Sets up the camera
            cam = new Cam2D(GraphicsDevice.Viewport, worldBounds, 1.0f, 4.0f, 0f, playerRec);

            //Play the cantina music
            MediaPlayer.Play(music[CANTINA]);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Set the previous keyboard and mouse states
            prevKb = kb;
            prevMouse = mouse;

            //Sets the keyboard and mouse
            kb = Keyboard.GetState();
            mouse = Mouse.GetState();

            //Updates the gambling timer
            animTimer.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
            fadeTime.Update(gameTime.ElapsedGameTime.TotalMilliseconds);


            //Updates the game based on the gamestate
            switch (gameState) 
            {
                case MENU:
                    //Updates the menu
                    UpdateMenu();
                    break;
                case INSTRUCTIONS:
                    //Plays the easter egg
                    if (MediaPlayer.State != MediaState.Playing && rng.Next(1,5001)== 1000)
                    {
                        MediaPlayer.Play(music[EASTER_EGG]);
                    }
                    //Updates the instructions
                    UpdateInstruction();
                    break;
                case GAME_PLAY:
                    //Plays the battle mustic
                    if(MediaPlayer.State != MediaState.Playing)
                    {
                        MediaPlayer.Play(music[BATTLE]);
                    }
                    //Updates the gameplay
                    UpdateGamePLay(gameTime);
                    break;
                case SHOP:
                    //Updates the shop
                    UpdateShop();
                    break;
                case GAME_OVER:
                    //Plays the game over music
                    if (MediaPlayer.State != MediaState.Playing)
                    {
                        MediaPlayer.Play(music[GAME_OVER_MUSIC]);
                    }
                    //Updates the gameover
                    UpdateGameOver();
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Updates the game over state
        /// </summary>
        public void UpdateGameOver()
        {
            //If space key is pressed or the back button is pressed
            if(kb.IsKeyDown(Keys.Space) || menuRecs[BACK].Contains(mouse.Position) && mouse.LeftButton == ButtonState.Pressed)
            {
                //Stops the music
                MediaPlayer.Stop();

                //Creates a click sound
                sounds[CLICK].CreateInstance().Play();

                //Changes the gamestate to the menu
                gameState = MENU;
            }
        }

        /// <summary>
        /// Updates the insturctions 
        /// </summary>
        public void UpdateInstruction()
        {
            //If the space key is pressed
            if(kb.IsKeyDown(Keys.Space))
            {
                //Sets the gamestate to menu
                gameState = MENU;
            }
        }

        //Pre:NA
        //Post:NA
        //Desc:Writes the stats
        public void WriteStats()
        {
            try
            {
                //Open the stats file
                outFile = File.CreateText("PlayerStats.txt");

                //Store in file
                outFile.WriteLine(coins);
                outFile.WriteLine(playerHp-100);
                outFile.WriteLine(playerDmg-10);
                outFile.WriteLine(playerSpeed-1);


            }
            catch (FileNotFoundException fnfe)
            {
                //Give an error message to the user
                Console.WriteLine("ERROR: " + fnfe.Message);

                //Makes new file
                CreateFile();
            }
            catch (Exception e)
            {
                //Give an error message to the user
                Console.WriteLine("ERROR: " + e.Message);
            }
            finally
            {
                //Close the file
                outFile.Close();
            }

        }

        //Pre:NA
        //Post:NA
        //Desc:Loads in the stats
        private void ReadStats()
        {
            //Stores the data from file
            string data;

            try
            {
                //Opens the file
                inFile = File.OpenText("PlayerStats.txt");

                //Loop through each line in the file
                for (int i = 0; i < 4; i++)
                {
                    //Store each datapoint in file
                    data = inFile.ReadLine();

                    //Store the datapoint into the right variables
                    switch (i)
                    {
                        case 0:
                            coins = Convert.ToInt32(data);
                            break;
                        case 1:
                            playerHp = 100 + Convert.ToInt32(data);
                            break;
                        case 2:
                            playerDmg = 10 + Convert.ToInt32(data);
                            break;
                        case 3:
                            playerSpeed = 1 + (float)(Convert.ToDouble(data));
                            break;
                    }
                }
             }
            catch(FileNotFoundException fnfe)
            {
                //Give an error message to the user
                Console.WriteLine("Error Reading in Stats: " + fnfe);

                //Resets stats
                ResetStats();

                //Makes new file
                CreateFile();
            }
            catch(FormatException fe)
            {
                //Give an error message to the user
                Console.WriteLine("Error Reading in Stats: " + fe);

                //Resets stats
                ResetStats();

                //Deletes then Creates the file again
                File.Delete("PlayerStats.txt");
                CreateFile();
            }
            finally
            {
                //Close the file
                if (inFile != null)
                {
                    inFile.Close();
                }

            }
        }

        /// <summary>
        /// Resets the stats of the player
        /// </summary>
        public void ResetStats()
        {
            //Resets stats
            coins = 0;
            playerHp = 100;
            playerDmg = 10;
            playerSpeed = 1;
        }

        //Pre:NA
        //Post:NA
        //Desc:Creates the stats file if none exist
        public void CreateFile()
        {
            //Creates a new file
            outFile = File.CreateText("PlayerStats.txt");

            //Writes all the player stats as 0
            for (int i = 0; i < 4; i++)
            {
                outFile.WriteLine("0");
            }

            //If the file wasnt null, close the file
            if (outFile != null)
            {
                //Close the file
                outFile.Close();
            }
        }

        /// <summary>
        /// Updates the shop
        /// </summary>
        public void UpdateShop()
        {
            //If the left mouse button is clicked
            if (mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton != ButtonState.Pressed)
            {
                //Return based on where the mouse is contained
                if (menuRecs[BACK].Contains(mouse.Position))
                {
                    //Plays sound instance
                    sounds[CLICK].CreateInstance().Play();
                    
                    //Returns to the menu
                    gameState = MENU;
                }
                else if(gambleRecs[MYSTERY_BOX].Contains(mouse.Position) && animTimer.IsInactive())
                {
                    //Plays sound instance
                    sounds[CLICK].CreateInstance().Play();
                    
                    //Removes a coin from the player
                    coins--;

                    //Gamble rng
                    GambleChance();
                }
            }

            //If the animation timer is active and the fade timer is inactive reset the fade timer
            if(animTimer.IsActive())
            {
                if(fadeTime.IsFinished())
                {
                    fadeTime.ResetTimer(true); 
                }
                //Updates the fading animation
                UpdateAnim();
            }

            //If the animation is finished
            if(animTimer.IsFinished())
            {
                //Resets the timer to false
                animTimer.ResetTimer(false);
            }

        }
        
        /// <summary>
        /// Updates the fading animation for gambling
        /// </summary>
        public void UpdateAnim()
        {
            //Depending on the fade timer, either fade in or out
            if(fadeOut == true)
            {
                fadeVal = (float)(fadeTime.GetTimeRemaining() / 500);
            }
            else
            {
                fadeVal = (float)(fadeTime.GetTimePassed() / 500);
            }

        }

        /// <summary>
        /// Calculates the gamble chance 
        /// </summary>
        public void GambleChance()
        {
            //calculates a random number
            int num = rng.Next(0,101);

            //Depending on the random number, give player a buff or nothing
            if(num <= 90)
            {
                //creates the womp effect
                sounds[WOMP].CreateInstance().Play();
                
                //Changes the gamble state to lost
                gambleState = 0;
            }
            else if(num < 93 && num > 90)
            {
                //Changes the gamble state to speed
                gambleState = 1;

                //Increase player speed
                playerSpeed += 0.1f;
            }
            else if(num < 96 && num >=93)
            {
                //Changes the gamble state to damage
                gambleState = 2;

                //Increase player damage
                playerDmg += 5;
            }
            else
            {
                //Changes the gamble state to health
                gambleState = 3;

                //Increase player hp
                playerHp += 10;
            }

            //Resets the animation
            animTimer.ResetTimer(true);
            animTimer.Activate();

            //Resets the fade
            fadeTime.ResetTimer(true);
            fadeTime.Activate();

            //Write the current stats to stats
            WriteStats();
        }

        /// <summary>
        /// Checks if the enemy has fired or not
        /// </summary>
        /// <param name="gameTime">Gametime is the time passed in the game</param>
        public void CheckEnemyFire(GameTime gameTime)
        {
            //Declare the bullet
            Bullet bullet;

            //Declare the enemy node and set it equal to the head of the linked list
            ENode curENode = levels[curLevel].GetEnemyHead();

            //While the current enemy node exists
            while (curENode != null)
            {
                //Set the bullet to the enemies bullet
                bullet = curENode.GetBullet();

                //Sets the node to the next node
                curENode = curENode.GetNext();

                //If the bullet isnt nothing
                if(bullet != null)
                {
                    //Add the bullet to the list
                    bullets.Add(bullet);
                }
            }

        }


        /// <summary>
        /// Update the game play
        /// </summary>
        /// <param name="gameTime">Gametime is the time passed in the game</param>
        public void UpdateGamePLay(GameTime gameTime)
        {
            //Set the player rectangle
            playerRec = player.GetPlayerRec();

            //Updates the player
            player.Update(gameTime);

            //Checks the collision 
            CheckCollision();

            //Checks if the enemy has fired
            CheckEnemyFire(gameTime);

            //Updates the current level
            levels[curLevel].UpdateLevel(gameTime);

            //Looks at the player
            cam.LookAt(new Point(playerRec.X, (levels[curLevel].GetCurLayer() * screenHeight + screenHeight / 2)));
            
            //Updates the bullets
            UpdateBullets(gameTime);

            //Kill the player if they are dead
            if (player.GetHp() <= 0)
            {
                //Stop the player
                MediaPlayer.Stop();

                //Sets the gamestate to game over
                gameState = GAME_OVER;
            }

            
        }

        /// <summary>
        /// Updates the bullets
        /// </summary>
        /// <param name="gameTime">Gametime is the time passed in the game</param>
        public void UpdateBullets(GameTime gameTime)
        {
            //Loop through the bullets
            for (int i = 0; i < bullets.Count; i++)
            {
                //Updates the bullets
                bullets[i].Update(gameTime);

                //If the bullet is offscreen
                if(bullets[i].GetXPos() < 0 || bullets[i].GetXPos() >= (10 * screenWidth) )
                {
                    //Removes the bullet
                    bullets.RemoveAt(i);
                }
            }

        }

        /// <summary>
        /// Updates the menu
        /// </summary>
        public void UpdateMenu()
        {
            //If the left button is pressed
             if(mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton != ButtonState.Pressed)
             {
                //Based on what is intersected, do an action
                if (menuRecs[START].Contains(mouse.Position) && isLevelSelect == false)
                {
                    //Sets level select to true
                    isLevelSelect = true;

                    //Creates and plays sound effect
                    sounds[CLICK].CreateInstance().Play();
                }
                else if (menuRecs[HOTH_STATE].Contains(mouse.Position) && isLevelSelect == true)
                {
                    //Sets the current level to hoth
                    curLevel = HOTH;

                    //Creates and plays sound effect
                    sounds[CLICK].CreateInstance().Play();
                }
                else if (menuRecs[FORGE_STATE].Contains(mouse.Position) && isLevelSelect == true)
                {
                    //Sets the current level to the star forge
                    curLevel = STAR_FORGE;

                    //Creates and plays sound effect
                    sounds[CLICK].CreateInstance().Play();
                }
                else if (menuRecs[BACK].Contains(mouse.Position) && isLevelSelect == true)
                {
                    //Sets the level select to false
                    isLevelSelect = false;

                    //Creates and plays sound effect
                    sounds[CLICK].CreateInstance().Play();
                }
                else if(menuRecs[START_GAME].Contains(mouse.Position) && isLevelSelect == true)
                {
                    //Turns of the music
                    MediaPlayer.Stop();

                    //Based on the current level, create the level
                    if(curLevel == HOTH)
                    {
                        //Creates the level
                        levels[HOTH] = new Level(hothTextures, new Rectangle[3, 10], new byte[3, 10]);

                        //Creates the player
                        player = new Player(lukeTxt, playerAnims, playerHp, playerDmg, playerSpeed,0.5f);
                    }
                    else
                    {
                        //Creates the level
                        levels[STAR_FORGE] = new Level(new Texture2D[] { starForgeImg, starForgeImg, starForgeImg, starForgeImg, starForgeImg}, new Rectangle[3, 10], new byte[3, 10]);
                        
                        //Creates the player
                        player = new Player(lukeTxt, playerAnims, playerHp, playerDmg, playerSpeed, 0.4f);
                    }

                    //Set the gamestate to game play
                    gameState = GAME_PLAY;
                }
                else if(menuRecs[SHOP_BUTTON].Contains(mouse.Position) && isLevelSelect == false)
                {
                    //Creates and plays sound effect
                    sounds[CLICK].CreateInstance().Play();
                    
                    //Sets the gamestate to shop
                    gameState = SHOP;
                }
                else if(instructionRecs[INSTRUCTIONS_BUTTON].Contains(mouse.Position))
                {
                    MediaPlayer.Stop();

                    //Creates and plays sound effect
                    sounds[CLICK].CreateInstance().Play();

                    //Sets the gamestate to the instructions
                    gameState = INSTRUCTIONS;
                }

             }
        }

        /// <summary>
        /// Checks the collision
        /// </summary>
        public void CheckCollision()
        {
            //Gets the list of platforms
            List<Platform> platforms = levels[curLevel].GetPlatforms();

            //Gets the players jump rectangle
            Rectangle playerJumpBottom = player.GetJumpRec();
           
            //Looks at the first rectangle
            if (playerJumpBottom.Intersects(new Rectangle(platforms[0].GetRectangle().X, platforms[0].GetRectangle().Top-100, platforms[0].GetRectangle().Width, 1)) && playerRec.Intersects(new Rectangle(platforms[0].GetRectangle().X, platforms[0].GetRectangle().Top-100, platforms[0].GetRectangle().Width - 30, 1)))
            {
                //Sets jump to false 
                player.SetIsJump(false);

                //Sets the players height
                player.SetHeight(platforms[0].GetRectangle().Top - 100 - playerRec.Height);
            }
            else
            {
                //Sets jump to true
                player.SetIsJump(true);
            }

            //Loops through the list of platforms
            for (int i = 1; i < platforms.Count; i++)
            {
                //Sets the players jump
                Rectangle playerJump = player.GetJumpRec();

                //Checks if the player intersected with the top of the platform
                if (playerJump.Intersects(new Rectangle(platforms[i].GetRectangle().X, platforms[i].GetRectangle().Top, platforms[i].GetRectangle().Width-30, 1))&& playerRec.Intersects(new Rectangle(platforms[i].GetRectangle().X, platforms[i].GetRectangle().Top, platforms[i].GetRectangle().Width-30, 1)))
                {
                    //Set the player jump to false
                    player.SetIsJump(false);

                    //Sets the height of the player
                    player.SetHeight(platforms[i].GetRectangle().Top - playerRec.Height);
                    break;
                }

            }

            //Sets the teleporters 
            Teleporter[] teleporters = new Teleporter[3];
            teleporters = levels[curLevel].GetTeleporter();

            //Loops through the teleporters
            for(int i = 0; i < teleporters.Length; i++)
            {
                //Sets the number and value
                int num = i+1;

                //Checks if the teleporter is with the player
                if(teleporters[i].GetRectangle().Intersects(player.GetPlayerRec()))
                {
                    //Checks if z or x was pressed
                    if(kb.IsKeyDown(Keys.Z)&&prevKb.IsKeyDown(Keys.Z) && levels[curLevel].GetCurLayer() != 2)
                    {

                        //Sets the current layer
                        levels[curLevel].SetCurLayer(num);

                        //Sets the players rectangle
                        player.SetRec(teleporters[i].GetSpawn());

                        //Adds enemies to the linked list
                        levels[curLevel].AddEnemyToLList();
                    }
                    else if(kb.IsKeyDown(Keys.X) && prevKb.IsKeyDown(Keys.X))
                    {
                        //Changes the game state to menu
                        gameState = MENU;

                        //Writes the stats 
                        WriteStats();
                    }
                    
                    break;
                }
            }

            //Sets the current node to the head
            CNode curNode = levels[curLevel].GetCoinHead();

            //Sets the count to 0
            int count = 0;

            //While the current node exists
            while (curNode != null)
            {
                //If the current node intersects with the player rectangle
                if(curNode.GetRec().Intersects(playerRec))
                {
                    //Creates an instance of the money sound
                    sounds[MONEY].CreateInstance().Play();

                    //Removes the coin
                    levels[curLevel].RemoveAt(count);
                    
                    //Adds another coin
                    coins++;
                    break;
                }
                    //increases the count
                    count++;

                //Sets the node to the next node
                curNode = curNode.GetNext();
            }

            //Sets the current node to the head
            ENode curENode = levels[curLevel].GetEnemyHead();

            //Sets the count to 0
            int countE = 0;

            //While the current node exists
            while (curENode != null)
            {
                //If the rey cast intersects with the player
                if (curENode.GetRayCast().Intersects(playerRec))
                {
                    //Sets the enemy to seeing the player
                    levels[curLevel].CanSeePlayer(countE,true);
                    
                    //Increases the count 
                    countE++;
                }
                else
                {
                    //Sets the enemy to seeing the player
                    levels[curLevel].CanSeePlayer(countE,false);

                    //Increases the count
                    countE++;
                }

                //Gets the next node
                curENode = curENode.GetNext();
            }

            //Checks enemy collision and enemy being dead
            EnemyCollision();
            EnemyDead();

            //Gets the chests from the level
            Chest[] chests = levels[curLevel].GetChests();
            
            //If the player tries to heal
            if(kb.IsKeyDown(Keys.F) && !prevKb.IsKeyDown(Keys.F))
            {
                //Loop through the chests
                for(int i = 0; i < chests.Length; i++)
                {
                    //If the chest intersects with the player
                    if(chests[i].GetRectangle().Intersects(playerRec))
                    {
                        //Heal the player
                        player.SetHealing(chests[i].GetHp());

                        //Kills the chest
                        levels[curLevel].SetChestHealth(i);
                    }

                }
            }
            
            //Loops through the bullets
            for (int i = 0; i < bullets.Count; i++)
            {
                //Checks if the bullets intersect with the player
                if(bullets[i].GetRectangle().Intersects(playerRec))
                {
                    //Remove the bullet
                    bullets.RemoveAt(i);

                    //Remove player health
                    player.SetHealth(5);
                }
            }


        }

        /// <summary>
        /// This is called every update and checks if the enemy is dead
        /// </summary>
        public void EnemyDead()
        {
            //Sets the current node
            ENode curENode = levels[curLevel].GetEnemyHead();

            //Sets the count to 0
            int countE = 0;

            //While the node exists
            while (curENode != null)
            {
                //If the enemy is dead
                if (curENode.GetHp() <= 0)
                {

                    //if the enemy is a probe droid
                    if(curENode.GetEnemy() is ProbeDroid)
                    {
                        //Add a coin to the list
                        ProbeDroid droid = (ProbeDroid)curENode.GetEnemy();
                        levels[curLevel].AddToCoinLList(droid.GetX(), droid.GetY());
                    }

                    //Remove the enemy
                    levels[curLevel].RemoveAtEnemy(countE);

                    //Plays the screaming sound
                    sounds[SCREAM].CreateInstance().Play();
                    
                    //Increases the count
                    countE++;
                }
                else
                {
                    //Increases the count
                    countE++;
                }

                //Sets the node to the next node
                curENode = curENode.GetNext();
            }

        }

        /// <summary>
        /// Checks the enemy's collision
        /// </summary>
        public void EnemyCollision()
        {
            //Sets the current node to the head
            ENode curENode = levels[curLevel].GetEnemyHead();
            
            //Sets the count to 0
            int countE = 0;

            //While the enemy isnt null
            while (curENode != null)
            {
                //If the enemy is being attacked by the player
                if (curENode.GetRec().Intersects(playerRec) && player.GetAttack() == true && player.GetAnim().GetCurFrame() == 6)
                {
                    //Sets the enemy health
                    levels[curLevel].SetHealth(countE,player.GetDamage());
                    
                    //Increases the count
                    countE++;
                }
                else
                {
                    //Increases the count
                    countE++;
                }

                //Sets the enemy to the next enemy
                curENode = curENode.GetNext();
            }
        }



        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Switches the gamestate 
            switch(gameState)
            {
                case MENU:
                    spriteBatch.Begin();

                    //If the level select is false
                    if(isLevelSelect == false)
                    {
                        //Draw the menu
                        spriteBatch.Draw(menuTxt[BACKGROUND], menuRecs[BACKGROUND], Color.White);
                        spriteBatch.Draw(menuTxt[START], menuRecs[START], Color.White);
                        spriteBatch.Draw(menuTxt[SHOP_BUTTON], menuRecs[SHOP_BUTTON],Color.White);
                        spriteBatch.Draw(instructionImgs[INSTRUCTIONS_BUTTON],instructionRecs[INSTRUCTIONS_BUTTON],Color.White);
                    }
                    else
                    {
                        //Draw the level select
                        spriteBatch.Draw(menuTxt[BACKGROUND], menuRecs[BACKGROUND], Color.White);
                        spriteBatch.Draw(menuTxt[FORGE_STATE], menuRecs[FORGE_STATE], Color.White);
                        spriteBatch.Draw(menuTxt[HOTH_STATE], menuRecs[HOTH_STATE], Color.White);
                        spriteBatch.Draw(menuTxt[BACK], menuRecs[BACK], Color.White);
                        spriteBatch.Draw(menuTxt[START], menuRecs[START_GAME], Color.White);
                    }

                    spriteBatch.End();

                    break;
                case INSTRUCTIONS:
                    spriteBatch.Begin();
                    //Draw the instruction
                    spriteBatch.Draw(instructionImgs[INSTRUCTIONS_IMAGE],instructionRecs[INSTRUCTIONS_IMAGE],Color.White);

                    spriteBatch.End();
                    break;
                case GAME_PLAY:
                    //Draw the level and player
                    spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, cam.GetTransformation());
                    levels[curLevel].Draw(spriteBatch);

                    if(player.GetHp() < 20)
                    {
                        player.Draw(spriteBatch,Color.Red);
                    }
                    else
                    {
                        player.Draw(spriteBatch,Color.White);
                    }
                    
                    //Draw the bullets
                    for(int i = 0; i < bullets.Count; i++)
                    {
                        bullets[i].Draw(spriteBatch);
                    }
                    spriteBatch.End();
                    break;

                case SHOP:
                    spriteBatch.Begin();

                    //Draw the shop
                    spriteBatch.Draw(shopImg, menuRecs[BACKGROUND], Color.White);

                    //Draw the back button
                    spriteBatch.Draw(menuTxt[BACK], menuRecs[BACK], Color.White);

                    //Draw the coin
                    coin.Draw(spriteBatch);

                    //Draw the number of coins
                    spriteBatch.DrawString(font,coins.ToString(),new Vector2(200,350),Color.Red);

                    //If the animation timer is active
                    if(animTimer.IsActive())
                    {
                        //Switches the gamble states
                        switch(gambleState)
                        {
                            case 0:
                                //SOUND PLAYS
                                break;
                            case 1:
                                //Plays sound effect
                                sounds[YAYYY].CreateInstance().Play();

                                //Draw the speed
                                spriteBatch.Draw(gambleTxt[SPEED], gambleRecs[SPEED], Color.Green*fadeVal);
                                break;
                            case 2:
                                //Plays sound effect
                                sounds[YAYYY].CreateInstance().Play();

                                //Draw the damage
                                spriteBatch.Draw(gambleTxt[DAMAGE], gambleRecs[DAMAGE], Color.Green*fadeVal);
                                break;
                            case 3:
                                //Plays sound effect
                                sounds[YAYYY].CreateInstance().Play();

                                //Draw the health
                                spriteBatch.Draw(gambleTxt[HEALTH], gambleRecs[HEALTH], Color.Green*fadeVal);
                                break;
                        }
                    }
                    else
                    {
                        //Draws the mystery box, speed, damage and health
                        spriteBatch.Draw(gambleTxt[MYSTERY_BOX], gambleRecs[MYSTERY_BOX], Color.White);
                        spriteBatch.Draw(gambleTxt[SPEED], gambleRecs[SPEED], Color.White);
                        spriteBatch.Draw(gambleTxt[DAMAGE], gambleRecs[DAMAGE], Color.White);
                        spriteBatch.Draw(gambleTxt[HEALTH], gambleRecs[HEALTH], Color.White);
                    }
                    
                    
                    spriteBatch.End();
                    break;
                case GAME_OVER:
                    //Draw the game over images
                    spriteBatch.Draw(shopImg, menuRecs[BACKGROUND], Color.White);
                    spriteBatch.Draw(menuTxt[BACK], menuRecs[BACK], Color.White);
                    break;
            }

            base.Draw(gameTime);
        }
    }
}
