using System;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.Rendering;

namespace IMShapes.Utils
{
    public static class DrawManager
    {
        public static MaterialPropertyBlock MaterialPropertyBlock;
        public static CommandBuffer CmdBuffer;

        private const string CmdBufferName = "IMShapes Draw Buffer";

        private static int _lastFrameWithCommands;
        
        public static void InitDraw()
        {
            // Only init if this frame does not yet have commands
            if (_lastFrameWithCommands == Time.frameCount)
                return;

            _lastFrameWithCommands = Time.frameCount;
            if (CmdBuffer == null)
            {
                CmdBuffer = new CommandBuffer();
                CmdBuffer.name = CmdBufferName;
            }
            CmdBuffer.Clear();
 
            MaterialValues.Init();
            MaterialPropertyBlock ??= new MaterialPropertyBlock();
        }

        private static void OnPreRender(Camera camera)
        {
            // If this frame did not have any commands, don't add it to the camera
            if (_lastFrameWithCommands != Time.frameCount || CmdBuffer == null)
                return;

            foreach (var buffer in camera.GetCommandBuffers(CameraEvent.BeforeImageEffects))
            {
                if (!string.Equals(buffer.name, CmdBufferName, StringComparison.Ordinal))
                    return;
                camera.RemoveCommandBuffer(CameraEvent.BeforeImageEffects, buffer);
            }

            if (_lastFrameWithCommands != Time.frameCount || CmdBuffer == null)
                return;
            
            camera.AddCommandBuffer(CameraEvent.BeforeImageEffects, CmdBuffer);
        }

        // Called before first scene is loaded
#if UNITY_EDITOR
        [UnityEditor.Callbacks.DidReloadScripts]
#endif
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Init()
        {
            _lastFrameWithCommands = -1;

            Camera.onPreRender -= OnPreRender;
            Camera.onPreRender += OnPreRender;
        }
    }
}
