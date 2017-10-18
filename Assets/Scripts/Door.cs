using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    private TopdownPlayer player;
    public Key.KeyType DoorKeyType = Key.KeyType.RED;
    public AudioClip open;

    public void OnCollisionEnter2D(Collision2D collision) {
        player = collision.gameObject.GetComponent<TopdownPlayer>();
        if(player) {
            if(DoorKeyType == Key.KeyType.RED && player.redKeys > 0) {
                player.redKeys--;
                AudioSource.PlayClipAtPoint(open, Camera.main.transform.position);
                Destroy(gameObject);
            }
            else if (DoorKeyType == Key.KeyType.BLUE && player.blueKeys > 0) {
                player.blueKeys--;
                AudioSource.PlayClipAtPoint(open, Camera.main.transform.position);
                Destroy(gameObject);
            }
        }
    }
}
