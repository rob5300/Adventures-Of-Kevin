using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class Game {

    public static Player player;
    public static TopdownPlayer topdownPlayer;
    public static UIReferencer ui;
    public static UIReferencerTopdown uiTopdown;
    public static GameObject uiGameOver;
    public static bool paused = false;
    public enum GameState {PLAYINGTOPDOWN, PLAYINGSIDESCROLLER, MENU, PLAYERDIED };

    public static GameState currentGameState = GameState.MENU;
    public static GameState lastGameState = currentGameState;

    public static void PlayerDied() {
        lastGameState = currentGameState;
        if (currentGameState == GameState.PLAYINGSIDESCROLLER) {
            currentGameState = GameState.PLAYERDIED;
            ui.DeathPanel.SetActive(true);
        }
        else if (currentGameState == GameState.PLAYINGTOPDOWN) {
            currentGameState = GameState.PLAYERDIED;
            uiTopdown.DeathPanel.SetActive(true);
        }
    }

    public static void GameOver() {
        currentGameState = GameState.PLAYERDIED;
        uiGameOver.GetComponent<GameOver>().StartGameOver();
    }

    public static void RestartGame() {
        if (lastGameState == GameState.PLAYINGSIDESCROLLER) {
            ui.DeathPanel.SetActive(false);
            player.Reset();
        }
        else if (lastGameState == GameState.PLAYINGTOPDOWN) {
            uiTopdown.DeathPanel.SetActive(false);
            topdownPlayer.Reset();
        }
        else {
            Debug.LogError("Gamestate set incorrectly, is: " + currentGameState);
        }
    }

    public static void ChangeLevel(string levelName) {
        //if (SceneManager.GetSceneByName(levelName) == null) {
        //    Debug.LogError("ChangeLevel(): Given level name '" + levelName + "' invalid.");
        //    return;
        //}

        if (levelName == "MainMenu") currentGameState = GameState.MENU;
        else if (levelName == "Sidescroller") currentGameState = GameState.PLAYINGSIDESCROLLER;
        else if(levelName == "Topdown") currentGameState = GameState.PLAYINGTOPDOWN;

        SceneManager.LoadScene(levelName);
    }
}
