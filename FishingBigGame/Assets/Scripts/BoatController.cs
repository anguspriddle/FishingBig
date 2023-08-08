using UnityEngine;

public class BoatController : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 3f;

    void Update()
    {
        // Boat movement and rotation
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Move forward and backward
        transform.Translate(Vector3.left * verticalInput * speed * Time.deltaTime);

        // Rotate left and right
        transform.Rotate(Vector3.up * horizontalInput * rotationSpeed * Time.deltaTime);
    }
}
