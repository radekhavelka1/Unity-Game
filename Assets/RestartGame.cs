using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void ResGame()
    {
        Debug.Log("Restart");
        SceneManager.LoadScene("SampleScene");
    }
}
