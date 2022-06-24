using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemEffect{
    public string itemName;     //아이템의 이름

    [Tooltip("HP,SP,EXP,MENTAL,HUNGRY,THIRSTY,DP,KARMA값만 사용가능")]
    public string[] part;         //부위

    public int[] num;             //수치

}


public class ItemaEffectDatabase : MonoBehaviour{

    [SerializeField]
    private ItemEffect[] itemEffect;

    private const string HP="HP",SP="SP",EXP = "EXP", MENTAL="MENTAL",HUNGRY="HUNGRY",THIRSTY="THIRSTY",DP = "DP",KARMA="KARMA";


    [SerializeField]
    private StatusController statusController; 

    [SerializeField]
    private WeaponManager weaponManager;

    public void UseItem(Item _item){

        if (_item.itemType == Item.ItemType.Equipment){
            //장착
            

            StartCoroutine(weaponManager.ChangeWeaponCoroutine(_item.WeaponType, _item.itemName));
        }

        else if(_item.itemType == Item.ItemType.Used){

            for ( int x = 0; x < itemEffect.Length; x++ ){

                if(itemEffect[x].itemName == _item.itemName){

                    for (int y = 0; y < itemEffect[x].part.Length; y++){

                        switch(itemEffect[x].part[y]){

                            case  "HP":

                                statusController.IncreaseHP(itemEffect[x].num[y]);
                                break;

                            case    "SP":   
                                statusController.IncreaseSP(itemEffect[x].num[y]);
                                break;

                            case    "SurvivalLevel":
                                statusController.IncreaseSurvivalLevel(itemEffect[x].num[y]);
                                break;

                            case    "MENTAL":
                                statusController.IncreaseMental(itemEffect[x].num[y]);
                                break;

                            case    "HUNGRY":
                                statusController.IncreaseHungry(itemEffect[x].num[y]);
                                break;

                            case    "THIRSTY":
                                statusController.IncreaseThirsty(itemEffect[x].num[y]);
                                break;

                            case    "DP":
                                statusController.IncreaseDp(itemEffect[x].num[y]);
                                break;

                            case    "KARMA":
                                statusController.IncreaseKarma(itemEffect[x].num[y]);
                                break;

                            default:
                                Debug.Log("잘못된 staus부위가 적용되고있습니다");
                                break;
                        }
                        Debug.Log("을사용했습니다");
                    }
                    return;
                }
            }
        }
        Debug.Log("일치하는아이템Name없음");
    }
}
