using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public string playerName;
    public int playerNum;

    public GameObject Sea;
    public GameObject RadarLeft;
    public GameObject RadarRight;

    public List<GameObject> shipList;
    public int totalHP, leftHP, rightHP;
    
    public List<Vector3> radarLocations;


    public bool isAlive = true;
    public bool canPlaceShips = true;
    public bool leftDestroyed, rightDestroyed;
    private GameManager gameManager;


    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        radarLocations.Add(this.transform.position + new Vector3(-5.5f, 0, 0));
        radarLocations.Add(this.transform.position + new Vector3(5.5f, 0, 0));

        string shipString = playerName + "_Ship";
        shipList = GameObject.FindGameObjectsWithTag(shipString).ToList();

    }
    void Update()
    {
        if (canPlaceShips)
            PlaceShips();
        if (Input.GetKeyDown(KeyCode.O))
        {
            SetLeftRadar();
            SetRightRadar();
        }
        GetHP();
    }

    private void GetHP()
    {
        leftHP = Sea.GetComponent<SeaScript>().leftHP;
        rightHP = Sea.GetComponent<SeaScript>().rightHP;
        totalHP = leftHP + rightHP;

        if (leftHP == 0)
            leftDestroyed = true;

        if (rightHP == 0)
            rightDestroyed = true;

    }


    public void SetLeftRadar()
    {
        PlayerScript leftPlayer = gameManager.GetPlayerLeft();

        if (!leftPlayer.leftDestroyed)
            Instantiate(leftPlayer.RadarRight, radarLocations[0], transform.rotation);
        else
            Instantiate(leftPlayer.RadarLeft, radarLocations[0], transform.rotation);
    }

    public void SetRightRadar()
    {
        PlayerScript rightPlayer = gameManager.GetPlayerRight();

        if (!rightPlayer.leftDestroyed)
            Instantiate(rightPlayer.RadarLeft, radarLocations[1], transform.rotation);
        else
            Instantiate(rightPlayer.RadarRight, radarLocations[1], transform.rotation);
    }

    public void PlaceShips()
    {
        foreach (GameObject ship in shipList)
        {
            ShipScript shipScript = ship.GetComponent<ShipScript>();
            shipScript.Move();
        }
    }
}
