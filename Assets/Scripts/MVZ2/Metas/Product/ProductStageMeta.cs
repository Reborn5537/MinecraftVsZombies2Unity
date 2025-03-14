﻿using System.Xml;
using MVZ2.IO;
using PVZEngine;

namespace MVZ2.Metas
{
    public class ProductStageMeta
    {
        public string Text { get; private set; }
        public int Price { get; private set; }
        public NamespaceID Unlocks { get; private set; }
        public XMLConditionList Conditions { get; private set; }
        public static ProductStageMeta FromXmlNode(XmlNode node, string defaultNsp)
        {
            var text = node.GetAttribute("text");
            var price = node.GetAttributeInt("price") ?? 0;
            var unlocks = node.GetAttributeNamespaceID("unlocks", defaultNsp);
            XMLConditionList conditions = null;
            var conditionsNode = node["conditions"];
            if (conditionsNode != null)
            {
                conditions = XMLConditionList.FromXmlNode(conditionsNode, defaultNsp);
            }

            return new ProductStageMeta()
            {
                Text = text,
                Price = price,
                Unlocks = unlocks,
                Conditions = conditions
            };
        }
    }
}
