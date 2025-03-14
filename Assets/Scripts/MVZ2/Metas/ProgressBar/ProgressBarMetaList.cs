﻿using System.Xml;

namespace MVZ2.Metas
{
    public class ProgressBarMetaList
    {
        public ProgressBarMeta[] metas;
        public static ProgressBarMetaList FromXmlNode(XmlNode node, string defaultNsp)
        {
            var resources = new ProgressBarMeta[node.ChildNodes.Count];
            for (int i = 0; i < resources.Length; i++)
            {
                resources[i] = ProgressBarMeta.FromXmlNode(node.ChildNodes[i], defaultNsp);
            }
            return new ProgressBarMetaList()
            {
                metas = resources,
            };
        }
    }
}
