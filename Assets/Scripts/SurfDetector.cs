using UnityEngine;
public class SurfDetector : MonoBehaviour
{
    [SerializeField] ParticleSystem surfEffect;
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
            surfEffect.Play();
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
            surfEffect.Stop();
    }
}