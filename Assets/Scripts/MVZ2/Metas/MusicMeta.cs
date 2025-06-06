﻿using System.Text;
using System.Xml;
using MVZ2.IO;
using PVZEngine;

namespace MVZ2.Metas
{
    public class MusicMeta
    {
        public string ID { get; private set; }
        public string Name { get; private set; }
        public NamespaceID Path { get; private set; }
        public NamespaceID Unlock { get; private set; }
        public string Source { get; private set; }
        public string Origin { get; private set; }
        public string Author { get; private set; }
        public string Description { get; private set; }
        public static MusicMeta FromXmlNode(XmlNode node, string defaultNsp)
        {
            var id = node.GetAttribute("id");
            var name = node.GetAttribute("name");
            var path = node.GetAttributeNamespaceID("path", defaultNsp);
            var unlock = node.GetAttributeNamespaceID("unlock", defaultNsp);
            var source = node["source"]?.InnerText;
            var origin = node["origin"]?.InnerText;
            var author = node["author"]?.InnerText;
            var descriptionNode = node["description"];
            var description = string.Empty;
            if (descriptionNode != null)
            {
                description = AlmanacMetaEntry.ConcatNodeParagraphs(descriptionNode);
            }
            return new MusicMeta()
            {
                ID = id,
                Name = name,
                Path = path,
                Unlock = unlock,
                Source = source,
                Origin = origin,
                Author = author,
                Description = description,
            };
        }
    }
}
