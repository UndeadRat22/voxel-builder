using UnityEngine;
using System.Collections.Generic;

namespace Deadrat22.Console
{
    public class DebugConsole : MonoBehaviour
    {
        private Queue<string> messages = new Queue<string>();

        public void EnqueMessage(object msg)
        {
            #if UNITY_EDITOR
                UnityEngine.Debug.Log(msg);
                return;
            #endif
            messages.Enqueue(msg.ToString());
        }
    }
}
