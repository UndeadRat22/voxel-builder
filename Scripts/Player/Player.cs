using System.Collections.Generic;

using UnityEngine;

using Jint;
using Jint.Runtime.Interop;

using Deadrat22.Console;

namespace Deadrat22
{
    public class Player : MonoBehaviour
    {

        private World world = null;
        private Builder builder = null;
        private DebugConsole console = null;

        public bool build;
        public string ScriptPath;

        private BlockType selectedBlocktype;
        private Vector3Int selectedPosition;
        private Vector3Int selectedDirection;
        private Engine engine;


        public void Start()
        {
            if (world == null)
                world = FindObjectOfType<World>();
            if (builder == null)
                builder = FindObjectOfType<Builder>();
            if (console == null)
                console = FindObjectOfType<DebugConsole>();
            selectedBlocktype = BlockTypeDatabase.GetBlockType("Stone");
            selectedPosition = new Vector3Int(0, 0, 0);
            selectedDirection = new Vector3Int(0, 0, 0);
        }

        public void Update()
        {
            if (build)
            {
                build = false;
                Run();
            }
        }

        private void StartJintEngine()
        {
            if (engine != null)
                return;
            engine = new Engine(cfg => cfg.AllowClr());
            //Game
            engine.SetValue("set_position", new System.Action<int, int, int>(SetPosition));
            engine.SetValue("set_direction", new System.Action<int, int, int>(SetDirection));
            engine.SetValue("set_type", new System.Action<string>(SetBlockType));
            engine.SetValue("place_block", new System.Action(SetBlock));
            engine.SetValue("position", selectedPosition);
            engine.SetValue("direction", selectedDirection);
            //debug
            engine.SetValue("log", new System.Action<object>(console.EnqueMessage));

        }

        public void Run()
        {
            StartJintEngine();

            /*TextAsset txt = Resources.Load("Javascript/FullScripts/build") as TextAsset;

            engine.Execute(txt.text);*/
            var list = new List<KeyValuePair<string, Statement>>();

            var placeblocknode = CreateCodePair("placeblock();", "PlaceBlock");
            var movenode = CreateCodePair("move();", "Move");
            var setrightnode = CreateCodePair("setdirection(-1, 0, 0);", "SetDirection");
            var setleftnode = CreateCodePair("setdirection(1, 0, 0);", "SetDirection");

            list.Add(placeblocknode);
            list.Add(setrightnode);
            list.Add(movenode);
            list.Add(placeblocknode);
            list.Add(movenode);
            list.Add(placeblocknode);

            foreach (var v in list)
                v.Value.Exec(v.Key);
        }

        private KeyValuePair<string, Statement> CreateCodePair(string code, string fileName)
        {
            Statement s = new Statement((TextAsset) Resources.Load(ScriptPath + fileName), engine);
            return new KeyValuePair<string, Statement>(code, s);
        }

        public void SetBlockType(string type)
        {
            var newtype = BlockTypeDatabase.GetBlockType(type);
            if (newtype != null)
                selectedBlocktype = newtype;
        }

        public void SetPosition(int x, int y, int z)
        {
            selectedPosition = new Vector3Int(x, y, z);
        }

        public void SetDirection(int x, int y, int z)
        {
            selectedDirection = new Vector3Int(x, y, z);
        }

        public void SetBlock()
        {
            builder.EnqueBlock(selectedPosition, new Block(selectedBlocktype.Id));
        }
    }
}