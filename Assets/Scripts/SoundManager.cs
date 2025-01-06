using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip[] soundsource;
    AudioSource audioSource;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void StartSound()
    {
        audioSource.PlayOneShot(soundsource[0]);
    }

    public void EatSound()
    {
        audioSource.PlayOneShot(soundsource[1]);
    }

    public void DieSound()
    {
        audioSource.PlayOneShot(soundsource[2]);
    }

}
