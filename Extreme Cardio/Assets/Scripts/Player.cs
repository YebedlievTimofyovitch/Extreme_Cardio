using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody playerRigidbody = null;

    #region Basic player movement variables

    [Header("Walking Variables")]
    [SerializeField] private float horizontalMovementSpeed = 1f;

    [Header("Jumping and Jumping Detection Variables")]
    [SerializeField] private float raycastLength = 0.0f;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded = true;


    [SerializeField] private float jumpStrength = 0.0f;
    [SerializeField] ForceMode jumpForceMode = ForceMode.Impulse;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerOnGround();
        HorizontolMovement();
        PlayerJump();
    }

    void HorizontolMovement()
    {
        float horizontalFloat = Time.deltaTime * Input.GetAxisRaw("Horizontal") * horizontalMovementSpeed;
        playerRigidbody.velocity = new Vector3(horizontalFloat , playerRigidbody.velocity.y , playerRigidbody.velocity.z);
    }

    void PlayerJump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            playerRigidbody.AddForce(transform.up * jumpStrength * Time.deltaTime, jumpForceMode);
        }
    }

    void PlayerOnGround()
    {
        Ray groundedRay = new Ray(transform.position, -transform.up);
        RaycastHit rayHitInfo;


        if (Physics.Raycast(groundedRay , out rayHitInfo , raycastLength , groundLayer , QueryTriggerInteraction.Ignore))
        {
            isGrounded = true;
        }
        else
            isGrounded = false;
    }

    
}
