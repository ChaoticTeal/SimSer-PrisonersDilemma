using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{

    // SerializeFields for assignment in-editor
    // Player movement speed
    [SerializeField] float speed = 5f;
    // Player identification string
    [Tooltip("Must be either P1 or P2")]
    [SerializeField] string playerID = "P1";

    // Private fields
    // Can the player move?
    bool canMove = true;
    // Horizontal input
    float horizontalInput;
    // Vertical input
    float verticalInput;
    // Player Rigidbody
    Rigidbody2D rigidbody;
    // Name of horizontal movement axis
    string horizontalAxis;
    // Name of vertical movement axis
    string verticalAxis;

    // Properties for access in other classes
    // Can the player move?
    public bool ShouldMove
    {
        get
        {
            return canMove;
        }

        set
        {
            canMove = value;
        }
    }

	// Use this for initialization
	void Start () 
	{
        // Set axis and button names - this allows the code to work for either player
        horizontalAxis = string.Format("{0}-Horizontal", playerID);
        verticalAxis = string.Format("{0}-Vertical", playerID);

        // Get reference to rigidbody
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        if(canMove)
            Move();
	}

    // Checks input and moves player
    void Move()
    {
        horizontalInput = Input.GetAxis(horizontalAxis);
        verticalInput = Input.GetAxis(verticalAxis);
        rigidbody.velocity = new Vector2(horizontalInput * speed * Time.deltaTime, verticalInput * speed * Time.deltaTime);
    }
}
