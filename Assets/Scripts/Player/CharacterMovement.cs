using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

    public float MovementSpeed = 5.0f;
    public float JumpPower = 10.0f;
    public string GroundLayerName = "Environment";
    public float GroundCheckOffset = 0.601f;
    public Vector2 velocityClamp = new Vector2(8.2f, 10f);
    public AudioClip jump;

    Rigidbody2D rbody;
    Animator animator;
    float xVelocity;
    float yVelocity;
    bool FacingRight = true;
    LayerMask groundLayer;
    bool isGroundedThisFrame;

    void Start () {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundLayer = LayerMask.NameToLayer(GroundLayerName);
    }
	
	void FixedUpdate() {
        if (Game.currentGameState == Game.GameState.PLAYERDIED) return;

        isGroundedThisFrame = isGrounded();

        //Set y velocity to eathier jump power, or past velocity
        if (Input.GetButton("Jump") && isGroundedThisFrame && rbody.velocity.y < 0.1) {
            yVelocity = JumpPower;
            AudioSource.PlayClipAtPoint(jump, Camera.main.transform.position);
        }
        else {
            yVelocity = rbody.velocity.y;
        }

        //Store the x velocity.
        if (!isGroundedThisFrame) {
            xVelocity = (Input.GetAxis("Horizontal") * MovementSpeed) / 3;
        }
        else {
            xVelocity = Input.GetAxis("Horizontal") * MovementSpeed;
        }

        //Flip the character if it walks left
        if (Input.GetAxis("Horizontal") > 0 && !FacingRight) {
            Flip();
        }
        else if (Input.GetAxis("Horizontal") < 0 && FacingRight) {
            Flip();
        }

        //Put both move values together and apply it to the rigidbody.
        rbody.AddForce(new Vector2(xVelocity, yVelocity));
        //Clamp the velocity.
        rbody.velocity = new Vector2(Mathf.Clamp(rbody.velocity.x, -velocityClamp.x, velocityClamp.x), Mathf.Clamp(rbody.velocity.y, -velocityClamp.y, velocityClamp.y));
    }

    bool isGrounded() {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(new Vector2(transform.position.x, (transform.position.y - GroundCheckOffset)), -Vector2.up, 0.07f);
        //Here we perform a raycast down under the character to check if they are on the ground and allowed to jump.
        if (hit && hit.transform.gameObject.layer == groundLayer) {
            return true;
        }
        else {
            return false;
        }
    }

    void Update() {
        if (Game.currentGameState == Game.GameState.PLAYERDIED) return;

        //Update animator values
        animator.SetFloat("HorizontalInput", Mathf.Abs(Input.GetAxis("Horizontal")));
        animator.SetFloat("YVelocity", rbody.velocity.y);
    }

    void Flip() {
        //Flip the character from Right to Left.
        FacingRight = !FacingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

   void OnDrawGizmosSelected() {
        Debug.DrawRay(new Vector2(transform.position.x, (transform.position.y - GroundCheckOffset)), -Vector2.up * 0.07f, Color.yellow);
    }
}
