using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builind : MonoBehaviour
{
    // 스캔오브젝트가 어미콜라이더인 상태에서 e를 누른다면 빌딩면이 사라지고 
    // 맵의 컬러가 회색이되며 반투명 or 어두운색 으로 변하면서 빌딩 이너부분이 게임오브젝트가 트루로 변한다 
    //필요한 요소는 
    // 어미건물 , 건물이너부분 

    [SerializeField]
    private GameObject outerBuilding;
    [SerializeField]
    private GameObject innderBuilding;
    [SerializeField]
    private GameObject door;
    [SerializeField]
    private GameObject currenChar;
    [SerializeField]
    private Material metarial;
    [SerializeField]
    private GameObject currenMap;

    ActionController actionController;

    
    public bool Atcive = false;
    
    private void Awake() 
    {
        Outer();
        actionController = FindObjectOfType<ActionController>();


    }

    private void Update() 
    {

    }


    public void Inner()
    {
        Debug.Log("Inner");
        StartCoroutine(actionController.DummyLoading(1.2f));
        Atcive = true;
        outerBuilding.SetActive(false);
        currenChar.SetActive(false);
        innderBuilding.SetActive(true);
        metarial.color = new Color(0.3f,0.3f,0.3f);
        ChangeLayersRecursively(currenMap.transform, "nonInterrect");

    }
    public void Outer()
    {
        if(Atcive)
        {
            StartCoroutine(actionController.DummyLoading(1.2f));
        }

        Debug.Log("Outer");
        Atcive =  false;
        outerBuilding.SetActive(true);
        currenChar.SetActive(true);
        innderBuilding.SetActive(false);
        metarial.color = new Color(1, 1, 1);
        ChangeLayersRecursively(currenMap.transform, "Item");
        
    }


    public void ChangeLayer(string name)
    {
        ChangeLayersRecursively(transform, name);
    }

    public void ChangeLayersRecursively(Transform trans, string name)
    {
        trans.gameObject.layer = LayerMask.NameToLayer(name);
        foreach (Transform child in trans)
        {
            ChangeLayersRecursively(child, name);
        }
    }
}
