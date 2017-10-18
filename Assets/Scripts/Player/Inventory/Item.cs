using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class Item {
    public string name;
    public string description;
    public string id;
    public int quantity;
    public Sprite sprite;
    public bool Equipible;
    public bool Equipped = false;
	
    //Itemlisting
    public static Dictionary<string, Item> ItemList = new Dictionary<string, Item>();
    public static Weapon sword = new Weapon("weapon.sword", "Sword", "A basic sword", 5, Resources.Load<Sprite>("Sprites/sword1"), Resources.Load<GameObject>("Items/Wooden Sword"));
    public static Weapon ironSword = new Weapon("weapon.ironsword", "Iron Sword", "A slightly stronger sword", 7, Resources.Load<Sprite>("Sprites/sword3"), Resources.Load<GameObject>("Items/Iron Sword"));
    public static Key redKey = new Key("key.redkey", "Red key", "A red key, used in red locks", Key.KeyType.RED, Resources.Load<Sprite>("Sprites/redkey"), Resources.Load<GameObject>("Items/Redkey"));
    public static Key blueKey = new Key("key.bluekey", "Blue key", "A blue key, used in red locks", Key.KeyType.BLUE, Resources.Load<Sprite>("Sprites/bluekey"), Resources.Load<GameObject>("Items/Bluekey"));
    public static Weapon gun = new Weapon("weapon.gun", "Gun", "Basic handgun", 5, Resources.Load<Sprite>("Sprites/Gun"));
    //--

    public abstract void Use();

    public Item(string id, string name, string description) {
        this.id = id;
        this.name = name;
        this.description = description;
        quantity = 1;
        ItemList.Add(id, this);
    }

    public Item(string id, string name, string description, Sprite icon) {
        this.id = id;
        this.name = name;
        this.description = description;
        sprite = icon;
        ItemList.Add(id, this);
    }

}

