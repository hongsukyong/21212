using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TimeFlow : MonoBehaviour
{

    public TextMeshProUGUI TimeText;
    public float InGameTime;
    public float InGameMin;
    public float InGamesec;

    public float fatigue;

    [SerializeField]
    StatusController statusController;


    private void Awake() 
    {
        fatigue = 1f;
    }
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        TimeFatigue();
        TimeText.text = InGameTime.ToString("00") + " : " + InGameMin.ToString("00");
        Flow();

    }

    void Flow()
    {
        InGamesec += Time.deltaTime*60;

        if(InGamesec > 59)
        {
            InGameMin++;
            InGamesec = 0;
        }
        if(InGameMin > 59)
        {
            InGameTime++;
            InGameMin = 0;
        }
        if(InGameTime > 23)
        {
            InGameTime = 0;
        }
    }

    void TimeFatigue()
    {
        if(InGameTime > 18 || InGameTime < 9)
        {
            fatigue = 2f;
        }
        else if (InGameTime > 12 && InGameTime < 18)
        {
            fatigue = 0.5f;
        }
    }
}
