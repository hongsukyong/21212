using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    GunContorller gunContorller;

    public GameObject scanObject;
    public GameObject Char;

    public Rigidbody2D rigid;

    public float h;     //이동방향 
    public float v;       //점프파워
    public int speed = 10; //이동속도 

    private StatusController statuscontroller;
    
    public SpriteRenderer  spriteRenderer; 

    [SerializeField]
    public bool SeeingLeft = false;
    public float Seeing;
    public bool moving;
    public WeaponManager weaponManager;

    Camp camp;





    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        rigid = GetComponent<Rigidbody2D>();
        statuscontroller = FindObjectOfType<StatusController>();
        camp = FindObjectOfType<Camp>();





    }


    void Update()
    {
        Decrease();
        h = Input.GetAxisRaw("Horizontal");
        if(camp.CampING)
        {
            h = 0;
        }
        

        if (h < 0)
        {
            SeeingLeft = true;

        }
        else if (h > 0)
        {
            SeeingLeft = false;
        }
        
        spriteRenderer.flipX = SeeingLeft;

        if (SeeingLeft)
        {
            Seeing = -1;
        }
        else if (!SeeingLeft)    
        {
            Seeing = 1;
        }


        if(!Inventory.inventoryActivated){

            if (statuscontroller.SliderGroups[1].value < 0.1)
            {

                Char.GetComponent<Rigidbody2D>().gravityScale = 10.0f;
                Char.GetComponent<Rigidbody2D>().angularDrag = 1f;
                speed = 5;
                Move();
            }
            else if(statuscontroller.SliderGroups[1].value > 0.1){
                Char.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
                Char.GetComponent<Rigidbody2D>().angularDrag = 0.5f;
                speed = 10;
                Move();
            }
        
            Debug.DrawRay(rigid.position, new Vector2(h, 0),new Color (0,1,0), 1f);
            
            RaycastHit2D rayhit = Physics2D.Raycast(rigid.position, new Vector2(1,0) * Seeing , 2f , LayerMask.GetMask("Item", "Building"));
    
            if(rayhit.collider == null)
            {
                scanObject = null;
            }
            else
            {
                scanObject = rayhit.collider.gameObject;
            }
            
            if(scanObject != null)
            {
                if(weaponManager.currentWeaponType == "PickAxe")
                    if(Input.GetKeyDown(KeyCode.F) || Input.GetButtonDown("Fire1")){
                    
                    if (rayhit.transform.tag == "Rock")
                    {
                        rayhit.transform.GetComponent<Rock>().Mining();
                    }
                }


            }
        }
        
    }
    private void FixedUpdate()
    {
        
    
    }

    public void Move()
    {
        if(Input.GetButton("Horizontal") || Input.GetButtonDown("Jump"))
            moving =true;

        if (Input.GetButton("Horizontal"))
        {
            
            rigid.velocity = new Vector2(speed * h, rigid.velocity.y); 
        }

        if (Input.GetButtonDown("Jump"))
        {
            
            rigid.AddForce(Vector2.up * v, ForceMode2D.Impulse);
        }
    }

    public void Decrease()
    {
        if(rigid.velocity.x != 0)
        {
            statuscontroller.DecreeseStamina(1);
        }
        if(rigid.velocity.y > 0)
        {
            statuscontroller.DecreeseStamina(2);
        }
    }



}
