using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : StatusController{

    public static PlayerStat instans;

    public StatusController statusController;

    public int[] needEXP;
    

    void Start(){

        instans = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
