using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    public List<AudioClip> playlist;
    public Text songTitleText;
    public AudioSource audioSource;
    private int currentTrackIndex = 0;

    void Start()
    {
        PlayTrack(currentTrackIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextTrack();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PreviousTrack();
        }
    }

    public void PlayTrack(int index)
    {
        if (index >= 0 && index < playlist.Count)
        {
            audioSource.Stop();
            audioSource.clip = playlist[index];
            audioSource.Play();
            currentTrackIndex = index;
            songTitleText.text = playlist[index].name;
        }
    }

    public void NextTrack()
    {
        currentTrackIndex++;
        if (currentTrackIndex >= playlist.Count)
        {
            currentTrackIndex = 0;
        }
        PlayTrack(currentTrackIndex);
    }

    public void PreviousTrack()
    {
        currentTrackIndex--;
        if (currentTrackIndex < 0)
        {
            currentTrackIndex = playlist.Count - 1;
        }
        PlayTrack(currentTrackIndex);
    }
}
