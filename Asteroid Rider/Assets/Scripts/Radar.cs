using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "Radar", menuName = "Radar")]
public class Radar : ScriptableObject
{
    //left = true | right = false
    public bool hasShot, targetSide;
    public string targetSeaName;

    public List<GameObject> seaTiles;


}
