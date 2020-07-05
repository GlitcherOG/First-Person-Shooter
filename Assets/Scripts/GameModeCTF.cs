using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeCTF : GameMode
{
    List<Flag> flags;
    public static new GameModeCTF Instance;

    private void Awake()
    {
        Instance = this;
    }
}
