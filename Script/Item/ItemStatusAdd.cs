using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemStatus
{
    
    public Item item;

    public ItemStatusType[] Type;
    public int[] Add;

    

    public enum ItemStatusType
    {
        HP,
        SP,
        SPIncrease,
        MentalIncrease,
        HUNGRY,
        HUNGRYDecrease,
        KarmaDeIncrease,
    }

}

public class ItemStatusAdd : MonoBehaviour
{
    
    [SerializeField] 
    ItemStatus[] itemStatus;


    

    [SerializeField]
    Inventory inventory;

    [SerializeField]
    private StatusController statusController;


    public void Addstatus()
    {
        for(int y = 0; y < itemStatus.Length;   y++) 
        {
            if(itemStatus[y].item == inventory.selecSlot[inventory.i].GetComponentInParent<Slot>().item)
            {
                for (int x = 0; x < itemStatus.Length; x++)
                {

                    if(itemStatus[y].Type[x] == ItemStatus.ItemStatusType.HP)
                    {
                        
                        statusController.Hp += itemStatus[y].Add[x];
                        Debug.Log("체력총량");
                        break;
                    }
                    else if (itemStatus[y].Type[x] == ItemStatus.ItemStatusType.SP)
                    {

                        statusController.Sp += itemStatus[y].Add[x];
                        Debug.Log("스테미너 총량");
                        break;
                    }
                    else if (itemStatus[y].Type[x] == ItemStatus.ItemStatusType.MentalIncrease)
                    {

                        statusController.currentMental += itemStatus[y].Add[x];
                        Debug.Log("스테미너 총량");
                        break;
                    }

                }// 대충 아이템이름이 맞으면 추가능력치 부여한걸 실행해서 스테이너나 hp를 올린다 는스크립트 
            }
        }
    }
    public void DeAdDstatus()
    {
        for (int y = 0; y < itemStatus.Length; y++)
        {
            if (itemStatus[y].item == inventory.selecSlot[inventory.i].GetComponentInParent<Slot>().item)
            {
                for (int x = 0; x < itemStatus.Length; x++)
                {

                    if (itemStatus[y].Type[x] == ItemStatus.ItemStatusType.HP)
                    {

                        statusController.Hp -= itemStatus[y].Add[x];
                        Debug.Log("체력총량");
                        break;
                    }
                    else if (itemStatus[y].Type[x] == ItemStatus.ItemStatusType.SP)
                    {

                        statusController.Sp -= itemStatus[y].Add[x];
                        Debug.Log("스테미너 총량");
                        break;
                    }
                    else if (itemStatus[y].Type[x] == ItemStatus.ItemStatusType.MentalIncrease)
                    {

                        statusController.currentMental -= itemStatus[y].Add[x];
                        Debug.Log("스테미너 총량");
                        break;
                    }

                }// 대충 아이템이름이 맞으면 추가능력치 부여한걸 실행해서 스테이너나 hp를 올린다 는스크립트 
            }
        }
    }

    
}


