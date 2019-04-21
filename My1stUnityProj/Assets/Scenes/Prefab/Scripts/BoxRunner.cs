using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRunner : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public float speed = 1f;
    public float jumpForce = 10f;
    public float minimumFloorHeight = 1;

    public bool isGrounded = false;
    public LayerMask whatIsGround;

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 currentPosition = transform.position;

        float deltaX = (horizontalInput * speed * Time.deltaTime);

        transform.position = new Vector3(
            currentPosition.x + deltaX,
            currentPosition.y,
            currentPosition.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(isGrounded == true)
            {
                Debug.Log("JUMP!");
                Rigidbody2D myRigidbody = gameObject.GetComponent<Rigidbody2D>();
                if (myRigidbody != null)
                {
                    myRigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Renderer render = GetComponent<Renderer>();
        render.material.color = Color.yellow;
    }

    private void FixedUpdate()
    {
        // Cast a ray straight down.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, minimumFloorHeight, whatIsGround.value);

        // If it hits something...
        if (hit.collider != null)
        {
            // Calculate the distance from the surface and the "error" relative
            // to the floating height.
            float distance = Mathf.Abs(hit.point.y - transform.position.y);

            //Debug.Log("Hit Object: " + hit.collider.gameObject.name + ", " + distance);

            Debug.DrawLine(transform.position, hit.point, Color.green);

            isGrounded = true;
        }
        else
        {
            isGrounded = false;
            Debug.DrawLine(transform.position, transform.position - (Vector3.up * minimumFloorHeight), Color.red);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.tag == "Ground")
        {
            //Debug.Log("Touched Ground!");
            //isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Ground")
        {
            //Debug.Log("Stopped Touching Ground!");
            //isGrounded = false;
        }
    }

}
