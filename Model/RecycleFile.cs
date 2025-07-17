namespace Recycli.Model
{
    public class RecycleFile
    {
        public required string Name { get; set; }
        public required int Size { get; set; } // Size in KB
        public required string Path { get; set; }
        public required List<object> Verbs { get; set; }
    }
}
