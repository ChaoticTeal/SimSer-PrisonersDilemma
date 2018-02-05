using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleFurniture : MonoBehaviour 
{
    // Private fields
    // Does the furniture have the key?
    bool hasKey;
    // Does the furniture have the safe?
    bool hasSafe;

    // Properties for access in other classes
    // Does the furniture have the key?
    public bool HasKey
    {
        get
        {
            return hasKey;
        }
        set
        {
            hasKey = value;
        }
    }

    // Does the furniture have the safe?
    public bool HasSafe
    {
        get
        {
            return hasSafe;
        }
        set
        {
            hasSafe = value;
        }
    }
}
