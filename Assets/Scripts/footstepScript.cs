using UnityEngine;

public class footstepScript : MonoBehaviour
{
    //public AudioClip[][] audioClips;

    public audiosources[] myAudioSources;
    public string[] sounds;
    public GameObject footstepSource;

    AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = footstepSource.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playFootStep()
    {
        playFootStep("dirt");
    }

    public void playFootStep(string material)
    {
        bool foundMat = false;
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i] == material)
            {
                foundMat = true;
                audioSource.clip = myAudioSources[i].audioClips[Random.Range(0, myAudioSources[i].audioClips.Length - 1)];
                audioSource.Play();
            }
        }
        if (!foundMat) Debug.Log("No correct material found");
    }
}
