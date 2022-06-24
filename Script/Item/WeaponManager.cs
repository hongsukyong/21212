using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static bool WeaponChangeIng = false; //무기 중복교체 실행방지
    public static Transform currentWeapon;
    public static Animator currentWeaponAnim;


    [SerializeField]
    private float WeaponChageDelay; //무기고체 딜레이 
    [SerializeField]
    private float chageWeaponEndDelay; //



    [SerializeField]
    private Gun[] guns;

    [SerializeField]
    private CloseWeapon[] hands;

    [SerializeField]
    private CloseWeapon[] axes;


    

    private Dictionary<string, Gun> gunDictionary = new Dictionary<string, Gun>();
    private Dictionary<string, CloseWeapon> handDictionary = new Dictionary<string, CloseWeapon>();
    private Dictionary<string, CloseWeapon> axeDictionary = new Dictionary<string, CloseWeapon>();



    [SerializeField]
    public string currentWeaponType;

    [SerializeField]
    private GunContorller gunContorller;

    [SerializeField]
    private HandController handContorller;

    [SerializeField]
    private AxeController axeController;


    void Start()
    {
        for (int i = 0; i < guns.Length; i++) //
        {
            gunDictionary.Add(guns[i].gunName, guns[i]);
        }
        for (int i = 0; i < hands.Length; i++) //
        {
            handDictionary.Add(hands[i].CloseWeaponName, hands[i]);
        }
        for (int i = 0; i < axes.Length;  i++)
        {
            axeDictionary.Add(axes[i].CloseWeaponName, axes[i]);
        }

    }


    void Update()
    {
        if(!WeaponChangeIng)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                StartCoroutine(ChangeWeaponCoroutine("Hand", "맨손"));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                StartCoroutine(ChangeWeaponCoroutine("Gun", "Glock"));
                
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                StartCoroutine(ChangeWeaponCoroutine("Axe", "Axe"));
            }

        }
    }

    public IEnumerator ChangeWeaponCoroutine(string _type, string _name)
    {
        WeaponChangeIng = true;
        //currentWeaponAnim.SetTrigger("") //집어넣는 에니메이션

        yield return new WaitForSeconds(WeaponChageDelay);

        canclePreWeaponAction();
        WeaponChange(_type, _name);


        yield return new WaitForSeconds(chageWeaponEndDelay); 

        currentWeaponType = _type;
        WeaponChangeIng = false;
    }

    private void canclePreWeaponAction()
    {
        switch(currentWeaponType)
        {
            case "Gun":
                    gunContorller.CancleReload();
                    GunContorller.isActivate = false;
                break;
            case "Hand":
                    HandController.isActivate = false;
                break;
            case "Axe" :
                    AxeController.isActivate = false;
                break;

        }
    }
    
    private void WeaponChange(string _type, string _name)
    {
        if(_type == "Gun")
        {
            gunContorller.GundChange(gunDictionary[_name]);
            
        }
        else if(_type == "Hand")
        {
            handContorller.CloseWeaponChange(handDictionary[_name]);
        }
        else if (_type == "Axe")
        {
            axeController.CloseWeaponChange(axeDictionary[_name]);
        }

    }
}

