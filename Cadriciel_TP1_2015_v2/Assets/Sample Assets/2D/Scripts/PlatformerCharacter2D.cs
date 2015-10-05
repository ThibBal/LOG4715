using UnityEngine;
using System.Collections;

public class PlatformerCharacter2D : MonoBehaviour 
{
	bool facingRight = true;							// For determining which way the player is currently facing.

	[SerializeField] float maxSpeed = 10f;				// The fastest the player can travel in the x axis.
	[SerializeField] float jumpForce = 200f;			// Amount of force added when the player jumps.	
	[SerializeField] float wallJumpForce = 800f;		// Amount of force at the horizontal when the plyaer jumps from a wall.
	[SerializeField] float jetpackForce = 80f;			// Amount of force added when the player use his jetpack.	
	[Range(0, 1)]
	[SerializeField] float crouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, 2)]
	[SerializeField] float  airControl = 1f;			// Whether or not a player can steer while jumping.
	[Range(0.01f, 0.2f)]
	[SerializeField] float jumpTime = 0.08f;			// The maximum jump time.
	[SerializeField] int maxJumps = 3;					// Number of maximum jumps before jetpack.
	[SerializeField] LayerMask whatIsGround;			// A mask determining what is ground to the character.
	[SerializeField] LayerMask whatIsWall;				// A mask determining what is wall to the character.
	[SerializeField] bool showMaxHeight = false;		// Show the max height with a simpe jump.

	int jumpsCount = 0;									// Number of jumps done
	bool jumping = false;								// Whether or not the player is jumping.
	bool touchingWall = false;							// Whether or not the player is touching a wall.
	public static bool jetpacking = false;				// Whether or not the player is using his jetpack.
	Transform groundCheck;								// A position marking where to check if the player is grounded.
	float groundedRadius = .2f;							// Radius of the overlap circle to determine if grounded.
	public static bool grounded = false;				// Whether or not the player is grounded.
	Transform ceilingCheck;								// A position marking where to check for ceilings
	float ceilingRadius = .01f;							// Radius of the overlap circle to determine if the player can stand up.
	Transform wallCheck;								// A position marking where to check if the player is over a wall.
	float wallRadius = .5f;								// Radius of the overlap circle to determine if touching a wall.
	Animator anim;										// Reference to the player's animator component.

    void Awake()
	{
		// Setting up references.
		groundCheck = transform.Find("GroundCheck");
		wallCheck = transform.Find("WallCheck");
		ceilingCheck = transform.Find("CeilingCheck");
		anim = GetComponent<Animator>();
	}

	void Update() { 
		if (showMaxHeight) {  
			//Not a great success...
			Debug.DrawLine (new Vector3 (rigidbody2D.position.x - 100, rigidbody2D.position.y +jumpForce/45), new Vector3 (rigidbody2D.position.x + 100, rigidbody2D.position.y + +jumpForce/45), Color.blue, 0, true);
		} 

	}


	void FixedUpdate()
	{
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
		anim.SetBool("Ground", grounded);

		// Is the player against a wall ?
		touchingWall = Physics2D.OverlapCircle(groundCheck.position, wallRadius, whatIsWall) && !grounded;

		// Set the vertical animation
		anim.SetFloat("vSpeed", rigidbody2D.velocity.y);
	}


	public void Move(float move, bool crouch, bool jump)
	{
		
		// If crouching, check to see if the character can stand up
		if (!crouch && anim.GetBool ("Crouch")) {
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle (ceilingCheck.position, ceilingRadius, whatIsGround))
				crouch = true; 
		}
		
		
		//only control the player if grounded or airControl is bigger than 0
		if (grounded || airControl > 0) {
			// Set whether or not the character is crouching in the animator
			anim.SetBool ("Crouch", crouch);

			// Reduce the speed if crouching by the crouchSpeed multiplier
			if (grounded) { move = (crouch ? move * crouchSpeed : move); }
			else { move = move*airControl; }

			// The Speed animator parameter is set to the absolute value of the horizontal input.
			anim.SetFloat ("Speed", Mathf.Abs (move));
			
			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !facingRight)
				// ... flip the player.
				Flip ();
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && facingRight)
				// ... flip the player.
				Flip ();
		}
		
		// If the player should jump...
		if (((jumpsCount < maxJumps) && !touchingWall) && jump) {
			jumpsCount++;
			//Debug.Log("This is my jump :"+jumpsCount+"");
			jumping = true;
			StartCoroutine (JumpRoutine ());
			//Check if the player needs to use his jetpack
		} else if (jumpsCount == maxJumps && CrossPlatformInput.GetButton ("Jump")) {
			rigidbody2D.AddForce (new Vector2 (jetpackForce, jetpackForce));
			jetpacking = true;
			Debug.Log ("I am using my jetpack");
		}

		// If you need to walljump
		if (touchingWall && jump) {
			if (facingRight && wallJumpForce > 0) { wallJumpForce = -wallJumpForce; }
			else if (!facingRight && wallJumpForce < 0) { wallJumpForce = -wallJumpForce; }
			Flip ();
			jumping = true;
			StartCoroutine(WallJumpRoutine());
			Debug.Log("I am doing a wall jump");
		}

		if (grounded && !jumping) { // not in the air
			jetpacking = false;
			jumpsCount = 0;
			rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);
			
		} else if (airControl > 0) {
			rigidbody2D.velocity = new Vector2 (move * maxSpeed * airControl, rigidbody2D.velocity.y);
		}

	}
	
	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
	//Source: http://gamasutra.com/blogs/DanielFineberg/20150825/244650/Designing_a_Jump_in_Unity.php
	// "The just right"
	IEnumerator JumpRoutine()
	{
		rigidbody2D.velocity = Vector2.zero;
		float timer = 0;

		while(CrossPlatformInput.GetButton ("Jump") && timer < jumpTime){
			//Calculate how far through the jump we are as a percentage
			//apply the full jump force on the first frame, then apply less force
			//each consecutive frame
			float proportion= timer / jumpTime;
			// Create the jump vector with a jumpForce modulate by the number of jumps (100% then 50%, then 33% etc.)
			Vector2 thisFrameJumpVector = Vector2.Lerp (new Vector2 (0f, jumpForce/jumpsCount), Vector2.zero, proportion);
			rigidbody2D.AddForce (thisFrameJumpVector);
			timer += Time.deltaTime;
			yield return null;
		}
		jumping = false;
		
	}

	IEnumerator WallJumpRoutine() {
		float timer = 0;
		while(Input.GetKey(KeyCode.Space) && timer < jumpTime) {
			float proportion = timer / jumpTime;
			Vector2 jumpVector = Vector2.Lerp(new Vector2(wallJumpForce, jumpForce), Vector2.zero, proportion);
			rigidbody2D.AddForce(jumpVector);
			timer += Time.deltaTime;
			yield return null;
		}
		jumping = false;
		touchingWall = false;
	}
}
