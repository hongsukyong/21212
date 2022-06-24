using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enermy : MonoBehaviour
{
    Rigidbody2D rigid;
    public float h; 
    float w;
    bool SeeRight;
    float d;

    Camp camp;
    PlayerController playerController;
    [SerializeField]
    private GameObject weapon;




    private void Awake() 
    {
        Invoke("Move", 5);
        camp = FindObjectOfType<Camp>();
        playerController = FindObjectOfType<PlayerController>();
    }
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        RaycastHit2D rayhit = Physics2D.Raycast(rigid.transform.position, Vector2.left, 4f, LayerMask.GetMask("Item", "Player"));
        rigid.velocity = new Vector2(h, 0);

        if(rayhit.collider == null)
        {
            weapon.SetActive(false);
        }

        if(rayhit.collider != null)
        {
            if (rayhit.collider.gameObject.tag == "Player")//충돌체의 테그가 플레이어라면  
            {   //전투시 일어날 행동
                weapon.SetActive(true);
            }
        }
    }

    void FixedUpdate()
    {


        
    }
    void Move()
    {
        h = Random.Range(-1, 1);
        Invoke("Move", 5);
    }
}
