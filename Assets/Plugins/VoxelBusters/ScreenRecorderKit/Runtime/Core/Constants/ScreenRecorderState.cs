using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.ScreenRecorderKit
{
    public enum ScreenRecorderState
    {
        Invalid = 0,

        Prepare,

        Record,

        Pause,

        Stop
    }
}