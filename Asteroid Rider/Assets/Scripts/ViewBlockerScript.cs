using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewBlockerScript : MonoBehaviour
{
    public GameManager gameManager;
    
    
    private void Start()
    {
       // var gm = gameManager.GetComponent<GameManager>();    
    }
    private void OnMouseDown()
    {
        Debug.Log("Clicked " + this.name);
        gameManager.SetCamera();
    }
}
