using System.Collections;
using System.Collections.Generic;

using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int currentPlayerNum;
    [SerializeField] private GameObject Canvas;
    [SerializeField] private List<GameObject> playerList;

    private GameObject currentPlayer;

    public GameObject mainCamera;
    public ShipManager shipManager;

    public TextMeshProUGUI playerTurnText;
    public TextMeshProUGUI statusText;

    private float textDelayTime = 3, timer;
    private bool altText = false;
    private bool gameOver = false;

    void Start()
    {

        shipManager = GameObject.FindGameObjectWithTag("ShipManager").GetComponent<ShipManager>();
        EnableShips();
        playerTurnText.text = currentPlayerNum.ToString();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            altText = false;
            NextPlayer();
            Canvas.SetActive(false);
            EnableShips();
        }

        if (Input.GetMouseButton(1))
        {
            SetActivemainCamera();
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

    void SetActivemainCamera()
    {

        //Between turn screen coords
        //Vector3(4.3499999,19.7999992,-10)
    }

    void EnableShips()
    {
        string playerString = "P" + (currentPlayerNum).ToString() + "_Ship";
        shipManager.SetShips(playerString);
    }
    
    private void NextPlayer()
    {
        currentPlayerNum++;
        currentPlayerNum %= playerList.Count;
        playerTurnText.text = currentPlayerNum.ToString();
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
