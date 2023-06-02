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
    private BoardManager boardManager;

    public int moveCount = 0;
    public bool roundFinished = false;

    [Header("Player Related")]
    [SerializeField] private PlayerUI[] playerUI;

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
        boardManager = GetComponent<BoardManager>();

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
            if (boardManager != null)
            {
                Cell randomCell = boardManager.GetRandomEmptyCell();
                if (randomCell != null)
                {
                    randomCell.MarkCell();
                }
            }
        }
    }


    public void ChooseFirstPlayer()
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
            if (boardManager.CheckHorizontal(row))
            {
                StartCoroutine(boardManager.ResetBoard());
                return;
            }
        }

        for (int column = 0; column < 3; column++)
        {
            if (boardManager.CheckVertical(column))
            {
                StartCoroutine(boardManager.ResetBoard());
                return;
            }
        }

        if (boardManager.CheckDiagonal())
        {
            StartCoroutine(boardManager.ResetBoard());
            return;
        }

        if (moveCount == 9 && !roundFinished)
        {
            Debug.Log("Nobody won");
            StartCoroutine(boardManager.ResetBoard());
            return;
        }

        SwitchPlayer();
    }
}

public enum Players
{
    P1,
    P2,
    AI
}