using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour 
{
    // SerializeFields for assignment in-editor
    // Reference to interaction logic for player 1
    [SerializeField] PlayerInteraction playerOne;
    // Reference to interaction logic for player 2
    [SerializeField] PlayerInteraction playerTwo;
    // Player identification for which gate this is
    [Tooltip ("Must be 1 or 2")]
    [SerializeField] int playerGate;

    // Private fields
    bool gateOpen;
	
	// Update is called once per frame
	void Update () 
	{
        CheckOpen();
        if (gateOpen)
            gameObject.SetActive(false);
	}

    // Determine whether or not the gate should open
    void CheckOpen()
    {
        // Check whether or not the gate should open
        switch (playerGate)
        {
            case 1:
                {
                    // If player 1 found the key and the safe, or if player 2 traded the key, open
                    if (playerOne.FoundKey && playerOne.FoundSafe ||
                        playerTwo.TradedKey)
                    {
                        gateOpen = true;
                    }
                    break;
                }
            case 2:
                {
                    // If player 2 found the key and the safe, or if player 1 traded the key, open
                    if (playerTwo.FoundKey && playerTwo.FoundSafe ||
                        playerOne.TradedKey)
                    {
                        gateOpen = true;
                    }
                    break;
                }
        }
    }
}
