//Author:Noah Segal
//File Name:Player.cs
//Project Name:PASS3
//Creation Date:2024-05-07
//Modification Date:2024-06-09
//Description:The player class that is controlled by the user
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
    class Player
    {
        //Declare movement constants
        private const int RIGHT = 0;
        private const int LEFT = 1;
        private const int RIGHT_MOVE = 2;
        private const int LEFT_MOVE = 3;
        private const int JUMP_RIGHT = 4;
        private const int JUMP_LEFT = 6;
        private const int ATTACK_RIGHT = 5;
        private const int ATTACK_LEFT = 7;
        private const int RESET = 0;

        //Declare lightsaber constant
        private const int LIGHTSABER = 0;

        //Declare the max speed
        private const float MAX_SPEED = 20f;

        //Declare the luke textures and animations
        private Texture2D[] lukeTxt;
        private Animation[] playerAnims;

        //Declare the player position
        private Vector2 playerPos = new Vector2(200,680);
        
        //Declare the x and y speed
        private float speedX = 0;
        private float speedY = 0;

        //Declare the player rectangle
        private Rectangle playerRec;

        //Declare the current image
        private int curImg = 0;

        //Declare if the player is left and jumped
        private bool isLeft = false;
        private bool isJump = false;

        //Declare the player stats
        private int health;
        private int maxHealth;
        private int damage;
        private float speed;

        //Declare if the player is attacking
        private bool isAttack = false;

        //Declare the gravity of the player
        private float gravity;

        /// <summary>
        /// Creates a copy of the player
        /// </summary>
        /// <param name="lukeTxt">Is the luke textures</param>
        /// <param name="playerAnims">Is the luke animations</param>
        /// <param name="health">Is the player health</param>
        /// <param name="damage">Is the player damage</param>
        /// <param name="speed">Is the player speed</param>
        /// <param name="gravity">Is the player gravity</param>
        public Player(Texture2D[] lukeTxt, Animation[] playerAnims, int health, int damage,float speed, float gravity)
        {
            //Sets the textures and animations
            this.lukeTxt = lukeTxt;
            this.playerAnims = playerAnims;

            //Sets the players image
            this.health = health;
            this.damage = damage;
            this.speed = speed;
            this.gravity = gravity;

            //Sets the players rectangle
            playerRec = new Rectangle((int)playerPos.X,(int)playerPos.Y,lukeTxt[0].Width, lukeTxt[0].Height+10);

            //Sets the max player health
            maxHealth = health;

            //Translates all the animations to the player
            for(int i = 0; i <playerAnims.Length;i++)
            {
                playerAnims[i].TranslateTo(playerRec.X,playerRec.Y);
            }
        }

        /// <summary>
        /// Updates the players
        /// </summary>
        /// <param name="gameTime">Is the games time</param>
        public void Update(GameTime gameTime)
        {
            //Checks which button is pressed
            if (Game1.kb.IsKeyDown(Keys.D) && isJump == false && isAttack == false)
            {
                //Makes the player run to the right
                speedY = RESET;
                playerAnims[0].Update(gameTime);
                curImg = RIGHT_MOVE;
                isLeft = false;
                speedX = 8*speed;
                playerRec.X += (int)speedX;
                playerAnims[0].TranslateTo(playerRec.X, playerRec.Y - 100);

            }
            else if (Game1.kb.IsKeyDown(Keys.A) && isJump == false && isAttack == false)
            {
                //Makes the player run to the left
                speedY = RESET;
                playerAnims[4].Update(gameTime);
                curImg = LEFT_MOVE;
                isLeft = true;
                speedX = -8*speed;
                playerRec.X += (int)speedX;
                playerAnims[4].TranslateTo(playerRec.X, playerRec.Y - 100);
            }
            else if(Game1.mouse.LeftButton == ButtonState.Pressed && Game1.prevMouse.LeftButton != ButtonState.Pressed && isJump == false && isAttack == false)
            {
                //Plays the lightsaber sound
                Game1.sounds[LIGHTSABER].CreateInstance().Play();
                
                //Sets attacking to true
                isAttack = true;

                //Sets either attack right or left
                if(curImg == RIGHT || curImg == RIGHT_MOVE)
                {
                    curImg = ATTACK_RIGHT;
                }
                else
                {
                    curImg = ATTACK_LEFT;
                }

                //Sets the reset frame
                playerAnims[3].SetFrame(RESET);
            }
            else
            {
                //Checks if the player is right or left
                if(isLeft == true && isJump == false && isAttack == false)
                {
                    //Resets the speed and sets image to left
                    speedY = RESET;
                    curImg = LEFT;
                    speedX = RESET;
                }
                else if(isJump == false && isAttack == false)
                {
                    //Resets the speed and sets image to right
                    speedY = RESET;
                    speedX = RESET;
                    curImg = RIGHT;
                }
            }

            //Sets attack to false if the animation is done
            if(isAttack == true)
            {
                if(playerAnims[3].GetCurFrame() == 11)
                {
                    isAttack = false;
                }
            }

            
            //Updates the player animation
            playerAnims[3].Update(gameTime);
            playerAnims[3].TranslateTo(playerRec.X-60,playerRec.Y-80);

            //if space key pressed and not currently jumping
            if (Game1.kb.IsKeyDown(Keys.Space) && !(Game1.prevKb.IsKeyDown(Keys.Space)) && isJump == false && isAttack == false)
            {
                //Jump
                speedY = 20;
                isJump = true;

            }
            
            //If the player has jumped
            if(isJump == true)
            {
                //Update the animation
                playerAnims[1].Update(gameTime);

                //Set the current image to right or left
                if (curImg == RIGHT || curImg == RIGHT_MOVE || curImg == JUMP_RIGHT)
                {
                    curImg = JUMP_RIGHT;
                }
                else
                {
                    curImg = JUMP_LEFT;
                }

                //Changes the y speed
                speedY -= gravity;
                MathHelper.Clamp(speedY,-MAX_SPEED,MAX_SPEED);
                
                //Changes the x and y position
                playerRec.Y -= (int)speedY;
                playerRec.X += (int)speedX;

                //Translates the player
                playerAnims[1].TranslateTo(playerRec.X, playerRec.Y - 100);
            }

            //Moves the player if they go off screen
            if (playerRec.X <= 10)
            {
                playerRec.X = 10;
            }
            else if(playerRec.X >= 10*Game1.screenWidth-10)
            {
                playerRec.X = 10 * Game1.screenWidth - 10;
            }

        }

        /// <summary>
        /// Gets the players damage
        /// </summary>
        /// <returns>Returns the players damage</returns>
        public int GetDamage()
        {
            //Returns the damage
            return damage;
        }

        /// <summary>
        /// Gets the players animation
        /// </summary>
        /// <returns>Returns the player animation</returns>
        public Animation GetAnim()
        {
            //Returns the player animation
            return playerAnims[3];
        }

        /// <summary>
        /// Gets if the player is attacking 
        /// </summary>
        /// <returns>Returns if the player is currently attacking</returns>
        public bool GetAttack()
        {
            //Returns if the player is attacking 
            return isAttack;
        }

        /// <summary>
        /// Gets the player jump
        /// </summary>
        /// <returns>Returns if the player is currently jumping </returns>
        public Rectangle GetJumpRec()
        {
            //Returns the jump rectangle
            return playerAnims[1].GetDestRec();
        }

        /// <summary>
        /// Sets the player jump
        /// </summary>
        /// <param name="jump">Sets if the player is currently jumping or not</param>
        public void SetIsJump(bool jump)
        {
            //Set if the player is jumping
            isJump = jump;
        }

        /// <summary>
        /// Sets the players height
        /// </summary>
        /// <param name="height">Sets the players height</param>
        public void SetHeight(double height)
        {
            //Sets the players rec
            playerRec.Y = (int)height+70;
        }

        /// <summary>
        /// Sets the players rectangle
        /// </summary>
        /// <param name="pos">Sets the players point</param>
        public void SetRec(Point pos)
        {
            //Sets the players point
            playerRec.X = pos.X;
            playerRec.Y = pos.Y;
        }

        /// <summary>
        /// Gets the players rectangle
        /// </summary>
        /// <returns>Returns the players rectangle</returns>
        public Rectangle GetPlayerRec()
        {
            //Returns the players rectangle
            return playerRec;
        }

        /// <summary>
        /// Gets the players hp
        /// </summary>
        /// <returns>Returns the players hp</returns>
        public int GetHp()
        {
            //Returns the hp
            return health;
        }

        /// <summary>
        /// Sets the players hp
        /// </summary>
        /// <param name="hp">Sets the hp</param>
        public void SetHealth(int hp)
        {
            //If the player isnt attacking
            if(isAttack == false)
            {
                //Sets the hp
               health -= hp;
            }
           
        }

        /// <summary>
        /// Sets the players healing 
        /// </summary>
        /// <param name="hp">Sets the hp</param>
        public void SetHealing(int hp)
        {
            //Sets the players health
            if(health+hp > maxHealth)
            {
                health = maxHealth;
            }
            else
            {
                health += hp;
            }
           
        }

        /// <summary>
        /// Draws the player
        /// </summary>
        /// <param name="spriteBatch">Is used to draw the image</param>
        /// <param name="color">Is the color of the player</param>
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            //Switches the players image
            switch(curImg)
            {
                //Draws the player based on the current image
                case RIGHT:
                    spriteBatch.Draw(lukeTxt[RIGHT],playerRec,color);
                    break;
                case LEFT:
                    spriteBatch.Draw(lukeTxt[LEFT],playerRec,color);
                    break;
                case RIGHT_MOVE:
                    playerAnims[0].Draw(spriteBatch,color);
                    break;
                case LEFT_MOVE:
                    playerAnims[4].DrawRotated(spriteBatch,color,SpriteEffects.FlipHorizontally);
                    break;
                case JUMP_RIGHT:
                    playerAnims[1].Draw(spriteBatch,color);
                    break;
                case ATTACK_RIGHT:
                    playerAnims[3].Draw(spriteBatch, color);
                    break;
                case JUMP_LEFT:
                    playerAnims[1].DrawRotated(spriteBatch, color,SpriteEffects.FlipHorizontally);
                    break;
                case ATTACK_LEFT:
                    playerAnims[3].DrawRotated(spriteBatch, color,SpriteEffects.FlipHorizontally);
                    break;
            }

        }
    }
}
