using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;


public class HoverHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject highlightTile;
    [SerializeField] private Grid grid;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        grid = gameManager.GetComponent<Grid>();
    }

    private void Update()
    {
        if(highlightTile != null)
        {
            Vector3Int mousePos = GetMousePosition();
            highlightTile.transform.position = grid.GetCellCenterWorld(mousePos);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        highlightTile.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlightTile.SetActive(false);
    }

    Vector3Int GetMousePosition()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return grid.LocalToCell(mouseWorldPos);
    }

}
