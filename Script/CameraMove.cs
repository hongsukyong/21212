using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public GameObject gotarget;
    // 플레이어 - 따라다니기위한 대상
    public float speed;
    // 따라다니는 속도
    public Vector3 difVal;
    // 벡터값 정의
    public int PlusX;
    public int PlusY;
    void Start()
    {

        difVal = transform.position - gotarget.transform.position;
        // difval 은 현제위치 - 목표위치

        
        difVal = new Vector3(Mathf.Abs(difVal.x) + PlusX, Mathf.Abs(difVal.y) +  PlusY,-10);
        //difval 선언한다 새로운 위치로 x는 difvax의 절대값, y는 difvaly의 절대값 , z는 -10으로
    }


    void Update()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, gotarget.transform.position+difVal, speed);
        //이것의 위치는 = 벡터3  러프하게 간다 (이것의 위치)에  (터갯의 위치 + difval) * speed 속도로
    }
}
