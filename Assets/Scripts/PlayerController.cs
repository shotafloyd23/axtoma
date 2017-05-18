using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	public Rigidbody myBody;
	public GameObject player;
	public float maxSpeed;
	public float moveAcceleration;
	public float jumpAcceleration;
	public float rayDistance;
	private bool isGrounded;
	public float gameOverhight = -5f;
	public AudioSource sfxAuido;
	public AudioClip[] audioFiles;
	private bool gameOver;
	public GameObject menu;
	public Text textScore;
	public int  score;
	public Text highScoreText;
	public int highScore;
	public int scoreSaver;
	public GameObject levelBuilder;
	void Start() {
		menu.SetActive (false);
		score = 0;
	}

	void FixedUpdate ()
	{
		GroundChecker();
		ConstantMove ();
	}

	void Update()
	{
		if(Input.GetKeyDown (KeyCode.Escape)){
			menu.SetActive(true);
		}
		if (transform.position.y < gameOverhight && gameOver == false) {
			sfxAuido.PlayOneShot (audioFiles [2]);
			gameOver = true;
			menu.SetActive (true);
		}
		if (isGrounded && (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown(0)))
		{
			Jump (); 

		}
		if (menu.activeSelf) {
			myBody.isKinematic = true;
			player.SetActive (false);
			levelBuilder.SetActive (false);


		}
		else {
			score = (int)transform.position.z;

		Debug.DrawLine(transform.position, transform.position+Vector3.down*rayDistance);
		Debug.Log(score);
			textScore.text = "Score = " + score;
		}

	}

	void OnCollisionEnter(Collision collisionInfo)
	{
		//isGrounded = true;
		sfxAuido.PlayOneShot (audioFiles [0]);

	}

	void OnCollisionExit(Collision collisionInfo)
	{
		
	}
	void GroundChecker ()
     {
		Ray ray = new Ray ();
		ray.origin = transform.position;
		ray.direction = Vector3.down;
		isGrounded = Physics.Raycast (ray,rayDistance);

				

		
	
	}


	/// <summary>
	/// this function moves the ball on z axis
	/// </summary>
	void ConstantMove()
	{
		Vector3 newVelocity = myBody.velocity;

		//don't move with higher speed than maxSpeed
		if (newVelocity.z >= maxSpeed)
		{
			newVelocity.z = maxSpeed;
		}

		//accelerate to max speed
		else
		{
			newVelocity.z = newVelocity.z + moveAcceleration;
		}

		myBody.velocity = newVelocity;
	}

	/// <summary>
	/// makes the player jump
	/// </summary>
	void Jump()
	{
		sfxAuido.PlayOneShot (audioFiles [1]);
		Vector3 jumpVelocity = myBody.velocity;
		jumpVelocity.y = jumpVelocity.y + jumpAcceleration;
		myBody.velocity = jumpVelocity;

	}
}
