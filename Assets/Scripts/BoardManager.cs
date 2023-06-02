using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public Cell[] cells;

    [SerializeField] private List<Cell> emptyCells = new List<Cell>();

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GetComponent<GameManager>();
        PopulateEmptyCells();
    }

    public void MarkCellAsUsed(Cell cell)
    {
        emptyCells.Remove(cell);
    }

    private void PopulateEmptyCells()
    {
        emptyCells.Clear();

        foreach (Cell cell in cells)
        {
            if (cell.cellState == CellState.Empty)
            {
                emptyCells.Add(cell);
            }
        }
    }

    public Cell GetRandomEmptyCell()
    {
        List<Cell> emptyCells = new List<Cell>();

        foreach (Cell cell in cells)
        {
            if (cell.cellState == CellState.Empty)
            {
                emptyCells.Add(cell);
            }
        }

        if (emptyCells.Count > 0)
        {
            int randomIndex = Random.Range(0, emptyCells.Count);
            return emptyCells[randomIndex];
        }

        return null;
    }

    public bool CheckHorizontal(int row)
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

        Debug.Log("(Horizontal Check) The winner is: " + startCell);

        return true;
    }

    public bool CheckVertical(int column)
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

        Debug.Log("(Vertical Check) The winner is: " + startCell);
        return true;
    }

    public bool CheckDiagonal()
    {
        CellState centerCell = cells[4].cellState;

        if (centerCell == CellState.Empty)
            return false;

        if ((cells[0].cellState == centerCell && cells[8].cellState == centerCell) ||
            (cells[2].cellState == centerCell && cells[6].cellState == centerCell))
        {
            Debug.Log("(Diagonal Check) The winner is: " + centerCell);
            return true;
        }

        return false;
    }

    public IEnumerator ResetBoard()
    {
        gameManager.roundFinished = true;
        yield return new WaitForSeconds(3f);

        foreach (Cell cell in cells)
        {
            cell.cellState = CellState.Empty;
            cell.ClearCell();
        }

        PopulateEmptyCells();
        gameManager.ChooseFirstPlayer();

        gameManager.moveCount = 0;
        gameManager.roundFinished = false;
    }
}
