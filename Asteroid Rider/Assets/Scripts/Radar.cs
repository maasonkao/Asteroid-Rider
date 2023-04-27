using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Radar : ScriptableObject
{

    public TextMeshProUGUI radarText;
    //left = true | right = false
    public bool hasShot, ownerSide, targetSide;
    public string seaName;

    public List<GameObject> radarTiles;
    public List<GameObject> seaTiles;


}
