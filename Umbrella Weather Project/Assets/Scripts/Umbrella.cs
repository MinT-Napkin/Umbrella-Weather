using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umbrella : MonoBehaviour
{
    // these vars will eventually be privated. serialized for testing
    [SerializeField] private int nUmbrella;
    [SerializeField] public bool umbrellaOpen { get; private set; }
    public WetMeter wetMeter;

    private void Start()
    {
        // nUmbrella = currentLevel.getNumberOfUmbrellas(); // number of umbrellas should be obtained from the level or smthn
        umbrellaOpen = false;  // not sure if you want the player to start w it open or not, set to false for now
        OpenUmbrella();
        Debug.Log("umbrellaOpen = " + umbrellaOpen);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            DebugToggleUmbrella();
        }
    }

    private void OpenUmbrella()
    {
        umbrellaOpen = true; // this deactivates `wetMeter.ObtainWetness`
        // animation of opening umbrella here
    }
    
    private void CloseUmbrella()
    {
        umbrellaOpen = false;
        // StartCoroutine(wetMeter.ObtainWetness());
        wetMeter.ObtainWetness();
        // animation of closing umbrella here
    }

    private void DebugToggleUmbrella()
    {
        if (umbrellaOpen)
        {
            CloseUmbrella();
        }
        else
        {
            OpenUmbrella();
        }
        Debug.Log("umbrellaOpen = " + umbrellaOpen);
    }
}
