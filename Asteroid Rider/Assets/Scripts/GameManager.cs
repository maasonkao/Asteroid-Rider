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
    [SerializeField] int currentTurn = 1;
    [SerializeField] int currentPlayer;
    [SerializeField] float textDelayTime;
    public TextMeshProUGUI statusText;
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

    }

    public void NextTurn()
    {
        currentTurn++;
        if (currentTurn >= playerCount)
        {
            currentTurn = 0;
        }
        currentPlayer = playerList[currentTurn].GetComponent<PlayerScript>().playerNum;
        string playerName = playerList[currentTurn].GetComponent<PlayerScript>().playerName;
        if(!BattleState.TryParse(playerName, out state))
        {
            Debug.LogWarning("Could not parse state: " + playerName);
        }
        SetText(playerName + " Turn");
        mainCamera.transform.position = new Vector3(0, 20, -10);
    }
    //second comp test


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
    /*    [SerializeField] private int currentPlayerNum = 0;
        [SerializeField] private GameObject Canvas;
        [SerializeField] private List<GameObject> playerList;
        [SerializeField] private PlayerScript currentPlayer;

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
            playerTurnText.text = currentPlayerNum.ToString();
            currentPlayer = playerList[currentPlayerNum].GetComponent<PlayerScript>();
        }

        void Update()
        {
            if (currentPlayer.canPlaceShips)
            {
                currentPlayer.PlaceShips();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentPlayer.canPlaceShips = false;
                altText = false;
                NextPlayer();
                Canvas.SetActive(false);
                Debug.Log("You pressed space" + currentPlayer.playerName);

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
            string playerString = "P" + (currentPlayerNum).ToString() + "_Ship";
            //shipManager.SetShips(playerString);
        }

        private void NextPlayer()
        {
            currentPlayerNum++;
            currentPlayerNum %= playerList.Count;
            playerTurnText.text = currentPlayerNum.ToString();
            currentPlayer = playerList[currentPlayerNum].GetComponent<PlayerScript>();

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
