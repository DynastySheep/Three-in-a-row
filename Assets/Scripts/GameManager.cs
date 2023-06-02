using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Players currentPlayer;
    public Players startingPlayer;
    [SerializeField] private Cell[] cells;

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
        Debug.Log("No win condition detected");
        SwitchPlayer();
    }
}

public enum Players
{
    P1,
    P2,
    AI
}