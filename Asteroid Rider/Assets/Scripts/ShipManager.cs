using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    public GameObject[] ships;
    public List<List<GameObject>> playerShipsList;

    public bool placementMode = true;

    private void Start()
    {
        //change this when ship drag and drop is implemented
        playerShipsList.Add(GameObject.FindGameObjectsWithTag("P1_Ship").ToList());
        playerShipsList.Add(GameObject.FindGameObjectsWithTag("P2_Ship").ToList());
    }

    void Update()
    {
        if (placementMode)
        {
            foreach(GameObject ship in ships)
            {
                ShipScript shipScript = ship.GetComponent<ShipScript>();
                shipScript.Move();
            }
        }
    }

    public void SetShips(string list)
    {
        ships = GameObject.FindGameObjectsWithTag(list);
    }
}
