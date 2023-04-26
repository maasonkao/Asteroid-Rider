using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewBlockerScript : MonoBehaviour
{
    public GameManager gameManager;
    [SerializeField] Canvas mainCanvas;


    private void Start()
    {
       // var gm = gameManager.GetComponent<GameManager>();    
    }
    private void OnMouseDown()
    {
        gameManager.SetCamera();
        mainCanvas.enabled = true;
    }
}
