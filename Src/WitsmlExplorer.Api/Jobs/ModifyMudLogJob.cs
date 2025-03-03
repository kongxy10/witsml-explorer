using WitsmlExplorer.Api.Models;

namespace WitsmlExplorer.Api.Jobs
{
    public record ModifyMudLogJob : Job
    {
        public MudLog MudLog { get; init; }

        public override string Description()
        {
            return $"ToModify - WellUid: {MudLog.WellUid}; WellboreUid: {MudLog.WellboreUid}; MudLogUid: {MudLog.Uid};";
        }

        public override string GetObjectName()
        {
            return MudLog.Name;
        }

        public override string GetWellboreName()
        {
            return MudLog.WellboreName;
        }

        public override string GetWellName()
        {
            return MudLog.WellName;
        }
    }
}
