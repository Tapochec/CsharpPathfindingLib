namespace CSPF.Common
{
    public interface INode
    {
        /// <summary>
        /// Use custom cost, or cost from <see cref="NodeType"/>
        /// </summary>
        bool UseCustomCost { get; set; }
        double? Cost { get; set; }
        INodeType? NodeType { get; set; }
    }
}
