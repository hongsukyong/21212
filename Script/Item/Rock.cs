using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField]
    private int Hp;
    [SerializeField]
    private float destroyTime;
    [SerializeField]
    private BoxCollider2D col;

    [SerializeField]
    private GameObject Go_rock;

[Tooltip("상호작용 후에 생겨날 오브젝트")]
    [SerializeField]
    private GameObject Go_debris;
    public void Mining()
    {
        Hp--;
        if(Hp <= 0){
            Destruction();
        }
    }

    private void Destruction()
    {
        col.enabled = false;
        Destroy(Go_rock);



        Go_debris.SetActive(true);
        Destroy(Go_debris, destroyTime);
        //상호작용넣기
        

    }





}
