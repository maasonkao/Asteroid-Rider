using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public Camera mainCamera;
    public List<GameObject> playerList;
    public UnityEvent endRound;
    
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
        playerList = GameObject.FindGameObjectsWithTag("Player").OrderBy(n => n.name).ToList();
        playerCount = playerList.Count();
        currentPlayer = playerList[currentTurn].GetComponent<PlayerScript>();
        currentPlayerNum = currentPlayer.playerNum;
        viewBlockerText = GameObject.FindGameObjectWithTag("ViewBlocker").GetComponent<TextMeshPro>();

    }

    public void NextTurn()
    {
        if (CheckShipPlacement() == false)
            return;

        mainCanvas.enabled = false;
        currentTurn++;

        if (currentTurn >= playerCount)
        {
            if (currentPlayer.canPlaceShips)
            {
                foreach (GameObject player in playerList)
                {
                    player.GetComponent<PlayerScript>().canPlaceShips = false;
                }
            }
            endRound.Invoke();
            currentTurn = 0;
        }

        currentPlayer = playerList[currentTurn].GetComponent<PlayerScript>();
        currentPlayerNum = currentPlayer.playerNum;
        string playerName = currentPlayer.playerName;
        viewBlockerText.SetText(playerName + " Start!");

        //Set camera to view blocker
        mainCamera.transform.position = new Vector3(0, 20, -10);
    }

    private bool CheckShipPlacement()
    {
        if (currentPlayer.canPlaceShips && currentPlayer.totalHP != 14)
        {
            SetText("You must place all your ships!");
            return false;
        }
        if (currentPlayer.canPlaceShips && (currentPlayer.leftHP == 0 || currentPlayer.rightHP == 0))
        {
            SetText("You must place ships on the left and right of the field");
            return false;
        }
        return true;
    }

    public void SetCamera()
    {
        //Set camera to player
        mainCamera.transform.position = playerList[currentTurn].transform.position + new Vector3(0, 0, -10);
    }

    public PlayerScript GetPlayerLeft(int player)
    {
        int leftIndex;
        int index = playerList.FindIndex(obj => obj.GetComponent<PlayerScript>().playerNum == player);
        if (index == -1)
        {
            Debug.LogError("Could not find referenced player!");
            return null;
        }
        else if(index == 0)
            leftIndex = playerCount - 1;
        else
            leftIndex = index - 1;
        
        while(leftIndex != index)
        {
            if (playerList[leftIndex].GetComponent<PlayerScript>().defeated)
                leftIndex--;
            else
                return playerList[leftIndex].GetComponent<PlayerScript>();
            if(leftIndex < 0)
                leftIndex = playerCount - 1;
        }

/*        for (int i=0; i<playerList.Count; i++)
        {
            if(playerList[i].GetComponent<PlayerScript>().playerNum == player)
                if(i == 0)
                    return playerList[playerList.Count - 1].GetComponent<PlayerScript>();
                else
                    return playerList[i - 1].GetComponent<PlayerScript>();
        }*/
        Debug.LogError("Could not find left player!");
        return null;
    }

    public PlayerScript GetPlayerRight(int player)
    {
        int rightIndex;
        int index = playerList.FindIndex(obj => obj.GetComponent<PlayerScript>().playerNum == player);
        if (index == -1)
        {
            Debug.LogError("Could not find referenced player!");
            return null;
        }
        else if (index == playerCount - 1)
            rightIndex = 0;
        else
            rightIndex = index + 1;

        while (rightIndex != index)
        {
            if (playerList[rightIndex].GetComponent<PlayerScript>().defeated)
                rightIndex++;
            else
                return playerList[rightIndex].GetComponent<PlayerScript>();
            if (rightIndex >= playerCount)
                rightIndex = 0;
        }
/*        for (int i=0; i<playerList.Count; i++)
        {
            if(playerList[i].GetComponent<PlayerScript>().playerNum == player)
                if(i == playerList.Count - 1)
                    return playerList[0].GetComponent<PlayerScript>();
                else
                    return playerList[i + 1].GetComponent<PlayerScript>();
        }*/
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
            statusText.text = "Click and drag to place your ships. Press 'R' to rotate.";
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
}
