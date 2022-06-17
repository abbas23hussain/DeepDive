using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OceanColors", menuName = "ScriptableObjects/OceanColorsScriptable", order = 1)]
public class OceanColorsScriptable : ScriptableObject
{
    public List<OceanColor> oceanColors;
}

[Serializable]
public class OceanColor
{
    public string presetName;
    public float density;
    public Color waterColor;
    public Color fogTop;
    public Color fogBottom;
    public Color overLayColor;
    public Color lipColor;
    public Color waveColor;
}