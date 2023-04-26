using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState
{
    START = 0,
    P1 = 1,
    P2 = 2,
    P3 = 3,
    P4 = 4,

    GAMEOVER = 10,

}

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public Camera mainCamera;
    public List<GameObject> playerList;

    

    public BattleState state;

    [SerializeField] int playerCount;
    [SerializeField] int currentTurn = 0;
    [SerializeField] int currentPlayerNum;
    [SerializeField] float textDelayTime;
    [SerializeField] PlayerScript currentPlayer;
    [SerializeField] TextMeshProUGUI statusText;
    [SerializeField] Canvas mainCanvas;
    bool altText;
    float timer;

    private void Start()
    {
        SetupBattle();
    }

    private void Update()
    {
        StatusText();
    }

    void SetupBattle()
    {
        state = BattleState.START;
        playerList = GameObject.FindGameObjectsWithTag("Player").OrderBy(n => n.name).ToList();
        playerCount = playerList.Count();
        currentPlayer = playerList[currentTurn].GetComponent<PlayerScript>();
        currentPlayerNum = currentPlayer.playerNum;
        Debug.Log("Current player is: " + currentPlayerNum);

    }

    public void NextTurn()
    {
        mainCanvas.enabled = false;
        currentTurn++;
        if (currentTurn >= playerCount)
        {
            currentTurn = 0;
        }
        currentPlayer = playerList[currentTurn].GetComponent<PlayerScript>();
        currentPlayerNum = currentPlayer.playerNum;
        string playerName = currentPlayer.playerName;
        Debug.Log("Current player is: " + currentPlayerNum);

        if (!BattleState.TryParse(playerName, out state))
        {
            Debug.LogWarning("Could not parse state: " + playerName);
        }
        SetText(playerName + " Turn");
        mainCamera.transform.position = new Vector3(0, 20, -10);
    }

    public void SetCamera()
    {
        mainCamera.transform.position = playerList[currentTurn].transform.position + new Vector3(0, 0, -10);

        switch (state)
        {
            case BattleState.P1:
                break;
            case BattleState.P2:
                break;
            case BattleState.P3:
                break;
            case BattleState.P4:
                break;
            default:
                Debug.LogWarning("Unknown state for camera");
                break;
        }
    }

    public void SetText(string text)
    {
        altText = true;
        timer = 0;
        statusText.text = text;
    }
    private void StatusText()
    {
        if(playerList[currentTurn].GetComponent<PlayerScript>().canPlaceShips)
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
    /*    [SerializeField] private int currentPlayerNumNum = 0;
        [SerializeField] private GameObject Canvas;
        [SerializeField] private List<GameObject> playerList;
        [SerializeField] private PlayerScript currentPlayerNum;

        public GameObject mainCamera;
        //    public ShipManager shipManager;

        public TextMeshProUGUI playerTurnText;
        public TextMeshProUGUI statusText;

        private float textDelayTime = 3, timer;
        private bool altText = false;
        private bool gameOver = false;

        void Start()
        {
            var tempList = GameObject.FindGameObjectsWithTag("Player").ToList();
            foreach(GameObject obj in tempList)
            {
                playerList.Add(obj.transform.parent.gameObject);
                Debug.Log("Added " + obj.transform.parent.gameObject.name);
            }
            EnableShips();
            playerTurnText.text = currentPlayerNumNum.ToString();
            currentPlayerNum = playerList[currentPlayerNumNum].GetComponent<PlayerScript>();
        }

        void Update()
        {
            if (currentPlayerNum.canPlaceShips)
            {
                currentPlayerNum.PlaceShips();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentPlayerNum.canPlaceShips = false;
                altText = false;
                NextPlayer();
                Canvas.SetActive(false);
                Debug.Log("You pressed space" + currentPlayerNum.playerName);

            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("You pressed Q");

            }

            if (Input.GetMouseButton(1))
            {
                SetActivemainCamera();
                Canvas.SetActive(true);
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
            string playerString = "P" + (currentPlayerNumNum).ToString() + "_Ship";
            //shipManager.SetShips(playerString);
        }

        private void NextPlayer()
        {
            currentPlayerNumNum++;
            currentPlayerNumNum %= playerList.Count;
            playerTurnText.text = currentPlayerNumNum.ToString();
            currentPlayerNum = playerList[currentPlayerNumNum].GetComponent<PlayerScript>();

        }

        public void SetText(string text)
        {
            altText = true;
            timer = 0;
            statusText.text = text;
        }

        private void StatusText()
        {
    *//*        if (shipManager.placementMode)
            {
                statusText.text = "Place your ships";
            }*//*
            if (!altText)
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
        }*/
}
