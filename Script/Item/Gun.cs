using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public string gunName;
    public float range;         //사거리  2D필요없음
    public float accuracy;      //정확도  2D필요없음
    public float firerate;      //연사속도  2D필요없음
    public float reloadTime;    //재장전    필요없음

    public float Damage;

    public int reloadBulletCount = 0;   
    public int currentBulletCount;
    public int maxBulletCount;
    public int carryBulletCount;

    public float retroActionForce;
    public float retroActtionFineSinghtForce;

    public Vector3 fineSightOrignForce;

    public Animator anim;
    public ParticleSystem muzzleFlash;

    public AudioClip fireSound;
    public AudioClip EmptySound;
}
