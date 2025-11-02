using UnityEngine;

public class ResetVehicle : MonoBehaviour
{
    private Vector3 _resetPosition; // optional: predefined reset position

    private void Awake()
    {
        // Store the initial position as the reset position
        _resetPosition = transform.position;
    }

    private void Update()
    {
        // Check if player pressed the 'R' key
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPosition();

            Time.timeScale = 1f; // Resume game if it was paused
        }
    }

    private void ResetPosition()
    {
        // Reset rotation
        transform.rotation = Quaternion.identity;

        // Move up a little bit to avoid ground collision
        transform.position = new Vector3(
            _resetPosition.x,
            _resetPosition.y,
            _resetPosition.z
        );

        GetComponent<Rigidbody2D>().velocity = Vector2.zero; // Reset velocity

        Debug.Log("Vehicle reset to upright position.");
    }
}
