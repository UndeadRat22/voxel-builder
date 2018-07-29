using System.Collections.Generic;
using UnityEngine;

namespace Deadrat22
{
    public class TextureSet
    {
        public string name;
        private List<Rect> textures = new List<Rect>();

        public TextureSet(string name)
        {
            this.name = name;
        }

        public void AddTexture(Rect texture)
        {
            textures.Add(texture);
        }

        public Rect GetTexture()
        {
            if (textures.Count == 0)
            {
                Debug.LogError("Block texture object is empty!");
                return new Rect();
            }

            if (textures.Count == 1) return textures[0];

            System.Random random = new System.Random();
            int randomNumber = random.Next(0, textures.Count);
            return textures[randomNumber];
        }
    }
}