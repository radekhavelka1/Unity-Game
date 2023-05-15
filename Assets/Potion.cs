using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Potion : MonoBehaviour
{
    public PlayerMovement player;
    public Text healthText;
    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
        if (playerInventory != null)
        {            
            player.Health += 25;
            if(player.Health > 100)
            {
                player.Health = 100;
            }
            Debug.Log(player.Health);
            healthText.text = "HP: " + player.Health.ToString();
            gameObject.SetActive(false);
        }
    }
}
