using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CrashDetector : MonoBehaviour
{
    bool hasCrashed = false;
    [SerializeField] float reloadSceneDelay = 0.5f;
    [SerializeField] ParticleSystem crashEffect;
    [SerializeField] AudioClip crashSFX;
    /* ------------------------------------------------------------------------------
    We use OnTriggerEnter2D instead of OnCollisionEnter2D
    because the player's collision happens as soon as he touches the ground.
    We can bypass this by setting a new collider (e.g. for player's head).
    The new collider must be used as a Trigger (Is Trigger = true).
    Then we can use OnTriggerEnter2D to decide what object the player collided at (e.g. the Ground)
    */
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground" && !hasCrashed)
        {
            hasCrashed = true;
            GetComponent<PlayerController>().DisableControls();
            crashEffect.Play();
            /* 
            We use PlayOneShot method when we want to play a particular SFX while
            the component may has a variety of SFX's.
            Audio Source Component should be added to the main object
            */
            GetComponent<AudioSource>().PlayOneShot(crashSFX);
            Invoke("ReloadScene", reloadSceneDelay);
        }
    }
    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}