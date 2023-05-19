﻿using UnityEngine;
using UnityEngine.Serialization;

namespace VoxelBusters.ScreenRecorderKit
{
	public partial class VideoRecorderSettings
	{
		/// <summary>
		/// Application Settings specific to Android platform.
		/// </summary>
		[System.Serializable]
		public class AndroidPlatformProperties
		{
            #region Fields

            [SerializeField, FormerlySerializedAs("m_videoMaxQuality"), Tooltip("Set the resolution at which you want to record. Setting higher resolution will have larger final video sizes.")]
            private     VideoRecorderQuality    m_videoQuality              = VideoRecorderQuality.QUALITY_720P;
            [SerializeField, Tooltip("Enabling custom bitrates lets you set recommended bitrates compared to default values which give very big file sizes")]
            private     CustomBitRateSetting    m_customVideoBitrate        = null;
            [SerializeField, FormerlySerializedAs("m_allowExternalStoragePermission"), Tooltip("Enable this if you want to use SavePreview feature. This adds external storage permission to the manifest. Default is true.")]
            private     bool                    m_usesSavePreview           = true;

            [Header("Advanced Settings")]
            [SerializeField, Tooltip("Enabling this will allow VideoRecorder to pause/resume audio sources to reduce load while starting/stopping recording. It is recommended to keep this setting on.")]
            private     bool                    m_allowControllingAudio     = true;
            [SerializeField, Tooltip("This captures app audio better when enabled")]
            private     bool                    m_prioritiseAppAudioWhenUsingMicrophone = false;

            #endregion

            #region Properties

            public VideoRecorderQuality VideoQuality => m_videoQuality;

            public CustomBitRateSetting CustomBitrateSetting => m_customVideoBitrate;

            public bool UsesSavePreview => m_usesSavePreview;

            public bool AllowControllingAudio => m_allowControllingAudio;

            public bool PrioritiseAppAudioWhenUsingMicrophone => m_prioritiseAppAudioWhenUsingMicrophone;

            #endregion

            #region Constructors

            public AndroidPlatformProperties(VideoRecorderQuality videoQuality = VideoRecorderQuality.QUALITY_720P, CustomBitRateSetting customBitRate = null,
                bool usesSavePreview = true, bool allowControllingAudio = true,
                bool prioritiseAppAudioWhenUsingMicrophone = false)
            {
                // set properties
                m_videoQuality                          = videoQuality;
                m_customVideoBitrate                    = customBitRate;
                m_usesSavePreview                       = usesSavePreview;
                m_allowControllingAudio                 = allowControllingAudio;
                m_prioritiseAppAudioWhenUsingMicrophone = prioritiseAppAudioWhenUsingMicrophone;
            }

            #endregion
        }
    }
}