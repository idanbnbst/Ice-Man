using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfDetector : MonoBehaviour
{
    [SerializeField] ParticleSystem surfEffect;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
            surfEffect.Play();
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
            surfEffect.Stop();
    }
}
