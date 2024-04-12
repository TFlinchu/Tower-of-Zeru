using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject pickupTextObject;

    private TMP_Text pickupText;

    void Awake()
    {
        if (instance != null) 
        {
            Destroy(gameObject);
        }
        else 
        {
            instance = this;
            pickupText = pickupTextObject.GetComponent<TMP_Text>();
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ShowPickupText(string itemName)
    {
        pickupText.text = "Press E to pick up " + itemName;
        pickupTextObject.SetActive(true); // Enable the GameObject
    }

    public void HidePickupText()
    {
        pickupText.text = "";
        pickupTextObject.SetActive(false); // Disable the GameObject
    }
}