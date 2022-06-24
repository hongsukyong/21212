using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class ActionController : MonoBehaviour,IPointerClickHandler
{

    public StatusController statusController;
    public PlayerController playerController; //플레이어 컨트롤러 스크립트
    Rigidbody2D rigid;                 //드로우 레이를 하기위한 리지드 컴포넌트  
    private float h;                   //레이히트를 위한 방향값
    [SerializeField]
    private GameObject LoadDummy;
    
    [SerializeField]
    private float range = 0; //습득 그낭한 최대거리

    public bool pickupActive = false; //습득 가능할 시 true 

    private RaycastHit2D hitinfo; //충돌정보



    public TextMeshProUGUI ActionText; //아이템 주울지 정보를 보여줄  텍스트

    [SerializeField]
    Inventory inventory; //

    public Button canclecamp;


    Camp camp;







    private void Start()
    {

        rigid = GetComponent<Rigidbody2D>();
        statusController= FindObjectOfType<StatusController>();
        camp = FindObjectOfType<Camp>();
        
    }

    void Update()
    {
        if (!Inventory.inventoryActivated){
            h = Input.GetAxisRaw("Horizontal");     //방향값 주기 

            RaycastHit2D hitinfo = Physics2D.Raycast(rigid.position, Vector2.right *  playerController.Seeing, range,LayerMask.GetMask("Building", "Item"));     //레이캐스트 


            if (hitinfo.collider != null)
            {

                ActionText = hitinfo.collider.gameObject.GetComponentInChildren<TextMeshProUGUI>();// 레이케스트 맞은 오브젝트에 TMP할당하는것


                if (hitinfo.transform.tag == "Item")
                {
                    pickupActive = true;


                    if(ActionText != null)
                    {
                    ActionText.text = hitinfo.transform.GetComponent<ItemPickup>().item.itemName + "조사" + "<color=yellow>" + "(E)" + "</color>"; //조사 텍스트 **
                    }
                }
                if(hitinfo.transform.gameObject.name.Contains("Camp"))
                {
                    pickupActive = false;

                    if (ActionText != null)
                    {
                        ActionText.text = "캠프파이어";
                    }
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        //캠핑들어갈자리 
                        Debug.Log("캠핑상호작용 ");
                        camp.CampStart();

                    }

                }


                if(hitinfo.transform.gameObject.tag == "Building")
                {
                    Debug.Log(hitinfo.transform.gameObject.name);
                    if(Input.GetKeyDown(KeyCode.E))
                        hitinfo.transform.gameObject.GetComponentInParent<Builind>().Inner();
                }
                if (hitinfo.transform.gameObject.tag == "Buidingdoor")
                {
                    Debug.Log(hitinfo.transform.gameObject.name);
                    if (Input.GetKeyDown(KeyCode.E))
                        hitinfo.transform.gameObject.GetComponentInParent<Builind>().Outer();
                }


            }
            else
            {


                pickupActive = false;
                if (ActionText != null)
                ActionText.text = "";

                
                
            }
            
            if (pickupActive && Input.GetKeyDown(KeyCode.E)) //아이템 획득 구문 - 여기에 아이템파괴되면 아이템 창고 열리는것도 추가 해야함 
            {
                Debug.Log("아이템 획득 함수 넣기");           //아이템획득

                inventory.AcquireItem(hitinfo.transform.GetComponent<ItemPickup>().item);

                Destroy(hitinfo.transform.gameObject);      // 디스트로이 넣어도 되지만 오브젝트 종류에 따라서 디스트로이가 아닌경우도 있음 
                statusController.currentSp -= 10;
            }

        }
        
    }

    private void FixedUpdate()
    {
        

        

    }
    IEnumerator Attack(){ 
        //공격

        yield return null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left){
            
            if(statusController.currentSp > 1000){

                StartCoroutine(Attack());
            }
        }
    }

    public IEnumerator DummyLoading(float x = 1.5f)   
    {   
        playerController.moving = false;
        LoadDummy.SetActive(true);
        yield return new WaitForSeconds(x);
        playerController.moving = true;
        LoadDummy.SetActive(false);

        
    }


}
