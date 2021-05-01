using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public Animator anim;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void onPlayGameButtonHighlighted()
    {
        anim.SetTrigger("Highlighted");

    }
}
