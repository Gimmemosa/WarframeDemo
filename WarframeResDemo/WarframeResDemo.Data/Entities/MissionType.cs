using System;

namespace WarframeResDemo.Data.Entities
{
    public class MissionType
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }

    public class ExcavationType : MissionType
    {
        public int FarmedResource { get; set; }
    }

    public class SurvivalType : MissionType
    {
        public DateTime Time { get; set; }
    }
}
