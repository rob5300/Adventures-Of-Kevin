using UnityEngine;
using System.Collections;

public class Savepoint : MonoBehaviour {

    public void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            collision.GetComponent<TopdownPlayer>().startPosition = transform.position;
            Destroy(gameObject);
        }
    }
}
