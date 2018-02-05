using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour 
{
    // SerializeFields for assignment in the editor
    // Search radius
    [SerializeField] float searchRadius = 1f;
    // Furniture layer
    [SerializeField] LayerMask furnitureLayer;
    // Player identification string
    [Tooltip("Must be either P1 or P2")]
    [SerializeField]
    string playerID = "P1";

    // Private fields
    // Has the player found the key?
    bool foundKey;
    // Has the player found the safe?
    bool foundSafe;
    // Did the player trade keys?
    bool tradedKey;
    // Name of cancel button
    string cancelButton;
    // Name of confirm button
    string confirmButton;

    // Properties for access in other classes
    // Has the player found the key?
    public bool FoundKey
    {
        get
        {
            return foundKey;
        }
    }

    // Has the player found the safe?
    public bool FoundSafe
    {
        get
        {
            return foundSafe;
        }
    }

    // Did the player trade the key?
    public bool TradedKey
    {
        get
        {
            return tradedKey;
        }
    }

    // Use this for initialization
    void Start () 
	{
        confirmButton = string.Format("{0}-Confirm", playerID);
        cancelButton = string.Format("{0}-Cancel", playerID);
    }
	
	// Update is called once per frame
	void Update () 
	{
        Check();
    }

    // Check for furniture in range and search furniture for relevant items
    void Check()
    {
        Collider2D[] furniture = Physics2D.OverlapCircleAll(transform.position, searchRadius, furnitureLayer);
        if (furniture.Length > 0 && Input.GetButtonDown(confirmButton))
        {
            if (furniture[0].GetComponent<InteractibleFurniture>().HasKey)
            {
                Debug.Log("Key");
                foundKey = true;
                // TODO - Write key popup logic
                // Confirm should be trade key, cancel should be keep key
                tradedKey = Choice();
            }
            else if(furniture[0].GetComponent<InteractibleFurniture>().HasSafe && foundKey && !tradedKey)
            {
                // TODO - safe popup logic
                Debug.Log("Safe");
            }
        }
    }

    bool Choice()
    {
        // Infinitely loop until a decision is made - this might be a terrible idea
        do
        {
            if (Input.GetButtonDown(confirmButton))
                return true;
            else if (Input.GetButtonDown(cancelButton))
                return false;
        } while (true);
    }
}
