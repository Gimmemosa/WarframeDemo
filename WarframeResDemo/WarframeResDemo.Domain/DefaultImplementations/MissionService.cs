using System;
using System.Collections.Generic;
using System.Text;
using WarframeResDemo.Data.Entities;
using WarframeResDemo.Data.Repositories;
using WarframeResDemo.Domain.Interfaces;

namespace WarframeResDemo.Domain.DefaultImplementations
{
    public class MissionService : IMissionService
    {
        private IMissionRepository _missionRepository;
        private IFractionRepository _fractionRepository;
        private IMissionTypeRepository _missionTypeRepository;
        private IPausedMissionRepository _pausedMissionRepository;
        private IEndedMissionRepository _endedMissionRepository;

        public MissionService(IMissionRepository missionRepository, IFractionRepository fractionRepository, IMissionTypeRepository missionTypeRepository, IPausedMissionRepository pausedMissionRepository, IEndedMissionRepository endedMissionRepository)
        {
            _missionRepository = missionRepository;
            _fractionRepository = fractionRepository;
            _missionTypeRepository = missionTypeRepository;
            _endedMissionRepository = endedMissionRepository;
            _pausedMissionRepository = pausedMissionRepository;
        }

        #region IMissionService Members
        public void ChangeFraction(int missionId, int fractionId)
        {
            var mission = _missionRepository.GetMissionDetails(missionId);
            mission.FractionId = fractionId;
            mission.Fraction = _fractionRepository.GetFractionDetails(fractionId);
            _missionRepository.UpdateMission(mission);
        }

        public void ChangeLevel(int missionId, int level)
        {
            var mission = _missionRepository.GetMissionDetails(missionId);
            mission.MissionLevel = level;
            _missionRepository.UpdateMission(mission);
        }

        public void ChangeType(int missionId, int missionTypeId)
        {
            var mission = _missionRepository.GetMissionDetails(missionId);
            mission.MissionTypeId = missionTypeId;
            mission.MissionType = _missionTypeRepository.GetTypeDetails(missionTypeId);
            _missionRepository.UpdateMission(mission);
        }

        public void PauseMission(int missionId, int resourceId, int resourceCount)
        {
            var pausedMission = new PausedMission();
            pausedMission.MissionId = missionId;
            pausedMission.ResourceId = resourceId;
            pausedMission.ResourceCount = resourceCount;
            _pausedMissionRepository.CreateMission(pausedMission);
        }

        public int IsPaused(int missionId, int resourceId)
        {
            int returningValue = 0;
            List<PausedMission> missions = _pausedMissionRepository.GetAllMissions();
            missions.ForEach(m =>
            {
                if (m.MissionId == missionId && m.ResourceId == resourceId)
                {
                    returningValue = m.Id;
                }
            });
            return returningValue;
        }

        public void EndMission(int missionId, int resourceId)
        {
            var mission = new EndedMissions();
            mission.MissionId = missionId;
            mission.ResourceId = resourceId;
            _endedMissionRepository.CreateMission(mission);
            List<PausedMission> missions = _pausedMissionRepository.GetAllMissions();
            missions.ForEach(m =>
            {
                if (missionId == m.MissionId && resourceId == m.ResourceId)
                {
                    _pausedMissionRepository.DeleteMission(m.Id);
                }
            });
        }
        #endregion
    }
}
