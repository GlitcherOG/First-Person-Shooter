using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    public Animator Player;
    public int animation;
    private void Start()
    {
        if (animation == 0)
        {
            Player.SetBool("Walking", true);
        }
        if (animation == 1)
        {
            Player.SetBool("Running", true);
        }
    }
}
