using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dd : MonoBehaviour
{
    public GameObject start;
    public GameObject ING;
    TimeFlow timeFlow;

    void Start()
    {
        timeFlow = FindObjectOfType<TimeFlow>();
    }

    // Update is called once per frame
    void Update()
    {
        if(start != null)
        {
            if(timeFlow.InGameTime < 20)
                start.SetActive(false);

            else if(timeFlow.InGameTime > 20)
                start.SetActive(true);
        }
    }
}
