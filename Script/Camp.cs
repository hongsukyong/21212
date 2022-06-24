using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Camp : MonoBehaviour
{
    [SerializeField]
    public GameObject campPanel;


    ActionController actionController;


    [SerializeField]
    private TextMeshProUGUI leftturntext;
    private int leftturn;
    public TimeFlow timeFlow;
    [SerializeField]
    PlayerController playerController;


    [SerializeField]
    GameObject createPanel;
    [SerializeField]
    GameObject mainPanel;

    public bool CampING;

    private GameObject bornFire;
    public GameObject bornFireing;






    private void Awake() 
    {

        actionController = FindObjectOfType<ActionController>();
    }


    // Update is called once per frame
    void Update()
    {

        
        CampING = campPanel.activeSelf;
        
        if(playerController.scanObject != null)
        {
            if(playerController.scanObject.gameObject.name.Contains("Fire"))
            {
                bornFire = playerController.scanObject.gameObject;
                bornFireing = bornFire.GetComponentInParent<dd>().ING.gameObject;
                


            }
            else 
            {
                bornFire =null;
            }
        }

        if (CampING)
        {
            timeFlow.TimeText.gameObject.SetActive(false);

        }
        else
            timeFlow.TimeText.gameObject.SetActive(true);





    }



    public void CampStart()
    {
        CampING= true;
        campPanel.SetActive(true);
        bornFireing.gameObject.SetActive(true);

        Destroy(bornFire);

        leftturn = 10;
        leftturntext.text = "남은 활동 : "+ leftturn.ToString("D2");

        StartCoroutine(CampPosition());
    }

    void UseTurn()
    {
        
    }

    public void EndCamp()
    {
        campPanel.SetActive(false);
        Destroy(bornFireing);
        timeFlow.InGameTime = 8;
        timeFlow.InGameMin = 0;
        timeFlow.InGamesec = 0;
        
    }
    public void Create() 
    {
        createPanel.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void Idle()
    {
        mainPanel.SetActive(true);
        createPanel.SetActive(false);
    }

    IEnumerator CampPosition()
    {
        StartCoroutine(actionController.DummyLoading());
        playerController.transform.position = new Vector2(bornFire.transform.position.x-3 ,  playerController.rigid.position.y);
        
        yield return new WaitForSeconds(1.5f);
        //로드더미를 배열로 만들어서 switch( karma> i)case 방식으로 카르마에따라서 나오는 이미지가 달라지도록 설정하면 된다.
    }


}
