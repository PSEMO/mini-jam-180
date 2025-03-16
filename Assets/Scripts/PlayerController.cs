using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float Speed = 10;
    [SerializeField] float jump = 7;

    [SerializeField] float MinimumEnergyForJumping = 10;
    bool CanDoubleJump = true;

    [SerializeField] float colliderDisableTimeForDropping = 0.25f;
    Rigidbody2D rb;

    [SerializeField] LayerMask GroundLayer;
    [SerializeField] Transform FeetPoint;

    PlayerManager playerManager;

    bool isRightLooking = true;
    bool isOnPlatform = false;

    BoxCollider2D playerCollider;

    //animator //animator;

    void Start()
    {
        //animator = GetComponent<//animator>();
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        playerManager = GetComponent<PlayerManager>();
    }

    void Update()
    {
        #region Taking inputs for basic movement

        bool Right = Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.RightArrow);

        bool Left = Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.LeftArrow);

        bool Up = Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.Space);

        bool Down = Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.LeftCommand);
        #endregion

        #region Basic movement

        if (Right)
        {
            if (!Left)
            {
                rb.linearVelocity = new Vector3(Speed, rb.linearVelocity.y, 0);
                //animator.SetBool("isRunning", true);

                if (!isRightLooking)
                {
                    isRightLooking = true;
                    Turn();
                }
            }
            else
            {
                rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
                //animator.SetBool("isRunning", false);
            }
        }
        else if (Left)
        {
            rb.linearVelocity = new Vector3(-Speed, rb.linearVelocity.y, 0);
            //animator.SetBool("isRunning", true);

            if (isRightLooking)
            {
                isRightLooking = false;
                Turn();
            }
        }
        else
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
            //animator.SetBool("isRunning", false);
        }

        if (Up)
        {
            Debug.Log(playerManager.Energy);
            Debug.Log(MinimumEnergyForJumping);
            Debug.Log(CanDoubleJump);
            if (CheckGround())
            {
                Jump();
                CanDoubleJump = true;
            }
            else if (CanDoubleJump && playerManager.Energy > MinimumEnergyForJumping)
            {
                Jump();
                CanDoubleJump = false;
            }
        }
        else if (Down)
        {
            if(CheckGround() && isOnPlatform && playerCollider.enabled)
            {
                StartCoroutine(DisablePlayerCollider(colliderDisableTimeForDropping));
            }
        }
        #endregion
    }

    IEnumerator DisablePlayerCollider(float disableTime)
    {
        playerCollider.enabled = false;
        yield return new WaitForSeconds(disableTime);
        playerCollider.enabled = true;
    }

    public void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jump, 0);
        //animator.SetBool("isJumping", true);
    }

    bool CheckGround()
    {
        if (Physics2D.OverlapBox(FeetPoint.position, new Vector2(FeetPoint.localScale.x, FeetPoint.localScale.y), 0, GroundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool isOnGround()
    {
        if (Physics2D.OverlapBox(FeetPoint.position, new Vector2(FeetPoint.localScale.x, FeetPoint.localScale.y), 0, GroundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Turn()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
    }

    private void OnDisable()
    {
        //animator.SetBool("isJumping", false);
        //animator.SetBool("isRunning", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CheckGround())
        {
            //animator.SetBool("isJumping", false);
        }
        if(collision.gameObject.CompareTag("Platform"))
        {
            isOnPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isOnPlatform = false;
        }
    }
}
