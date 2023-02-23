using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GXPEngine;
using System.Deployment.Internal;
using System.Reflection;

public class Enemy : Sprite
{
    string startingCorner;
    public Boolean hitTheHitBox = false;
    int keyToBePressed;
    float currentBeat = 0;
    AnimationSprite gfx;

    public Enemy(String startingCornerIn, string filename) : base ("enemyHitbox.png")
    {
        currentBeat = BeatHandler.GetBeatFloat();
        alpha = 0;
        SetOrigin(this.width / 2, this.height / 2);
        SetColor(255, 0, 0);
        startingCorner = startingCornerIn;

        switch (startingCorner)
        {
            case "Top_Left":
                x = 0;
                y = 384;
                keyToBePressed = Key.Q;
                break;
            case "Top_Right":
                x = game.width;
                y = 384;
                keyToBePressed = Key.E;
                break;
            case "Bottom_Left":
                x = 0;
                y = game.height;
                keyToBePressed = Key.A;
                break;
            case "Bottom_Right":
                x = game.width;
                y = game.height;
                keyToBePressed = Key.D;
                break;
        }

        gfx = new AnimationSprite(filename, 4, 1);
        gfx.SetOrigin(gfx.width * 0.5f, gfx.height - 25);
        gfx.scale = 0.75f;
        AddChild(gfx);
    }

    void Update()
    {
        Move();
        CheckIfMissed();
        gfx.Animate(0.05f);
    }

    public void CheckHitOrMiss(int keyPressed)
    {
        if (hitTheHitBox && keyPressed == keyToBePressed)
        {
            Hit();
        }
        else
        {
            Miss();
        }
    }

    void Hit()
    {
        MyGame myGame = (MyGame)game;

        // Update lives:
        myGame.lives++;

        if (myGame.lives > 15)
        {
            myGame.lives = 15;
        }
        HUD.UpdateLives(myGame.lives);

        // Update combo
        myGame.combo++;
        HUD.UpdateCombo(myGame.combo);

        // Calculate score with combo in mind:
        float offset = 0f;

        if (this.x < myGame.width / 2)
        {
            if (this.x > MainMenu.squaresXLeft)
            {
                offset = this.x - MainMenu.squaresXLeft;
            }

            else if (this.x < MainMenu.squaresXLeft)
            {
                offset = MainMenu.squaresXLeft - this.x;
            }
        }

        if (this.x > myGame.width / 2)
        {
            if (this.x > MainMenu.squaresXRight)
            {
                offset = this.x - MainMenu.squaresXRight;
            }

            else if (this.x < MainMenu.squaresXRight)
            {
                offset = MainMenu.squaresXRight - this.x;
            }
        }

        myGame.score += Mathf.Abs((int)((1000f - offset * 15f) * (float)((myGame.combo * 0.01f + 1f))));
        HUD.UpdateScore(myGame.score);

        // Remove enemy:
        myGame.enemies.Remove(this);
        Destroy();

        // Add Particle:
        Particle newParticle = new Particle("hit!.png", BlendMode.NORMAL, 500);
        newParticle.SetColor(Color.White, Color.White).
        SetScale(1, 1).
        SetVelocity(0, -2);
        newParticle.SetXY(game.width / 2, game.height / 2 - 100);
        myGame.layer0.LateAddChild(newParticle);

        // Create lightning:
        if (Input.GetKeyDown(Key.Q))
        {
            myGame.CreateLightning(this.x, this.y);
        }

        if (Input.GetKeyDown(Key.E))
        {
            myGame.CreateLightning(this.x, this.y);
        }

        if (Input.GetKeyDown(Key.A))
        {
            myGame.CreateLightning(this.x, this.y);
        }

        if (Input.GetKeyDown(Key.D))
        {
            myGame.CreateLightning(this.x, this.y);
        }
    }

    void Miss()
    {
        // Update lives and combo:
        MyGame myGame = (MyGame)game;
        myGame.lives--;
        myGame.combo = 0;
        HUD.UpdateCombo(myGame.combo);
        HUD.UpdateLives(myGame.lives);

        // Add Particle
        Particle newParticle = new Particle("miss!.png", BlendMode.NORMAL, 500);
        newParticle.SetColor(Color.White, Color.White).
        SetScale(1, 1).
        SetVelocity(0, -2);
        newParticle.SetXY(game.width / 2, game.height / 2 - 100);
        myGame.layer0.LateAddChild(newParticle);
    }

    void Move()
    {
        MyGame myGame = (MyGame)game;
        switch (startingCorner)
        {
            case "Top_Left":
                x = (BeatHandler.GetBeatFloat() - currentBeat) * MainMenu.squaresXLeft / 4f;
                break;
            case "Top_Right":
                x = myGame.width - ((BeatHandler.GetBeatFloat() - currentBeat) * MainMenu.squaresXLeft / 4f);
                break;
            case "Bottom_Left":
                x = (BeatHandler.GetBeatFloat() - currentBeat) * MainMenu.squaresXLeft / 4f;
                y = myGame.height - ((BeatHandler.GetBeatFloat() - currentBeat) * (myGame.height - MainMenu.squaresYDown) / 4f);
                break;
            case "Bottom_Right":
                x = myGame.width - ((BeatHandler.GetBeatFloat() - currentBeat) * MainMenu.squaresXLeft / 4f);
                y = myGame.height - ((BeatHandler.GetBeatFloat() - currentBeat) * (myGame.height - MainMenu.squaresYDown) / 4f);
                break;
        }
    }

    void CheckIfMissed()
    {
        if (hitTheHitBox)
        {
            Boolean isHittingBox = false;

            // check if enemy is hitting drum:
            GameObject[] cols = GetCollisions();
            foreach (GameObject other in cols)
            {
                if (other is HitBox)
                {
                    isHittingBox = true;
                }
            }

            // Make the player miss if enemy is not touching anymore:
            if (!isHittingBox)
            {
                Miss();
                MyGame myGame = (MyGame)game;
                myGame.enemies.Remove(this);
                Destroy();
            }
        }
    }

    void OnCollision(GameObject other)
    {
        if (other is HitBox && !hitTheHitBox)
        {
            hitTheHitBox = true;
        }
    }
}