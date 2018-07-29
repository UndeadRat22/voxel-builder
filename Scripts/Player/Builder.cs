using System.Collections.Generic;
using UnityEngine;

namespace Deadrat22
{
    public class Builder : MonoBehaviour
    {
        private World world = null;

        [SerializeField]
        private float delayTime = .2f;

        private Queue<KeyValuePair<Vector3Int, Block>> blocks = new Queue<KeyValuePair<Vector3Int, Block>>();

        private void Awake()
        {
            if (world == null)
                world = FindObjectOfType<World>();
        }

        private float elapsedTime = 0.0f;

        private void Update()
        {
            //If list is empty builder doesn't have shit to build;
            if (blocks.Count == 0)
                return;
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= delayTime)
            {
                var b = blocks.Dequeue();
                world.SetBlock(b.Key, b.Value);
                elapsedTime -= delayTime;
            }
        }

        public void EnqueBlock(Vector3Int pos, Block block)
        {
            blocks.Enqueue(new KeyValuePair<Vector3Int, Block>(pos, block));
        }
    }
}