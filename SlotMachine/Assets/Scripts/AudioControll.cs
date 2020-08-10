using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioControll : MonoBehaviour
{
    public AudioClip otherClip;


    public void StopAudio()
   {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Pause();
   }
    public void PlayAudio()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
    }


    IEnumerator Start()
    {
        AudioSource audio = GetComponent<AudioSource>();

        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        audio.clip = otherClip;
        audio.Play();
    }
}
