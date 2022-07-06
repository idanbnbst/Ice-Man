using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool canMove = true, isReversed = false, objectOnGround = false;

    [Header("Player Mechanics")]
    [SerializeField] float torqueAmount = 20f;
    [SerializeField] float baseSpeed = 20f;
    [SerializeField] float boostSpeed = 30f;
    [SerializeField] float jumpForce = 20f;

    [Header("Reverse Params")]
    [SerializeField] float maxCoolDownTimer = 1f;
    [SerializeField] float reverseDelaySeconds = 1f;
    float coolDownTimer;
    // [SerializeField] float flipAmount = 200f;
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
            if (objectOnGround)
                Jump();
            if (!isReversed)
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
        else if (Input.GetKey(KeyCode.DownArrow) && coolDownTimer < 0)
        {
            playerRG2D.transform.Rotate(new Vector3(0, 180, 0)); // Flip object
            setIsReverse();
            StartCoroutine(reverseForceAffect());
            coolDownTimer = maxCoolDownTimer;
        }
        // else if (Input.GetKey(KeyCode.UpArrow))
        //     playerRG2D.transform.Rotate(new Vector3(-flipAmount * Time.deltaTime, 0, 0));
        // else if (Input.GetKey(KeyCode.DownArrow))
        //     playerRG2D.transform.Rotate(new Vector3(flipAmount * Time.deltaTime, 0, 0));
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            playerRG2D.velocity += new Vector2(0f, jumpForce);
    }

    void setIsReverse()
    {
        if (!isReversed)
            isReversed = true;
        else
            isReversed = false;
    }

    // This functions returns an IEnumerator, means we can call it with StartCoroutine(f)
    // and the function will be able to be executed with delay by using yield retun new WaitForSeconds(float seconds)
    IEnumerator reverseForceAffect()
    {
        surfaceEffector2D.forceScale = 0.02f;
        yield return new WaitForSeconds(reverseDelaySeconds);
        surfaceEffector2D.speed *= -1;
        surfaceEffector2D.forceScale = 1;
    }
    void BoostPlayer()
    {
        // Need to fix an issue when boost is enabled while reversed
        if (Input.GetKeyDown(KeyCode.B))
            surfaceEffector2D.speed = boostSpeed;
        else if (Input.GetKeyUp(KeyCode.B))
            surfaceEffector2D.speed = baseSpeed;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!objectOnGround)
        {
            objectOnGround = true;
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (objectOnGround)
        {
            objectOnGround = false;
        }
    }
}