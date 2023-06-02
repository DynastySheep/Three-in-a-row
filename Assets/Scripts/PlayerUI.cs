using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class PlayerDisplay : MonoBehaviour
{
    [SerializeField] private Image currentSymbol;
    [SerializeField] private Sprite[] symbols;
    [SerializeField] private TextMeshProUGUI playerTitle;

    private void Start()
    {
        playerTitle = GetComponent<TextMeshProUGUI>();
    }


    public void ChangeSymbol(int index)
    {
        currentSymbol.sprite = symbols[index];
    }

    public void SetAIText()
    {
        playerTitle.text = "AI";
    }
}