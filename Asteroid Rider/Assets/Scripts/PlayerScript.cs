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

    private static float radarPos = 5.5f;


    void Start()
    {
        radarLocations.Add(this.transform.position + new Vector3(-radarPos, 0, 0));
        radarLocations.Add(this.transform.position + new Vector3(radarPos, 0, 0));

        string shipString = playerName + "_Ship";
        shipList = GameObject.FindGameObjectsWithTag(shipString).ToList();
        RadarLeft.SetActive(false);
        RadarRight.SetActive(false);
    }
    void Update()
    {
        if (canPlaceShips)
            PlaceShips();
        GetHP();
    }

    private void GetHP()
    {
        leftHP = Sea.GetComponent<SeaScript>().leftHP;
        rightHP = Sea.GetComponent<SeaScript>().rightHP;
        totalHP = leftHP + rightHP;

    }



    private void TurnOnRadar()
    {
        RadarLeft.SetActive(true);
        RadarRight.SetActive(true);
    }

    public void SetRadar(GameObject radar, bool isLeft)
    {
        if (isLeft)
            RadarLeft = radar;
        else
            RadarRight = radar;
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
