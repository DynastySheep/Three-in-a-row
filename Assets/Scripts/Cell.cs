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
        int startIndex = (int)gameManager.startingPlayer;
        int currentPlayer = (int)gameManager.currentPlayer;

        if (currentPlayer == startIndex)
        {
            cellState = CellState.X;
            spriteReference.sprite = cellSprites[0];
        }
        else
        {
            cellState = CellState.O;
            spriteReference.sprite = cellSprites[1];
        }
    }

    public void ClearCell()
    {
        spriteReference.sprite = null;
    }
}

public enum CellState
{
    Empty,
    X,
    O
}