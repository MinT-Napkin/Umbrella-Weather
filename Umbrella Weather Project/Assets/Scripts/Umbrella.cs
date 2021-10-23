using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script handles the status of the player's umbrella (open/closed), and will activate/deactivate a hitbox above
/// the player's head that will block rain particles.
/// 
/// It interacts with the following scripts:
/// - `PlayerMovement.cs`: to handle the player's movement when the umbrella is open
/// - TODO: `WetMeter.cs`: to activate/deactivate the wet-o-meter depending on the umbrella's status
/// - TODO: The script used by the fans will use `UmbrellaOpen` to determine whether the player should be affected.
/// </summary>

public class Umbrella : MonoBehaviour
{
    // there's supposed to be a number of umbrellas that the player has, I'm assuming they'll break after a while or something. will be implemented later
    private int nUmbrella;

    // flag to keep track of whether the umbrella is open or closed.
    private bool umbrellaOpen = false;
    public bool UmbrellaOpen
    {
        get { return umbrellaOpen; }
        private set { umbrellaOpen = value; }
    }

    [SerializeField] private GameObject hitbox;
    
    // this var is used by `PlayerMovement.cs` to calculate the character's Y velocity depending on the umbrella's status
    private float gravityMultiplier = 1f;
    public float GravityMultiplier
    {
        get { return gravityMultiplier; }
        private set { gravityMultiplier = value; }
    }
    
    // some constant values used for the gravity multiplier
    private const float CLOSED_GRAV_MULIPLIER = 1f;
    private const float OPEN_GRAV_MULIPLIER = .2f;

    private void Start()
    {
        umbrellaOpen = false;
        gravityMultiplier = CLOSED_GRAV_MULIPLIER;
        hitbox.SetActive(false);
        
        // Debug.Log("umbrellaOpen = " + umbrellaOpen);
    }

    /* This script just has an `Update` for debugging purposes. The opening/closing of the umbrella will probably
     * be handled by `PlayerMovement.cs` or some other script that handles input in the future.
     *
     * For now, the key used to open or close the umbrella is `J`. There is no visual indicator bc im lazy, but it
     * prints a message in the debug log.
     */
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            DebugToggleUmbrella();
        }
    }

    /* This function opens the umbrella. This changes how the player is affected by gravity.
     * TODO: add animation of opening umbrella (when it's finished)
     * TODO: stop the wet-o-meter from ticking when the umbrella is open.
     */
    private void OpenUmbrella()
    {
        umbrellaOpen = true; // this deactivates `wetMeter.ObtainWetness`
        gravityMultiplier = OPEN_GRAV_MULIPLIER;
        hitbox.SetActive(true);
        // animation of opening umbrella here
    }
    
    /* This function closes the umbrella.
     * TODO: add animation of closing umbrella (when it's finished)
     * TODO: start the wet-o-meter's ticking given the player isn't under cover
     */
    private void CloseUmbrella()
    {
        umbrellaOpen = false;
        gravityMultiplier = CLOSED_GRAV_MULIPLIER;
        hitbox.SetActive(false);
        // animation of closing umbrella here
    }

    /* This function is used for toggling the umbrella between being open and closed. It's only in this script for now
     * for debugging purposes, but will probably be moved to the player controller script in the future. This function
     * is used by `Update()`
     */
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
        // Debug.Log("umbrellaOpen = " + umbrellaOpen);
    }
}
