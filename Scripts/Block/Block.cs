using UnityEngine;

namespace Deadrat22
{
    public struct Block
    {
        private ushort id;
        public ushort Id { get { return id; } }

        public Block(ushort id)
        {
            this.id = id;
        }
    }
}