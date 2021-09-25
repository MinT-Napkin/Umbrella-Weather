using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WetMeter : MonoBehaviour
{
    private float wetness;
    private const float WETRATE = .1f; // def gonna need some tuning

    public Umbrella umbrella;
    
    void Start()
    {
        wetness = 0;
    }
    
    public void IncrementWetness()
    {
        wetness += WETRATE;
        Debug.Log("wetness: " + wetness);
    }

    /*public IEnumerator ObtainWetness()
    {
        while (!umbrella.umbrellaOpen && wetness <= 1) // you should get wet when your umbrella is closed & you haven't reached the maximum wetness
        {
            IncrementWetness();
            yield return new WaitForSeconds(0.5f);
        }
    }*/

    // LERP THROUGH COROUTINE BEST METHOD
    public void ObtainWetness()
    {
        
    }

}
