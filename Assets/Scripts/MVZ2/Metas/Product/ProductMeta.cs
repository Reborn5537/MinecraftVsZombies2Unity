﻿using System.Collections.Generic;
using System.Linq;
using System.Xml;
using MVZ2.IO;
using MVZ2Logic;
using PVZEngine;

namespace MVZ2.Metas
{
    public class ProductMeta
    {
        public string ID { get; private set; }
        public SpriteReference Sprite { get; private set; }
        public NamespaceID BlueprintID { get; private set; }
        public NamespaceID Required { get; private set; }
        public ProductTalkMeta[] Talks { get; private set; }
        public ProductStageMeta[] Stages { get; private set; }
        public int Index { get; private set; }
        public static ProductMeta FromXmlNode(XmlNode node, string defaultNsp, int index)
        {
            var id = node.GetAttribute("id");
            var sprite = node.GetAttributeSpriteReference("sprite", defaultNsp);
            var blueprintId = node.GetAttributeNamespaceID("blueprintId", defaultNsp);
            var required = node.GetAttributeNamespaceID("required", defaultNsp);

            List<ProductTalkMeta> talks = new List<ProductTalkMeta>();
            var talksNode = node["talks"];
            if (talksNode != null)
            {
                for (int i = 0; i < talksNode.ChildNodes.Count; i++)
                {
                    var childNode = talksNode.ChildNodes[i];
                    if (childNode.Name == "talk")
                    {
                        talks.Add(ProductTalkMeta.FromXmlNode(childNode, defaultNsp));
                    }
                }
            }

            List<ProductStageMeta> stages = new List<ProductStageMeta>();
            var stagesNode = node["stages"];
            if (stagesNode != null)
            {
                for (int i = 0; i < stagesNode.ChildNodes.Count; i++)
                {
                    var childNode = stagesNode.ChildNodes[i];
                    if (childNode.Name == "stage")
                    {
                        stages.Add(ProductStageMeta.FromXmlNode(childNode, defaultNsp));
                    }
                }
            }
            return new ProductMeta()
            {
                ID = id,
                Sprite = sprite,
                BlueprintID = blueprintId,
                Required = required,
                Talks = talks.ToArray(),
                Stages = stages.ToArray(),
                Index = index,
            };
        }
        public string GetMessage(NamespaceID characterId)
        {
            var meta = Talks.FirstOrDefault(t => t.Character == characterId);
            if (meta == null)
                return string.Empty;
            return meta.Text;
        }
        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(ID);
        }
    }
}
