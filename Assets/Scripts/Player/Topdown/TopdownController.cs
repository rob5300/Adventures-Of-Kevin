using UnityEngine;
using System.Collections;

public class TopdownController : MonoBehaviour {

    public float MoveSpeed;
    public Vector2 velocityClamp;

    private Rigidbody2D rbody;
    private float xVelocity;
    private float yVelocity;
    
    public void Start() {
        rbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        if (Game.currentGameState == Game.GameState.PLAYERDIED) return;

        xVelocity = Input.GetAxis("Horizontal") * MoveSpeed;
        yVelocity = Input.GetAxis("Vertical") * MoveSpeed;

        //Put both move values together and apply it to the rigidbody.
        rbody.AddForce(new Vector2(xVelocity, yVelocity));
        //Clamp the velocity.
        rbody.velocity = new Vector2(Mathf.Clamp(rbody.velocity.x, -velocityClamp.x, velocityClamp.x), Mathf.Clamp(rbody.velocity.y, -velocityClamp.y, velocityClamp.y));
    }
}
