using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Witsml.Data;
using Witsml.Data.Measures;
using Witsml.Extensions;

using WitsmlExplorer.Api.Jobs.Common;
using WitsmlExplorer.Api.Models;
using WitsmlExplorer.Api.Models.Measure;
using WitsmlExplorer.Api.Services;

namespace WitsmlExplorer.Api.Query
{
    public class WbGeometryQueries
    {
        public static WitsmlWbGeometrys GetWitsmlWbGeometryByWellbore(string wellUid, string wellboreUid)
        {
            return RequiredElementsQuery(wellUid, wellboreUid, "");
        }

        public static WitsmlWbGeometrys GetWitsmlWbGeometryById(string wellUid, string wellboreUid, string wbGeometryUid)
        {
            return RequiredElementsQuery(wellUid, wellboreUid, wbGeometryUid);
        }

        public static WitsmlWbGeometrys GetWitsmlWbGeometryIdOnly(string wellUid, string wellboreUid, string wbGeometryUid)
        {
            return new WitsmlWbGeometrys
            {
                WbGeometrys = new WitsmlWbGeometry
                {
                    Uid = wbGeometryUid,
                    UidWell = wellUid,
                    UidWellbore = wellboreUid,
                }.AsSingletonList()
            };
        }

        private static WitsmlWbGeometrys RequiredElementsQuery(string wellUid, string wellboreUid, string wbGeometryUid)
        {
            return new WitsmlWbGeometrys
            {
                WbGeometrys = new WitsmlWbGeometry
                {
                    Uid = wbGeometryUid,
                    UidWellbore = wellboreUid,
                    UidWell = wellUid,
                    Name = "",
                    NameWellbore = "",
                    NameWell = "",
                    DTimReport = "",
                    MdBottom = MeasureWithDatum.ToEmptyWitsml<WitsmlMeasuredDepthCoord>(),
                    GapAir = LengthMeasure.ToEmptyWitsml<WitsmlLengthMeasure>(),
                    DepthWaterMean = LengthMeasure.ToEmptyWitsml<WitsmlLengthMeasure>(),
                    CommonData = new WitsmlCommonData()
                    {
                        SourceName = "",
                        ItemState = "",
                        Comments = "",
                        DTimCreation = "",
                        DTimLastChange = "",
                    }
                }.AsSingletonList()
            };
        }

        public static WitsmlWbGeometrys GetSectionsByWbGeometryId(string wellUid, string wellboreUid, string wbGeometryUid)
        {
            return new WitsmlWbGeometrys
            {
                WbGeometrys = new WitsmlWbGeometry
                {
                    Uid = wbGeometryUid,
                    UidWell = wellUid,
                    UidWellbore = wellboreUid,
                    WbGeometrySections = new WitsmlWbGeometrySection
                    {
                        Uid = "",
                        TypeHoleCasing = "",
                        MdTop = MeasureWithDatum.ToEmptyWitsml<WitsmlMeasuredDepthCoord>(),
                        MdBottom = MeasureWithDatum.ToEmptyWitsml<WitsmlMeasuredDepthCoord>(),
                        TvdTop = MeasureWithDatum.ToEmptyWitsml<WitsmlWellVerticalDepthCoord>(),
                        TvdBottom = MeasureWithDatum.ToEmptyWitsml<WitsmlWellVerticalDepthCoord>(),
                        IdSection = LengthMeasure.ToEmptyWitsml<WitsmlLengthMeasure>(),
                        OdSection = LengthMeasure.ToEmptyWitsml<WitsmlLengthMeasure>(),
                        WtPerLen = LengthMeasure.ToEmptyWitsml<WitsmlMassPerLengthMeasure>(),
                        Grade = "",
                        CurveConductor = "",
                        DiaDrift = LengthMeasure.ToEmptyWitsml<WitsmlLengthMeasure>(),
                        FactFric = ""
                    }.AsSingletonList()
                }.AsSingletonList()
            };
        }

        public static WitsmlWbGeometrys CreateWbGeometry(WbGeometry wbGeometry)
        {
            return new WitsmlWbGeometrys
            {
                WbGeometrys = new WitsmlWbGeometry
                {
                    UidWell = wbGeometry.WellUid,
                    UidWellbore = wbGeometry.WellboreUid,
                    Uid = wbGeometry.Uid,
                    Name = wbGeometry.Name,
                    NameWell = wbGeometry.WellName,
                    NameWellbore = wbGeometry.WellboreName,
                    DTimReport = StringHelpers.ToUniversalDateTimeString(wbGeometry.DTimReport),
                    MdBottom = wbGeometry.MdBottom != null ? new WitsmlMeasuredDepthCoord { Uom = wbGeometry.MdBottom.Uom, Value = wbGeometry.MdBottom.Value.ToString(CultureInfo.InvariantCulture) } : null,
                    GapAir = wbGeometry.GapAir != null ? new WitsmlLengthMeasure { Uom = wbGeometry.GapAir.Uom, Value = wbGeometry.GapAir.Value.ToString(CultureInfo.InvariantCulture) } : null,
                    DepthWaterMean = wbGeometry.DepthWaterMean != null ? new WitsmlLengthMeasure { Uom = wbGeometry.DepthWaterMean.Uom, Value = wbGeometry.DepthWaterMean.Value.ToString(CultureInfo.InvariantCulture) } : null,
                    CommonData = new WitsmlCommonData()
                    {
                        Comments = wbGeometry.CommonData.Comments,
                        ItemState = wbGeometry.CommonData.ItemState,
                        SourceName = wbGeometry.CommonData.SourceName,
                        DTimCreation = null,
                        DTimLastChange = null
                    }
                }.AsSingletonList()
            };
        }

        public static WitsmlWbGeometrys UpdateWbGeometrySection(WbGeometrySection wbGeometrySection, ObjectReference wbGeometryReference)
        {
            WitsmlWbGeometrySection wbgs = new()
            {
                Uid = wbGeometrySection.Uid,
                Grade = wbGeometrySection.Grade,
                TypeHoleCasing = wbGeometrySection.TypeHoleCasing,
                CurveConductor = StringHelpers.OptionalBooleanToString(wbGeometrySection.CurveConductor),
                DiaDrift = wbGeometrySection.DiaDrift?.ToWitsml<WitsmlLengthMeasure>(),
                IdSection = wbGeometrySection.IdSection?.ToWitsml<WitsmlLengthMeasure>(),
                OdSection = wbGeometrySection.OdSection?.ToWitsml<WitsmlLengthMeasure>(),
                MdBottom = wbGeometrySection.MdBottom?.ToWitsml<WitsmlMeasuredDepthCoord>(),
                MdTop = wbGeometrySection.MdTop?.ToWitsml<WitsmlMeasuredDepthCoord>(),
                TvdBottom = wbGeometrySection.TvdBottom?.ToWitsml<WitsmlWellVerticalDepthCoord>(),
                TvdTop = wbGeometrySection.TvdTop?.ToWitsml<WitsmlWellVerticalDepthCoord>(),
                WtPerLen = wbGeometrySection.WtPerLen?.ToWitsml<WitsmlMassPerLengthMeasure>(),
                FactFric = wbGeometrySection.FactFric?.ToString(CultureInfo.InvariantCulture)
            };

            return new WitsmlWbGeometrys
            {
                WbGeometrys = new WitsmlWbGeometry
                {
                    UidWell = wbGeometryReference.WellUid,
                    UidWellbore = wbGeometryReference.WellboreUid,
                    Uid = wbGeometryReference.Uid,
                    WbGeometrySections = wbgs.AsSingletonList()
                }.AsSingletonList()
            };
        }

        public static WitsmlWbGeometrys CopyWbGeometrySections(WitsmlWbGeometry targetWbGeometry, IEnumerable<WitsmlWbGeometrySection> componentsToCopy)
        {
            return new()
            {
                WbGeometrys = new List<WitsmlWbGeometry> {
                    new WitsmlWbGeometry
                    {
                        UidWell = targetWbGeometry.UidWell,
                        UidWellbore = targetWbGeometry.UidWellbore,
                        Uid = targetWbGeometry.Uid,
                        WbGeometrySections = componentsToCopy.ToList()
                    }
                }
            };
        }

        public static WitsmlWbGeometrys DeleteWbGeometrySections(string wellUid, string wellboreUid, string wbGeometryUid, IEnumerable<string> wbGeometrySectionUids)
        {
            return new WitsmlWbGeometrys
            {
                WbGeometrys = new WitsmlWbGeometry
                {
                    UidWell = wellUid,
                    UidWellbore = wellboreUid,
                    Uid = wbGeometryUid,
                    WbGeometrySections = wbGeometrySectionUids.Select(uid => new WitsmlWbGeometrySection
                    {
                        Uid = uid
                    }).ToList()
                }.AsSingletonList()
            };
        }

    }
}
