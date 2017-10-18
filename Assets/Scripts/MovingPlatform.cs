using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

    public float MovementPerSecond = 1.0f;

    public float roamStart = 0f;
    public float roamEnd = 0f;

    private Vector2 roamStartVector;
    private Vector2 roamEndVector;
    private bool flip = false;

    public void Awake() {
        roamStartVector = new Vector2(transform.position.x + roamStart, transform.position.y);
        roamEndVector = new Vector2(transform.position.x + roamEnd, transform.position.y);
    }

    public void Update() {
        if (transform.position.x > roamStartVector.x && !flip) {
            flip = true;
        }
        else if (transform.position.x < roamEndVector.x && flip) {
            flip = false;
        }
    }

    void FixedUpdate() {
        if(!flip) GetComponent<Rigidbody2D>().MovePosition(new Vector2(transform.position.x + (MovementPerSecond / 20), transform.position.y));
        else GetComponent<Rigidbody2D>().MovePosition(new Vector2(transform.position.x + -(MovementPerSecond / 20), transform.position.y));
    }

    void OnDrawGizmosSelected() {
        Debug.DrawRay(new Vector2(transform.position.x + roamStart, transform.position.y - 1.5f), Vector2.up * 3, Color.red);
        Debug.DrawRay(new Vector2(transform.position.x + roamEnd, transform.position.y - 1.5f), Vector2.up * 3, Color.red);
    }
}
