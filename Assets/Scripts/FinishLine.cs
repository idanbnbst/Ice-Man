using UnityEngine;
using UnityEngine.SceneManagement;
public class FinishLine : MonoBehaviour
{
    [SerializeField] float reloadSceneTime = 1f;
    [SerializeField] ParticleSystem finishEffect;
    [SerializeField] AudioClip finishSFX;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            finishEffect.Play();
            GetComponent<AudioSource>().PlayOneShot(finishSFX);
            Invoke("ReloadScene", reloadSceneTime);
        }
    }
    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}