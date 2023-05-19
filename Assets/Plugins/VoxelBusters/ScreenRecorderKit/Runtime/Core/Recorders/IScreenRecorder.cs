using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.CoreLibrary;

namespace VoxelBusters.ScreenRecorderKit
{
    public interface IScreenRecorder
    {
        #region Properties

        string Name { get; }

        ScreenRecorderState State { get; }

        #endregion

        #region Methods

        bool CanRecord();

        bool IsRecording();

        bool IsPausedOrRecording();

        void PrepareRecording(CompletionCallback callback = null);

        void StartRecording(CompletionCallback callback = null);

        void PauseRecording(CompletionCallback callback = null);

        void StopRecording(CompletionCallback callback = null);
        void StopRecording(bool flushMemory, CompletionCallback callback = null);

        void DiscardRecording(CompletionCallback callback = null);

        void SaveRecording(CompletionCallback<ScreenRecorderSaveRecordingResult> callback = null);
        void SaveRecording(string fileName, CompletionCallback<ScreenRecorderSaveRecordingResult> callback = null);

        void OpenRecording(CompletionCallback callback = null);

        void SetOnRecordingAvailable(SuccessCallback<ScreenRecorderRecordingAvailableResult> callback = null);

        void Flush();

        //void ShareRecording(string text = null, string subject = null, CompletionCallback callback = null);

        #endregion
    }
}