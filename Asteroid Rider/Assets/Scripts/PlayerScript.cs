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
    
    public Vector3 location;
    public List<Vector3> radarLocations;

    public bool isDefeated;
    public bool canPlaceShips;

    /*

    - SetRadar(Player, string)
    + CreateRadar(Player, string)
    - GetTotalHP

     */

    // Start is called before the first frame update
    void Start()
    {
        string shipString = "P" + playerNum + "_Ship";
        shipList = GameObject.FindGameObjectsWithTag(shipString).ToList();
        RadarLeft.SetActive(false);
        RadarRight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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
