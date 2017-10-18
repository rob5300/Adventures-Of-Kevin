using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;

public class Player : MonoBehaviour {

    public string playername = "Player";
    public int lives = 3;
    public float health = 20.0f;
    public Inventory inventory = new Inventory(3);
    public int SelectedSlot = 0;
    public Slider healthSlider;
    public Text healthText;
    public SpriteRenderer heldItem;

    public AudioClip attack;
    public AudioClip damage;
    public AudioClip die;

    private Vector3 startPosition;
    private List<GameObject> overlapGameObjects = new List<GameObject>();
    private List<Entity> foundEntities = new List<Entity>();
    private List<Enemy> foundEnemies = new List<Enemy>();
    private Entity groundItem;
    private float timeTillNextAttack = 0f;

    void Awake() {
        if (Game.player == null) Game.player = this;
        startPosition = transform.position;
        Game.currentGameState = Game.GameState.PLAYINGSIDESCROLLER;
    }

    public void Update() {
        //Update attack timer.
        timeTillNextAttack -= Time.deltaTime;

        //Get all gameobjects arround the player.
        overlapGameObjects = Physics2D.OverlapCircleAll(transform.position, 2.0f).Select(x => x.gameObject).ToList<GameObject>();
        groundItem = null;
        if (overlapGameObjects.Where(x => x.GetComponent<Enemy>() != null).Any()) {
            //Store the found Enemies.
            foundEnemies = overlapGameObjects.Where(x => x.GetComponent<Enemy>() != null).Select(x => x.GetComponent<Enemy>()).ToList();
        }
        if (overlapGameObjects.Where(x => x.GetComponent<Entity>() != null).Any()) {
            //Store the found Entities.
            foundEntities = overlapGameObjects.Where(x => x.GetComponent<Entity>() != null).Where(x => x.GetComponent<Entity>().Type == Entity.EntityType.Item).Select(x => x.GetComponent<Entity>()).ToList();
            groundItem = foundEntities.First();
            Game.ui.ItemInfoParent.SetActive(true);
            Game.ui.ItemName.text = groundItem.Name;
        }
        else {
            Game.ui.ItemInfoParent.SetActive(false);
        }

        //Check if the player has fallen down too far, if soo kill them.
        if(transform.position.y < -10) {
            TakeDamage(9999);
        }

        CheckInputs();
    }

    public void LateUpdate() {
        //Update UI elements.
        UpdateUI();
    }

    private void CheckInputs() {
        //If there is an ground item and the player presses E, swap the items.
        if (Input.GetKeyDown(KeyCode.E) && groundItem != null) {
            PickupItemFromEntity(groundItem);
        }

        //If there is atleast one enemy and the player presses M1, attack.
        if (Input.GetButtonDown("Fire1") && foundEnemies.Count != 0) {
            Attack();
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0) {
            //Scrolling up, go back an item slot.
            if (inventory.SlotInRange(SelectedSlot + 1)){
                SelectedSlot++;
            }
        }
        else if(Input.GetAxis("Mouse ScrollWheel") > 0) {
            //Scrolling down, go down an item slot.
            if (inventory.SlotInRange(SelectedSlot - 1)){
                SelectedSlot--;
            }
        }

        //Check key presses to change selected slot
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            SelectedSlot = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            SelectedSlot = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            SelectedSlot = 2;

        }

        //Set health to max. Cheating purposes.
        if (Input.GetKeyDown(KeyCode.H)) {
            health = 20f;
        }
    }

    public void TakeDamage(float damageToDeal) {
        if (damageToDeal <= 0) return;
        if (health - damageToDeal <= 0) {
            health = 0;
            Die();
        }
        else {
            AudioSource.PlayClipAtPoint(damage, Camera.main.transform.position);
            health -= damageToDeal;
            GetComponent<Animator>().SetBool("DamageFlash", true);
        }
        healthSlider.value = health;
        healthText.text = health.ToString();
    }

    public void Attack() {
        if (inventory.GetItem(SelectedSlot) == null || foundEnemies.Count == 0 || timeTillNextAttack > 0) return;

        float damageToDeal = ((Weapon)inventory.GetItem(SelectedSlot)).Damage;
        if (transform.localScale.x == 1) {
            foundEnemies = foundEnemies.Where(x => x.transform.position.x >= this.transform.position.x).ToList();
        }
        else{
            foundEnemies = foundEnemies.Where(x => x.transform.position.x <= this.transform.position.x).ToList();
        }

        if (foundEnemies.Count != 0) {
            //We have enemies to attack.
            timeTillNextAttack = 0.333f;
            GetComponent<Animator>().SetTrigger("Attacking");
            AudioSource.PlayClipAtPoint(attack, Camera.main.transform.position);
            foreach (Enemy enemy in foundEnemies) {
                enemy.TakeDamage(damageToDeal);
            } 
        }
    }

    void PickupItemFromEntity(Entity ent) {
        //Pickup the item entity.
        //We dont update the held item here, as that is handled in UpdateUI().
        if (Item.ItemList[ent.ItemId] is Weapon) {
            if (inventory.GetItem(SelectedSlot) != null) {
                Instantiate(((Weapon)inventory.GetItem(SelectedSlot)).Prefab, transform.position, Quaternion.identity);
            }
            inventory.AddItem(ent.ItemId, SelectedSlot);
            Destroy(ent.gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Enemy") {
            TakeDamage(coll.gameObject.GetComponent<Enemy>().Damage);
            gameObject.layer = 11;
            Invoke("ResetLayer", 3);
        }
    }

    private void Die() {
        if (Game.currentGameState != Game.GameState.PLAYERDIED) {
            lives--;
            AudioSource.PlayClipAtPoint(die, Camera.main.transform.position);
            Game.PlayerDied();
        }
        else if (lives <= 0) {
            Game.GameOver();
        }
    }

    public void Reset() {
        health = 20.0f;

        healthSlider.value = health;
        healthText.text = health.ToString();

        transform.position = startPosition;
        Game.currentGameState = Game.GameState.PLAYINGSIDESCROLLER;
    }

    public void UpdateUI() {
        //Update ground item UI
        if(groundItem != null) Game.ui.ItemImage.sprite = Item.ItemList[groundItem.ItemId].sprite;

        //Update Item slot ui.
        switch (SelectedSlot) {
            case 0:
                Game.ui.slotGroup0.alpha = 1f;
                Game.ui.slotGroup1.alpha = 0.5f;
                Game.ui.slotGroup2.alpha = 0.5f;
                Game.ui.slotSelector0.SetActive(true);
                Game.ui.slotSelector1.SetActive(false);
                Game.ui.slotSelector2.SetActive(false);
                break;
            case 1:
                Game.ui.slotGroup0.alpha = 0.5f;
                Game.ui.slotGroup1.alpha = 1f;
                Game.ui.slotGroup2.alpha = 0.5f;
                Game.ui.slotSelector0.SetActive(false);
                Game.ui.slotSelector1.SetActive(true);
                Game.ui.slotSelector2.SetActive(false);
                break;
            case 2:
                Game.ui.slotGroup0.alpha = 0.5f;
                Game.ui.slotGroup1.alpha = 0.5f;
                Game.ui.slotGroup2.alpha = 1f;
                Game.ui.slotSelector0.SetActive(false);
                Game.ui.slotSelector1.SetActive(false);
                Game.ui.slotSelector2.SetActive(true);
                break;
            default:
                Debug.LogError("SelectedSlot out of range!");
                break;
        }

        //Update held item sprite.
        if (inventory.GetItem(SelectedSlot) != null) {
            heldItem.sprite = inventory.GetItem(SelectedSlot).sprite;
        }
        else {
            heldItem.sprite = null;
        }

        //Update UI Sprites shown for items
        if (inventory.GetItem(0) != null)
            Game.ui.slotSprite0.sprite = inventory.GetItem(0).sprite;
        else
            Game.ui.slotSprite0.sprite = Game.ui.emptyItemImage;

        if (inventory.GetItem(1) != null)
            Game.ui.slotSprite1.sprite = inventory.GetItem(1).sprite;
        else
            Game.ui.slotSprite1.sprite = Game.ui.emptyItemImage;

        if (inventory.GetItem(2) != null)
            Game.ui.slotSprite2.sprite = inventory.GetItem(2).sprite;
        else
            Game.ui.slotSprite2.sprite = Game.ui.emptyItemImage;
    }

    private void ResetLayer() {
        gameObject.layer = 0;
        GetComponent<Animator>().SetBool("DamageFlash", false);
    }
}
