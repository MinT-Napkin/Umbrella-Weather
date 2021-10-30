using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Abstract class for object to be interacted with.
/// Action dependent on overlap with interactable's collider
/// and the press of a key.
/// </summary>
public abstract class Interactable : MonoBehaviour
{
    //Action to occur when player interacts with object
    public abstract void ButtonAction();
}
