using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



public class TurnManager : MonoBehaviour
{
    public List<int> playersList;
    public int currentTurn;
    public int initialPlayers;
    public int currentPlayer;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<initialPlayers; i++)
        {
            playersList.Add(i);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
