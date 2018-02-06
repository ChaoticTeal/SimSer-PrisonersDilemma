using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUps : MonoBehaviour {

    [SerializeField]
    Canvas popUpCanvas;


	// Use this for initialization
	void Start () {
        popUpCanvas.enabled = false;
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            popUpCanvas.enabled = true;
        }

        if(Input.GetButton("P1-Confirm") || Input.GetButton("P2-Confirm"))
        {
            popUpCanvas.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            popUpCanvas.enabled = false;
        }
    }

}
