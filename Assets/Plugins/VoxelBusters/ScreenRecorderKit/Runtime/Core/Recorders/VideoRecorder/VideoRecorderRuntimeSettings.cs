using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.ScreenRecorderKit
{
    public class VideoRecorderRuntimeSettings
    {
        #region Properties

        public bool? EnableMicrophone { get; private set; }

        //private bool? ShowControls { get; private set; }

        #endregion

        #region Constructors

        public VideoRecorderRuntimeSettings(bool? enableMicrophone = null/*, bool? showControls = null*/)
        {
            // Set properties
            EnableMicrophone    = enableMicrophone;
            //ShowControls        = showControls;
        }

        #endregion

        #region Operator methods

        public static implicit operator VideoRecorderRuntimeSettings(VideoRecorderRuntimeSettingsOption[] options)
        {
            var     newInstance = new VideoRecorderRuntimeSettings();
            foreach (var item in options)
            {
                if (item.OptionType == VideoRecorderRuntimeSettingsOption.RuntimeSettingsOptionType.EnableMicrophone)
                {
                    newInstance.EnableMicrophone    = item.BoolValue;
                }
                /*else if (item.OptionType == VideoRecorderRuntimeSettingsOption.RuntimeSettingsOptionType.ShowControls)
                {
                    newInstance.ShowControls        = item.BoolValue;
                }*/
            }
            return newInstance;
        }

        #endregion
    }
}