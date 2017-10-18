using UnityEngine;
using System.Collections;

public class UIReferencerTopdown : MonoBehaviour {

    public GameObject DeathPanel;

    void Awake() {
        if (Game.uiTopdown == null) Game.uiTopdown = this;
    }

}
