using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

    public void Awake() {
        Game.uiGameOver = gameObject;
    }

    public void Start() {
        Game.uiGameOver = gameObject;
    }

	public void StartGameOver() {
        GetComponent<Animator>().SetTrigger("Gameover");
        Invoke("ChangeLevel", 3);
    }

    void ChangeLevel() {
        Game.ChangeLevel("MainMenu");
    }
}
