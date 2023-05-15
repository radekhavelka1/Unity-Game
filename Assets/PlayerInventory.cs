using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerInventory : MonoBehaviour
{
    public int NumberOfClipboards { get; private set; }

    public Canvas mujCanvas;

    public UnityEvent OnThresholdReached; // Declare a Unity Event

    private bool thresholdReached = false;

    public void ClipboardsCollected()
    {
        NumberOfClipboards++;

        if (NumberOfClipboards >= 1 && !thresholdReached) // Check if the threshold has been reached
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
