using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatP : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI HpText;
    [SerializeField]
    private TextMeshProUGUI SpText;
    [SerializeField]
    private TextMeshProUGUI SpincreaseText;
    [SerializeField]
    private TextMeshProUGUI HungryText;
    [SerializeField]
    private TextMeshProUGUI ThirstyText;
    [SerializeField]
    private TextMeshProUGUI KarmaText;
    [SerializeField]
    private TextMeshProUGUI MentalText;

    [SerializeField]
    private TextMeshProUGUI SuvLvText;

    
    public TextMeshProUGUI buff;

    public StatusController statusController;

    void Start()
    {
        
    }


    void Update()
    {
        HpText.text = "HP : " + statusController.currentHp + "/" + statusController.Hp;
        SpText.text = "스테미너 :" + statusController.currentSp.ToString("F0") + "/" + statusController.Sp;
        SpincreaseText.text = "스테미나 회복속도 : "  +statusController.SteminaIncreeseSpeed.ToString();
        HungryText.text = "배고픔 : " +statusController.currentHungry.ToString("F0") + "/" + statusController.Maxhungry;
        ThirstyText.text = "목마름 : " +statusController.currentThirsty.ToString("F0") + "/" + statusController.thirsty;
        KarmaText.text = "카르마 : " +statusController.Karma.ToString();
        MentalText.text = "맨탈 : " +statusController.currentMental + "/" + statusController.Mental;
        SuvLvText.text = "생존레벨 : " + statusController.intsurvivalLevel.ToString();
        BuffController();
    }

    void BuffController()
    {
        //if(Buff == null)
        buff.text = "";
    }
}
