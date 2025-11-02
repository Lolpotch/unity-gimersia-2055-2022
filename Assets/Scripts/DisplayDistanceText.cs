using UnityEngine;
using TMPro;

public class DisplayDistanceText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _distanceText;  // UI text to display the distance
    [SerializeField] private Transform _playerTrans;         // Reference to the player’s transform

    private Vector2 _startPosition; // Player starting position

    private void Start()
    {
        // Record the player's starting position at the beginning
        _startPosition = _playerTrans.position;
    }

    private void Update()
    {
        // Calculate distance from starting position
        Vector2 distance = (Vector2)_playerTrans.position - _startPosition;

        // We only care about horizontal distance (ignore Y)
        distance.y = 0f;

        // Ensure it doesn't go negative (e.g., moving backward)
        if (distance.x < 0)
            distance.x = 0;

        // Display the horizontal distance in the text, formatted with no decimals
        _distanceText.text = distance.x.ToString("F0") + " m";
    }
}
