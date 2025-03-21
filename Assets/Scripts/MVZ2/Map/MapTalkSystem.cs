﻿using MVZ2.Talk;
using MVZ2.Talks;
using MVZ2Logic.Archives;
using MVZ2Logic.Maps;
using PVZEngine.Level;

namespace MVZ2.Map
{
    public class MapTalkSystem : MVZ2TalkSystem
    {
        public MapTalkSystem(IMapInterface map, TalkController talk) : base(talk)
        {
            this.map = map;
        }

        public override IArchiveInterface GetArchive()
        {
            return null;
        }
        public override IMapInterface GetMap()
        {
            return map;
        }
        public override LevelEngine GetLevel()
        {
            return null;
        }
        private IMapInterface map;
    }
}
