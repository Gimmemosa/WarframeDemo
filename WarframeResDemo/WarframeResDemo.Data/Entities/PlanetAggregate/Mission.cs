namespace WarframeResDemo.Data.Entities
{
    public class Mission
    {
        public int Id { get; set; }
        public int FractionId { get; set; }
        public Fraction Fraction { get; set; }
        public int MissionTypeId { get; set; }
        public MissionType MissionType { get; set; }
        public string MissionName { get; set; }
        public int MissionLevel { get; set; }
        public int PlanetId { get; set; }
        public Planet Planet { get; set; }
    }
}
