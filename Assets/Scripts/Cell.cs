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
    private BoardManager boardManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        boardManager = gameManager.GetComponent<BoardManager>();
    }

    private void OnMouseDown()
    {
        if (cellState == CellState.Empty)
        {
            MarkCell();
        }
    }

    public void MarkCell()
    {
        if (!gameManager.roundFinished)
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

            boardManager.MarkCellAsUsed(this);
            gameManager.CheckWinCondition();
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