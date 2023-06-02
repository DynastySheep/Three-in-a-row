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

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void OnMouseDown()
    {
        if (cellState != 0)
            return;

        MarkCell();
        gameManager.SwitchPlayer();
    }

    private void MarkCell()
    {
        cellState = (CellState)gameManager.currentPlayer;
        int currentStateIndex = (int)cellState;
        spriteReference.sprite = cellSprites[currentStateIndex];
    }
}

public enum CellState
{
    Empty,
    X,
    O
}