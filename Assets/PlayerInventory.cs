using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public int NumberOfClipboards { get; private set; }

    public Canvas mujCanvas;

    private bool thresholdReached = false;

    public void ClipboardsCollected()
    {
        NumberOfClipboards++;

        if(NumberOfClipboards == 1)
        {
            // Pause the game
            Time.timeScale = 0f;

            // Load the pause screen scene
            SceneManager.LoadScene("Story_1", LoadSceneMode.Additive);;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        if(NumberOfClipboards == 2)
        {
            // Pause the game
            Time.timeScale = 0f;

            // Load the pause screen scene
            SceneManager.LoadScene("Story_2", LoadSceneMode.Additive);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        if (NumberOfClipboards == 3)
        {
            // Pause the game
            Time.timeScale = 0f;

            // Load the pause screen scene
            SceneManager.LoadScene("Story_3", LoadSceneMode.Additive);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        if (NumberOfClipboards == 4)
        {
            // Pause the game
            Time.timeScale = 0f;

            // Load the pause screen scene
            SceneManager.LoadScene("Story_4", LoadSceneMode.Additive);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        if (NumberOfClipboards == 5)
        {
            // Pause the game
            Time.timeScale = 0f;

            // Load the pause screen scene
            SceneManager.LoadScene("Story_5", LoadSceneMode.Additive);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        if (NumberOfClipboards == 6)
        {
            // Pause the game
            Time.timeScale = 0f;

            // Load the pause screen scene
            SceneManager.LoadScene("Story_6", LoadSceneMode.Additive);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        if (NumberOfClipboards == 7)
        {
            // Pause the game
            Time.timeScale = 0f;

            // Load the pause screen scene
            SceneManager.LoadScene("Story_7", LoadSceneMode.Additive);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        if (NumberOfClipboards == 8)
        {
            // Pause the game
            Time.timeScale = 0f;

            // Load the pause screen scene
            SceneManager.LoadScene("Story_8", LoadSceneMode.Additive);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        if (NumberOfClipboards == 9)
        {
            // Pause the game
            Time.timeScale = 0f;

            // Load the pause screen scene
            SceneManager.LoadScene("Story_9", LoadSceneMode.Additive);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }


        if (NumberOfClipboards >= 10 && !thresholdReached) // Check if the threshold has been reached
        {
            thresholdReached = true;
            SceneManager.LoadScene("EndScreen");
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        UnityEngine.UI.Text txt = mujCanvas.GetComponent<UnityEngine.UI.Text>();
        txt.text = $"Clipboards found: {NumberOfClipboards} / 10";
    }

    
}
