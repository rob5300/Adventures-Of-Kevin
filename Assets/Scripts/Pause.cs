using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

    public GameObject menu;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Toggle();
        }
    }

    public void ToMainMenu() {
        Time.timeScale = 1;
        Game.paused = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void Toggle() {
        if (Game.paused) {
            menu.SetActive(false);
            Time.timeScale = 1;
            Game.paused = false;
        }
        else {
            menu.SetActive(true);
            Time.timeScale = 0;
            Game.paused = true;
        }

    }
}
