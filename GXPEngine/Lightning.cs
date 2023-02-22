using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Lightning : AnimationSprite
{
    public Lightning() : base("ThunderSprite.png", 8, 2, 14)
    {
    }

    void Update()
    {
        Animate();
        if (_currentFrame >= 13)
        {
            Destroy();
        }
    }
}