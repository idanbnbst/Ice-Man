using System.Collections;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    bool canMove = true;
    bool isOnGround = false;
    bool isFlipped = false;

    [Header("Player Mechanics")]
    [SerializeField] float torqueAmount = 20f;
    [SerializeField] float baseSpeed = 20f;
    [SerializeField] float boostSpeed = 30f;
    [SerializeField] float jumpForce = 20f;
    [Header("Reverse Params")]
    [SerializeField] float reverseDelayTime = 1f;
    float coolDownTimer;
    Rigidbody2D playerRG2D;
    SurfaceEffector2D surfaceEffector2D;
    void Start()
    {
        coolDownTimer = 0;
        playerRG2D = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
    }
    void Update()
    {
        if (canMove)
        {
            RotatePlayer();

            if (isOnGround)
                Jump();

            if (!isFlipped)
                BoostPlayer();
        }
    }
    public void DisableControls()
    {
        canMove = false;
    }
    void RotatePlayer()
    {
        coolDownTimer -= Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow))
            playerRG2D.AddTorque(torqueAmount);

        else if (Input.GetKey(KeyCode.RightArrow))
            playerRG2D.AddTorque(-torqueAmount);

        else if (Input.GetKey(KeyCode.DownArrow) && coolDownTimer <= 0)
        {
            SetFlipState();
            coolDownTimer = reverseDelayTime;
            playerRG2D.transform.Rotate(new Vector3(0, 180, 0)); // Flip object
            StartCoroutine(ReverseForceAffect());
        }
    }
    // This functions returns an IEnumerator, means we can call it with StartCoroutine(f)
    // and the function will be executed with delay by yield return
    IEnumerator ReverseForceAffect()
    {
        surfaceEffector2D.forceScale = 0.02f;
        yield return new WaitForSeconds(reverseDelayTime);
        surfaceEffector2D.speed *= -1;
        surfaceEffector2D.forceScale = 1f;
    }
    void SetFlipState()
    {
        if (isFlipped)
            isFlipped = false;
        else
            isFlipped = true;
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            playerRG2D.velocity += new Vector2(0f, jumpForce);
    }
    void BoostPlayer()
    {
        if (Input.GetKeyDown(KeyCode.B))
            surfaceEffector2D.speed = boostSpeed;
        else if (Input.GetKeyUp(KeyCode.B))
            surfaceEffector2D.speed = baseSpeed;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "Ground")
            return;
        if (!isOnGround)
            isOnGround = true;
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag != "Ground")
            return;
        if (isOnGround)
            isOnGround = false;
    }
}