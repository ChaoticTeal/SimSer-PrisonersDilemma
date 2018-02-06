using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    [SerializeField]
    Canvas keySafeCanvas;
    [SerializeField]
    Canvas questionCanvas;
    [SerializeField]
    Text inputField;
    [SerializeField]
    Text mainInputField;
    [SerializeField]
    Dropdown answerToAssessment;
    [SerializeField]
    string nextScene;


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

    //Dropdown box value
    int dropBoxValue;

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
        dropBoxValue = answerToAssessment.value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
            SceneManager.LoadScene(nextScene);
    }

    // Check for furniture in range and search furniture for relevant items
    void Check()
    {
        Collider2D[] furniture = Physics2D.OverlapCircleAll(transform.position, searchRadius, furnitureLayer);
        if (furniture.Length > 0 && Input.GetButtonDown(confirmButton))
        {
            if (furniture[0].GetComponent<InteractibleFurniture>().HasKey)
            {
                furniture[0].GetComponent<InteractibleFurniture>().HasKey = false;
                foundKey = true;
                keySafeCanvas.enabled = true;
                mainInputField.text = "Typically the end result of the Prisoners Dilemma is both criminals placing their self interest before the groups. " +
                    "Despite how it appears, it's actually the most logical answer since personal interest option is far more desirable. " +
                    "It's also an example of how crowds are foolish as they are wise; that certain individual's choices are ruinous for the group.";
                // Confirm should be trade key, cancel should be keep key
                StartCoroutine(TradeChoice());
                
            }
            else if(furniture[0].GetComponent<InteractibleFurniture>().HasSafe && foundKey && !tradedKey)
            {
                furniture[0].GetComponent<InteractibleFurniture>().HasSafe = false;
                keySafeCanvas.enabled = true;
                inputField.text = "The safe has been found and unlocked. Answer the question to escape.";
                mainInputField.text = "Prisoner's Dilemma can be interpreted in multiple ways." +
                    " It can be a model that represents the difficulties of getting rational and selfish people to cooperate for the common good. It tests conditions that make cooperating appealing. " +
                    "Another model is a representaion of choosing between a selfish choice and an altruistic one. " +
                    "These modals lead to the idea that the Prisoner's Dilemma has something to say about the nature of morality.";
                StartCoroutine(SafeAssessmentQuestion());
                
            }
        }
    }

    IEnumerator TradeChoice()
    {
        // Infinitely loop until a decision is made - this might be a terrible idea
        // Also disable movement for the active player
        gameObject.GetComponent<PlayerMovement>().CanMove = false;
        bool chosen = false;
        yield return new WaitForSeconds(1.0f);
        while (!chosen)
        {
            if (Input.GetButtonDown(confirmButton))
            {
                tradedKey = true;
                chosen = true;
                keySafeCanvas.enabled = false;
            }
            else if (Input.GetButtonDown(cancelButton))
            {
                tradedKey = false;
                chosen = true;
                keySafeCanvas.enabled = false;
            }
            yield return null;
        }
        gameObject.GetComponent<PlayerMovement>().CanMove = true;
    }

    IEnumerator SafeAssessmentQuestion()
    {
        // Infinitely loop until a decision is made - this might be a terrible idea
        // Also disable movement for the active player
        gameObject.GetComponent<PlayerMovement>().CanMove = false;
        bool chosen = false;
        yield return new WaitForSeconds(1.0f);
        questionCanvas.enabled = true;
        
        while (!chosen)
        {
            if (dropBoxValue == 2)
            {
                foundSafe = true;
                chosen = true;
                questionCanvas.enabled = false;
            }
            else
            {
                
                chosen = false;
                
            }
            yield return null;
        }
        gameObject.GetComponent<PlayerMovement>().CanMove = true;
    }
}
