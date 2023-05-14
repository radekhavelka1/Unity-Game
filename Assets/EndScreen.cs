using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("ITS HERE");
        PlayerInventory inventory = FindObjectOfType<PlayerInventory>();
        inventory.OnThresholdReached.AddListener(ShowEndScreen);
    }

    public void ShowEndScreen()
    {
        Debug.Log("ShowEndScreen() function called");
        SceneManager.LoadScene("End Screen");
    }
}
