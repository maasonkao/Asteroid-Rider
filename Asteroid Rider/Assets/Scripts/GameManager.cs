using System.Collections;
using System.Collections.Generic;

using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int playerCount;
    [SerializeField] private int currentPlayer;
    [SerializeField] private GameObject Canvas;
    //change this to player 
    public SeaScript P1_Sea;
    public SeaScript P2_Sea;
    public GameObject cameras;
    public ShipManager shipManager;


    public TextMeshProUGUI playerTurnText;
    public TextMeshProUGUI statusText;

    public float textDelayTime = 3, timer;
    
    private bool altText = false;
    private bool gameOver = false;

    void Start()
    {
        P1_Sea = GameObject.Find("P1_Sea").GetComponent<SeaScript>();
        P2_Sea = GameObject.Find("P2_Sea").GetComponent<SeaScript>();

        //change to just ShipManager
        shipManager = GameObject.FindGameObjectWithTag("P1_ShipManager").GetComponent<ShipManager>();
        EnableShips();
        playerTurnText.text = currentPlayer.ToString();
    }

    void Update()
    {
        if(P1_Sea.leftDestroyed == true && P1_Sea.rightDestroyed == true)
        {
            GameOver();
        }
        if (P2_Sea.leftDestroyed == true && P2_Sea.rightDestroyed == true)
        {
            GameOver();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            altText = false;
            NextPlayer();
            cameras.transform.position = new Vector3(4.3499999f, 19.7999992f, -10);
            Canvas.SetActive(false);
            EnableShips();
        }

        if (Input.GetMouseButton(1))
        {
            SetActiveCamera();
            Canvas.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            shipManager.placementMode = false;
        }

        if (!gameOver)
            StatusText();
        else
            SetText("You Win!");
    }

    void SetActiveCamera()
    {
        if(currentPlayer == 1)
            cameras.transform.position = new Vector3(4.3499999f, 4.5f, -10);
        else if(currentPlayer == 2)
            cameras.transform.position = new Vector3(52.7999992f, 4.5f, -10);

        //Between turn screen coords
        //Vector3(4.3499999,19.7999992,-10)
    }

    void EnableShips()
    {
        string playerString = "P" + (currentPlayer).ToString() + "_Ship";
        shipManager.SetShips(playerString);
    }
    
    private void NextPlayer()
    {
        if (currentPlayer == playerCount)
            currentPlayer = 1;
        else
            currentPlayer += 1;

        playerTurnText.text = currentPlayer.ToString();
    }

    public void SetText(string text)
    {
        altText = true;
        timer = 0;
        statusText.text = text;
    }

    private void StatusText()
    {
        if (shipManager.placementMode)
        {
            statusText.text = "Place your ships";
        }
        else if (!altText)
        {
            statusText.text = "Click on a radar tile to fire";
        }
        else
        {
            if (timer < textDelayTime)
            {
                timer += Time.deltaTime;
            }
            else
            {
                altText = false;
                timer = 0;
            }
        }
    }

    private void GameOver()
    {
        gameOver = true;
    }
}
