using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("World/Entity")]
[System.Serializable]
public class Entity : MonoBehaviour {

    public EntityType Type = EntityType.WorldObject;

    public string Name = "New Entity";
    public string Description = "Description";
    public string ItemId = "item.none";
    public int Quantity = 1;
    public bool Interactable = true;

    //bool held = false;

    public enum EntityType {Item, WorldObject, Dummy}

    void FixedUpdate() {

    }
}
