using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Slot : MonoBehaviour, IPointerClickHandler{

    public Item item;           // 파라미터로 들어올 아이템정보
    public int itemCount;       // 아이템 갯수
    public Image itemImage;     // 아이템 이미지 

    [Tooltip("아이템갯수")]
    [SerializeField]
    private Text textitemCount;

    public Inventory inventory;

    private ItemaEffectDatabase effectDatabase;

    [Tooltip("아이템갯수 배경")]
    [SerializeField]
    private GameObject countImg;

    WeaponManager weaponManager;
    
    



    private void Start() {
        weaponManager = FindObjectOfType<WeaponManager>();
        effectDatabase = FindObjectOfType<ItemaEffectDatabase>();

    }

    private void Update() {


    }

    //이미지의 투명도 조절
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }   


    // 인벤토리에 아이템 추가
    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;

        if(item.itemType != Item.ItemType.Equipment)
        //아이템타입이 장착이 아니라면 아이템카운트텍스트를 나타낸다
        {
            textitemCount.text = itemCount.ToString(); // 아이템카운트 텍스트는 파라미터로 받은 아이템카운트를 투 스트링으로 표현한다(int) 타입이기 때문에 스트링으로 바료표현할 수 없음 
        }

        else
        {
            textitemCount.text = "";
        }

        if (item != null && item.itemType != Item.ItemType.Equipment)
        {       // 아이템이 null 이 아니고 아이템타입이 장비가 아니라면 

            if (itemCount != 1)
            {     // 아이템갯수가 1이나 0이 아닐경우
                countImg.SetActive(true);                                               // 아이템갯수 텍스트를 액티브시킨다
            }

        }

        SetColor(1); //아이템이 들어오면 아이템 이미지의 컬러알파값을 0에서 1로 바꾼다 

        
    }

    // 아이템 갯수 조절하는 
    public void SetSlotCount(int _count) // 슬롯의 아이템갯수를 변경할 수 있는 함수 
    {
        itemCount += _count; //
        textitemCount.text = itemCount.ToString();

        if(itemCount > 1)
        {
            countImg.SetActive(true);
        }
        else if(itemCount <= 0) //
        {
            ClearSlot(); //
            
        }
    }

    //슬롯 초기화
    public void ClearSlot()
    {
        item = null; //
        itemCount = 0;
        itemImage.sprite = null; //
        SetColor(0);

        textitemCount.text = "";
        countImg.SetActive(false);
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right){
        //우클릭을 눌렀는데 

            
            if(item != null){

                if(item.itemType != Item.ItemType.Equipment){

                    inventory.PanelOn();

                }

                else if (item.itemType == Item.ItemType.Equipment){

                    Debug.Log("진입");
                    EquipSlotEquip();
                    ClearSlot();
                    
                }
            }

        
        }
    }

    public void SlotUseItem()
    {
        if (item != null)
        {

            if (item.itemType != Item.ItemType.Equipment)
            {
                //아이템이있다면
                effectDatabase.UseItem(item);


                //유즈아이템을 호출한다 
                if (item.itemType == Item.ItemType.Used)
                {
                    //아이템타입이 소모품이라면 
                    SetSlotCount(-1);
                    //갯수를 하나 뺀다 

                }

            }

            else if (item.itemType == Item.ItemType.Equipment)
            {
                Debug.Log("진입");
                EquipSlotEquip();
                ClearSlot();


            }
        }
    }

    public void EquipSlotEquip() {

        string[] str =new string[4];

        str[0] = "head";
        str[1] = "right";
        str[2] = "backpack";
        str[3] = "left";


        
        for( int i = 0; i <= inventory.equipslots.Length-1;  i++){
                Debug.Log("진입");
            if (item.name.Contains(str[i])){             
            //아이템이 스트링 0~3까지 보유하고있다면


                    inventory.equipslots[i].AddItem(item);
                    //이큅슬롯 0~3까지 에드아이템 

                break;
            }
        }


    }


}
