using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioClip deathSound; // Drag the death sound AudioClip here through the Inspector

    //--------------------------------------------------------------------------------------------------------------------------------------------

    void Awake()
    {
        // Implement Singleton pattern to ensure only one instance of AudioManager
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

       // DontDestroyOnLoad(gameObject);
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    public void PlayDeathSound(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(deathSound, position);
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

}