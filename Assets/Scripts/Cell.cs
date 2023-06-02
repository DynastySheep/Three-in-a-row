using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Cell : MonoBehaviour
{
    [SerializeField] private CellState cellState;


    [Header("Sprite Settings")]
    [SerializeField] private SpriteRenderer spriteReference;
    [SerializeField] private Sprite[] cellSprites;

    private void OnMouseDown()
    {
        if (cellState != 0)
            return;

        MarkCell();
    }

    private void MarkCell()
    {
        cellState = CellState.O;
        int currentStateIndex = (int)cellState;
        spriteReference.sprite = cellSprites[currentStateIndex - 1];

        Debug.Log(this.gameObject.name);
    }
}

public enum CellState
{
    Empty,
    X,
    O
}