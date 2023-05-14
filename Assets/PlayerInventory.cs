using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
            OnThresholdReached.Invoke(); // Invoke the Unity Event
        }

        UnityEngine.UI.Text txt = mujCanvas.GetComponent<UnityEngine.UI.Text>();
        txt.text = $"Clipboards found: {NumberOfClipboards} / 10";
    }
}
