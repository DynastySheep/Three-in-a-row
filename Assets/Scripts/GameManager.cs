using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Players currentPlayer;
    public Players startingPlayer;
    public List<Players> activePlayers = new List<Players>();
    public List<Cell> emptyCells = new List<Cell>();

    private int moveCount = 0;
    public bool roundFinished = false;

    [SerializeField] private Cell[] cells;
    [Header("Player Related")]
    [SerializeField] private PlayerDisplay[] playerUI;

    [SerializeField] private bool againstAI = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        PopulateEmptyCells();

        if (againstAI)
        {
            activePlayers.Add(Players.P1);
            activePlayers.Add(Players.AI);

            playerUI[1].SetAIText();
        }
        else
        {
            activePlayers.Add(Players.P1);
            activePlayers.Add(Players.P2);
        }

        ChooseFirstPlayer();
    }

    private void Update()
    {
        if (currentPlayer == Players.AI)
        {
            if (emptyCells.Count > 0)
            {
                int randomIndex = Random.Range(0, emptyCells.Count);
                Cell randomCell = emptyCells[randomIndex];
                randomCell.MarkCell();
            }
        }
    }

    private void PopulateEmptyCells()
    {
        if (emptyCells.Count > 0)
            emptyCells.Clear();

        emptyCells.AddRange(cells);
    }

    public void RemoveEmptyCell(Cell cell)
    {
        if (emptyCells.Contains(cell))
        {
            emptyCells.Remove(cell);
        }
    }

    private void ChooseFirstPlayer()
    {
        int randomIndex = Random.Range(0, activePlayers.Count);
        currentPlayer = (Players)activePlayers[randomIndex];
        startingPlayer = currentPlayer;

        if (playerUI != null)
        {
            if (startingPlayer == 0)
            {
                playerUI[0].ChangeSymbol(0);
                playerUI[1].ChangeSymbol(1);
            }
            else
            {
                playerUI[0].ChangeSymbol(1);
                playerUI[1].ChangeSymbol(0);
            }
        }
    }

    public void SwitchPlayer()
    {
        if (againstAI)
        {
            if (currentPlayer == Players.P1)
                currentPlayer = Players.AI;
            else
                currentPlayer = Players.P1;
        }
        else
        {
            if (currentPlayer == Players.P1)
                currentPlayer = Players.P2;
            else
                currentPlayer = Players.P1;
        }
    }

    public void CheckWinCondition()
    {
        moveCount++;

        for (int row = 0; row < 3; row++)
        {
            if (CheckHorizontal(row))
            {
                StartCoroutine(ResetBoard());
                return;
            }
        }

        for (int column = 0; column < 3; column++)
        {
            if (CheckVertical(column))
            {
                StartCoroutine(ResetBoard());
                return;
            }
        }

        if (CheckDiagonal())
        {
            StartCoroutine(ResetBoard());
            return;         
        }

        if (moveCount == 9 && !roundFinished)
        {
            Debug.Log("Nobody won");
            StartCoroutine(ResetBoard());
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

    private IEnumerator ResetBoard()
    {
        roundFinished = true;
        yield return new WaitForSeconds(3f);
        foreach (Cell cell in cells)
        {
            cell.cellState = CellState.Empty;
            cell.ClearCell();
            PopulateEmptyCells();
            ChooseFirstPlayer();
        }

        moveCount = 0;
        roundFinished = false;
    }
}

public enum Players
{
    P1,
    P2,
    AI
}