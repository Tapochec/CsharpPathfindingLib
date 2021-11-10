using System;
using System.Collections.Generic;
using System.Text;

namespace PathfindingLib.Core
{
    public interface INodeTypesManager
    {
        IReadOnlyList<INodeType> GetAllNodeTypes(bool includeDefaultType = true);
        INodeType this[string typeName] { get; }
        void Add(INodeType nodeType);
        void Remove(string typeName);
        void Change(string typeName, INodeType newType);
    }
}
