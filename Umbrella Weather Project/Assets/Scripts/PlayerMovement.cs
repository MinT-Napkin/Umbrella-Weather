using System;
using UnityEngine;
using System.Collections;

/// <summary>
/// Calculates all movement values such as movespeed, jump height,
/// gravity, etc. Calls Controller2D move function with velocity vector.
/// </summary>

[RequireComponent (typeof (Controller2D))]
public class PlayerMovement : MonoBehaviour {

	public float maxJumpHeight = 4;
	public float minJumpHeight = 1;		// performed when tapping jump
	public float timeToJumpApex = .4f;
	public float moveSpeed = 10;

	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;

	public Vector2 wallJumpClimb;
	public Vector2 wallJumpOff;
	public Vector2 wallLeap;

	public float wallSlideSpeedMax = 3;
	public float wallStickTime = .25f;
	float timeToWallUnstick;

	private float gravity;

	// stuff i changed
	public Umbrella umbrella;

	float maxJumpVelocity;
	float minJumpVelocity;
	public Vector3 velocity;
	float velocityXSmoothing;

	Controller2D controller;
	Animator anim;
	SpriteRenderer srender;
	Transform playerPos;

	public Vector2 directionalInput;
	bool wallSliding;
	int wallDirX;
	
	// stuff i changed
	private const float MAX_FALL_OPEN = -3f;

	void Start() {
		controller = GetComponent<Controller2D> ();
		anim = GetComponent<Animator>();
		srender = GetComponent<SpriteRenderer> ();
		playerObj = GetComponent<Transform> ();

		gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);
	}

	void Update() {
		CalculateVelocity ();
		HandleWallSliding ();
		Animation(); // animates character

		controller.Move (velocity * Time.deltaTime, directionalInput);

		if (controller.collisions.above || controller.collisions.below) {
			if (controller.collisions.slidingDownMaxSlope) {
				velocity.y += controller.collisions.slopeNormal.y * -gravity * Time.deltaTime;
			} else {
				velocity.y = 0;
			}
		}
	}

	public void SetDirectionalInput (Vector2 input) {
		directionalInput = input;
	}

	public void OnJumpInputDown() {
		if (wallSliding) {
			if (wallDirX == directionalInput.x) {
				velocity.x = -wallDirX * wallJumpClimb.x;
				velocity.y = wallJumpClimb.y;
			}
			else if (directionalInput.x == 0) {
				velocity.x = -wallDirX * wallJumpOff.x;
				velocity.y = wallJumpOff.y;
			}
			else {
				velocity.x = -wallDirX * wallLeap.x;
				velocity.y = wallLeap.y;
			}
		}
		if (controller.collisions.below) {
			if (controller.collisions.slidingDownMaxSlope) {
				if (directionalInput.x != -Mathf.Sign (controller.collisions.slopeNormal.x)) { // not jumping against max slope
					velocity.y = maxJumpVelocity * controller.collisions.slopeNormal.y;
					velocity.x = maxJumpVelocity * controller.collisions.slopeNormal.x;
				}
			} else {
				velocity.y = maxJumpVelocity;
			}
		}
	}

	//variable jump height method
	public void OnJumpInputUp() {
		if (velocity.y > minJumpVelocity) {
			velocity.y = minJumpVelocity;
		}
	}	

	void HandleWallSliding() {
		wallDirX = (controller.collisions.left) ? -1 : 1;
		wallSliding = false;
		if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0) {
			wallSliding = true;

			if (velocity.y < -wallSlideSpeedMax) {
				velocity.y = -wallSlideSpeedMax;
			}

			if (timeToWallUnstick > 0) {
				velocityXSmoothing = 0;
				velocity.x = 0;

				if (directionalInput.x != wallDirX && directionalInput.x != 0) {
					timeToWallUnstick -= Time.deltaTime;
				}
				else {
					timeToWallUnstick = wallStickTime;
				}
			}
			else {
				timeToWallUnstick = wallStickTime;
			}

		}

	}

	void CalculateVelocity() {
		float targetVelocityX = directionalInput.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
		// CalculateYVelocityAcc(); // velocity.y += gravity * Time.deltaTime;
		if (umbrella)
		{
			CalculateYVelocityVel();
		}
		else
		{
			velocity.y += gravity * Time.deltaTime;
		}
	}

	/* This function is used to calculate the player's Y-axis velocity.
     * If the player's umbrella is open, it lowers the effect of gravity
     * by reducing the player's maximum falling velocity
     */ 
	void CalculateYVelocityVel()
	{
		if (umbrella.UmbrellaOpen) // if the player is falling and not on the ground ...
		{
			if (velocity.y < MAX_FALL_OPEN)
			{
				velocity.y = MAX_FALL_OPEN;
			}
			else
			{
				velocity.y += gravity * Time.deltaTime;
			}
		}
		else
		{
			velocity.y += gravity * Time.deltaTime;
		}
	}
	
	// Controls the animation variables, see animator tab for animation speed and such
	Transform playerObj;
	public Vector2 animPos;
	void Animation()
	{
		var yPos = Mathf.Round((playerObj.position.y) * 100) / 100;
		
		anim.SetInteger("X", Mathf.RoundToInt(directionalInput.x)); // walking & sliding animations
		anim.SetBool("isSliding", wallSliding); 
		
		if (directionalInput.x > 0) {// flips animation
			srender.flipX = false;
		}
		if (directionalInput.x < 0) {
			srender.flipX = true;
		}
		
		if (Mathf.Round((animPos.y - yPos)*100)/100 > 0) {// jumping & falling animations
			anim.SetInteger("Y", -1);
		}
		if ((animPos.y - yPos) < 0) {
			anim.SetInteger("Y", 1);
		}
		if ((animPos.y - yPos) == 0) {
			anim.SetInteger("Y", 0);
		}

		if (directionalInput.x != 0 || animPos.y != yPos){// detects movement
			anim.SetBool("isMoving", true);
		}
		if (directionalInput.x == 0 && animPos.y == yPos) {
			anim.SetBool("isMoving", false);
		}
		
		animPos.y = yPos; // records previous movement
	}
	
	/* THIS FUNCTION IS NOT CURRENTLY BEING USED. JUST KEEPING IT FOR A
     * BIT IN CASE IT BECOMES USEFUL.
     *
     * This function is used to calculate the player's Y-axis velocity.
     * If the player's umbrella is open, it lowers the effect of gravity
     * by reducing the acceleration.
     */ 
	void CalculateYVelocityAcc()
	{
		if (velocity.y < 0 && !controller.collisions.below) // if the player is falling and not on the ground ...
		{
			velocity.y += umbrella.GravityMultiplier * gravity * Time.deltaTime;
		}
		else
		{
			velocity.y += gravity * Time.deltaTime;
		}
	}
	
}
