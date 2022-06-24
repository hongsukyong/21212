using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CloseWeaponController : MonoBehaviour{


    [SerializeField]
    protected CloseWeapon CurrentCloseWeapon;

    public PlayerController playerController;



    protected bool AttackIng = false;
    protected bool isSwing = false;


    void Start()
    {


    }



    protected void TryAttack()
    {
        if (Input.GetButton("Fire1") && !Inventory.inventoryActivated)
        {
            if (!AttackIng)

                StartCoroutine(AttackCoroutine());

        }
    }

    protected IEnumerator AttackCoroutine()
    {


        AttackIng = true;
        CurrentCloseWeapon.anim.SetTrigger("Attack");

        yield return new WaitForSeconds(CurrentCloseWeapon.attackDeleyActive);
        isSwing = true;

        StartCoroutine(HitCoroutine());

        yield return new WaitForSeconds(CurrentCloseWeapon.attackDeleyDeactive);
        isSwing = false;

        yield return new WaitForSeconds(CurrentCloseWeapon.attackDeley - CurrentCloseWeapon.attackDeleyActive - CurrentCloseWeapon.attackDeleyDeactive);
        AttackIng = false;


    }

    protected abstract IEnumerator HitCoroutine();


    protected bool CheckObject()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector2.right * playerController.Seeing, CurrentCloseWeapon.range, LayerMask.GetMask("Item"));

        if (rayHit.collider != null)
        {
            Debug.Log(rayHit.transform.name);
            return true;
        }
        else
            return false;
    }

    public virtual void CloseWeaponChange(CloseWeapon _closeWeapon) //완성함수이지만 추가편집이 가능하다 virtual
    {
        if (WeaponManager.currentWeapon != null) //
        {
            WeaponManager.currentWeapon.gameObject.SetActive(false);
        }



        CurrentCloseWeapon = _closeWeapon;
        WeaponManager.currentWeapon = CurrentCloseWeapon.GetComponent<Transform>();
        WeaponManager.currentWeaponAnim = CurrentCloseWeapon.anim;




        CurrentCloseWeapon.transform.localPosition = Vector2.zero;
        CurrentCloseWeapon.gameObject.SetActive(true);

    }
}
