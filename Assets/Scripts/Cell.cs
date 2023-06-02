using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Cell : MonoBehaviour
{
    public CellState cellState;

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
        if (cellState == CellState.Empty)
        {
            Debug.Log("Cell Marked");
            MarkCell();
            gameManager.CheckWinCondition();
        }
    }

    private void MarkCell()
    {
        int playerIndex = (int)gameManager.currentPlayer;

        if (playerIndex == 0)
        {
            cellState = CellState.X;
        }
        else if (playerIndex == 1 || playerIndex == 2)
        {
            cellState = CellState.O;
        }

        spriteReference.sprite = cellSprites[playerIndex];
    }
}

public enum CellState
{
    Empty,
    X,
    O
}