using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharactersScriptable", menuName = "ScriptableObjects/CharactersScriptable", order = 1)]
public class CharactersScriptable : ScriptableObject
{
    public List<Character> characters;
}

[Serializable]
public class Character
{
    public string characterName;
    public Mesh characterMesh;
}
