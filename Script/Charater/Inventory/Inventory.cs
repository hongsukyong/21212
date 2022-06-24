using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false;
    [SerializeField]
    private GameObject inventoryBase;

    [SerializeField]
    private GameObject SlotParents;

    private Slot[] slots;
    public EquipSlot[] equipslots;

    private int selectitem;
    public GameObject[] selecSlot;

    public int i ;

    public ItemStatusAdd itemStatusAdd;



    public GameObject useCanclePanel;       // 사용 버리기의 배경이될 판넬

    public GameObject useButtonPanel;       //사용버튼판넬

    public GameObject cancleButtonPanel;    //캔슬버튼 판넬 

    private ItemaEffectDatabase effectDatabase;

    [SerializeField]
    private Image useImg;                   //선택중인지 보여주기위한 이미지
    [SerializeField]
    private Image cancelImg;                //''


    void Start(){

        slots = SlotParents.GetComponentsInChildren<Slot>();

        i = 4;

        useImg.gameObject.SetActive(false);
        cancelImg.gameObject.SetActive(false);
    }



    void Update()
    {
        if(inventoryActivated)
        {
            EnterUse();

            PanelOn();
            
            if(useCanclePanel.activeSelf)
                panelMove();


        }


        if (inventoryActivated && !useCanclePanel.activeSelf)
        {      //유즈캔슬판넬이 꺼져있고 인벤토리가 활성화된 상태라면
            MoveSelecSlot();                                        // 슬롯이동가능하다 
            selecSlot[i].SetActive(true);                   //선택아이템 확인할 수 있따 
        }

        TryOpenInventory();

    }

    private void MoveSelecSlot()
    {
        if (i < selecSlot.Length - 1 && i > 0)
        {  //13보다 작거나 0 클경우 



            if (Input.GetKeyDown(KeyCode.RightArrow))
            {      //오른쪽버튼누르면 +1
                i++;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {  //왼쪽버튼누르면 -1
                i--;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {    //위버튼누르면
                if (i > 7)
                {                                //7보다클경우 
                    selecSlot[i].SetActive(false);      //-3
                    i -= 3;

                }
                else if (i == 6 || i == 5)
                {               //5나6일경우

                    selecSlot[i].SetActive(false);
                    i = 3;                              //3

                }
                else if (i == 4)
                {               //4일경우
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    { //위버튼누르면
                        selecSlot[i].SetActive(false);      //2
                        i = 2;
                    }
                }
                else if (i == 3 || i == 2)
                {               //3이나2일경우
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    { //위버튼누르면
                        selecSlot[i].SetActive(false);
                        i -= 2;                             //-2
                    }
                }
                else if (i == 1 || i == 0)
                {               //1이나0일경우 
                    selecSlot[i].SetActive(false);
                    i = 12;                              //12

                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {  //아래버튼누르면

            }
        }
        if (0 < i && i < 12)
        {
            selecSlot[i + 1].SetActive(false);
            selecSlot[i - 1].SetActive(false);
        }
        else if (selecSlot[0].activeSelf)
        {

            selecSlot[i + 1].SetActive(false);

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                i++;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {

                i = 12;
                selecSlot[0].SetActive(false);
            }
        }
        else if (selecSlot[12].activeSelf)
        {

            selecSlot[i - 1].SetActive(false);

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                i = 0;
                selecSlot[12].SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {

                i--;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                selecSlot[i].SetActive(false);
                i -= 3;
            }
        }
    }
    
    private void TryOpenInventory(){

        if(Input.GetKeyDown(KeyCode.I)){

            inventoryActivated = !inventoryActivated;

            if (inventoryActivated){   

                Debug.Log("InventoryOpen");
                OpenInventory();
                
            }
            else{

                CloseInventory();
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!useCanclePanel.activeSelf)
                CloseInventory();
                inventoryActivated = false;
        }
    }

    private void OpenInventory(){
        
        inventoryBase.SetActive(true);
    }

    private void CloseInventory(){

        inventoryBase.SetActive(false);
    }

    public void AcquireItem(Item _item, int _count =1){ //아이템 습득{

        if (Item.ItemType.Equipment != _item.itemType)
        {      //아이템타입이 이큅먼트가아니라면

            for( int i = 0; i < slots.Length; i++ ){                 //for
            
                if(slots[i].item != null){                           //슬롯i번째에 이이템이 널이아니라면 
                
                    if(slots[i].item.itemName == _item.itemName){    //아이템 슬롯에 네임이 있다면 
                    
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < slots.Length; i++){                 //for
        
            if (slots[i].item ==null){
            
                slots[i].AddItem(_item,_count);
                return;
            }
        }   
    }

    public void PanelOn()          
    {

            if(selecSlot[i].GetComponentInParent<Slot>().item != null)  //선택된슬롯의 아이템이  null 이 아니라면 
            {
                if ( (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) && !useCanclePanel.activeSelf)    //스페이스바나 엔터를 누를시 
                {
                    useCanclePanel.SetActive(true);             //판넬이켜진다 
                }
            }

    }

    void panelMove()
    {
        if(useCanclePanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))   //esc누르면 판넬거진다 
            {
                useCanclePanel.SetActive(false);

            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))   //아래화살표누르면 캔슬imㅎ가 선택된다 
            {
                useImg.gameObject.SetActive(false);
                cancelImg.gameObject.SetActive(true);

            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))     //동일
            {
                useImg.gameObject.SetActive(true);
                cancelImg.gameObject.SetActive(false);
            }
        }
    }

    private void EnterUse()
    {
        if (useImg.gameObject.activeSelf)
        {   //사용버튼이 활성화된상태라면 
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                if(i > 3)
                {
                    if(selecSlot[i].GetComponentInParent<Slot>().item.itemType != Item.ItemType.Equipment)
                    {
                        UseNomalItem();
                    }
                    else  //아이템타입이 이큅먼트면 이큅트슬롯실행한다 
                    {
                        EquipToStatus();
                    }
                    useImg.gameObject.SetActive(false);
                    useCanclePanel.SetActive(false);
                }
                else if (i <= 3)
                {
                    EquipToSlot();
                    useImg.gameObject.SetActive(false);
                    useCanclePanel.SetActive(false);
                }
            }
        }
        else if (cancelImg.gameObject.activeSelf)   //사용버튼이 활성화된상태라면 
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                cancelImg.gameObject.SetActive(false);
                CacleItem();
            }
        }
    }
    void UseNomalItem()
    {
        selecSlot[i].GetComponentInParent<Slot>().SlotUseItem();
    }

    private void CacleItem()
    {
        useCanclePanel.SetActive(false);
    }
    
    private void EquipToSlot()
    {
        for (int j = 1; j < selecSlot.Length; j++)
        {                 //for
            if (slots[j].item == null)
            {
                itemStatusAdd.DeAdDstatus();
                slots[j].AddItem(selecSlot[i].GetComponentInParent<EquipSlot>().item);
                selecSlot[i].GetComponentInParent<Slot>().ClearSlot();

                return;
            }
        }
    }

    


    ///<summary>
    ///착용아이템에따른 스테이터스 업데이트 
    ///</summary>
    ///<param name="equipslots[i]"> 1,2,3,4번슬롯에 각각들어갈 스테이터스 업데이트 효과를 부여함</param>

    private void EquipToStatus()
    {
            
            if(equipslots[3].item == null)
            {
                itemStatusAdd.Addstatus();
            }
        selecSlot[i].GetComponentInParent<Slot>().EquipSlotEquip();
        selecSlot[i].GetComponentInParent<Slot>().ClearSlot();
        //대충 3번슬롯아이템이없으면 
        // 에드스테이터스 함수를 실행한다 
        // 이큅슬롯에 장착한다
        // 슬롯을 비운다 함수 
        

        
                

    } 
    
}



