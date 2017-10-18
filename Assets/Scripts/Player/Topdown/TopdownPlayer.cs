using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TopdownPlayer : MonoBehaviour {

    public float health = 20f;
    public bool hasGun = false;
    public int redKeys = 0;
    public int blueKeys = 0;
    public Sprite gunWieldingSprite;
    public GameObject bullet;
    public Transform bulletPoint;
    public Slider healthSlider;
    public Text healthText;

    public AudioClip shoot;
    public AudioClip damaged;
    public AudioClip die;
    public AudioClip getItem;

    private Entity entity;
    public Vector3 startPosition;
    private float timeSinceLastFire = 0f;
    private int lives = 3;

    void Awake() {
        if (Game.topdownPlayer == null) Game.topdownPlayer = this;
        Game.currentGameState = Game.GameState.PLAYINGTOPDOWN;
        startPosition = transform.position;
    }

    public void Update() {
        if (Input.GetButton("Fire1") && hasGun && timeSinceLastFire < 0f) {
            Shoot();
        }
        timeSinceLastFire -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.H)) {
            health = 20f;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        entity = collision.GetComponent<Entity>();
        if (entity) {
            AudioSource.PlayClipAtPoint(getItem, Camera.main.transform.position);
            if (entity.ItemId == "key.bluekey") {
                blueKeys++;
                Destroy(entity.gameObject);
            }
            else if (entity.ItemId == "key.redkey") {
                redKeys++;
                Destroy(entity.gameObject);
            }
            else if(entity.ItemId == "weapon.gun") {
                hasGun = true;
                GetComponentInChildren<SpriteRenderer>().sprite = gunWieldingSprite;
                Destroy(entity.gameObject);
            }
        }
    }

    public void Shoot() {
        AudioSource.PlayClipAtPoint(shoot, Camera.main.transform.position);
        Instantiate(bullet, bulletPoint.position, bulletPoint.rotation);
        timeSinceLastFire = 0.200f;
    }

    public void TakeDamage(float damageToDeal) {
        if (damageToDeal <= 0) return;
        if (health - damageToDeal <= 0) {
            health = 0;
            Die();
        }
        else {
            AudioSource.PlayClipAtPoint(damaged, Camera.main.transform.position);
            health -= damageToDeal;
        }
        healthSlider.value = health;
        healthText.text = health.ToString();
    }

    private void Die() {
        lives--;
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        AudioSource.PlayClipAtPoint(die, Camera.main.transform.position);
        Game.PlayerDied();
    }

    public void Reset() {
        health = 20.0f;

        healthSlider.value = health;
        healthText.text = health.ToString();

        transform.position = startPosition;
        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        Game.currentGameState = Game.GameState.PLAYINGTOPDOWN;
    }
}
