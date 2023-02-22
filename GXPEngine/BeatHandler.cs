using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public static class BeatHandler
{
    public static float beat = 0f;

    public static void ChangeBeat(SoundChannel song, float MsPerBeat)
    {
        beat = song.Position / (float)MsPerBeat;
        int currentBeat = Mathf.Floor(beat / 4);
    }

    public static int GetBeat()
    {
        return (int)beat;
    }

    public static float GetBeatFloat()
    {
        return beat;
    }

}