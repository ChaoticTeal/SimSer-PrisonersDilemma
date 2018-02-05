using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureManager : MonoBehaviour 
{
    // SerializeFields for use in-editor
    // List of furniture on this side
    [SerializeField] List<InteractibleFurniture> furniture;
    // Player interaction logic
    [SerializeField] PlayerInteraction player;

    // Private fields
    // Which element in the list holds the key?
    int keyNumber;
    // Which element in the list holds the safe?
    int safeNumber;

    // Use this for initialization
    void Start () 
	{
        // Initialize the RNG
        Random.InitState((int)Time.time);
        // Randomly generate an element number to hide the key
        keyNumber = Randomize();
        furniture[keyNumber].HasKey = true;
        Debug.Log(keyNumber);
    }
	
	// Update is called once per frame
	void Update () 
	{
        // Has the player found and not traded the key?
        bool hideSafe = (player.FoundKey && !player.TradedKey);
        // If so, hide the safe
        if (hideSafe)
            HideSafe();
	}

    // Generate a random number
    int Randomize()
    {
        // Randomly generate from a range of numbers
        int rand = Mathf.RoundToInt(Random.Range(1, furniture.Count * 10));
        // Return the remainder when divided by the number of elements in the list
        // This will always return a number between 0 and the last element's index
        return rand % furniture.Count;
    }

    // Hides the safe if necessary
    void HideSafe()
    {
        // Don't hide more than one safe, return if a safe has already been hidden
        foreach (InteractibleFurniture f in furniture)
            if (f.HasSafe)
                return;
        // Same procedure as the key, randomly decide which element should have the safe and hide it
        safeNumber = Randomize();
        furniture[safeNumber].HasSafe = true;
        Debug.Log(safeNumber);
    }
}
