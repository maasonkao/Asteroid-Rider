using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RadarScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> radarTiles;
    [SerializeField] private List<GameObject> seaTiles;
    [SerializeField] private Grid grid;
    [SerializeField] private string seaName;

    private GameManager gameManager;
    [SerializeField] private SeaScript seaScript;

    public TextMeshProUGUI radarText;
    public bool hasShot, isLeft, isDestroyed;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        grid = GameObject.Find("GameManager").GetComponent<Grid>();

        seaScript = GameObject.FindGameObjectWithTag(seaName).GetComponent<SeaScript>();

        foreach (Transform child in transform)
        {
            if(child.gameObject.name == "Canvas")
                continue;
            radarTiles.Add(child.gameObject);
        }

        switch (name[0])
        {
            case 'L':
                isLeft = true;
                seaTiles = seaScript.leftSea;
                break;
            case 'R':
                isLeft = false;
                seaTiles = seaScript.rightSea;
                break;
            default:
                Debug.LogError("Left or right not set!");
                break;

        }

        radarText.text = seaName;
    }

    private void Update()
    {
        if ((isLeft && seaScript.leftDestroyed) || (!isLeft && seaScript.rightDestroyed))
            isDestroyed = true;
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            hasShot = false;
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

        foreach (var thing in radarTiles){
            if (grid.LocalToCell(thing.transform.position) == mousePos)
            {
                CheckTile(thing.name);
            }
        }
        Debug.Log(mousePos);
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
                Debug.LogWarning("Tile Type not assigned in radar switch case.\n Error: " + seaTileType);
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
