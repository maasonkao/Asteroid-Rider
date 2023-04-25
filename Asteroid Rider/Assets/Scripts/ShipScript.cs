using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ShipScript : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private ShipType shipType;
    [SerializeField] private int health,damage;
    [SerializeField] private List<GameObject> touchingTiles;

    private GameManager gameManager;
    private float startingPosX, startingPosY;
    private bool isBeingHeld = false;
    public bool isSunk = false;

    enum ShipType
    {
        unknown = 0,
        twoDot = 2,
        threeDotStraight = 10,
        threeDotL = 11,
        fourDotSquare = 20,
        fourDotStraight = 21,
        fourDotT = 22,
        fiveDotCross = 30,

    };

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        grid = gameManager.GetComponent<Grid>();
        damage = 0;

        switch (shipType)
        {
            case ShipType.twoDot:
                health = 2;
                break;
            case ShipType.threeDotStraight:
            case ShipType.threeDotL:
                health = 3;
                break;
            case ShipType.fourDotSquare:
            case ShipType.fourDotStraight:
            case ShipType.fourDotT:
                health = 4;
                break;
            case ShipType.fiveDotCross:
                health = 5;
                break;
            default:
                Debug.LogWarning("Warning! Ship Type not set.");
                break;
        }
    }

    private void Update()
    {
        CheckDamage();
    }

    public void Move()
    {
        Vector3Int mousePos = GetMousePosition();
        if (isBeingHeld)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Rotate();
            }
            transform.position = grid.GetCellCenterWorld(mousePos);
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3Int mousePos = GetMousePosition();
            startingPosX = mousePos.x;
            startingPosY = mousePos.y;

            isBeingHeld = true;
        }
    }

    private void OnMouseUp()
    {
        isBeingHeld = false;
    }

    public void Rotate()
    {
            transform.Rotate(transform.rotation.x, transform.rotation.y, 90);
    }

    Vector3Int GetMousePosition()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return grid.LocalToCell(mouseWorldPos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Sea"))
        {
            touchingTiles.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Contains("Sea"))
        {
            touchingTiles.Remove(collision.gameObject);
        }
    }

    public void CheckDamage()
    {
        int tempDamge = 0;
        foreach(GameObject obj in touchingTiles)
        {
            if (obj.GetComponent<TileScript>().GetTileType() == TileScript.TileType.hitTile)
                tempDamge++;
        }
        damage = tempDamge;
        if (damage == health && isSunk == false)
        {
            isSunk = true;
            gameManager.SetText(name + " has been sunk!");
        }

    }
}
