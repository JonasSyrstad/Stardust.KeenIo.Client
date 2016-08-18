namespace Stardust.KeenIo.Client
{
    public static class KeenGlobalConfig
    {
        public static void SetWriterKey(string key)
        {
            KeenWriteAuthorizationAttribute.WriterKey = key;
        }

        public static void SetReaderKey(string key)
        {
            KeenWriteAuthorizationAttribute.WriterKey = key;
        }
    }
}