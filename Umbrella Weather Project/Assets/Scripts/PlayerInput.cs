using UnityEngine;
using System.Collections;

/// <summary>
/// Listens for keyboard input and calls respective PlayerMovement functions.
/// </summary>

[RequireComponent (typeof (PlayerMovement))]
public class PlayerInput : MonoBehaviour {

	PlayerMovement player;

	void Start () {
		player = GetComponent<PlayerMovement> ();
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
	}
}
