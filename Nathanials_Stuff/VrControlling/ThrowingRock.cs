using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingRock : MonoBehaviour {

    public AudioClip soundToPlayRock;
    public AudioClip soundToPlaySplash;
    public float volume;
    public AudioSource audio;
    private bool alreadyCollidedWithOcean = false;

    private void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "SeaRock")
        {
            Debug.Log("PLAYING");
            audio.PlayOneShot(soundToPlayRock, volume);
        }

        if (collision.gameObject.tag == "Untagged" && !alreadyCollidedWithOcean)
        {
            Debug.Log("Sea");
            audio.PlayOneShot(soundToPlaySplash, volume);
            GetComponent<MeshRenderer>().enabled = true;
            alreadyCollidedWithOcean = true;
            Destroy(this.gameObject, 3);
        }
    }
}
