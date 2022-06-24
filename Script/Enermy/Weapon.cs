using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    CapsuleCollider2D capcol;
    StatusController statusController;
    GameObject enermy;
    public GameObject mom;

    void Start()
    {
        capcol = GetComponent<CapsuleCollider2D>();
        statusController = FindObjectOfType<StatusController>();
        enermy = GetComponentInParent<Enermy>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            int a = 0;
            float b = 0;

            if( a == b )
            {
                b += Time.deltaTime;
                Damaged();
                a = 2;
            }
            if(a < b )
            {
                Damaged();
                b = -2;
            }
        }
    }


    void Damaged()
    {
        statusController.currentHp -= 1;
        Debug.Log("피감소");
    }
    
}




