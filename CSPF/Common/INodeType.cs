namespace CSPF.Common
{
    public interface INodeType
    {
        /// <summary>
        /// Unique node type identifier
        /// </summary>
        string Name { get; set; }

        double? NodeCost { get; set; }
        string? NodeColor { get; set; }
    }
}
