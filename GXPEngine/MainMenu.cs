using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GXPEngine;
using System.Reflection.Emit;
using System.Runtime.Remoting.Channels;
using System.Xml.Schema;
using System.IO;

public class MainMenu : GameObject
{
    AnimationSprite maincharacter;
    EasyDraw highscore;
    EasyDraw yourScore;
    public static float squaresXLeft;
    public static float squaresXRight;
    public static float squaresYUp;
    public static float squaresYDown;
    Boolean isInTitleScreen = true;
    Boolean isInMenu = false;
    Boolean isInFreaky = false;
    Boolean isInDontCare = false;
    Boolean isInAtomic = false;
    Boolean isInAfterMenu = false;
    Sprite click;
    int timer = 0;
    public MainMenu()
    {
        CreateTitleScreen();
    }
    void Update()
    {
        if (isInTitleScreen)
        {
            timer += Time.deltaTime;
            if (timer >= 750)
            {
                click.alpha *= -1;
                timer = 0;
            }
        }

        if (!isInTitleScreen) maincharacter.Animate(0.025f);

        // Clicked on a button:
        if (isInMenu)
        {
            if (Input.GetKeyDown(Key.Q))
            {
                isInMenu = false;
                isInAtomic = true;
                ClickedAtomic();
            }

            if (Input.GetKeyDown(Key.A))
            {
                isInMenu = false;
                isInFreaky = true;
                ClickedFreaky();
            }

            if (Input.GetKeyDown(Key.E))
            {
                isInMenu = false;
                isInDontCare = true;
                ClickedDontCare();
            }

            if (Input.GetKeyDown(Key.D))
            {
                DestroyChildren();
                isInMenu = false;
                isInTitleScreen= true;
                CreateTitleScreen();
            }
        }
        else
        {
            if (isInAtomic)
            {
                // Pressed back:
                if (Input.GetKeyDown(Key.D))
                {
                    isInAtomic = false;
                    isInMenu = true;
                    CreateMenu();
                }

                // Pressed start:
                if (Input.GetKeyDown(Key.A))
                {
                    isInAtomic = false;
                    StartAtomic();
                }
            }

            if (isInDontCare)
            {
                // Pressed back:
                if (Input.GetKeyDown(Key.D))
                {
                    isInDontCare = false;
                    isInMenu = true;
                    CreateMenu();
                }

                // Pressed start:
                if (Input.GetKeyDown(Key.A))
                {
                    isInDontCare = false;
                    StartDontCare();
                }
            }

            if (isInFreaky)
            {
                // Pressed back:
                if (Input.GetKeyDown(Key.D))
                {
                    isInFreaky = false;
                    isInMenu = true;
                    CreateMenu();
                }

                // Pressed start:
                if (Input.GetKeyDown(Key.A))
                {
                    isInFreaky = false;
                    StartFreaky();
                }
            }

            if (isInAfterMenu)
            {
                // Pressed back:
                if (Input.GetKeyDown(Key.D))
                {
                    isInAfterMenu = false;
                    isInMenu = true;
                    CreateMenu();
                }

                // Pressed replay:
                if (Input.GetKeyDown(Key.A))
                {
                    isInAfterMenu = false;

                    MyGame myGame = (MyGame)game;
                    switch (myGame.level)
                    {
                        case "Freaky":
                            StartFreaky();
                            break;
                        case "DontCare":
                            StartDontCare();
                            break;
                        case "Atomic":
                            StartAtomic();
                            break;
                    }
                }
            }

            if (isInTitleScreen)
            {
                if (Input.GetKeyDown(Key.Q) || Input.GetKeyDown(Key.A) || Input.GetKeyDown(Key.E) || Input.GetKeyDown(Key.D))
                {
                    CreateMenu();
                    isInMenu = true;
                    isInTitleScreen = false;
                }
            }
        }
    }

    void CreateTitleScreen()
    {
        Sprite menu = new Sprite("TitleScreen.png");
        AddChild(menu);

        click = new Sprite("ContinueButton.png");
        click.SetOrigin(0, 0);
        click.SetXY(360, 500);
        AddChild(click);
    }

    void ClickedAtomic()
    {
        DestroyChildren();

        // Make new screen:
        MyGame myGame = (MyGame)game;
        Sprite menu = new Sprite("LevelSelectAtomic.png");
        AddChild(menu);
        // Read highscore
        StreamReader reader = new StreamReader("HighscoreAtomic.txt");
        string input = reader.ReadLine();

        highscore = new EasyDraw(400, 100);
        highscore.SetOrigin(0, 0);
        highscore.SetXY(480, 180);
        highscore.TextAlign(CenterMode.Center, CenterMode.Center);
        highscore.TextFont(Utils.LoadFont("Dash.otf", 30));
        highscore.Fill(255, 0, 255);
        highscore.Text(input);
        AddChild(highscore);

        maincharacter = new AnimationSprite("MainCharacterAnimation.png", 8, 1);
        maincharacter.SetOrigin(maincharacter.width / 2, maincharacter.height / 2);
        maincharacter.scale = 0.5f;
        maincharacter.SetXY(game.width / 2, game.height / 2 + 100);
        maincharacter.SetCycle(5, 2);
        AddChild(maincharacter);
    }

    void StartAtomic()
    {
        MyGame myGame = (MyGame)game;
        myGame.channel = new Sound("Atomic_Infraction.mp3").Play();
        myGame.channel.Volume = 0.16f;
        myGame.bpm = 108 * 2;
        myGame.level = "Atomic";

        //Calculate speed:
        myGame.msPerBeat = 1000f / (myGame.bpm / 60f);
        myGame.msToMove = Convert.ToInt16(myGame.msPerBeat * 4);

        StartGame();
    }

    void ClickedDontCare()
    {
        DestroyChildren();

        // Make new screen:
        MyGame myGame = (MyGame)game;
        Sprite menu = new Sprite("LevelSelectDontCare.png");
        AddChild(menu);
        // Read highscore
        StreamReader reader = new StreamReader("HighscoreDontCare.txt");
        string input = reader.ReadLine();

        highscore = new EasyDraw(400, 100);
        highscore.SetOrigin(0, 0);
        highscore.SetXY(480, 180);
        highscore.TextAlign(CenterMode.Center, CenterMode.Center);
        highscore.TextFont(Utils.LoadFont("Dash.otf", 30));
        highscore.Fill(255, 0, 255);
        highscore.Text(input);
        AddChild(highscore);

        maincharacter = new AnimationSprite("MainCharacterAnimation.png", 8, 1);
        maincharacter.SetOrigin(maincharacter.width / 2, maincharacter.height / 2);
        maincharacter.scale = 0.75f;
        maincharacter.SetXY(game.width / 2, game.height / 2 + 100);
        maincharacter.SetCycle(5, 2);
        AddChild(maincharacter);
    }
    void StartDontCare()
    {
        MyGame myGame = (MyGame)game;
        myGame.channel = new Sound("Dont_Care_Infraction.mp3").Play();
        myGame.channel.Volume = 1f;
        myGame.bpm = 124 * 2;
        myGame.level = "DontCare";

        //Calculate speed:
        myGame.msPerBeat = 1000f / (myGame.bpm / 60f);
        myGame.msToMove = Convert.ToInt16(myGame.msPerBeat * 4);

        StartGame();
    }

    void ClickedFreaky()
    {
        DestroyChildren();

        // Make new screen:
        MyGame myGame = (MyGame)game;
        Sprite menu = new Sprite("LevelSelectFreaky.png");
        AddChild(menu);
        // Read highscore
        StreamReader reader = new StreamReader("HighscoreFreaky.txt");
        string input = reader.ReadLine();

        highscore = new EasyDraw(400, 100);
        highscore.SetOrigin(0, 0);
        highscore.SetXY(480, 180);
        highscore.TextAlign(CenterMode.Center, CenterMode.Center);
        highscore.TextFont(Utils.LoadFont("Dash.otf", 30));
        highscore.Fill(255, 0, 255);
        highscore.Text(input);
        AddChild(highscore);

        maincharacter = new AnimationSprite("MainCharacterAnimation.png", 8, 1);
        maincharacter.SetOrigin(maincharacter.width / 2, maincharacter.height / 2);
        maincharacter.scale = 0.5f;
        maincharacter.SetXY(game.width / 2, game.height / 2 + 100);
        maincharacter.SetCycle(5, 2);
        AddChild(maincharacter);
    }

    void StartFreaky()
    {
        MyGame myGame = (MyGame)game;
        myGame.channel = new Sound("Freaky_Infraction.mp3").Play();
        myGame.channel.Volume = 0.14f;
        myGame.bpm = 114 * 2;
        myGame.level = "Freaky";

        //Calculate speed:
        myGame.msPerBeat = 1000f / (myGame.bpm / 60f);
        myGame.msToMove = Convert.ToInt16(myGame.msPerBeat * 4);

        StartGame();
    }

    void CreateMenu()
    {
        DestroyChildren();
        Sprite background = new Sprite("LevelSelect.png");
        AddChild(background);

        maincharacter = new AnimationSprite("MainCharacterAnimation.png", 8, 1);
        maincharacter.SetOrigin(maincharacter.width / 2, maincharacter.height / 2);
        maincharacter.scale = 0.5f;
        maincharacter.SetXY(game.width / 2, game.height / 2 + 100);
        maincharacter.SetCycle(5, 2);
        AddChild(maincharacter);
    }

    void StartGame()
    {
        MyGame myGame = (MyGame)game;
        myGame.MakeLayers();

        myGame.score = 0;
        myGame.combo = 0;

        // Add Hitbox 1 (top left):
        HitBox hitBox1 = new HitBox();
        hitBox1.scale = 0.2f;
        hitBox1.SetOrigin(hitBox1.width / 2, hitBox1.height / 2);
        hitBox1.SetXY((game.width / 2) - 250, (game.height / 2) - 25);
        myGame.layer1.AddChild(hitBox1);

        squaresXLeft = hitBox1.x + 50;
        squaresYUp = hitBox1.y;

        // Add Hitbox 2 (top right):
        HitBox hitBox2 = new HitBox();
        hitBox2.scale = 0.2f;
        hitBox2.SetOrigin(hitBox2.width / 2, hitBox2.height / 2);
        hitBox2.SetXY((game.width / 2) + 200, (game.height / 2) - 25);
        myGame.layer1.AddChild(hitBox2);

        // Add Hitbox 3 (bottom left):
        HitBox hitBox3 = new HitBox();
        hitBox3.scale = 0.2f;
        hitBox3.SetOrigin(hitBox3.width / 2, hitBox3.height / 2);
        hitBox3.SetXY((game.width / 2) - 250, (game.height / 2) + 180);
        myGame.layer1.AddChild(hitBox3);

        // Add Hitbox 4 (bottom right):
        HitBox hitBox4 = new HitBox();
        hitBox4.scale = 0.2f;
        hitBox4.SetOrigin(hitBox4.width / 2, hitBox4.height / 2);
        hitBox4.SetXY((game.width / 2) + 200, (game.height / 2) + 180);
        myGame.layer1.AddChild(hitBox4);

        squaresXRight= hitBox4.x;
        squaresYDown = hitBox4.y + 20;

        // Add Main Character:
        MainCharacter mainCharacter = new MainCharacter();
        mainCharacter.SetOrigin(mainCharacter.width / 2, mainCharacter.height/2);
        mainCharacter.scale = 0.5f;
        mainCharacter.SetXY(game.width / 2, game.height / 2 + 50);
        myGame.layer1.AddChild(mainCharacter);

        // Add Scoreboard
        HUD.DrawScoreboard();
        myGame.layer2.AddChild(HUD.scoreBoard);

        // Add Lives
        HUD.DrawLives(myGame.lives);
        myGame.layer1.AddChild(HUD.lives);
        myGame.layer1.AddChild(HUD.energyText);

        // Add Combo
        HUD.DrawCombo(myGame.combo);
        myGame.layer2.AddChild(HUD.comboDisplay);

        // Add background
        myGame.StartGame();

        myGame.completedLevel = false;
        myGame.isPlaying = true;
        myGame.RemoveChild(this);
    }

    public void AfterMenu()
    {
        DestroyChildren();
        MyGame myGame = (MyGame)game;
        
        isInMenu = false;
        isInFreaky = false;
        isInDontCare = false;
        isInAtomic = false;
        isInAfterMenu = true;

        switch (myGame.completedLevel)
        {
            case true:
                Sprite menu = new Sprite("YouWinScreen.png");
                AddChild(menu);
                break;
            case false:
                Sprite menu2 = new Sprite("YouFailedScreen.png");
                AddChild(menu2);
                break;
        }

        // Add highscore:
        highscore = new EasyDraw(400, 100);
        int input;
        int displayScore = 0;

        switch (myGame.level)
        {
            case "Freaky":
                StreamReader reader1 = new StreamReader("HighScoreFreaky.txt");
                input = int.Parse(reader1.ReadLine());
                reader1.Close();
                if (myGame.score > input && myGame.completedLevel)
                {
                    StreamWriter writer1 = new StreamWriter("HighScoreFreaky.txt");
                    writer1.WriteLine(Convert.ToString(myGame.score));
                    writer1.Close();
                    displayScore = myGame.score;
                }
                
                else
                {
                    displayScore = input;
                }
                break;
            case "DontCare":
                StreamReader reader2 = new StreamReader("HighScoreDontCare.txt");
                input = int.Parse(reader2.ReadLine());
                reader2.Close();
                if (myGame.score > input && myGame.completedLevel)
                {
                    StreamWriter writer2 = new StreamWriter("HighScoreDontCare.txt");
                    writer2.WriteLine(Convert.ToString(myGame.score));
                    writer2.Close();
                    displayScore = myGame.score;
                }

                else
                {
                    displayScore = input;
                }
                break;
            case "Atomic":
                StreamReader reader3 = new StreamReader("HighScoreAtomic.txt");
                input = int.Parse(reader3.ReadLine());
                reader3.Close();
                if (myGame.score > input && myGame.completedLevel)
                {
                    StreamWriter writer3 = new StreamWriter("HighScoreAtomic.txt");
                    writer3.WriteLine(Convert.ToString(myGame.score));
                    writer3.Close();
                    displayScore = myGame.score;
                }

                else
                {
                    displayScore = input;
                }

                break;
        }

        highscore.SetOrigin(0, 0);
        highscore.SetXY(myGame.width / 2 - 600, 300);
        highscore.TextAlign(CenterMode.Center, CenterMode.Center);
        highscore.TextFont(Utils.LoadFont("Dash.otf", 30));
        highscore.Fill(255, 0, 255);
        highscore.Text(Convert.ToString(displayScore));
        AddChild(highscore);

        // Add your score:
        yourScore = new EasyDraw(400, 100);
        yourScore.SetOrigin(0, 0);
        yourScore.SetXY(myGame.width / 2 + 200, 300);
        yourScore.TextAlign(CenterMode.Center, CenterMode.Center);
        yourScore.TextFont(Utils.LoadFont("Dash.otf", 30));
        yourScore.Fill(255, 0, 255);
        yourScore.Text(Convert.ToString(myGame.score));
        AddChild(yourScore);

        maincharacter = new AnimationSprite("MainCharacterAnimation.png", 8, 1);
        maincharacter.SetOrigin(maincharacter.width / 2, maincharacter.height / 2);
        maincharacter.scale = 0.5f;
        maincharacter.SetXY(game.width / 2, game.height / 2 + 100);
        maincharacter.SetCycle(5, 2);
        AddChild(maincharacter);

        // Reset game stats:
        myGame.lives = 15;
        myGame.isPlaying = false;
        myGame.currentBeat = 0;
        myGame.lastBeat = 0;
        BeatHandler.beat = 0f;
    }

    void DestroyChildren()
    {
        // Remove menu:
        List<GameObject> children = GetChildren();
        foreach (GameObject child in children)
        {
            child.Destroy();
        }
    }
}