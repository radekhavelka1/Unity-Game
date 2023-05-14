using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public int NumberOfClipboards { get; private set; }

    public Canvas mujCanvas;

    public void ClipboardsCollected()
    {
        NumberOfClipboards++;
        UnityEngine.UI.Text txt = mujCanvas.GetComponent<UnityEngine.UI.Text>();
        txt.text = $"Clipboards found: {NumberOfClipboards} / 10";
    }
}
