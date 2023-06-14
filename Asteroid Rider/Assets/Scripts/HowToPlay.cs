using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlay : MonoBehaviour
{
    bool isEnabled = false;
    GameObject tutorialImg;
    [SerializeField] Camera mainCamera;

    private void Start()
    {
        tutorialImg = GameObject.FindGameObjectWithTag("tutorial");
    }


    public void Toggle()
    {
        if (!isEnabled)
        {

            tutorialImg.SetActive(true);
            tutorialImg.transform.position = mainCamera.transform.position;
        }
        else
        {
            tutorialImg.SetActive(false);
        }
    }

}
