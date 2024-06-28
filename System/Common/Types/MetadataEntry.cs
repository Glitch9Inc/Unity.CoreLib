using System;
using Glitch9.Collections;

namespace Glitch9
{
    [Serializable]
    public class Metadata : ReferencedDictionary<MetadataEntry, string, string>
    {
    }
    

    [Serializable]
    public class MetadataEntry : IKeyValuePair<string, string>
    {
        public string Key
        {
            get => metadataName;
            set => metadataName = value;
        }

        public string Value
        {
            get => metadataValue;
            set => metadataValue = value;
        }
        
        public string metadataName;
        public string metadataValue;

        public MetadataEntry()
        {
        }
        
        public MetadataEntry(string metadataName, string metadataValue)
        {
            this.metadataName = metadataName;
            this.metadataValue = metadataValue;
        }

        public override string ToString()
        {
            return $"{metadataName}: {metadataValue}";
        }
    }
}