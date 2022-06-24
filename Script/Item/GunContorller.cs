using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunContorller : MonoBehaviour
{
    public static bool isActivate = false;

    public PlayerController playerController;
    [SerializeField]
    public Gun currentGun;                  //현재 총
    private float currentFireRate;          //연사속도 계산

    [SerializeField]
    private Camera maninCamera;             //카메라

    private bool Reloadind;                 //로딩 불값

    private AudioSource audioSource;

    [SerializeField]
    private Vector2 originPos;
    // 정조준 구현하기위한 원래 위치값, 정조준하고나서 돌아오기위해서

    private bool fineShightMode;



    private RaycastHit2D bulletHinfo;

    private Vector3 bulletMove;

    public GameObject Char;

    float h;

    [SerializeField]
    private GameObject hitEffect;

    [SerializeField]
    public Text text;


    private void Start() {
        audioSource = GetComponent<AudioSource>();
        

        WeaponManager.currentWeapon = currentGun.GetComponent<Transform>();
        WeaponManager.currentWeaponAnim = currentGun.anim;
    }



    void Update()
    {

        if(isActivate){
            GunFireRateCalc();
            TryFire();
            TryReload();
            TryFindShights();

            Debug.DrawRay(Char.GetComponent<Rigidbody2D>().position, Vector2.right * playerController.Seeing, new Color(0,1,0) , 2f);

            text.text = currentGun.currentBulletCount.ToString()  + " / " + currentGun.carryBulletCount.ToString();
        }
        else
            text.text = null;
    }

    private void TryReload()
    {
        if(Input.GetKeyDown(KeyCode.R) && !Reloadind && currentGun.currentBulletCount < currentGun.reloadBulletCount)
        {
            StartCoroutine(ReloadCoroutine());
        }
    }

    public void CancleReload()
    {
        if(Reloadind)
        {
            StopAllCoroutines();
            Reloadind = false;
        }
    }

    private void GunFireRateCalc()
    {
        if(currentFireRate > 0)
        {
            currentFireRate -= Time.deltaTime * 60;
        }
    }

    private void TryFire()
    {
        if(Input.GetButtonDown("Fire1") && currentFireRate <= 0 && !Reloadind)
        {
            Fire();
            currentGun.anim.SetBool("Fire", true);
        }
        else
        {
            currentGun.anim.SetBool("Fire", false);
        }
    }

    private void Fire()
    {
        if(!Reloadind)
        {
            if(currentGun.currentBulletCount > 0)
            {   
                Shoot();
            }
            else 
            {
                StartCoroutine(ReloadCoroutine());
            }
        }
    }

    private void Shoot()
    {
        currentGun.currentBulletCount--;
        currentFireRate = currentGun.firerate; // 연사속도 재계산
        PlaySE(currentGun.fireSound);
        currentGun.muzzleFlash.Play();
        Hit();
        Debug.Log("발사");
        
    }

    private void Hit()
    { 
        

        RaycastHit2D bulletHinfo = Physics2D.Raycast(Char.GetComponent<Rigidbody2D>().position, Vector2.right * playerController.Seeing , 20f, LayerMask.GetMask("Item"));

        if(bulletHinfo.collider != null)
        {
            Debug.Log(bulletHinfo.transform.name);
            GameObject clone = Instantiate(hitEffect, bulletHinfo.point,Quaternion.LookRotation(bulletHinfo.normal));
            Destroy(clone, 0.3f);
        }
    }

    private void PlaySE(AudioClip _clip)
    {
        audioSource.clip = _clip;
        audioSource.Play();
    }


    IEnumerator ReloadCoroutine()
    {
        if (currentGun.carryBulletCount > 0) // 현제 들고있는 총알이 0보다 크다면 
        {
            Reloadind = true;

            currentGun.anim.SetTrigger("Reload"); //제장전 에니메이션 

            //버리는탄창안에있는 탄알을 같이 버리지 않도록..
            currentGun.carryBulletCount += currentGun.currentBulletCount;
            currentGun.currentBulletCount = 0;

            yield return new WaitForSeconds(currentGun.reloadTime);

            if(currentGun.carryBulletCount >= currentGun.reloadBulletCount)         //현제 가지고있는 총알이 리로드불렛카운트보다 크다면
            {
                currentGun.currentBulletCount = currentGun.reloadBulletCount;       //현제불렛카운트는 리로드불렛 카운트다;
                currentGun.carryBulletCount -= currentGun.reloadBulletCount;        //가지고 있는 불렛 카운트에서 뺀다 리로드불렛만큼
                
            }
            else 
            {
                currentGun.currentBulletCount = currentGun.carryBulletCount;
                currentGun.carryBulletCount = 0;
            }

            Reloadind = false;
        }
        else if (currentGun.currentBulletCount == 0)
        {
            Debug.Log("총알없음사운드");
            PlaySE(currentGun.EmptySound);
        }


    }

    private void TryFindShights()
    {
        if(Input.GetButton("Fire2"))
        {
            FineShight();
        }
        else 
        {
            maninCamera.orthographicSize = 7;
            maninCamera.GetComponent<CameraMove>().difVal = new Vector3(5, 4.418f , -10);
        }
    }

    private void FineShight()
    {
        fineShightMode = !fineShightMode;
        maninCamera.orthographicSize = 4; 
        maninCamera.GetComponent<CameraMove>().difVal = new Vector3(3.84f, 1, -10);

        //currentGun.anim
    }

    public void GundChange(Gun _gun)
    {
        if(WeaponManager.currentWeapon != null) //
        {
            WeaponManager.currentWeapon.gameObject.SetActive(false);
        }


        
        currentGun = _gun;
        WeaponManager.currentWeapon = currentGun.GetComponent<Transform>();
        WeaponManager.currentWeaponAnim = currentGun.anim;
        

        
        
        currentGun.transform.localPosition = Vector2.zero;
        currentGun.gameObject.SetActive(true);

        isActivate = true;
    }

}
