using System.Collections.Generic;

using UnityEngine;

namespace Deadrat22
{
    public class Statement
    {
        private Jint.Engine engine;
        public Dictionary<string, int> variables;
        private TextAsset file;

        public Statement(TextAsset file, Jint.Engine engine)
        {
            this.file = file;
            this.engine = engine;
            variables = new Dictionary<string, int>();
        }

        /// <summary>
        /// Sets the dictionary variables in the Jint Engine
        /// </summary>
        public void SetVariables()
        {
            if (variables == null)
                return;
            foreach (var pair in variables)
                engine.SetValue(pair.Key, (object)pair.Value);
        }

        public System.Exception Exec(string code)
        {
            if (file == null)
            {
                return null;
            }
            string fullCode = file.text + code;
            try {
                engine.Execute(fullCode);
            } catch (System.Exception e)
            {
                return e;
            }
            return null;
       }
    }
}