using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLine : MonoBehaviour
{
    [SerializeField] float reloadSceneDelay = 0.5f;
    bool isOutOfBounds = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !isOutOfBounds)
        {
            isOutOfBounds = true;
            GetComponent<AudioSource>().Play();
            Invoke("ReloadScene", reloadSceneDelay);
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
