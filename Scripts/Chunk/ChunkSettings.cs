namespace Deadrat22
{
    public static class ChunkSettings
    {
        private static int chunkSize = 16; // 1 << 4
        private static int chunkHeight = 256; // 1 << 8

        public static int ChunkSize { get { return chunkSize; } }
        public static int ChunkHeight { get { return ChunkHeight; } }
    }
}