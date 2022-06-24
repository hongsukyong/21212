using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour{

    static public DragSlot instans;

    public Slot dragSlot;

    [SerializeField]
    private Image imgaeItem;

    private void Start() {
        instans = this;
    }

    public void DragSetIMage(Image _imgaeItem){
        imgaeItem.sprite = _imgaeItem.sprite;
        SetColor(1);

    }

    public void SetColor(float _alpha){
        Color color = imgaeItem.color;
        color.a = _alpha;
        imgaeItem.color  = color;
    }
}
