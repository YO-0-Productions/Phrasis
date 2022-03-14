using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private int value;

    public int Value
    {
        get { return value; }
        set 
        { 
            this.value = value;
            RefreshText();
        }
    }

    private void Start()
    {
        RefreshText();
    }

    private void RefreshText()
    {
        gameObject.GetComponentInChildren<TextMeshPro>().text = value.ToString();
    }
}
