using UnityEngine;
using System.Collections;

public class TopDownEnemy : MonoBehaviour {

    private Quaternion lookRotation;
    private Vector3 diff;

    public float health = 5f;
    public float Damage = 3f;
    public float lerp = 0.1f;
    public GameObject objectToDrop;
    public GameObject bullet;
    public Transform bulletPoint;
    private float timeSinceLastFire = 0;
    private GameObject firedBullet;

    void Update() {
        timeSinceLastFire -= Time.deltaTime;
        //Alteration of the Mouselook behaviour.
        diff = Game.topdownPlayer.transform.position - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, diff, 10f);
        if (hit) {
            if (hit.transform.tag == "Player") {
                lookRotation = Quaternion.Euler(0f, 0f, rot_z - 90);
                if(timeSinceLastFire < 0) {
                    Shoot();
                }
            }
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, lerp);
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

    public void Shoot() {
        firedBullet = (GameObject)Instantiate(bullet, bulletPoint.position, bulletPoint.rotation);
        firedBullet.GetComponent<EnemyBullet>().foward = bulletPoint.up;
        timeSinceLastFire = 0.5f;
    }

    public void Destroy() {
        if (objectToDrop != null) {
            Instantiate(objectToDrop, this.transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
