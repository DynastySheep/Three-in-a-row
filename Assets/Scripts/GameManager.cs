using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Players currentPlayer;
    public Players startingPlayer;
    [SerializeField] private Cell[] cells;

    [Header("Player Related")]
    [SerializeField] private PlayerDisplay[] playerDisplays;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        ChooseFirstPlayer();
    }

    private void ChooseFirstPlayer()
    {
        currentPlayer = (Players)Random.Range(0,2);
        startingPlayer = currentPlayer;

        if (playerDisplays != null)
        {
            if (startingPlayer == 0)
            {
                playerDisplays[0].ChangeSymbol(0);
                playerDisplays[1].ChangeSymbol(1);
            }
            else
            {
                playerDisplays[0].ChangeSymbol(1);
                playerDisplays[1].ChangeSymbol(0);
            }
        }
    }

    public void SwitchPlayer()
    {
        if (currentPlayer == Players.P1)
            currentPlayer = Players.P2;
        else
            currentPlayer = Players.P1;
    }

    public void CheckWinCondition()
    {
        for (int row = 0; row < 3; row++)
        {
            if (CheckHorizontal(row))
            {
                ResetBoard();
                return;
            }
        }

        for (int column = 0; column < 3; column++)
        {
            if (CheckVertical(column))
            {
                ResetBoard();
                return;
            }
        }

        if (CheckDiagonal())
        {
            ResetBoard();
            return;         
        }

        SwitchPlayer();
    }

    private bool CheckHorizontal(int row)
    {
        int startIndex = row * 3;
        CellState startCell = cells[startIndex].cellState;

        if (startCell == CellState.Empty)
            return false;

        for (int column = 1; column < 3; column++)
        {
            int currentIndex = startIndex + column;

            if (cells[currentIndex].cellState != startCell)
                return false;
        }

        Debug.Log("(Horizontal Check)The winner is : " +startCell);

        return true;
    }

    private bool CheckVertical(int column)
    {
        CellState startCell = cells[column].cellState;

        if (startCell == CellState.Empty)
            return false;

        for (int row = 1; row < 3; row++)
        {
            int currentIndex = row * 3 + column;

            if (cells[currentIndex].cellState != startCell)
                return false;
        }

        Debug.Log("(Vertical Check)The winner is : " +startCell);
        return true;
    }

    private bool CheckDiagonal()
    {
        CellState centerCell = cells[4].cellState;

        if (centerCell == CellState.Empty)
            return false;

        if (cells[0].cellState == centerCell && cells[8].cellState == centerCell || cells[2].cellState == centerCell && cells[6].cellState == centerCell)
        {
            Debug.Log("(Diagonal Check)The winner is : " +centerCell);
            return true;
        }

        return false;
    }

    private void ResetBoard()
    {
        foreach (Cell cell in cells)
        {
            cell.cellState = CellState.Empty;
            cell.ClearCell();
            ChooseFirstPlayer();
        }
    }
}

public enum Players
{
    P1,
    P2,
    AI
}