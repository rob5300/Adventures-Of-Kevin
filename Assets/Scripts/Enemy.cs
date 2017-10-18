using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float health = 5f;
    public float Damage = 3f;
    public GameObject objectToDrop;

    public float MovementPerSecond = 1.0f;

    public bool movement = true;

    public float roamStart = 0f;
    public float roamEnd = 0f;

    private Vector2 roamStartVector;
    private Vector2 roamEndVector;

    public void Awake() {
        roamStartVector = new Vector2(transform.position.x + roamStart, transform.position.y);
        roamEndVector = new Vector2(transform.position.x + roamEnd, transform.position.y);
    }

    public void TakeDamage(float damageToTake) {
        if (health - damageToTake <= 0.01) {
            health = 0;
        }
        else {
            health -= damageToTake;
        }
        GetComponent<Animator>().SetFloat("Health", health);
    }

    public void Update() {
        if (movement) {
            if (transform.position.x > roamStartVector.x) {
                transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
                MovementPerSecond *= 1f;
            }
            else if (transform.position.x < roamEndVector.x) {
                transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
                MovementPerSecond *= 1f;
            }
            transform.Translate(new Vector3(MovementPerSecond * Time.deltaTime, 0, 0));
        }
    }

    void OnDrawGizmosSelected() {
        if (movement) {
            Debug.DrawRay(new Vector2(transform.position.x + roamStart, transform.position.y - 1.5f), Vector2.up * 3, Color.red);
            Debug.DrawRay(new Vector2(transform.position.x + roamEnd, transform.position.y - 1.5f), Vector2.up * 3, Color.red); 
        }
    }

    public void Destroy() {
        if(objectToDrop != null) {
            Instantiate(objectToDrop, this.transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
