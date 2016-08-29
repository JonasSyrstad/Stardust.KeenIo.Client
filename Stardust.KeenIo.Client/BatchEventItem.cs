namespace Stardust.KeenIo.Client
{
    internal class BatchEventItem
    {
        public string Collection { get; set; }

        public object EventEntry { get; set; }
    }
}