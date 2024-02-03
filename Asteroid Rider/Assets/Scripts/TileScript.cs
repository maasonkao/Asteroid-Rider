using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    unknown = 0,
    seaTile = 10,
    shipTile = 20,
    missTile = 30,
    hitTile = 40,
    invalidPlacementTile = 41,
    radarTile = 50,
};

public class TileScript : MonoBehaviour
{
    [SerializeField] private string tileName;
    [SerializeField] private TileType tileType;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private int overlap = 0;




    private void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        UpdateTileColor();
    }

    void Update()
    {
        //move to OnTriggers
        UpdateTileColor();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(tileType == TileType.radarTile)
        {
            return;
        }
        //Add new bool to see if tile has been hit
        if (collision.gameObject.tag.Contains("Ship"))
        {
            overlap++;
            if(overlap > 1)
                SetTileType(TileType.invalidPlacementTile);
            else
                SetTileType(TileType.shipTile);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        overlap--;
        if (overlap == 1)
            SetTileType(TileType.shipTile);
        else if(overlap == 0)
            SetTileType(TileType.seaTile);
    }

    private void UpdateTileColor()
    {

        switch (tileType)
        {
            case TileType.seaTile:
                spriteRenderer.color = new Color32(0x81, 0x85, 0xD1, 0x83);
                break;
            case TileType.shipTile:
                spriteRenderer.color = new Color32(0xD1, 0x91, 0x23, 0x99);
                break;
            case TileType.radarTile:
                spriteRenderer.color = new Color32(0x63, 0x1B, 0x1C, 0x83);
                break;
            case TileType.missTile:
                spriteRenderer.color = new Color32(0xF5, 0xFF, 0x00, 0xF0);
                break;
            case TileType.hitTile:
            case TileType.invalidPlacementTile:
                spriteRenderer.color = Color.red;
                break;
            default:
                Debug.LogWarning("Tyle Type Not Assigned.\n Error: " + tileType);
                break;
        }
    }

    public void SetTileType(TileType type)
    {
        tileType = type;
    }

    public TileType GetTileType()
    {
        return tileType;
    }
}
