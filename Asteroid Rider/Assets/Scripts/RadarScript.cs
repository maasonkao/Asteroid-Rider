using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;

public class RadarScript : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private List<GameObject> radarTiles;
    [SerializeField] private List<GameObject> seaTiles;
    [SerializeField] private Grid grid;
    [SerializeField] private string seaName;

    private GameManager gameManager;
    private Camera _mainCamera;
    [SerializeField] private SeaScript seaScript;

    public TextMeshProUGUI radarText;
    public bool hasShot, isLeft, isDestroyed;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }
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

        switch (isLeft)
        {
            case true:
                seaTiles = seaScript.leftSea;
                break;
            case false:
                seaTiles = seaScript.rightSea;
                break;
        }

        if(isLeft)
            radarText.text = seaName + " Left";
        else
            radarText.text = seaName + " Right";
            name = radarText.text;
    }

    private void Update()
    {
        //Change to unity action/event
        if (((isLeft && seaScript.leftDestroyed) || (!isLeft && seaScript.rightDestroyed)) && !isDestroyed)
        {
            isDestroyed = true;
            StartCoroutine(DestroyRadar());
        }
        

        if (Input.GetKeyDown(KeyCode.Space))
            hasShot = false;
        RadarParity();
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        Vector3Int mousePos = GetMousePosition();
        if (hasShot)
        {
            gameManager.SetText("You have already used this Radar!");
            return;
        }

        foreach (var tile in radarTiles){
            if (grid.LocalToCell(tile.transform.position) == mousePos)
            {
                CheckTile(tile.name);
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
                gameManager.SetText("Miss!");
                break;
            case TileType.shipTile:
                targetSeaTile.GetComponent<TileScript>().SetTileType(TileType.hitTile);
                hasShot = true;
                gameManager.SetText("Hit!");
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
        Mouse mouse = Mouse.current;
        Vector3 mouseWorldPos = _mainCamera.ScreenToWorldPoint(mouse.position.ReadValue());

        return grid.LocalToCell(mouseWorldPos);
    }

    IEnumerator DestroyRadar()
    {
        Debug.Log("DestroyRadar Coroutine started");
        radarText.text += " has been destroyed!";
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
        yield return new WaitForSeconds(1);
    }

    private void RadarParity()
    {
        foreach(GameObject tile in seaTiles)
        {
            GameObject targetRadarTile = radarTiles.Where(obj => obj.name == tile.name).FirstOrDefault();
            switch(tile.GetComponent<TileScript>().GetTileType())
            {
                case TileType.seaTile:
                case TileType.shipTile:
                    break;
                case TileType.missTile:
                    targetRadarTile.GetComponent<TileScript>().SetTileType(TileType.missTile);
                    break;
                case TileType.hitTile:
                    targetRadarTile.GetComponent<TileScript>().SetTileType(TileType.hitTile);
                    break;
                default:
                    Debug.LogWarning("Tile Type unknown");
                    break;

            }
        }
    }
}
