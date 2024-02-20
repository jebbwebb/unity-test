using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
     public float groundCheckDistance = 1f;
public LayerMask groundLayer;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation=true;
    }

    private void Update()
    {
        // Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + transform.TransformDirection(movement));

        // Jumping
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

   private bool IsGrounded()
{
    // Offset the raycast origin slightly below the player model
    Vector3 raycastOrigin = transform.position + Vector3.down * 0.1f; // Adjust the value (0.1f) to your desired offset

    // Perform a raycast downwards to check for ground contact
    RaycastHit hit;
    bool isGrounded = Physics.Raycast(raycastOrigin, Vector3.down, out hit, groundCheckDistance, groundLayer);
    
    Debug.DrawRay(raycastOrigin, Vector3.down * groundCheckDistance, isGrounded ? Color.green : Color.red); // Visualize the raycast as green if grounded, red otherwise

    if (isGrounded)
    {
        Debug.Log("Ground Hit: " + hit.collider.name); // Log the name of the ground object hit
        Debug.Log("Hit Point: " + hit.point); // Log the point where the raycast hit the ground
        Debug.Log("Hit Normal: " + hit.normal); // Log the normal vector of the ground hit
    }
    else
    {
        Debug.Log("Not Grounded"); // Log when the player is not grounded
    }

    return isGrounded;
}


}
