namespace CSPF.Common
{
    public interface INodeType
    {
        string Name { get; set; }

        double? NodeCost { get; set; }
        string? NodeColor { get; set; }
    }
}
