using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFlip : MonoBehaviour
{

    SpriteRenderer spr;
    public PlayerController playerController;

    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerController.spriteRenderer.flipX)
            spr.flipX = false;
        else
            spr.flipX = true;

    }

}
