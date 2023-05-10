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
    public GameObject TargetRadarLeft;
    public GameObject TargetRadarRight;

    public List<GameObject> shipList;
    public int totalHP, leftHP, rightHP;
    
    public List<Vector3> radarLocations;


    public bool isAlive = true;
    public bool canPlaceShips = true;
    public bool leftDestroyed, rightDestroyed;
    [SerializeField] private GameManager gameManager;


    void Start()
    {
        //gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        radarLocations.Add(Sea.transform.position + new Vector3(-9.9f, 0, 0));
        radarLocations.Add(Sea.transform.position + new Vector3(9.9f, 0, 0));

        string shipString = playerName + "_Ship";
        shipList = GameObject.FindGameObjectsWithTag(shipString).ToList();

    }
    void Update()
    {
        if (canPlaceShips)
            PlaceShips();
        if (Input.GetKeyDown(KeyCode.O))
        {
            canPlaceShips = false;
            SetTargetRadarLeft();
            SetTargetRadarRight();
        }
        if(!canPlaceShips && TargetRadarLeft == null)
            SetTargetRadarLeft();
        if(!canPlaceShips && TargetRadarRight == null)
            SetTargetRadarRight();

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


    public void SetTargetRadarLeft()
    {
        PlayerScript leftPlayer = gameManager.GetPlayerLeft(playerNum);

        if (!leftPlayer.rightDestroyed)
        {
            TargetRadarLeft = Instantiate(leftPlayer.RadarRight, radarLocations[0], transform.rotation) as GameObject;
            TargetRadarLeft.SetActive(true);
        }
        else
        {
            TargetRadarLeft = Instantiate(leftPlayer.RadarLeft, radarLocations[0] + new Vector3(4.4f, 0, 0), transform.rotation) as GameObject;
            TargetRadarLeft.SetActive(true);
        }
    }

    public void SetTargetRadarRight()
    {
        PlayerScript rightPlayer = gameManager.GetPlayerRight(playerNum);

        if (!rightPlayer.leftDestroyed)
        {
            TargetRadarRight = Instantiate(rightPlayer.RadarLeft, radarLocations[1], transform.rotation) as GameObject;
            TargetRadarRight.SetActive(true);
        }
        else
        {
            TargetRadarRight = Instantiate(rightPlayer.RadarRight, radarLocations[1] + new Vector3(-4.4f, 0, 0), transform.rotation) as GameObject;
            TargetRadarRight.SetActive(true);
        }
    }

    public void PlaceShips()
    {
        foreach (GameObject ship in shipList)
        {
            ShipScript shipScript = ship.GetComponent<ShipScript>();
            shipScript.Move();
        }
    }

    public void ReloadRadar()
    {
        Debug.Log(name + " reload radar!");
    }
}
