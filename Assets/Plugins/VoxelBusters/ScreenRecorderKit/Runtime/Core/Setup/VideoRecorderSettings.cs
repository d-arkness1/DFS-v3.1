using UnityEngine;
using VoxelBusters.CoreLibrary;

namespace VoxelBusters.ScreenRecorderKit
{
    [System.Serializable]
    public partial class VideoRecorderSettings : SettingsPropertyGroup
    {
        #region Fields

        [SerializeField]
        private     bool                        m_usesMicrophone;
        [SerializeField]
        private     IosPlatformProperties       m_iosProperties;
        [SerializeField]
        private     AndroidPlatformProperties   m_androidProperties;

        #endregion

        #region Properties

        public bool UsesMicrophone
        {
            get => m_usesMicrophone;
            private set => m_usesMicrophone = value;
        }

        public IosPlatformProperties IosProperties
        {
            get => m_iosProperties;
            private set => m_iosProperties  = value;
        }

        public AndroidPlatformProperties AndroidProperties
        {
            get => m_androidProperties;
            private set => m_androidProperties  = value;
        }

        #endregion

        #region Constructors

        public VideoRecorderSettings(bool isEnabled = true, bool usesMicrophone = true,
            IosPlatformProperties iosProperties = null, AndroidPlatformProperties androidProperties = null)
            : base(name: "Replay Kit Settings", isEnabled: isEnabled)
        {
            // set properties
            UsesMicrophone      = usesMicrophone;
            IosProperties       = iosProperties ?? new IosPlatformProperties();
            AndroidProperties   = androidProperties ?? new AndroidPlatformProperties();
        }

        #endregion
    }
}