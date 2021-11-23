using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Interactible object that will animate and take umbrella from player upon interaction.
/// 
/// TODO: Update happy count when interacted withs
/// TODO: Animate when interacted with
/// TODO: Prevent more than one interaction
/// </summary>
public class SadPerson : Interactable
{

    public Animator sadPersonAnim;

    // Start is called before the first frame update
    void Start()
    {
        sadPersonAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Called by PlayerInput.cs when interact key pressed
    public override void ButtonAction() {
        sadPersonAnim.SetBool("umbrellaGiven", true);
        Debug.Log("I'm happy!");
    }
}
