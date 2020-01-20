using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarframeResDemo.Data.Entities
{
    public class PausedMission
    {
        public int Id { get; set; }
        public int MissionId { get; set; }
        public float Progress { get; set; }
    }
}
