using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.ScreenRecorderKit;
using UnityEngine.Events;

public class Recorder : MonoBehaviour
{
    private Camera _camera;

    public UnityEvent onRecordingStarted = new UnityEvent();
    public UnityEvent onRecordingStopped = new UnityEvent();
    public UnityEvent onRecordingSaved = new UnityEvent();

    private IScreenRecorder recorder;

    // Start is called before the first frame update
    private void Start()
    {
        ScreenRecorderBuilder builder = ScreenRecorderBuilder.CreateGifRecorder();
        recorder = builder.Build();

        recorder.SetOnRecordingAvailable((result) =>
        {
            GifTexture gifTexture = result.Data as GifTexture;
            SaveRecording();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleRecord()
    {
        if (recorder.IsRecording())
        {
            StopRecording();
        }
        else
        {
            StartRecording();
        }
    }

    public void StartRecording()
    {
        recorder.StartRecording(callback: (success, error) =>
        {
            if (success)
            {
                Debug.Log("Start recording successful.");
                onRecordingStarted.Invoke();
            }
            else
            {
                Debug.Log($"Start recording failed with error [{error}]");
            }
        });
    }

    public void StopRecording()
    {
        recorder.StopRecording((success, error) =>
        {
            if (success)
            {
                Debug.Log("Stop recording successful.");
            }
            else
            {
                Debug.Log($"Stop recording failed with error [{error}]");
            }
        });

        // Moved out for instantaneous result
        onRecordingStopped.Invoke();
    }

    void SaveRecording()
    {
        Debug.Log("Saving recording...");
        recorder.SaveRecording(null, (result, error) =>
        {
            if (error == null)
            {
                Debug.Log("Saved recording successfully :" + result.Path);
                onRecordingSaved.Invoke();
            }
            else
            {
                Debug.Log($"Failed saving recording [{error}]");
            }
        });
    }
}
