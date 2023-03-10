using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Diagnostics;
using System.Linq;
using System.Xml.Schema;
using System.Threading;

public class MyGame : Game
{
    public SoundChannel channel;
    public float lives = 15;
    public int score = 0;
    public int combo = 0;
    public List<Enemy> enemies = new List<Enemy>();
    public float bpm;
    public string level;
    public float msPerBeat;
    public int msToMove;
    public Boolean isPlaying = false;
    public Boolean completedLevel = false;
    public Pivot screenshake;

    public int currentBeat = 0;
    public int lastBeat = 0;

    public Pivot layer0;
    public Pivot layer1;
    public Pivot layer2;

    Sprite background;
    public Boolean isShaking = false;
    public int timeRan = 0;
    int lifeTime = 200;

    public MyGame() : base(1366, 768, false)
    {
        targetFps = 60;
        MakeMainMenu();
        Freaky.ReadLevel();
        DontCare.ReadLevel();
        Atomic.ReadLevel();
    }

    void Update()
    {
        if (isPlaying)
        {
            Beats();
            LoseGame();
            DrumInput();

            if (isShaking)
            {
                timeRan += Time.deltaTime;
                screenshake.x *= (Mathf.Pow(-1, timeRan));
                screenshake.y *= (Mathf.Pow(-1, timeRan));

                if (timeRan >= lifeTime)
                {
                    timeRan = 0;
                    isShaking = false;
                    screenshake.x = 0;
                    screenshake.y = 0;
                }
            }
        }
    }

    public void StartGame()
    {
        background = new Sprite("background.png");
        background.SetOrigin(0, 0);
        layer0.AddChild(background);
    }

    void CreateEnemy()
    {
        switch (level)
        {
            case "DontCare":
                DontCare.GetEnemyPosition();

                if (DontCare.currentValue > 0)
                {
                    Enemy enemy;
                    switch (DontCare.startingCorner)
                    {
                        case "Top_Left":
                            enemy = new Enemy(DontCare.startingCorner, "Enemy_TopL.png");
                            layer1.AddChild(enemy);
                            enemies.Add(enemy);
                            break;
                        case "Top_Right":
                            enemy = new Enemy(DontCare.startingCorner, "Enemy_TopR.png");
                            layer1.AddChild(enemy);
                            enemies.Add(enemy);
                            break;
                        case "Bottom_Left":
                            enemy = new Enemy(DontCare.startingCorner, "Enemy_BotL.png");
                            layer2.AddChild(enemy);
                            enemies.Add(enemy);
                            break;
                        case "Bottom_Right":
                            enemy = new Enemy(DontCare.startingCorner, "Enemy_BotR.png");
                            layer2.AddChild(enemy);
                            enemies.Add(enemy);
                            break;
                    }
                }

                // End Song go back to menu:
                if (BeatHandler.GetBeat() >= 420)
                {
                    completedLevel = true;

                    for (int i = enemies.Count - 1; i >= 0; i--)
                    {
                        enemies.Remove(enemies[i]);
                    }

                    // Remove enemies:
                    List<GameObject> children = GetChildren();
                    foreach (GameObject child in children)
                    {
                        if (child is Enemy)
                        {
                            Enemy enemy = (Enemy)child;
                            enemies.Remove(enemy);
                            enemy.Remove();
                        }
                    }

                    DestroyChildren();
                    if (channel.IsPlaying) channel.Stop();
                    MainMenu mainMenu = new MainMenu();
                    mainMenu.AfterMenu();
                    AddChild(mainMenu);
                }
                break;
            
            case "Freaky":
                Freaky.GetEnemyPosition();

                if (Freaky.currentValue > 0)
                {
                    Enemy enemy;
                    switch (Freaky.startingCorner)
                    {
                        case "Top_Left":
                            enemy = new Enemy(Freaky.startingCorner, "Enemy_TopL.png");
                            layer1.AddChild(enemy);
                            enemies.Add(enemy);
                            break;
                        case "Top_Right":
                            enemy = new Enemy(Freaky.startingCorner, "Enemy_TopR.png");
                            layer1.AddChild(enemy);
                            enemies.Add(enemy);
                            break;
                        case "Bottom_Left":
                            enemy = new Enemy(Freaky.startingCorner, "Enemy_BotL.png");
                            layer2.AddChild(enemy);
                            enemies.Add(enemy);
                            break;
                        case "Bottom_Right":
                            enemy = new Enemy(Freaky.startingCorner, "Enemy_BotR.png");
                            layer2.AddChild(enemy);
                            enemies.Add(enemy);
                            break;
                    }
                }

                // End Song go back to menu:
                if (BeatHandler.GetBeat() >= 711)
                {
                    completedLevel = true;

                    for (int i = enemies.Count - 1; i >= 0; i--)
                    {
                        enemies.Remove(enemies[i]);
                    }

                    // Remove enemies:
                    List<GameObject> children = GetChildren();
                    foreach (GameObject child in children)
                    {
                        if (child is Enemy)
                        {
                            Enemy enemy = (Enemy)child;
                            enemies.Remove(enemy);
                            enemy.Remove();
                        }
                    }

                    DestroyChildren();
                    if (channel.IsPlaying) channel.Stop();
                    MainMenu mainMenu = new MainMenu();
                    mainMenu.AfterMenu();
                    AddChild(mainMenu);
                }
                break;
            
            case "Atomic":
                Atomic.GetEnemyPosition();
                
                if (Atomic.currentValue > 0)
                {
                    Enemy enemy;
                    switch (Atomic.startingCorner)
                    {
                        case "Top_Left":
                            enemy = new Enemy(Atomic.startingCorner, "Enemy_TopL.png");
                            layer1.AddChild(enemy);
                            enemies.Add(enemy);
                            break;
                        case "Top_Right":
                            enemy = new Enemy(Atomic.startingCorner, "Enemy_TopR.png");
                            layer1.AddChild(enemy);
                            enemies.Add(enemy);
                            break;
                        case "Bottom_Left":
                            enemy = new Enemy(Atomic.startingCorner, "Enemy_BotL.png");
                            layer2.AddChild(enemy);
                            enemies.Add(enemy);
                            break;
                        case "Bottom_Right":
                            enemy = new Enemy(Atomic.startingCorner, "Enemy_BotR.png");
                            layer2.AddChild(enemy);
                            enemies.Add(enemy);
                            break;
                    }
                }

                // End Song go back to menu:
                if (BeatHandler.GetBeat() >= 620)
                {
                    completedLevel = true;

                    for (int i = enemies.Count - 1; i >= 0; i--)
                    {
                        enemies.Remove(enemies[i]);
                    }

                    // Remove enemies:
                    List<GameObject> children = GetChildren();
                    foreach (GameObject child in children)
                    {
                        if (child is Enemy)
                        {
                            Enemy enemy = (Enemy)child;
                            enemies.Remove(enemy);
                            enemy.Remove();
                        }
                    }

                    DestroyChildren();
                    if (channel.IsPlaying) channel.Stop();
                    MainMenu mainMenu = new MainMenu();
                    mainMenu.AfterMenu();
                    AddChild(mainMenu);
                }
                break;
        }
    }
    void LoseGame()
    {
        // Losing:
        if (lives <= 0)
        {
            // Tape stop:
            channel.Frequency -= 200;
            if (channel.Frequency <= 35000) channel.Stop();

            for (int i  = enemies.Count - 1; i >= 0; i--)
            {
                enemies.Remove(enemies[i]);
            }
            
            // Remove enemies:
            List<GameObject> children = GetChildren();
            foreach (GameObject child in children)
            {
                if (child is Enemy)
                {
                    Enemy enemy = (Enemy)child;
                    enemies.Remove(enemy);
                    enemy.Remove();
                }
            }

            DestroyChildren();
            if (channel.IsPlaying) channel.Stop();
            MainMenu mainMenu = new MainMenu();
            mainMenu.AfterMenu();
            AddChild(mainMenu);
        }
    }

    void DrumInput()
    {
        //Drum input:
        if (Input.GetKeyDown(Key.Q))
        {
            foreach (GameObject other in enemies)
            {
                Enemy enemy = (Enemy)other;
                enemy.CheckHitOrMiss(Key.Q);
                break;
            }
        }

        if (Input.GetKeyDown(Key.A))
        {
            foreach (GameObject other in enemies)
            {
                Enemy enemy = (Enemy)other;
                enemy.CheckHitOrMiss(Key.A);
                break;
            }
        }

        if (Input.GetKeyDown(Key.E))
        {
            foreach (GameObject other in enemies)
            {
                Enemy enemy = (Enemy)other;
                enemy.CheckHitOrMiss(Key.E);
                break;
            }
        }

        if (Input.GetKeyDown(Key.D))
        {
            foreach (GameObject other in enemies)
            {
                Enemy enemy = (Enemy)other;
                enemy.CheckHitOrMiss(Key.D);
                break;
            }
        }
    }

    void Beats()
    {
        BeatHandler.ChangeBeat(channel, msPerBeat);

        //Create enemy every beat:
        currentBeat = BeatHandler.GetBeat();
        if (currentBeat != lastBeat)
        {
            lastBeat = currentBeat;
            CreateEnemy();
        }
    }

    void DestroyChildren()
    {
        List<GameObject> children = GetChildren();
        foreach (GameObject child in children)
        {
            child.Destroy();
        }
    }

    void MakeMainMenu()
    {
        MainMenu mainMenu = new MainMenu();
        AddChild(mainMenu);
    }

    public void CreateLightning(float xIn, float yIn)
    {
        Lightning lightning = new Lightning();
        lightning.SetOrigin(lightning.width / 2, lightning.height);
        lightning.SetXY(xIn, yIn);
        layer2.AddChild(lightning);
    }

    public void MakeLayers()
    {
        screenshake = new Pivot();
        screenshake.x = 0;
        AddChild(screenshake);
        layer0 = new Pivot();
        screenshake.AddChild(layer0);
        layer1 = new Pivot();
        screenshake.AddChild(layer1);
        layer2 = new Pivot();
        screenshake.AddChild(layer2);
    }

    static void Main()
    {
        new MyGame().Start();
    }
}