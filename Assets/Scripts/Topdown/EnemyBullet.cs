using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {

    public Vector3 foward;
    TopdownPlayer hitPlayer;

    void Start() {
        GetComponent<Rigidbody2D>().velocity = foward * 10;
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "Bullet") {
            hitPlayer = collision.gameObject.GetComponent<TopdownPlayer>();
            if (hitPlayer) {
                hitPlayer.TakeDamage(3);
            }
            Destroy(gameObject);
        }
    }
}
