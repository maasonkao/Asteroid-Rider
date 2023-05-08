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
    [SerializeField] TextMeshPro viewBlockerText;

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
        viewBlockerText = GameObject.FindGameObjectWithTag("ViewBlocker").GetComponent<TextMeshPro>();

    }

    public void NextTurn()
    {
        if (currentPlayer.canPlaceShips && currentPlayer.totalHP != 14)
        {
            SetText("You must place all your ships!");
            return;
        }
        if (currentPlayer.canPlaceShips && (currentPlayer.leftHP == 0 || currentPlayer.rightHP == 0))
        {
            SetText("You must place ships on the left and right of the field");
            return;
        }
        mainCanvas.enabled = false;
        currentTurn++;

        if (currentPlayer.canPlaceShips)
            currentPlayer.canPlaceShips = false;

        if (currentTurn >= playerCount)
            currentTurn = 0;

        currentPlayer = playerList[currentTurn].GetComponent<PlayerScript>();
        currentPlayerNum = currentPlayer.playerNum;
        string playerName = currentPlayer.playerName;
        viewBlockerText.SetText(playerName + " Start!");

        if (!BattleState.TryParse(playerName, out state))
        {
            Debug.LogWarning("Could not parse state: " + playerName);
        }
        SetText(playerName + " Turn");

        //Set camera to view blocker
        mainCamera.transform.position = new Vector3(0, 20, -10);
    }

    public void SetCamera()
    {
        //Set camera to player
        mainCamera.transform.position = playerList[currentTurn].transform.position + new Vector3(0, 0, -10);
        Blah();
    }

    public PlayerScript GetPlayerLeft(int player)
    {
        for(int i=0; i<playerList.Count; i++)
        {
            if(playerList[i].GetComponent<PlayerScript>().playerNum == player)
                if(i == 0)
                    return playerList[playerList.Count - 1].GetComponent<PlayerScript>();
                else
                    return playerList[i - 1].GetComponent<PlayerScript>();
        }
        Debug.LogError("Could not find left player!");
        return null;
    }

    public PlayerScript GetPlayerRight(int player)
    {
        for(int i=0; i<playerList.Count; i++)
        {
            if(playerList[i].GetComponent<PlayerScript>().playerNum == player)
                if(i == playerList.Count - 1)
                    return playerList[0].GetComponent<PlayerScript>();
                else
                    return playerList[i + 1].GetComponent<PlayerScript>();
        }
        Debug.LogError("Could not find right player!");
        return null;
    }

    public PlayerScript GetPlayerLeft()
    {
        if (currentTurn == 0)
            return playerList[playerCount - 1].GetComponent<PlayerScript>();

        return playerList[currentTurn - 1].GetComponent<PlayerScript>();
    }

    public PlayerScript GetPlayerRight()
    {
        if (currentTurn == playerCount - 1)
            return playerList[0].GetComponent<PlayerScript>();

        return playerList[currentTurn + 1].GetComponent<PlayerScript>();
    }

    public void SetText(string text)
    {
        altText = true;
        timer = 0;
        statusText.text = text;
    }
    private void StatusText()
    {
        if(currentPlayer.canPlaceShips && !altText)
            statusText.text = "Place your ships";
        else if (!altText)
            statusText.text = "Click on a radar tile to fire";
        else
        {
            if (timer < textDelayTime)
                timer += Time.deltaTime;
            else
            {
                altText = false;
                timer = 0;
            }
        }
    }

    public void Blah(){
        SetText("Blaaaaah");
    }
}
