using UnityEngine;
using UnityEngine.UI;

public class UIReferencer : MonoBehaviour {

    public GameObject DeathPanel;
    public GameObject ItemInfoParent;
    public Text ItemName;
    public Image ItemImage;
    public Sprite emptyItemImage;

    public CanvasGroup slotGroup0;
    public GameObject slotSelector0;
    public Image slotSprite0;
    public CanvasGroup slotGroup1;
    public GameObject slotSelector1;
    public Image slotSprite1;
    public CanvasGroup slotGroup2;
    public GameObject slotSelector2;
    public Image slotSprite2;
    public Text itemNameText;

    void Awake() {
        if(Game.ui == null) Game.ui = this;
    }

    //Methods within this class should call other methods, not create method logic here.
    public void RestartGame() {
        Game.RestartGame();
    }
}
