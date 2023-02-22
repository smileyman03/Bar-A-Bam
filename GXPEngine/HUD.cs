using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GXPEngine;

public static class HUD
{
    public static EasyDraw scoreBoard;
    public static EasyDraw lives;
    public static EasyDraw comboDisplay;
    public static Sprite energyText;

    public static EasyDraw DrawScoreboard()
    {
        scoreBoard = new EasyDraw(250, 100);
        scoreBoard.SetOrigin(0, 0);
        scoreBoard.SetXY(620, -25);
        scoreBoard.TextAlign(CenterMode.Min, CenterMode.Center);
        scoreBoard.TextFont(Utils.LoadFont("Dash.otf", 30));
        scoreBoard.Text("Score: " + 0);
        return scoreBoard;
    }

    public static void UpdateScore(int score)
    {
        scoreBoard.ClearTransparent();
        scoreBoard.TextFont(Utils.LoadFont("Dash.otf", 30));
        scoreBoard.Text("Score: " + score);
    }

    public static EasyDraw DrawLives(int liveTotal)
    {
        lives = new EasyDraw(200, 800);
        lives.SetOrigin(0, 0);
        lives.Fill(Color.Gray);
        lives.Rect(100, 403, 200, 20);
        lives.Fill(153, 255, 0);
        lives.Rect(100, 403, 200, 20);
        lives.SetXY(580, 200);

        energyText = new Sprite("ForEnergyBar.png");
        energyText.SetOrigin(0, 0);
        energyText.scale = 0.04f;
        energyText.SetXY(650, 595);
        return scoreBoard;
    }
    public static void UpdateLives(int liveTotal)
    {
        lives.ClearTransparent();
        lives.Fill(Color.Gray);
        lives.Rect(100, 403, 200, 20);
        lives.Fill(153, 255, 0);
        lives.Rect(100, 403, (200 / 15) * liveTotal, 20);
    }

    public static EasyDraw DrawCombo(int combo)
    {
        comboDisplay = new EasyDraw(800, 1200);
        comboDisplay.SetOrigin(0, 0);
        comboDisplay.SetXY(640, -540);
        comboDisplay.TextAlign(CenterMode.Min, CenterMode.Center);
        comboDisplay.TextFont(Utils.LoadFont("Dash.otf", 20));
        comboDisplay.Text("Combo: " + combo);
        return comboDisplay;
    }

    public static void UpdateCombo(int combo)
    {
        comboDisplay.ClearTransparent();
        comboDisplay.TextFont(Utils.LoadFont("Dash.otf", 20));
        comboDisplay.Text("Combo: " + combo);
    }

}