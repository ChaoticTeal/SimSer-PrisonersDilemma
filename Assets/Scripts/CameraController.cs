using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour 
{

    // SerializeFields for assignment in-editor
    // Camera movement speed
    [SerializeField] float cameraSpeed = 5f;
    // Maximum vertical position of camera
    [SerializeField] float maxY = 10f;
    // Minimum vertical position of camera
    [SerializeField] float minY = 0f;
    // List of things that must be on camera
    [SerializeField] List<Transform> targets;

    // Private fields
    // Average vertical position of targets
    float midY;
    // Total vertical position of all targets
    float totalY;
    // Number of targets
    int totalTargets;
    // Position to move to
    Vector3 newPosition;

	// Use this for initialization
	void Start () 
	{
        // Get the total number of elements in the list
        totalTargets = targets.Count;
	}
	
	// Update is called once per frame
	void Update () 
	{
        Move();
	}

    private void Move()
    {
        totalY = 0f;
        // Add up all target vertical positions
        foreach(Transform t in targets)
            totalY += t.position.y;
        // Divide by number of targets to find average
        midY = totalY / totalTargets;
        // Constrain position
        midY = Mathf.Min(midY, maxY);
        midY = Mathf.Max(midY, minY);
        // Set up new position vector
        newPosition = new Vector3(transform.position.x, midY, transform.position.z);
        // Move to average vertical position
        transform.position = Vector3.Lerp(transform.position, newPosition, cameraSpeed * Time.deltaTime);
    }
}
