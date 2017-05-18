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
    public int score;
    public Text highScoreText;
    public int highScore;
    public GameObject levelBuilder;
    public ParticlesManager particlesManager;

    void Start()
    {
        menu.SetActive(false);
        score = 0;

        highScore = PlayerPrefs.GetInt("HighScore");
        Debug.Log(highScore);
    }

    void FixedUpdate()
    {
        GroundChecker();
        ConstantMove();
    }

    void Update()
    {
        //display menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(true);
        }

        //game over condition
        if (transform.position.y < gameOverhight && gameOver == false)
        {
            GameOver();
        }

        //jump condition
        if (isGrounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            Jump();
        }
        
        if (menu.activeSelf)
        {
            myBody.isKinematic = true;
            player.SetActive(false);
            levelBuilder.SetActive(false);
        }
        else
        {
            score = (int)transform.position.z;

            Debug.DrawLine(transform.position, transform.position + Vector3.down * rayDistance);
            //Debug.Log(score);
            textScore.text = "Score = " + score;
        }

    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        //isGrounded = true;
        sfxAuido.PlayOneShot(audioFiles[0]);
        particlesManager.SpawnParticles();
    }

    void GroundChecker()
    {
        Ray ray = new Ray();
        ray.origin = transform.position;
        ray.direction = Vector3.down;
        isGrounded = Physics.Raycast(ray, rayDistance);
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
        sfxAuido.PlayOneShot(audioFiles[1]);
        particlesManager.SpawnParticles();

        Vector3 jumpVelocity = myBody.velocity;
        jumpVelocity.y = jumpVelocity.y + jumpAcceleration;
        myBody.velocity = jumpVelocity;
    }

    void GameOver()
    {
        sfxAuido.PlayOneShot(audioFiles[2]);
        gameOver = true;
        menu.SetActive(true);

        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }
}
