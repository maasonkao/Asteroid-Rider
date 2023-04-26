using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Radar : ScriptableObject
{
    public GameManager gameManager;
    public SeaScript seaScript;
    public TextMeshProUGUI radarText;
    public bool hasShot;
    public Grid grid;
    public string seaName;

    public List<GameObject> radarTiles;
    public List<GameObject> seaTiles;

    private void OnEnable()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        grid = GameObject.Find("GameManager").GetComponent<Grid>();

        seaName = "P" + name[1] + "_Sea";
        seaScript = GameObject.Find(seaName).GetComponent<SeaScript>();

        radarTiles = GameObject.FindGameObjectsWithTag(name).ToList<GameObject>();

        switch (name[2])
        {
            case 'L':
                seaTiles = seaScript.leftSea;
                break;
            case 'R':
                seaTiles = seaScript.rightSea;
                break;
            default:
                Debug.LogError("Left or right not set!");
                break;

        }
        radarText.text = seaName;
    }
    private void OnMouseDown()
    {
        if (hasShot)
        {
            gameManager.SetText("You have already used this Radar!");
            return;
        }

        Vector3Int mousePos = GetMousePosition();

        foreach (var thing in radarTiles)
        {
            if (grid.LocalToCell(thing.transform.position) == mousePos)
            {
                CheckTile(thing.name);
            }
        }
    }

    private void CheckTile(string tileName)
    {
        GameObject targetRadarTile = radarTiles.Where(obj => obj.name == tileName).FirstOrDefault();
        GameObject targetSeaTile = seaTiles.Where(obj => obj.name == tileName).FirstOrDefault();

        TileScript.TileType seaTileType = targetSeaTile.GetComponent<TileScript>().GetTileType();

        switch (seaTileType)
        {
            case TileScript.TileType.seaTile:
                targetSeaTile.GetComponent<TileScript>().SetTileType(TileScript.TileType.missTile);
                hasShot = true;
                break;
            case TileScript.TileType.shipTile:
                targetSeaTile.GetComponent<TileScript>().SetTileType(TileScript.TileType.hitTile);
                hasShot = true;
                break;
            case TileScript.TileType.missTile:
            case TileScript.TileType.hitTile:
                gameManager.SetText("You already shot this tile!");
                break;
            default:
                Debug.LogWarning("Tyle Type not assigned in radar switch case.\n Error: " + seaTileType);
                break;
        }

        seaTileType = targetSeaTile.GetComponent<TileScript>().GetTileType();
        targetRadarTile.GetComponent<TileScript>().SetTileType(seaTileType);
    }
    Vector3Int GetMousePosition()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return grid.LocalToCell(mouseWorldPos);
    }
}
