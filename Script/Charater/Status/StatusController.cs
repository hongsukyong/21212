using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour{

    ActionController actionController;
    PlayerController playerController; //플레이어컨트롤러 

    private float dlaytime = 3f;       //딜레이를주기위한 변수
    private float TickDlayTime;        //딜레이를주기위한 변수2

    #region hp
    // 시작체력 , 현제채력
    [Tooltip("체력총량")]
    [SerializeField] 
    public int Hp;

    public int currentHp;
    #endregion 

    #region sp
    //시작 스테미나, 현제스테미나 
    [Tooltip("스테미너총량")]
    [SerializeField]
    public int Sp;
    public float currentSp;
    
    //스테미나 회복속도
    [Tooltip("스테미너 회복속도")]
    [SerializeField]
    public float SteminaIncreeseSpeed;



    public TimeFlow timeFlow;


    //스테미나 바로회복되지않게 딜레이 계산에 사용할 딜레이타임
    [Tooltip("스테미나 회복에 걸리는 시간")]
    [SerializeField]
    private int SteminaDelay;
    private int currentSteminaDelayTime;

    //스테미나가 감소할지 여부, 스테미나를 사용했는가 
    [SerializeField]
    private bool spUsed;
    #endregion

    #region survivalLevel
    //경험치
    [Tooltip("경험치")]
    [SerializeField]
    private int survivalLevel; //경험치
    public int intsurvivalLevel; //레벨 
    #endregion

    #region Mental
    //멘탈 
    [Tooltip("멘탈총량")]
    [SerializeField]
    public int Mental;
    public int currentMental;

    //멘탈회복
    [SerializeField]
    private int mentalIcreese;
    #endregion
    
    #region Hungry Thirsty
    //배고픔 
    [Tooltip("배고픔총량")]
    [SerializeField]
    public int Maxhungry;
    public float currentHungry;

    [Tooltip("배고픔 줄어드는 시간")]
    [SerializeField]
    private int HungryDecreasetTme;
    private int currnetHungryDecreaseTime;

    [Tooltip("목마름")]
    [SerializeField]
    public int thirsty;
    public float currentThirsty;

    [Tooltip("배고픔줄어드는 시간")]
    [SerializeField]
    private int ThirstyDecreasetime;
    private int currentThirstyDecreaseTime;
    #endregion

    #region  Dp
    //방어력
    [Tooltip("방어력")]
    [SerializeField]
    private int Dp;
    private int currentDp;

    #endregion

    #region   Karma
    [Tooltip("카르마")]
    [SerializeField]

    public int Karma;
    #endregion

    #region  //슬라이더 그룹 지정 
    [SerializeField]
    public Slider[] SliderGroups;
    private const int HP=0, SP=1, DP=2, HUNGRY=3, THIRSTY =4, MENTAL = 5, SurvivalLevel = 6, KARMA = 7;
    #endregion



    //인트 시작값 지정
    private void Start(){
        
        currentHp = Hp;
        currentDp = Dp;
        currentSp = Sp;
        currentThirsty = thirsty;
        currentMental = Mental;
        currentHungry = Maxhungry;
        Karma = 50;
        intsurvivalLevel  = 1;

        
    }

    //
    private void Update() {
        Hungry();
        Tirsty();
        GaugeUpdate();
        SPRecover();

        if(Sp > 0.1){
            SPRechargeTime();
            }

        else{
            TickDlayTime += Time.deltaTime;

                if(TickDlayTime > dlaytime){
                    SPRechargeTime();
                    TickDlayTime =0;
                }
            }
    }

    //헝그리 깎이는속도 조절
    private void Hungry() {

        if(currentHungry > 0)
        {
            if(currnetHungryDecreaseTime <= HungryDecreasetTme)
                currnetHungryDecreaseTime++;
            else{

                currentHungry -= Time.deltaTime;
                currnetHungryDecreaseTime = 0;
            }
        }
        else 
            Debug.Log("아주배고픕니다.");

    }

    // 떨스티깎이는속도조절
    private void Tirsty() {
    
        if (currentThirsty > 0)
        {
            if (currentThirstyDecreaseTime <= ThirstyDecreasetime)
                currentThirstyDecreaseTime++;
            else
            {
                currentThirsty -= Time.deltaTime;
                currentThirstyDecreaseTime = 0;
            }
        }
        else
            Debug.Log("아주목마릅니다.");
    }


    //게이지업데이트 
    private void GaugeUpdate() 
    {
        

        SliderGroups[HP].value = (float)currentHp / Hp;
        SliderGroups[SP].value = (float)currentSp / Sp;
        SliderGroups[DP].value = (float)currentHp / Hp;
        SliderGroups[HUNGRY].value = (float)currentHungry / Maxhungry;
        SliderGroups[THIRSTY].value = (float)currentThirsty / thirsty;
        SliderGroups[MENTAL].value = (float)currentMental / Mental; //수정필요
        SliderGroups[SurvivalLevel].value = (float)SurvivalLevel / SurvivalLevel ;  //수정필요
        SliderGroups[KARMA].maxValue = 100;
        SliderGroups[KARMA].value =  Karma;
    }

    public void DecreeseStamina(float _count)
    {
        spUsed = true;
        currentSteminaDelayTime = 0;

        if(currentSp - _count > 0)       
            currentSp -= _count*timeFlow.fatigue * 0.01f ;
        else 
            currentSp = 0;
    }
    
    private void SPRechargeTime()
    {
        
            if(spUsed){
                if(currentSteminaDelayTime < SteminaDelay){
                    currentSteminaDelayTime++;}
                else{
                    spUsed = false;
                }
            }
        
        
    }

    private void SPRecover(){
        if(!spUsed && currentSp < Sp){
            currentSp += SteminaIncreeseSpeed * 0.01f;
        }
    }

#region 아이템사용으로인한 회복감소 

    public void IncreaseHP(int _count){


            if(currentHp + _count < HP)
                currentHp += _count;
            else
                currentHp = HP;

    }
    public void DecreaseHP(int _count){
        
        if(currentDp > 0)
        {
            DecreaseDP(_count);
            return;
        }
        currentHp -= _count;

        if(currentHp <= 0)
            Debug.Log("HP가 0입니다");
    }

    public void IncreaseSP(int _count)
    {
        if (currentSp + _count < Sp)
        {
            currentSp += _count;
        }
            
        else
        {
            Debug.Log("메롱23");
        }
    }
    public void DecreaseSP(int _count)
    {
        if (currentSp > 0)
        {
            currentSp = 0;
            return;
        }
        currentSp -= _count;

        if (currentSp <= 0)
            Debug.Log("SP가 0입니다");
    }

    public void IncreaseDp(int _count)
    {

        if (currentDp + _count < DP)
            currentDp += _count;
        else
            currentDp = Dp;
    }
    public void DecreaseDP(int _count)
    {
        if(currentDp - _count <= 0){
            currentDp = 0;
            return;
        }
        currentDp -= _count;

        if (currentDp <= 0)
            Debug.Log("DP가 0입니다");
    }

    public void IncreaseHungry(int _count)
    {

        if (currentHungry + _count < HUNGRY)
            currentHungry += _count;
        else
            currentHungry = Maxhungry;
    }
    public void DecreaseHungry(int _count)
    {
        if(currentHungry - _count <= 0){
            currentHungry = 0;

        }
        currentHungry -= _count;

        if (currentHungry <= 0)
            Debug.Log("HUNGRY가 0입니다");
    }

    public void IncreaseThirsty(int _count)
    {

        if (currentThirsty + _count < HUNGRY)
            currentThirsty += _count;
        else
            currentThirsty = thirsty;
    }
    public void DecreaseThirsty(int _count)
    {
        if (currentThirsty - _count <= 0){
            currentThirsty = 0; 
            return;
        }

        currentThirsty -= _count;

        if (currentThirsty <= 0)
            Debug.Log("Thirsty가 0입니다");
    }

    public void IncreaseKarma(int _count)
    {
        Karma += _count;
    }
    public void DecreaseKarma(int _count)
    {
        Karma -= _count;
    }

    public void IncreaseMental(int _count)
    {
        if(_count + currentMental < Mental){
            currentMental += _count;
            }
        else{
            currentMental = Mental;
            }
    }
    public void DecreaseMental(int _count)
    {
        currentMental -= _count;
    }
    
    public void IncreaseSurvivalLevel(int _count)
    {
        if (survivalLevel +_count < SliderGroups[SurvivalLevel].maxValue){

            survivalLevel += _count;
            }
        else{
            intsurvivalLevel++;
            SliderGroups[SurvivalLevel].value = 0;
            SliderGroups[SurvivalLevel].value += (survivalLevel + _count - SliderGroups[SurvivalLevel].maxValue);
        }
        
    }
#endregion

}
