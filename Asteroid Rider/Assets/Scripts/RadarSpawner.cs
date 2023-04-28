using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RadarSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> radarTiles;
    [SerializeField] private List<GameObject> seaTiles;
    [SerializeField] private Grid grid;
    [SerializeField] private string seaName;

    private GameManager gameManager;
    private SeaScript seaScript;
    private bool isOwnerLeftSide;
    
    public Radar radar;
    public TextMeshProUGUI radarText;
    public bool hasShot = false;
    public string targetPlayerName;



    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        grid = GameObject.Find("GameManager").GetComponent<Grid>();
        SetRadar();

    }



    void Update()
    {
        
    }

    public void SetRadar()
    {
        PlayerScript targetPlayerScript;
        if (isOwnerLeftSide)
        {
            targetPlayerScript = gameManager.GetPlayerLeft();
            targetPlayerName = targetPlayerScript.playerName;
            
            //if target player right side is still alive, use right side tiles
            if(!targetPlayerScript.rightDestroyed)
                seaTiles = targetPlayerScript.Sea.GetComponent<SeaScript>().rightSea;
            else
                seaTiles = targetPlayerScript.Sea.GetComponent<SeaScript>().leftSea;

        }
        else
        {
            targetPlayerScript = gameManager.GetPlayerRight();
            targetPlayerName = targetPlayerScript.playerName;

            if (!targetPlayerScript.leftDestroyed)
                seaTiles = targetPlayerScript.Sea.GetComponent<SeaScript>().leftSea;
            else
                seaTiles = targetPlayerScript.Sea.GetComponent<SeaScript>().rightSea;

        }
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

        TileType seaTileType = targetSeaTile.GetComponent<TileScript>().GetTileType();

        switch (seaTileType)
        {
            case TileType.seaTile:
                targetSeaTile.GetComponent<TileScript>().SetTileType(TileType.missTile);
                hasShot = true;
                break;
            case TileType.shipTile:
                targetSeaTile.GetComponent<TileScript>().SetTileType(TileType.hitTile);
                hasShot = true;
                break;
            case TileType.missTile:
            case TileType.hitTile:
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
