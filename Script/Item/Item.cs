using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject    //모노비해이블은 오브젝트에 붙여줘야 효력이 있지만 
{                                       //스크립터블 오브젝트는 상관없다

    public string itemName;         //아이템 이름
    public Sprite itemImage;        //아이템 이미지
    public GameObject itemPrefab;   //아이템 프리펩
    public ItemType itemType;       //아이템타입


    public string WeaponType;       //아이템 타입
    public string Description;








    
    public enum ItemType
    {
        Equipment,
        Used,
        Ingredient,
        ETC,
    }

}




