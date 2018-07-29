using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Deadrat22
{
    public class MeshData
    {
        public List<Vector3> vertices = new List<Vector3>();
        public List<int> indices = new List<int>();
        public List<Vector2> uv = new List<Vector2>();

        public void Clear()
        {
            vertices.Clear();
            indices.Clear();
            uv.Clear();
        }

        private static void AddQuadTextureCoordinates(Rect texture, ref MeshData data)
        {
            data.uv.Add(new Vector2(texture.x, texture.y));
            data.uv.Add(new Vector2(texture.x, texture.y + texture.height));
            data.uv.Add(new Vector2(texture.x + texture.width, texture.y + texture.height));
            data.uv.Add(new Vector2(texture.x + texture.width, texture.y));
        }

        private static void AddQuadTriangleIndices(ref MeshData data)
        {
            data.indices.Add(data.vertices.Count - 4);
            data.indices.Add(data.vertices.Count - 3);
            data.indices.Add(data.vertices.Count - 2);

            data.indices.Add(data.vertices.Count - 4);
            data.indices.Add(data.vertices.Count - 2);
            data.indices.Add(data.vertices.Count - 1);
        }

        public static void AddBlockFaceData(Chunk c, Vector3Int block_pos, ref MeshData data)
        {
            //if (c.GetBlock(block_pos).Id == 0) return;
            BlockType blocktype = BlockTypeDatabase.GetBlockType(c.GetBlock(block_pos).Id);
            if (!blocktype.Solid) return;
            foreach (Direction dir in DirectionUtils.Directions)
            {
                Vector3Int neighbour_pos = block_pos + DirectionUtils.CastToVector3Int(dir);
                if (!BlockTypeDatabase.GetBlockType(c.GetBlock(neighbour_pos).Id).Solid) //if the neigboring block is NOT solid, then we need to update the meshdata to show our cube faces
                {
                    switch (dir)
                    {
                        case Direction.north:
                            data.vertices.Add(new Vector3(block_pos.x + 0.5f, block_pos.y - 0.5f, block_pos.z + 0.5f));
                            data.vertices.Add(new Vector3(block_pos.x + 0.5f, block_pos.y + 0.5f, block_pos.z + 0.5f));
                            data.vertices.Add(new Vector3(block_pos.x - 0.5f, block_pos.y + 0.5f, block_pos.z + 0.5f));
                            data.vertices.Add(new Vector3(block_pos.x - 0.5f, block_pos.y - 0.5f, block_pos.z + 0.5f));

                            AddQuadTriangleIndices(ref data);
                            AddQuadTextureCoordinates(blocktype.TextureSet.GetTexture(), ref data);
                            break;
                        case Direction.east:
                            data.vertices.Add(new Vector3(block_pos.x + 0.5f, block_pos.y - 0.5f, block_pos.z - 0.5f));
                            data.vertices.Add(new Vector3(block_pos.x + 0.5f, block_pos.y + 0.5f, block_pos.z - 0.5f));
                            data.vertices.Add(new Vector3(block_pos.x + 0.5f, block_pos.y + 0.5f, block_pos.z + 0.5f));
                            data.vertices.Add(new Vector3(block_pos.x + 0.5f, block_pos.y - 0.5f, block_pos.z + 0.5f));

                            AddQuadTriangleIndices(ref data);
                            AddQuadTextureCoordinates(blocktype.TextureSet.GetTexture(), ref data);
                            break;
                        case Direction.south:
                            data.vertices.Add(new Vector3(block_pos.x - 0.5f, block_pos.y - 0.5f, block_pos.z - 0.5f));
                            data.vertices.Add(new Vector3(block_pos.x - 0.5f, block_pos.y + 0.5f, block_pos.z - 0.5f));
                            data.vertices.Add(new Vector3(block_pos.x + 0.5f, block_pos.y + 0.5f, block_pos.z - 0.5f));
                            data.vertices.Add(new Vector3(block_pos.x + 0.5f, block_pos.y - 0.5f, block_pos.z - 0.5f));

                            AddQuadTriangleIndices(ref data);
                            AddQuadTextureCoordinates(blocktype.TextureSet.GetTexture(), ref data);
                            break;
                        case Direction.west:
                            data.vertices.Add(new Vector3(block_pos.x - 0.5f, block_pos.y - 0.5f, block_pos.z + 0.5f));
                            data.vertices.Add(new Vector3(block_pos.x - 0.5f, block_pos.y + 0.5f, block_pos.z + 0.5f));
                            data.vertices.Add(new Vector3(block_pos.x - 0.5f, block_pos.y + 0.5f, block_pos.z - 0.5f));
                            data.vertices.Add(new Vector3(block_pos.x - 0.5f, block_pos.y - 0.5f, block_pos.z - 0.5f));

                            AddQuadTriangleIndices(ref data);
                            AddQuadTextureCoordinates(blocktype.TextureSet.GetTexture(), ref data);
                            break;
                        case Direction.up:
                            data.vertices.Add(new Vector3(block_pos.x - 0.5f, block_pos.y + 0.5f, block_pos.z + 0.5f));
                            data.vertices.Add(new Vector3(block_pos.x + 0.5f, block_pos.y + 0.5f, block_pos.z + 0.5f));
                            data.vertices.Add(new Vector3(block_pos.x + 0.5f, block_pos.y + 0.5f, block_pos.z - 0.5f));
                            data.vertices.Add(new Vector3(block_pos.x - 0.5f, block_pos.y + 0.5f, block_pos.z - 0.5f));

                            AddQuadTriangleIndices(ref data);
                            AddQuadTextureCoordinates(blocktype.TextureSet.GetTexture(), ref data);
                            break;
                        case Direction.down:
                            data.vertices.Add(new Vector3(block_pos.x - 0.5f, block_pos.y - 0.5f, block_pos.z - 0.5f));
                            data.vertices.Add(new Vector3(block_pos.x + 0.5f, block_pos.y - 0.5f, block_pos.z - 0.5f));
                            data.vertices.Add(new Vector3(block_pos.x + 0.5f, block_pos.y - 0.5f, block_pos.z + 0.5f));
                            data.vertices.Add(new Vector3(block_pos.x - 0.5f, block_pos.y - 0.5f, block_pos.z + 0.5f));

                            AddQuadTriangleIndices(ref data);
                            AddQuadTextureCoordinates(blocktype.TextureSet.GetTexture(), ref data);
                            break;
                    }
                }
            }
        }
    }
}