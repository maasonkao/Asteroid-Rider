using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerScript : MonoBehaviour
{

    public string playerName;
    public int playerNum;

    public GameObject Sea;
    public GameObject RadarLeft;
    public GameObject RadarRight;
    public GameObject TargetRadarLeft;
    public GameObject TargetRadarRight;
    public UnityEvent gameOver;

    public List<GameObject> shipList;
    public int totalHP, leftHP, rightHP;
    
    public List<Vector3> radarLocations;


    public bool canPlaceShips = true;
    public bool leftDestroyed, rightDestroyed, defeated;
    [SerializeField] private GameManager gameManager;


    void Start()
    {
        //gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        defeated = false;
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

        if(totalHP == 0)
        {
            defeated = true;
            gameOver.Invoke();
        }

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
        if (!canPlaceShips && TargetRadarLeft == null)
            SetTargetRadarLeft();
        if (!canPlaceShips && TargetRadarRight == null)
            SetTargetRadarRight();

        TargetRadarLeft.GetComponent<RadarScript>().ReloadRadar();
        TargetRadarRight.GetComponent<RadarScript>().ReloadRadar();
    }

    public bool ValidShipPlacement()
    {
        if(!canPlaceShips)
            return true;
        
        int startingHP = 0;
        foreach(GameObject ship in shipList)
        {
            startingHP += ship.GetComponent<ShipScript>().health;
        }
        if(startingHP != totalHP)
        {
            gameManager.SetText("You must place all your ships!");
            return false;
        }
        if(leftHP == 0 || rightHP == 0)
        {
            gameManager.SetText("You must place ships on the left and right of the field");
            return false;
        }
        return true;
    }
}
