using System.Collections.Generic;
using System.Xml;

namespace Deadrat22
{
    public static class BlockTypeDatabase
    {

        private static Dictionary<ushort, BlockType> types = new Dictionary<ushort, BlockType>();
        private static int latestFreeId { get { return types.Count; } }

        private static ushort airId = AddBlockType("Air", false);
        private static ushort voidId = AddBlockType("Void", false);
        public static ushort AirId { get { return airId; } }
        public static ushort VoidId { get { return voidId; } }

        /// <summary>
        /// Loads all blocktypes from the file "Definitions/blocks.xml"
        /// </summary>
        public static void LoadBlockTypes()
        {
            XmlDocument document = new XmlDocument();
            UnityEngine.TextAsset file = UnityEngine.Resources.Load("Definitions/blocks") as UnityEngine.TextAsset;
            document.LoadXml(file.text);

            XmlNodeList blocks = document.GetElementsByTagName("block");
            for (int i = 0; i < blocks.Count; i++)
            {
                try {
                    var block = blocks.Item(i);
                    var attribs = block.ChildNodes;
                    string name = attribs.Item(0).InnerText;
                    string texturename = attribs.Item(1).InnerText;
                    bool solid = attribs.Item(2).InnerText == "True" ? true : false;
                    AddBlockType(name, solid, texturename);
                } catch (System.Exception e)
                {

                }
            }
        }

        /// <summary>
        /// Gets the blocktype for a given id
        /// </summary>
        /// <param name="id">the id to look for</param>
        /// <returns></returns>
        public static BlockType GetBlockType(ushort id)
        {
            //check if the given id belongs to the list
            if (id > (ushort)latestFreeId)
                return null;
            else
                return types[id];
        }


        /// <summary>
        /// Gets the blocktype for a given name
        /// </summary>
        /// <param name="name">the name to look for</param>
        /// <returns></returns>
        public static BlockType GetBlockType(string name)
        {
            foreach (var pair in types)
            {
                if (pair.Value.Name == name)
                    return pair.Value;
            }
            return null;
        }

        /// <summary>
        /// Adds a new blocktype to the type list and
        /// returns the id of the added blocktype
        /// </summary>
        /// <param name="name">name of the type</param>
        /// <param name="solid">if the block is solid</param>
        /// <param name="texturename">the texture name for the block</param>
        /// <returns></returns>
        public static ushort AddBlockType(string name, bool solid, string texturename = null)
        {
            if (texturename == null) texturename = name;
            ushort id = (ushort)latestFreeId;
            types.Add(id, new BlockType(id, name, solid, texturename));
            return id;
        }
    }
}