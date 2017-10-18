using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEditor.SceneManagement;

public class EditorScript : MonoBehaviour {
    //This script goes in a folder called Editor, and creates a new menu in the editor for each of the methods below
    //They load a saved scene if the game is not running.

    [MenuItem ("Scenes/Sidescroller")]
    static void LoadSide() {
        if (!EditorApplication.isPlaying)
            EditorSceneManager.OpenScene("Assets/Scenes/Sidescroller.unity");
    }

    [MenuItem("Scenes/Topdown")]
    static void LoadTopdown() {
        if (!EditorApplication.isPlaying)
            EditorSceneManager.OpenScene("Assets/Scenes/Topdown.unity");
    }

}
