using UnityEngine;
using System.Collections;

/// <summary>
/// Listens for keyboard input and calls respective PlayerMovement functions.
/// </summary>

[RequireComponent (typeof (PlayerMovement))]
public class PlayerInput : MonoBehaviour {

	PlayerMovement player;
	BoxCollider2D collider;
	Controller2D controller;

	void Start () {
		player = GetComponent<PlayerMovement> ();
		collider = GetComponent<BoxCollider2D>();
		controller = GetComponent<Controller2D>();
	}

	void Update () {
		Vector2 directionalInput = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		player.SetDirectionalInput (directionalInput);

		if (Input.GetKeyDown (KeyCode.Space)) {
			player.OnJumpInputDown ();
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			player.OnJumpInputUp ();
		}
		//Interact key listener; calls ButtonAction of all interactables touching player
		if (Input.GetKeyDown (KeyCode.F)) {
			Collider2D[] interacts = new Collider2D[10];
			if (collider.OverlapCollider(controller.interactFilter, interacts) > 0) {
				foreach (Collider2D inter in interacts) {
					if (inter != null) {
						inter.GetComponent<Interactable>().ButtonAction();
					}
                }
			}
        }
	}
}
