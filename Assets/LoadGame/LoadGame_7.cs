using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame_7 : MonoBehaviour
{
    public GameObject audioListener;
    public void ExitPauseScreen()
    {
        Time.timeScale = 1f; // Unpause the game
        Debug.Log("Should exit");
        SceneManager.UnloadSceneAsync("Story_8");
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        // Disable the AudioListener in the main scene when the pause screen is active
        audioListener.SetActive(false);
    }
}
