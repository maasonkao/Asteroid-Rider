using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ViewBlockerScript : MonoBehaviour
{
    public GameManager gameManager;
    [SerializeField] Canvas mainCanvas;
    public TextMeshPro viewBlockerText;

    private void OnMouseDown()
    {
        gameManager.SetCamera();
        mainCanvas.enabled = true;
    }

    void OnMouseOver()
    {
        viewBlockerText.color = new Color32(0xD1, 0x91, 0x23, 0xFF);
    }

    void OnMouseExit()
    {
        viewBlockerText.color = Color.white;

    }
}
