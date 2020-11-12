using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody playerRigidbody = null;

    #region Basic player movement variables

    [Header("Walking Variables")]
    [SerializeField] private float X_MovementSpeed = 1f;
    [SerializeField] private float Z_MovementSpeed = 1f;

    [Header("Jumping and Jumping Detection Variables")]
    [SerializeField] private float raycastLength = 0.0f;
    [SerializeField] private LayerMask groundLayer = LayerMask.GetMask();
    [SerializeField] private int jumpingRaycastDirectionMultiplier = 1;
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
        XMovement();
        ZMovement();
        PlayerJump();

        SwitchGravityAndControls();
    }

    void XMovement()
    {
        float XFloat = Time.deltaTime * Input.GetAxis("Horizontal") * X_MovementSpeed;
        playerRigidbody.velocity = new Vector3(XFloat , playerRigidbody.velocity.y , playerRigidbody.velocity.z);
    }

    void ZMovement()
    {
        float ZFloat = Time.deltaTime * Z_MovementSpeed;
        playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x , playerRigidbody.velocity.y , ZFloat);
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
        Ray groundedRay = new Ray(transform.position, -transform.up * jumpingRaycastDirectionMultiplier);
        RaycastHit rayHitInfo;


        if (Physics.Raycast(groundedRay , out rayHitInfo , raycastLength , groundLayer , QueryTriggerInteraction.Ignore))
        {
            isGrounded = true;
        }
        else
            isGrounded = false;
    }

    private void SwitchGravityAndControls()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            jumpingRaycastDirectionMultiplier *= -1;
            Physics.gravity *= -1;
            jumpStrength *= -1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SectionTrigger")
        {
            SectionManager sm = other.transform.parent.GetComponent<SectionManager>();
            if (sm != null)
                sm.AddSection();
        }
        else if (other.tag == "SectionDeleter")
        {
            SectionManager sm = other.transform.parent.GetComponent<SectionManager>();
            sm.RemoveSection();
        }
    }
}
