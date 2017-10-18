using UnityEngine;
using System.Collections;
using System;

public class Weapon : Item {

    public float Damage;
    public GameObject Prefab;

    public Weapon(string id, string name, string description, float attackDamage, Sprite icon, GameObject prefab) :base(id, name, description, icon) {
        Damage = attackDamage;
        Prefab = prefab;
    }

    public Weapon(string id, string name, string description, float attackDamage, Sprite icon) : base(id, name, description, icon) {
        Damage = attackDamage;
    }

    public override void Use() {
        throw new NotImplementedException();
    }
}
