using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class SeaScript : MonoBehaviour
{
    public List<GameObject> leftSea;
    public List<GameObject> rightSea;
    public List<GameObject> allTiles;
    public int leftHP, rightHP;
    public bool leftDestroyed = false, rightDestroyed = false;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in transform)
        {
            allTiles.Add(child.gameObject);
        }

        for(int i = 0; i < allTiles.Count() / 2; i++)
        {
            leftSea.Add(allTiles[i]);
            rightSea.Add(allTiles[i + 32]);
        }
    }

    private void Update()
    {
        int tempLeft = 0, tempRight = 0;
        for(int i=0; i<leftSea.Count(); i++)
        {
            if(leftSea[i].GetComponent<TileScript>().GetTileType() == TileScript.TileType.shipTile)
            {
                tempLeft++;
            }

            if (rightSea[i].GetComponent<TileScript>().GetTileType() == TileScript.TileType.shipTile)
            {
                tempRight++;
            }
        }
        leftHP = tempLeft;
        rightHP = tempRight;
        if (leftHP == 0)
            leftDestroyed = true;
        else
            leftDestroyed = false;

        if (rightHP == 0)
            rightDestroyed = true;
        else
            rightDestroyed = false;
    }


}
