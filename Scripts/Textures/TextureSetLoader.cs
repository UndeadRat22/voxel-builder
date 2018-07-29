using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Deadrat22
{
    public class TextureSetLoader : MonoBehaviour
    {
        public string pathToTextureResources = "Textures";
        public Material material;
        [HideInInspector]
        public Texture2D tileSheet;

        private Dictionary<string, TextureSet> textureSets = new Dictionary<string, TextureSet>();
        private void Awake()
        {
            LoadTextures();
        }

        /// <summary>
        /// Loads all textures from the resource location
        /// </summary>
        public void LoadTextures()
        {
            var resourceTextures = Resources.LoadAll<Texture2D>(pathToTextureResources);
            Texture2D packedTextures = new Texture2D(64, 64) { filterMode = FilterMode.Point };
            Rect[] rects = packedTextures.PackTextures(resourceTextures, 0, 8192, false);

            for (int i = 0; i < resourceTextures.Length; i++)
            {
                TextureSet tex = new TextureSet(resourceTextures[i].name);
                tex.AddTexture(rects[i]);
                AddTexture(tex);
            }

            tileSheet = packedTextures;
            material.mainTexture = packedTextures;
        }
        /// <summary>
        /// Adds the given textureset to the dictionary
        /// </summary>
        /// <param name="textureSet">The textureset to add</param>
        public void AddTexture(TextureSet textureSet)
        {
            textureSets.Add(textureSet.name, textureSet);
        }

        /// <summary>
        /// Returns the texture from the dictionary by name
        /// </summary>
        /// <param name="name">name to look for</param>
        /// <returns></returns>
        public TextureSet GetByName(string name)
        {
            if (!textureSets.ContainsKey(name))
            {
                Debug.LogError("There is no loaded texture by the name " + name);
                return null;
            }
            return textureSets[name];
        }
    }
}