using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Entity))]
public class EntityInspector : Editor {

    public override void OnInspectorGUI() {
        Entity entity = (Entity)target;

        //Entity type dropdown
        entity.Type = (Entity.EntityType)EditorGUILayout.EnumPopup("Type", entity.Type);

        if (entity.Type == Entity.EntityType.Item) {
            //Draw things for item entities

            //Name
            entity.Name = EditorGUILayout.TextField("Name", entity.Name);

            //Item id for the item this entity represents
            entity.ItemId = EditorGUILayout.TextField("Item Id", entity.ItemId);

            //Quantity Slider
            if (entity.Type == Entity.EntityType.Item && entity.Interactable)
                entity.Quantity = EditorGUILayout.IntSlider("Quantity", entity.Quantity, 1, 64);

            //Interactable Toggle
            entity.Interactable = EditorGUILayout.Toggle("Interactable", entity.Interactable);

            //Here we check if the id is correct, and display the information for the item that corrisponds to that id:
            EditorGUILayout.BeginVertical(EditorStyles.textArea);
            if (entity.ItemId != null || entity.ItemId != "") {
                string itemid = entity.ItemId;
                if (CheckId(itemid)) {
                    EditorGUILayout.LabelField("Item information:", EditorStyles.boldLabel);
                    Item founditem = Item.ItemList[entity.ItemId];
                    EditorGUILayout.LabelField("Item Name: ", founditem.name);
                    EditorGUILayout.LabelField("Description: ", founditem.description);

                    //Icon
                    EditorGUILayout.LabelField("Item Icon: ");
                    Rect rect = GUILayoutUtility.GetRect(100, 100);
                    EditorGUI.DrawTextureTransparent(rect, founditem.sprite.texture, ScaleMode.ScaleToFit);

                    //Weapon info
                    if (founditem is Weapon) {
                        Weapon weapon = (Weapon)founditem;
                        EditorGUILayout.LabelField("Attack: ", "" + weapon.Damage);
                    }
                }
                else {
                    EditorGUILayout.LabelField("Error, " + entity.ItemId + " is invalid!", EditorStyles.boldLabel);
                }
            }
            else {
                EditorGUILayout.LabelField("Error, itemid is null!", EditorStyles.boldLabel);
            }
            EditorGUILayout.EndVertical();

        }
        else if (entity.Type == Entity.EntityType.WorldObject) {
            //Draw things for a world object.

            //Name
            entity.Name = EditorGUILayout.TextField("Name", entity.Name);

            EditorGUILayout.BeginHorizontal();
            //Description Box
            EditorGUILayout.PrefixLabel("Description");
            entity.Description = EditorGUILayout.TextArea(entity.Description);
            EditorGUILayout.EndHorizontal();

            //Interactable Toggle
            entity.Interactable = EditorGUILayout.Toggle("Interactable", entity.Interactable);
        }
    }

    bool CheckId(string id) {
        return Item.ItemList.ContainsKey(id);
    }
}
