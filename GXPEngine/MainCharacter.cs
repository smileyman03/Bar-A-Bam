using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class MainCharacter : AnimationSprite
{
    int timer;
    Boolean idle = false;
    Boolean wait = false;
    int lastBeat;
    public MainCharacter() : base("MainCharacterAnimation.png", 8, 1)
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(Key.Q))
        {
            SetFrame(2);
            timer = 0;
            idle = false;
            wait = false;
        }

        if (Input.GetKeyDown(Key.E))
        {
            SetFrame(1);
            timer = 0;
            idle = false;
            wait = false;
        }

        if (Input.GetKeyDown(Key.A))
        {
            SetFrame(4);
            timer = 0;
            idle = false;
            wait = false;
        }

        if (Input.GetKeyDown(Key.D))
        {
            SetFrame(3);
            timer = 0;
            idle = false;
            wait = false;
        }

        if (_currentFrame <= 6 && !idle)
        {
            timer += Time.deltaTime;
            if (timer > 200)
            {
                if (!wait)
                {
                    wait = true;
                    SetFrame(0);
                    timer = 0;
                }
            }

            if (timer > 200)
            {
                if (wait)
                {
                    idle = true;
                    currentFrame = 5;
                }
            }
        }

        if (idle)
        {
            if (BeatHandler.GetBeat() != lastBeat)
            {
                lastBeat = BeatHandler.GetBeat();
                switch (currentFrame)
                {
                    case 5:
                        currentFrame = 6;
                        break;
                    case 6:
                        currentFrame = 5;
                        break;
                }
            }
        }
    }
}