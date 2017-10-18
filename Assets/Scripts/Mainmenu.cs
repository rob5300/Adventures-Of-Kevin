using UnityEngine;
using System.Collections;

public class Mainmenu : MonoBehaviour {

	public void LoadLevel(string levelname) {
        Game.ChangeLevel(levelname);
    }

    public void Quit() {
        Application.Quit();
    }

}
