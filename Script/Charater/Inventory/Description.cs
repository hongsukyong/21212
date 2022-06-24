using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Description : MonoBehaviour
{
    [SerializeField] 
    private GameObject description;

    [SerializeField]
    private TextMeshProUGUI NameText;

    [SerializeField]
    private TextMeshProUGUI Descriptiontext;

    [SerializeField]
    private Inventory inventory;

    private void Start() 
    {

    }

    void Update()
    {

        itemName();


    }

    private void itemName()
    {
        if (inventory.selecSlot[inventory.i].GetComponentInParent<Slot>().item == null)
        {
            NameText.text = "";
            Descriptiontext.text = "";
        }
        else
        {
            NameText.text = inventory.selecSlot[inventory.i].GetComponentInParent<Slot>().item.itemName.ToString();
            Descriptiontext.text = inventory.selecSlot[inventory.i].GetComponentInParent<Slot>().item.Description.ToString();
        }
        description.SetActive(inventory.selecSlot[inventory.i].GetComponentInParent<Slot>().item != null);
    }


}
