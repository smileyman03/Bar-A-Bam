using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Atomic
{
    static int topL = 1;
    static int topR = 2;
    static int botL = 3;
    static int botR = 4;

    public static string startingCorner;
    public static int currentValue;
    static int amountOfBeats;

    public static List<int> level = new List<int>();
    public static void ReadLevel()
    {
        StreamReader newReader = new StreamReader("Atomic.csv");
        while (!newReader.EndOfStream)
        {
            var line = newReader.ReadLine();
            var values = line.Split(',');

            // Store value in list:
            foreach (var v in values)
            {
                if (v == "0")
                {
                    level.Add(0);
                }

                if (v == "topL")
                {
                    level.Add(topL);
                }

                if (v == "topR")
                {
                    level.Add(topR);
                }

                if (v == "botL")
                {
                    level.Add(botL);
                }

                if (v == "botR")
                {
                    level.Add(botR);
                }
            }
        }
        newReader.Close();

        foreach (int i in level)
        {
            amountOfBeats++;
        }
    }

    static void StepLevelHandling(int stepValue)
    {
        switch (stepValue)
        {
            case 1:
                startingCorner = "Top_Left";
                break;
            case 2:
                startingCorner = "Top_Right";
                break;
            case 3:
                startingCorner = "Bottom_Left";
                break;
            case 4:
                startingCorner = "Bottom_Right";
                break;
        }
    }

    public static void GetEnemyPosition()
    {
        if (BeatHandler.GetBeat() <= amountOfBeats)
        {
            currentValue = level[BeatHandler.GetBeat() - 1];
            StepLevelHandling(currentValue);
        }

        else
        {
            currentValue = 0;
        }
    }
}