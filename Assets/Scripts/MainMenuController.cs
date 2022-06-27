using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown charactersDD;
    [SerializeField] private TMP_Dropdown boatsDD;
    [SerializeField] private CharactersScriptable charactersData;
    [SerializeField] private BoatsManager boatsManager;

    private void Awake()
    {
        AddListeners();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void Start()
    {
        charactersDD.options.Clear();
        boatsDD.options.Clear();
        PopulateCharacters();
       // PopulateBoats();
    }

    private void PopulateCharacters()
    {
        var chars = charactersData.characters.Select(i=>i.characterName).ToList();
        charactersDD.AddOptions(chars);
    }

    //private void PopulateBoats()
    //{
    //    var boatList = boatsManager.boats.Select(i=>i.boatName).ToList();
    //    boatsDD.AddOptions(boatList);
    //}

    private void AddListeners()
    {
        charactersDD.onValueChanged.AddListener(OnCharacterSelected);
        boatsDD.onValueChanged.AddListener(OnBoatSelected);
    }

    private void RemoveListeners()
    {
        charactersDD.onValueChanged.RemoveAllListeners();
    }

    private void OnCharacterSelected(int index)
    {
        string selectedCharacterName = charactersDD.options[index].text;
        Character selectedCharacter =
            charactersData.characters.FirstOrDefault(i => i.characterName == selectedCharacterName);
        EventManager.onCharacterSelected?.Invoke(selectedCharacter);
    }

    private void OnBoatSelected(int index)
    {
        string selectedBoatName = boatsDD.options[index].text;
        Boat selectedBoat = boatsManager.boats.FirstOrDefault(i => i.boatName == selectedBoatName);
        EventManager.onBoatSelected?.Invoke(selectedBoat);
    }
}
