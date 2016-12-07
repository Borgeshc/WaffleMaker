using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour
{
    public AudioClip[] songs;
    AudioSource source;
    
	void Start () {
        source = GetComponent<AudioSource>();
	}
	
	void Update () {
        if(!source.isPlaying)
        {
            source.clip = songs[Random.Range(0, songs.Length)];
            source.Play();
        }
	}
}
