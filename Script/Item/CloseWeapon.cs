using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWeapon : MonoBehaviour{

    public string CloseWeaponName;  //손에들고있는 아이템

    //무기유형 구분값
    
    public bool isAxe;
    public bool isPickaxe;
    public bool isHand;


    public float range;     //공격범위
    public int damage;  //공격력
    public float workSpeed; ///작업속도 
    public float attackDeley;   // 공격딜레이
    public float attackDeleyActive; // 공격활성화
    public float attackDeleyDeactive; // 공격비활성화

    public Animator anim;
    

}
