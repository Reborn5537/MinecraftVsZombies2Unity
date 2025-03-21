﻿using System.Collections.Generic;
using UnityEngine;

namespace MVZ2.Supporters
{
    [SerializeField]
    class SponsorInfos
    {
        public SponsorInfos(SponsorItem[] items)
        {
            List<SponsorSaveItem> saveItems = new List<SponsorSaveItem>();
            foreach (var sponsor in items)
            {
                var planSaveItems = new List<SponsorPlanSaveItem>();
                foreach (var plan in sponsor.Plans)
                {
                    if (SponsorPlans.TryGetPlanByID(plan.PlanID, out var id))
                    {
                        planSaveItems.Add(new SponsorPlanSaveItem()
                        {
                            rank = id.rank,
                            rankType = id.type,
                        });
                    }
                }
                if (planSaveItems.Count > 0)
                {
                    saveItems.Add(new SponsorSaveItem()
                    {
                        name = sponsor.User.Name,
                        plans = planSaveItems.ToArray()
                    });
                }
            }
            sponsors = saveItems.ToArray();
        }
        public long lastUpdateTime;
        public SponsorSaveItem[] sponsors;
    }
    [SerializeField]
    class SponsorSaveItem
    {
        public string name;
        public SponsorPlanSaveItem[] plans;
    }
    [SerializeField]
    class SponsorPlanSaveItem
    {
        public int rank;
        public int rankType;
    }
    public static class SponsorPlans
    {
        public static class Furnace
        {
            public const int TYPE = 1;
            public const int FURNACE = 15;
            public const int GUNPOWDER_BARREL = 30;
            public const int BLAST_FURNACE = 50;
        }
        public static class Sensor
        {
            public const int TYPE = 2;
            public const int MOONLIGHT_SENSOR = 10;
        }

        public static bool TryGetPlanByID(string id, out (int type, int rank) plan)
        {
            return planMap.TryGetValue(id, out plan);
        }

        private static readonly Dictionary<string, (int type, int rank)> planMap = new Dictionary<string, (int type, int rank)>()
        {
            { "25afa204d56311ef9a3552540025c377", (SponsorPlans.Sensor.TYPE, SponsorPlans.Sensor.MOONLIGHT_SENSOR) },
            { "3d36f9e0d56311efa99a52540025c377", (SponsorPlans.Furnace.TYPE, SponsorPlans.Furnace.FURNACE) },
            { "6f26d290d56311efa5aa52540025c377", (SponsorPlans.Furnace.TYPE, SponsorPlans.Furnace.GUNPOWDER_BARREL) },
            { "b3a2d76cd56211efb2f852540025c377", (SponsorPlans.Furnace.TYPE, SponsorPlans.Furnace.BLAST_FURNACE) },
        };
    }
}
