using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour
{
    [SerializeField] private Image currentSymbol;
    [SerializeField] private Sprite[] symbols;


    public void ChangeSymbol(int index)
    {
        currentSymbol.sprite = symbols[index];
    }
}