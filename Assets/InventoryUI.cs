using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    private TextMeshProUGUI clipboardText;
    // Start is called before the first frame update
    void Start()
    {
        clipboardText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateClipBoardText(PlayerInventory inventory)
    {
        clipboardText.text = inventory.NumberOfClipboards.ToString();
    }
}
