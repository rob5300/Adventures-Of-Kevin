using System;
using UnityEngine;

public class Key : Item {

    public enum KeyType {RED, BLUE};
    public KeyType keyType;
    public GameObject Prefab;

    public Key(string id, string name, string description, KeyType keytype, Sprite icon, GameObject prefab) :base(id, name, description, icon) {
        Prefab = prefab;
        keyType = keytype;
    }

    public override void Use() {
        throw new NotImplementedException();
    }
}