using UnityEngine;
using UnityEngine.U2D; // Needed for SpriteShapeController

// Allows the script to run and update even while in the Unity Editor (not just during Play mode)
[ExecuteInEditMode]
public class EnvironmentGenerator : MonoBehaviour
{
    // The Sprite Shape Controller this script will modify
    [SerializeField] SpriteShapeController _spriteShapeController;
    [SerializeField] private bool _isOnEditMode = true;

    // Number of points along the terrain
    [SerializeField, Range(3, 100)] private int _levelLength = 50;

    // Distance between each generated point along the X-axis
    [SerializeField, Range(1f, 50f)] private float _xMultiplier = 2f;

    // Height scale of the terrain along the Y-axis
    [SerializeField, Range(1f, 50f)] private float _yMultiplier = 2f;

    // How smooth the curve tangents will be
    [SerializeField, Range(0f, 1f)] private float _curveSmoothness = 0.5f;

    // Step size for Perlin noise sampling (affects terrain roughness)
    [SerializeField] private float _noiseStep = 0.5f;

    // How far down to extend the shape to “close” the bottom of the terrain
    [SerializeField] private float _bottom = 5f;

    // Last generated point (for convenience)
    private Vector3 _lastPos;

    // Runs automatically when a value changes in the Inspector (since ExecuteInEditMode is used)

    public void OnValidate()
    {
        // Safety check to prevent null reference errors
        if (_spriteShapeController == null || !_isOnEditMode)
            return;

        // Shortcut to the spline we’ll be modifying
        var spline = _spriteShapeController.spline;
        spline.Clear(); // Clear existing points before generating new terrain

        // Loop through and create points along the terrain
        for (int i = 0; i < _levelLength; i++)
        {
            // Calculate the position of each point using Perlin noise for smooth randomness
            _lastPos = transform.position + new Vector3(
                i * _xMultiplier,                                  // X position
                Mathf.PerlinNoise(0, i * _noiseStep) * _yMultiplier, // Y position from Perlin noise
                0f
            );  

            // Insert the point into the spline
            spline.InsertPointAt(i, _lastPos);

            // Adjust tangents to make curves smooth (except for start and end points)
            if (i != 0 && i != _levelLength - 1)
            {
                spline.SetTangentMode(i, ShapeTangentMode.Continuous);
                spline.SetLeftTangent(i, Vector3.left * _xMultiplier * _curveSmoothness);
                spline.SetRightTangent(i, Vector3.right * _xMultiplier * _curveSmoothness);
            }
        }

        // Add two extra points to “close” the bottom of the shape
        // (this helps form a filled terrain instead of an open line)
        spline.InsertPointAt(_levelLength, new Vector3(_lastPos.x, transform.position.y - _bottom, 0f));
        spline.InsertPointAt(_levelLength + 1, new Vector3(transform.position.x, transform.position.y - _bottom, 0f));
    }
}
