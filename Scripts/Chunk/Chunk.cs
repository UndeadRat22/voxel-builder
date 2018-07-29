using UnityEngine;
namespace Deadrat22
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshCollider))]
    public class Chunk : MonoBehaviour
    {
        private Block[,,] blocks = new Block[ChunkSettings.ChunkSize, ChunkSettings.ChunkSize, ChunkSettings.ChunkSize];
        public static int ChunkSize { get { return ChunkSettings.ChunkSize; } }

        public World world;
        public Vector3Int chunkPosition;

        private MeshFilter filter;
        public bool update = false;

        #region mono
        private void Awake()
        {
            filter = GetComponent<MeshFilter>();
        }
        private void Start()
        {
            UpdateChunk();
        }
        private void Update()
        {
            if (update)
            {
                update = false;
                UpdateChunk();
            }
        }
        #endregion

        /// <summary>
        /// Returns the block in the given position
        /// </summary>
        /// <param name="p">Pos to look for</param>
        /// <returns></returns>
        public Block GetBlock(Vector3Int p)
        {
            if (!InChunkBoundaries(p))
                return world.GetBlock(chunkPosition + p);
            else
                return blocks[p.x, p.y, p.z];
        }

        /// <summary>
        /// Sets the bock in the chunk or in the world if it's not in this chunk
        /// </summary>
        /// <param name="p">where to set</param>
        /// <param name="block">what to set</param>
        public void SetBlock(Vector3Int p, Block block)
        {
            if (!InChunkBoundaries(p))
                world.SetBlock(chunkPosition + p, block);
            else
                blocks[p.x, p.y, p.z] = block;
        }
        /// <summary>
        /// Returns if the given position is within chunk's boundaries
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool InChunkBoundaries(Vector3Int pos)
        {
            return !((pos.x >= ChunkSize || pos.y >= ChunkSize || pos.z >= ChunkSize) || (pos.x < 0 || pos.y < 0 || pos.z < 0));
        }

        /// <summary>
        /// Force Update the chunk mesh&collider
        /// </summary>
        private void UpdateChunk()
        {
            MeshData meshdata = GenerateMeshData();
            RenderMesh(meshdata);
        }
        /// <summary>
        /// Generates the meshdata for this chunk
        /// </summary>
        /// <returns></returns>
        private MeshData GenerateMeshData()
        {
            MeshData meshdata = new MeshData();
            for (int x = 0; x < ChunkSize; x++)
            {
                for (int y = 0; y < ChunkSize; y++)
                {
                    for (int z = 0; z < ChunkSize; z++)
                    {
                        MeshData.AddBlockFaceData(this, new Vector3Int(x, y, z), ref meshdata);
                    }
                }
            }
            return meshdata;
        }
        /// <summary>
        /// Generates the unity mesh for the meshcollider and meshfilter using our custom MeshData
        /// </summary>
        /// <param name="meshdata">the data to use to generate the mesh trigonometry</param>
        private void RenderMesh(MeshData meshdata)
        {
            Mesh mesh = new Mesh();
            mesh.vertices = meshdata.vertices.ToArray();
            mesh.triangles = meshdata.indices.ToArray();
            mesh.uv = meshdata.uv.ToArray();

            filter.mesh = mesh;

            meshdata.Clear();
        }
    }
}