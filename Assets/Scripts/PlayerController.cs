using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Attributes
    private Rigidbody2D rigidBody;
    public float thrust = 20.0f;
    public LayerMask groundLayerMask;
    public Animator animator;
    public float runSpeed = 3.0f;

    private static PlayerController sharedInstance;
    private Vector3 initialPosition;
    private Vector2 initialVelocity;
    private float initialGravity;

    private const string HIGHEST_SCORE_KEY = "highestScore";


    //Initialize game before Start
    private void Awake()
    {
        sharedInstance = this;
        rigidBody = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
        initialVelocity = rigidBody.velocity;
        animator.SetBool("isAlive", true);
        initialGravity = rigidBody.gravityScale;
    }

    // Update at a fixed period of time
    private void FixedUpdate()
    {
        GameState currState = GameManager.getInstance().currentGameState;

        if (currState == GameState.InGame)
        {
            if (rigidBody.velocity.x < runSpeed)
            {
                rigidBody.velocity = new Vector2(runSpeed, rigidBody.velocity.y);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        bool canJump = GameManager.getInstance().currentGameState == GameState.InGame;
        bool isOnTheGround = IsOnTheGround();
        animator.SetBool("isGrounded", isOnTheGround);

        if (canJump && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && isOnTheGround) {
            Jump();
        }   
    }

    // METHODS

    // Start is called before the first frame update
    public void StartGame()
    {
        animator.SetBool("isAlive", true);
        transform.position = initialPosition;
        rigidBody.velocity = initialVelocity;
        rigidBody.gravityScale = initialGravity;
    }

    // Access to object
    public static PlayerController getInstance()
    {
        return sharedInstance;
    }

    void Jump() {
            rigidBody.AddForce(Vector2.up * thrust, ForceMode2D.Impulse);
    }

    bool IsOnTheGround() {
        return Physics2D.Raycast(this.transform.position, Vector2.down, 1.0f, groundLayerMask.value);
    }

    // Set hit bunny animation and set gameover
    public void KillPlayer() {
        animator.SetBool("isAlive", false);
        

        //Save max score
        int highestScore = PlayerPrefs.GetInt(HIGHEST_SCORE_KEY);
        int currentScore = GetDistance();
        if (currentScore > highestScore) {
            PlayerPrefs.SetInt(HIGHEST_SCORE_KEY, currentScore);
            
        }
        print("Saved highest score: " + GetMaxScore());


        rigidBody.gravityScale = 0.0f;
        rigidBody.velocity = Vector2.zero;
        GameManager.getInstance().GameOver();
    }

    //Calculate distance
    public int GetDistance() {
        int distance = (int) Vector2.Distance(initialPosition,transform.position);
        return distance;
    }

    // Return highest score
    public int GetMaxScore() {
        return PlayerPrefs.GetInt(HIGHEST_SCORE_KEY);
    }

    // Reset highest score
    public void ResetHighestScore() {
        PlayerPrefs.SetInt(HIGHEST_SCORE_KEY, 0);
    }
}
