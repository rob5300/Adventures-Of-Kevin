using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    Vector3 foward;
    TopDownEnemy hitEnemy;

    void Start() {
        foward = Game.topdownPlayer.bulletPoint.up;
        GetComponent<Rigidbody2D>().velocity = foward * 10;
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Bullet") {
            hitEnemy = collision.gameObject.GetComponent<TopDownEnemy>();
            if (hitEnemy) {
                hitEnemy.TakeDamage(5);
            }
            Destroy(gameObject);
        }
    }
}
