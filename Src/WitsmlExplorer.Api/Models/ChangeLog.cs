namespace WitsmlExplorer.Api.Models
{
    // ChangeLog extends ObjectOnWellbore despite not having uid and name fields in WITSML
    // These fields are set in the API to conform to ObjectOnWellbore
    public class ChangeLog : ObjectOnWellbore
    {
        public string UidObject { get; set; }
        public string NameObject { get; set; }
        public string LastChangeType { get; set; }
        public CommonData CommonData { get; set; }
    }
}
