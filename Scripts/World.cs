using System.Collections.Generic;
using UnityEngine;
namespace Deadrat22
{
    public class World : MonoBehaviour
    {
        public Dictionary<Vector3Int, Chunk> chunks = new Dictionary<Vector3Int, Chunk>();

        [SerializeField]
        private GameObject chunkPrefab;
        private void Awake()
        {
            BlockTypeDatabase.LoadBlockTypes();
        }
        /// <summary>
        /// Creates an empty chunk in the given position
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Chunk CreateChunk(Vector3Int position)
        {
            Chunk chunk = Instantiate(chunkPrefab, position, Quaternion.identity).GetComponent<Chunk>();

            chunk.chunkPosition = position;
            chunk.world = this;
            chunks.Add(position, chunk);

            return chunk;
        }

        /// <summary>
        /// Returns the chunk that contains the given block position
        /// </summary>
        /// <param name="blockPosition">block position to look for</param>
        /// <returns></returns>
        public Chunk GetChunk(Vector3Int blockPosition)
        {
            Vector3Int pos = GetChunkPositionFromBlock(blockPosition);
            Chunk containerChunk = null;
            chunks.TryGetValue(pos, out containerChunk);

            return containerChunk;
        }

        /// <summary>
        /// Returns the position of the chunk that contains the given block position
        /// </summary>
        /// <param name="blockPosition"></param>
        /// <returns></returns>
        public Vector3Int GetChunkPositionFromBlock(Vector3Int blockPosition)
        {
            Vector3Int pos = new Vector3Int();
            float multiple = Chunk.ChunkSize;
            pos.x = Mathf.FloorToInt(blockPosition.x / multiple) * Chunk.ChunkSize;
            pos.y = Mathf.FloorToInt(blockPosition.y / multiple) * Chunk.ChunkSize;
            pos.z = Mathf.FloorToInt(blockPosition.z / multiple) * Chunk.ChunkSize;
            return pos;
        }

        /// <summary>
        /// Returns the world block in the specified position
        /// </summary>
        /// <param name="pos">the position of the block</param>
        /// <returns></returns>
        public Block GetBlock(Vector3Int pos)
        {
            Chunk containerChunk = GetChunk(pos);
            if (containerChunk != null)
            {
                Block block = containerChunk.GetBlock(pos - containerChunk.chunkPosition);

                return block;
            }
            else
            {
                return new Block(BlockTypeDatabase.AirId);
            }
        }

        /// <summary>
        /// Sets the block to the specified block in the world
        /// </summary>
        /// <param name="blockPosition">where to place</param>
        /// <param name="block">what to place</param>
        public void SetBlock(Vector3Int blockPosition, Block block)
        {
            Chunk chunk = GetChunk(blockPosition);
            if (chunk == null)
            {
                Vector3Int chunkPos = GetChunkPositionFromBlock(blockPosition);
                chunk = CreateChunk(chunkPos);
            }
            if (chunk != null)
            {
                chunk.SetBlock(blockPosition - chunk.chunkPosition, block);
                chunk.update = true;
            }
        }
    }
}