using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PathfindingLib.Core
{
    public class NodeTypesManager : INodeTypesManager
    {
        private List<INodeType> _types;
        private string _defaultTypeName;

        public NodeTypesManager(INodeType defaultType)
        {
            _types = new List<INodeType>() { defaultType };
            _defaultTypeName = defaultType.Name;
        }

        public INodeType GetDefaultType()
        {
            return _types.Find(t => t.Name == _defaultTypeName);
        }

        public IReadOnlyList<INodeType> GetAllNodeTypes(bool includeDefaultType = true)
        {
            if (includeDefaultType)
            {
                return _types;
            }
            else
            {
                return _types.Where(t => t.Name != _defaultTypeName).ToList();
            }
        }

        public INodeType this[string typeName]
        {
            get
            {
                INodeType type = _types.First(t => t.Name == typeName);

                if (type == null)
                {
                    throw new Exception($"Node type with name \"{typeName}\" doesn't exists.");
                }

                return type;
            }
        }

        public void Add(INodeType nodeType)
        {
            if (_types.Find(t => t.Name == nodeType.Name) != null)
            {
                throw new Exception($"Node type with name \"{nodeType.Name}\" already exists.");
            }

            _types.Add(nodeType);
        }

        public void Remove(string typeName)
        {
            INodeType type = _types.First(t => t.Name == typeName);

            if (type == null)
            {
                throw new Exception($"Node type with name \"{typeName}\" doesn't exists.");
            }

            _types.Remove(type);
        }

        public void Change(string typeName, INodeType newType)
        {
            INodeType type = _types.First(t => t.Name == typeName);

            if (type == null)
            {
                throw new Exception($"Node type with name \"{typeName}\" doesn't exists.");
            }

            if (_types.First(t => t.Name == newType.Name) != null)
            {
                throw new Exception($"Node type with name \"{newType.Name}\" already exists.");
            }

            type = newType;
        }
    }
}
