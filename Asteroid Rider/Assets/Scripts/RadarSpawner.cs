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



    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        grid = GameObject.Find("GameManager").GetComponent<Grid>();


    }



    void Update()
    {
        
    }
}
