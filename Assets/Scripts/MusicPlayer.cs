using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource player = null;

    public List<AudioClip> playlist;

    public TextMeshProUGUI currentTrack = null;

    private int index = 0;

    bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
            player = gameObject.GetComponent<AudioSource>();
        index = Random.Range(0, playlist.Count);
        player.PlayOneShot(playlist[index]);
        currentTrack.text = playlist[index].name;
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.isPlaying && !paused)
            PlayNext();
    }

    public void Pause()
    {
        player.Pause();
        paused = true;
    }

    public void Resume()
    {
        player.UnPause();
        paused = false;
    }

    public void PlayNext()
    {
        ++index;
        if (index >= playlist.Count)
            index = 0;
        player.Stop();
        player.PlayOneShot(playlist[index]);
        currentTrack.text = playlist[index].name;
        paused = false;
    }

    public void PlayPrevious()
    {
        --index;
        if (index < 0)
            index = playlist.Count - 1;
        player.Stop();
        player.PlayOneShot(playlist[index]);
        currentTrack.text = playlist[index].name;
        paused = false;
    }
}
