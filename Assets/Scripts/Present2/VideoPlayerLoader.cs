using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerLoader : MonoBehaviour
{
    VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "myVideo1.mp4");

        videoPlayer.url = filePath;
        videoPlayer.Play();
    }
}
