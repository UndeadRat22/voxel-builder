using UnityEngine;
namespace Deadrat22
{
    public class BlockType
    {

        private ushort id;
        private string name;
        private bool solid;

        private string textureName;
        private TextureSet textureSet;

        public ushort Id { get { return id; } }
        public string Name { get { return name; } }
        public bool Solid { get { return solid; } }

        //unused for now till i figure out how to create a texture atlas
        public string TextureName { get { return textureName; } }
        public TextureSet TextureSet { get { return textureSet; } }

        public BlockType(ushort id, string name, bool solid, string textureName)
        {
            this.id = id;
            this.name = name;
            this.solid = solid;
            this.textureName = textureName;
            if (solid) textureSet = Object.FindObjectOfType<TextureSetLoader>().GetByName(textureName);
        }
    }
}