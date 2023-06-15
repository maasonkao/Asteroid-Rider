using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlay : MonoBehaviour
{

    public void Toggle()
    {
        bool tog = this.gameObject.activeSelf ? false : true;
        this.gameObject.SetActive(tog);

    }

}
