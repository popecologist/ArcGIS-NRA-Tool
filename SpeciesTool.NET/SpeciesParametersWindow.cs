/*
 * This file is part of the ArcGIS National Responsibility Assessment Tool
 * source code (https://github.com/popecologist/ArcGIS-NRA-Tool or
 * http://xxx.github.io).
 *
 * Copyright (c) 2020 Yu-Pin Lin / 林裕彬
 * Department of Bioenvironmental Systems Engineering
 * National Taiwan University
 * No. 1, Sec. 4, Roosevelt Road
 * Taipei
 * 10617 Taiwan
 * R.O.C.
 * Office Phone: 886-2-33663467; Fax: 886-2-2368-6980
 * E-mail: yplin@ntu.edu.tw
 * http://homepage.ntu.edu.tw/~yplin/Scales-Taiwan.htm
 *
 * The development of the ArcGIS-NRA-Tool was mainly funded
 * by Minister of Science and Technology  of Taiwan
 * ( National Science Council of Taiwan) (NSC101-2923-I-002-001-MY2),
 * and a contribution from the EU FP7 project
 * SCALES: Securing the Conservation of biodiversity across
 * Administrative Levels and spatial, temporal, and Ecological Scales,
 * under the European Union’s Framework Program 7
 * (grant Code: 226852 FP7-ENVIRONMENT ENV.2008.2.1.4.4.;
 * www. scales-project.net.
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, version 3.
 *
 * This program is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
 * General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program. If not, see <http://www.gnu.org/licenses/>.
 *
 * Please cite and refer to the tool in any report, scientific paper or
 * other type of publication either in print or electronically with the
 * reference mentioned below.
 *
 * References:
 * Lin Y-P, Schmeller D S, Ding T S, Wang Y Ch,  Lien W-Y, Henle K,
 * Klenke R A (2020): A GIS-based policy support tool to determine national
 * responsibilities and priorities for biodiversity conservation.
 * PLoS ONE. doi: xxxxxx
 */

﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using Path = System.IO.Path;

namespace SpeciesTool.NET
{
    using Properties;

    /// <summary>
    /// Designer class of the dockable window add-in. It contains user interfaces that
    /// make up the dockable window.
    /// </summary>
    public partial class SpeciesParametersWindow : UserControl
    {
        #region Types

        public class SavingParameters
        {
            public decimal ByAreaLowValue { get; set; }

            public decimal ByAreaHighValue { get; set; }

            public decimal ByBiomesLowValue { get; set; }

            public decimal ByBiomesHighValue { get; set; }

            public decimal Coefficient { get; set; }

            public int CalculationType { get; set; }

            public string IUCNCategoriesPath { get; set; }

            public bool AddThematicLayers { get; set; }

            public string ResultsPath { get; set; }

            public bool OverwriteResult { get; set; }

            public bool LayersFiltered { get; set; }

            public SavingParameters()
            {
                LayersFiltered = false;

                ByAreaLowValue = 500000;
                ByAreaHighValue = 500000000;

                ByBiomesLowValue = 2;
                ByBiomesHighValue = 5;

                Coefficient = 2;
                CalculationType = (int)CalculationTypeEnum.ByBiomes;
                IUCNCategoriesPath = string.Empty;
                AddThematicLayers = true;
                ResultsPath = string.Empty;
                OverwriteResult = true;
            }

            private static string GetStoragePath()
            {
                var a_Path = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ThisAddIn.Name);
                Directory.CreateDirectory(a_Path);
                return Path.Combine(a_Path, "SavingParameters.xml");
            }

            public void Save()
            {
                try
                {
                    var a_Serializer = new XmlSerializer(this.GetType());
                    using (var a_StreamWriter = new StreamWriter(GetStoragePath())) a_Serializer.Serialize(a_StreamWriter, this);
                }
                catch
                {
                }

            }

            public static SavingParameters Load()
            {
                SavingParameters a_ReturnValue = null;
                try
                {
                    var a_FileName = GetStoragePath();
                    if (File.Exists(a_FileName))
                    {
                        var a_Serializer = new XmlSerializer(typeof(SavingParameters));
                        using (var a_StreamReader = new StreamReader(a_FileName)) a_ReturnValue = ((SavingParameters)a_Serializer.Deserialize(a_StreamReader));
                    }
                }
                catch
                {
                    a_ReturnValue = null;
                }
                return a_ReturnValue ?? new SavingParameters();
            }
        }

        private enum LayerTypesEnum
        {
            Species,

            Biomes,

            Regions,

            FocalRegions,
        }

        private class LayerInfo : IDisposable
        {
            public string LayerName { get; set; }

            public string LayerAlias { get; set; }

            public IFeatureLayer Layer { get; set; }

            public string NameFieldName { get; set; }

            public int NameFieldIndex { get; set; }

            public List<Utils.RowInfo> SelectedValues { get; set; }

            public double CalculatedArea { get; private set; }

            public IGeometry CalculatedUnitedGeometry { get; private set; }

            public LayerInfo()
            {
                LayerName = string.Empty;
                LayerAlias = string.Empty;
                Layer = null;
                NameFieldName = string.Empty;
                NameFieldIndex = -1;
                SelectedValues = null;
                CalculatedArea = 0;
                this.CalculatedUnitedGeometry = null;
            }

            public virtual void Dispose()
            {
                Layer = null;
                if (SelectedValues != null)
                {
                    SelectedValues.Clear();
                    SelectedValues = null;
                }
                if (this.CalculatedUnitedGeometry != null)
                {
                    Marshal.ReleaseComObject(this.CalculatedUnitedGeometry);
                    this.CalculatedUnitedGeometry = null;
                }

            }

            protected void Assign(LayerInfo a_Source)
            {
                this.LayerName = a_Source.LayerName;
                this.LayerAlias = a_Source.LayerAlias;
                this.Layer = a_Source.Layer;
                this.NameFieldName = a_Source.NameFieldName;
                this.NameFieldIndex = a_Source.NameFieldIndex;
                this.SelectedValues = a_Source.SelectedValues;
                this.CalculatedArea = a_Source.CalculatedArea;
                this.CalculatedUnitedGeometry = a_Source.CalculatedUnitedGeometry;
            }

            public virtual bool Validate(
                out string a_ErrorMessage, bool a_NameFieldIsMandatory, bool a_SelectedValuesMandatory)
            {
                var a_ErrorMessages = new List<string>();
                if (Layer == null) a_ErrorMessages.Add("Layer not found");
                if (a_NameFieldIsMandatory && NameFieldIndex == -1) a_ErrorMessages.Add("Name field not found");
                if (a_SelectedValuesMandatory && SelectedValues != null && SelectedValues.Count == 0) a_ErrorMessages.Add("Nothing to analize(No values ​​selected)");
                a_ErrorMessage = string.Join(Environment.NewLine, a_ErrorMessages.ToArray());
                return string.IsNullOrEmpty(a_ErrorMessage);
            }

            public void CalculateArea(
                IQueryFilter a_QueryFilter, bool a_CalculateUnitedGeometry, Action<IFeature> a_FeatureAction)
            {
                double a_Area = 0;
                var a_TypeMissing = Type.Missing;
                var a_GeometryBag = a_CalculateUnitedGeometry ? new GeometryBagClass() : null;
                IFeatureCursor a_FeatureCursor = null;
                try
                {
                    a_FeatureCursor = Layer.Search(a_QueryFilter, true);
                    a_FeatureCursor.DoForCursor(
                        delegate(IFeature a_Feature)
                            {
                                a_Area += a_Feature.Shape.GetArea();
                                if (a_GeometryBag != null) a_GeometryBag.AddGeometry(a_Feature.ShapeCopy, ref a_TypeMissing, ref a_TypeMissing);
                                if (a_FeatureAction != null) a_FeatureAction(a_Feature);
                            });
                    if (a_GeometryBag != null)
                    {
                        this.CalculatedUnitedGeometry = new PolygonClass();
                        (this.CalculatedUnitedGeometry as ITopologicalOperator).ConstructUnion(a_GeometryBag);
                    }
                    CalculatedArea = a_Area;
                }
                finally
                {
                    if (a_GeometryBag != null) Marshal.ReleaseComObject(a_GeometryBag);
                    if (a_FeatureCursor != null) Marshal.ReleaseComObject(a_FeatureCursor);
                }
            }
        }

        private class MultipleLayerInfo : LayerInfo
        {
            public List<LayerInfo> LayerInfos { get; private set; }

            public LayerInfo ActiveLayerInfo
            {
                get
                {
                    return this;
                }
                set
                {
                    Assign(value);
                }
            }

            public MultipleLayerInfo()
                : base()
            {
                LayerInfos = new List<LayerInfo>();
            }

            public override void Dispose()
            {
                if (LayerInfos != null)
                {
                    foreach (var a_LayersInfo in LayerInfos) a_LayersInfo.Dispose();
                    LayerInfos.Clear();
                    LayerInfos = null;
                }
            }

            public override bool Validate(
                out string a_ErrorMessage, bool a_NameFieldIsMandatory, bool a_SelectedValuesMandatory)
            {
                var a_ErrorMessages = new List<string>();
                if (LayerInfos.Count == 0) a_ErrorMessages.Add("Nothing to analize(No values selected)");
                else
                    foreach (var a_LayerInfo in LayerInfos)
                    {
                        string a_Message;
                        if (!a_LayerInfo.Validate(out a_Message, a_NameFieldIsMandatory, a_SelectedValuesMandatory)) a_ErrorMessages.Add(a_Message);
                    }
                a_ErrorMessage = string.Join(Environment.NewLine, a_ErrorMessages.ToArray());
                return string.IsNullOrEmpty(a_ErrorMessage);
            }
        }

        private class BiomeRowInfoEx : Utils.RowInfo
        {
            public double Area { get; set; }

            public double IntersectionArea { get; set; }

            public BiomeRowInfoEx()
            {
                Area = 0;
                IntersectionArea = 0;
            }

            public BiomeRowInfoEx(IFeature a_BiomeFeature, IGeometry a_Geometry, int a_DisplayValueFieldIndex)
                : this()
            {
                ObjectId = a_BiomeFeature.HasOID ? a_BiomeFeature.OID : -1;
                DisplayValue = a_DisplayValueFieldIndex == -1
                                   ? ObjectId.ToString()
                                   : (a_BiomeFeature.Value[a_DisplayValueFieldIndex] ?? string.Empty).ToString();
                Area = a_BiomeFeature.Shape.GetArea();

                var a_IntersectionGeometry = ((ITopologicalOperator)a_BiomeFeature.Shape).Intersect(
                    a_Geometry, esriGeometryDimension.esriGeometry2Dimension);
                try
                {
                    if (a_IntersectionGeometry != null && !a_IntersectionGeometry.IsEmpty)
                    {
                        IntersectionArea = a_IntersectionGeometry.GetArea();
                    }
                }
                finally
                {
                    if (a_IntersectionGeometry != null) Marshal.ReleaseComObject(a_IntersectionGeometry);
                }
            }
        }

        private class BiomesInfoEx : IDisposable
        {
            public class IntersectionInfo
            {
                public HashSet<int> Ids { get; private set; }
                public double FullArea { get; set; }
                public double IntersectionArea { get; set; }

                public IntersectionInfo()
                {
                    Ids = new HashSet<int>();
                }
            }


            private ISpatialFilter m_SpatialFilter = null;

            //we need
            //1. Link to LayerInfo
            //2. For different layer types we must store next parameters
            //2.1 Area of intersection summary and for each biome
            public LayerInfo BiomesLayerInfo { get; private set; }

            public Dictionary<LayerTypesEnum, IntersectionInfo> IntersectedInfos { get; private set; }


            public double CalculateIntersectionsArea(LayerTypesEnum a_LayerType, IGeometry a_Geometry, LayerTypesEnum? a_CheckLayerType = null)
            {
                var a_IntersectionInfo = new IntersectionInfo();
                IntersectionInfo a_CheckIntersectionInfo = null;
                if (a_CheckLayerType.HasValue)
                    a_CheckIntersectionInfo = IntersectedInfos[a_CheckLayerType.Value];
                IntersectedInfos[a_LayerType] = a_IntersectionInfo;
                m_SpatialFilter.Geometry = a_Geometry;
                var a_FeatureCursor = BiomesLayerInfo.Layer.Search(m_SpatialFilter, true);
                a_FeatureCursor.DoForCursor(
                    a_Feature =>
                        {
                            if (a_CheckIntersectionInfo == null || a_CheckIntersectionInfo.Ids.Contains(a_Feature.OID))
                            {
                                var a_BiomeRowInfoEx = new BiomeRowInfoEx(a_Feature, a_Geometry, BiomesLayerInfo.NameFieldIndex);
                                a_IntersectionInfo.IntersectionArea += a_BiomeRowInfoEx.IntersectionArea;
                                a_IntersectionInfo.FullArea += a_BiomeRowInfoEx.Area;
                                a_IntersectionInfo.Ids.Add(a_Feature.OID);
                            }
                            Application.DoEvents();
                        });
                return IntersectedInfos[a_LayerType].IntersectionArea;
            }

            public BiomesInfoEx(LayerInfo a_BiomesLayerInfo)
            {
                BiomesLayerInfo = a_BiomesLayerInfo;
                IntersectedInfos = new Dictionary<LayerTypesEnum, IntersectionInfo>();
                m_SpatialFilter = new SpatialFilterClass();
                m_SpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            }

            public void Dispose()
            {
                if (m_SpatialFilter != null)
                {
                    Marshal.ReleaseComObject(m_SpatialFilter);
                    m_SpatialFilter = null;
                }
                IntersectedInfos.Clear();
            }
        }

        private class LayerRendererInfo
        {
            public string Label { get; set; }

            public Color FillColor { get; set; }

            public esriSimpleFillStyle FillStyle { get; set; }

            public string FieldValue { get; set; }

            public LayerRendererInfo()
            {
                this.Label = string.Empty;
                this.FillColor = Color.GhostWhite;
                FillStyle = esriSimpleFillStyle.esriSFSSolid;
                FieldValue = string.Empty;
            }

            public static IEnumerable<LayerRendererInfo> LayerRendererInfoForEnum<TEnumType>() where TEnumType : struct
            {
                var a_RendererInfos = (from TEnumType a_Enum in Enum.GetValues(typeof(TEnumType))
                                       select
                                           new LayerRendererInfo()
                                               {
                                                   FieldValue = a_Enum.GetEnumDescription(),
                                                   Label = a_Enum.GetEnumDescription(),
                                                   FillColor = a_Enum.GetEnumColor()
                                               }).ToList();
                return a_RendererInfos;
            }
        }

        private class CountryStatisticInfo
        {
            public string Name { get; set; }

            public Dictionary<string, PriorityEnum> SpeciesInfo { get; private set; }

            private static Dictionary<string, PriorityEnum> m_DescriptionToPriority = null;

            public CountryStatisticInfo()
            {
                this.Name = string.Empty;
                this.SpeciesInfo = new Dictionary<string, PriorityEnum>(StringComparer.InvariantCultureIgnoreCase);
                if (m_DescriptionToPriority == null)
                {
                    m_DescriptionToPriority = new Dictionary<string, PriorityEnum>();
                    foreach (PriorityEnum a_Value in Enum.GetValues(typeof(PriorityEnum)))
                        m_DescriptionToPriority.Add(a_Value.GetEnumDescription(), a_Value);
                }
            }

            public void ReadData(IFeature a_Feature)
            {
                var a_SpeciesName = a_Feature.GetFieldValue(ResultsShapeFileHelper.fnSpeciesName, string.Empty);
                var a_Priority = a_Feature.GetFieldValue(ResultsShapeFileHelper.fnPriority, string.Empty);
                if (!SpeciesInfo.ContainsKey(a_SpeciesName))
                {
                    PriorityEnum a_Value;
                    if (!m_DescriptionToPriority.TryGetValue(a_Priority, out a_Value))
                        throw new Exception(string.Format("{0} is unknown as Priority value", a_Priority));
                    SpeciesInfo[a_SpeciesName] = a_Value;
                }
            }

            public string GetInfo()
            {
                var a_Sb = new StringBuilder();
                a_Sb.AppendFormat("{0}: ", Name);
                a_Sb.AppendFormat("{0} species, ", this.SpeciesInfo.Count);
                var a_DDNE = 0;
                var a_HighOrVeryHigh = 0;
                foreach (var a_Priority in SpeciesInfo.Values)
                {
                    if (a_Priority == PriorityEnum.DataDeficientNotEvaluated)
                        a_DDNE++;
                    else if (a_Priority == PriorityEnum.HighPriority || a_Priority == PriorityEnum.VeryHighPriority)
                        a_HighOrVeryHigh++;
                }
                if (this.SpeciesInfo.Count == a_DDNE)
                    a_Sb.Append("group 0: Data Deficient/Not Evaluated");
                else
                {
                    var a_RoundedNumOfCountries = (int)(Math.Truncate((SpeciesInfo.Count - 1)/5.0) + 1)*5;
                    var a_Group = 1;
                    if (a_HighOrVeryHigh != 0)
                        a_Group = ((a_HighOrVeryHigh - 1)/ a_RoundedNumOfCountries) * 5 + 1;
                    a_Sb.AppendFormat("group {0}({1})", a_Group, a_HighOrVeryHigh);
                }
                return a_Sb.ToString();
            }
        }

        private class NRCPStatisticInfo: IDisposable
        {
            public Dictionary<string, NRCPStatisticShapeFileHelper.NRCPStatisticFeatureClassRow> Rows { get; set; }

            public void AnalyzeRow(IFeature a_Feature)
            {
                var a_CountryName = a_Feature.GetFieldValue(ResultsShapeFileHelper.fnFocalRegionNames, string.Empty);
                if (string.IsNullOrEmpty(a_CountryName))
                    return;
                NRCPStatisticShapeFileHelper.NRCPStatisticFeatureClassRow a_RowInfo;
                if (!Rows.TryGetValue(a_CountryName, out a_RowInfo))
                {
                    Rows[a_CountryName] = new NRCPStatisticShapeFileHelper.NRCPStatisticFeatureClassRow()
                    {
                        CountryName = a_CountryName,
                        Shape = a_Feature.ShapeCopy
                    };
                }
                a_RowInfo = Rows[a_CountryName];
                //global distribution
                var a_Value = a_Feature.GetFieldValue(ResultsShapeFileHelper.fnGlobalDistribution, string.Empty);
                if (GlobalDistributionEnum.Wide.GetEnumDescription() == a_Value)
                    a_RowInfo.Wide++;
                if (GlobalDistributionEnum.Regional.GetEnumDescription() == a_Value)
                    a_RowInfo.Regional++;
                if (GlobalDistributionEnum.Local.GetEnumDescription() == a_Value)
                    a_RowInfo.Local++;

                //regional distribution
                a_Value = a_Feature.GetFieldValue(ResultsShapeFileHelper.fnRegionalDistribution, string.Empty);
                if (RegionalDistributionEnum.Low.GetEnumDescription() == a_Value)
                    a_RowInfo.Low++;
                if (RegionalDistributionEnum.High.GetEnumDescription() == a_Value)
                    a_RowInfo.High++;

                //priority
                a_Value = a_Feature.GetFieldValue(ResultsShapeFileHelper.fnPriority, string.Empty);
                if (PriorityEnum.HighPriority.GetEnumDescription() == a_Value ||
                    PriorityEnum.VeryHighPriority.GetEnumDescription() == a_Value)
                    a_RowInfo.HighAndVeryHigh++;
                if (PriorityEnum.MediumPriority.GetEnumDescription() == a_Value)
                    a_RowInfo.Medium++;
                if (PriorityEnum.BasicPriority.GetEnumDescription() == a_Value)
                    a_RowInfo.Basic++;

                //class
                a_Value = a_Feature.GetFieldValue(ResultsShapeFileHelper.fnClass, string.Empty);
                if (ClassEnum.Class1.GetEnumDescription() == a_Value)
                    a_RowInfo.Class1++;
                if (ClassEnum.Class2.GetEnumDescription() == a_Value)
                    a_RowInfo.Class2++;
                if (ClassEnum.Class3.GetEnumDescription() == a_Value)
                    a_RowInfo.Class3++;
                if (ClassEnum.Class4.GetEnumDescription() == a_Value)
                    a_RowInfo.Class4++;

                //score
                a_RowInfo.Score += a_Feature.GetFieldValue(ResultsShapeFileHelper.fnScore, 0);

                //category
                a_Value = a_Feature.GetFieldValue(ResultsShapeFileHelper.fnCategory, string.Empty);
                if (IUCNCategoryEnum.ExtinctInTheWild.GetEnumDescription() == a_Value)
                    a_RowInfo.ExtinctInWild++;
                if (IUCNCategoryEnum.CriticallyEndangered.GetEnumDescription() == a_Value)
                    a_RowInfo.CriticallyEndangered++;
                if (IUCNCategoryEnum.Endangered.GetEnumDescription() == a_Value)
                    a_RowInfo.Endangered++;
                if (IUCNCategoryEnum.Vulnerable.GetEnumDescription() == a_Value)
                    a_RowInfo.Vulnerable++;
                if (IUCNCategoryEnum.NearThreatened.GetEnumDescription() == a_Value)
                    a_RowInfo.NearThreatened++;
                if (IUCNCategoryEnum.LeastConcern.GetEnumDescription() == a_Value)
                    a_RowInfo.LeastConcern++;
                if (IUCNCategoryEnum.DataDeficient.GetEnumDescription() == a_Value)
                    a_RowInfo.DataDeficient++;
                if (IUCNCategoryEnum.NotEvaluated.GetEnumDescription() == a_Value)
                    a_RowInfo.NotEvaluated++;
            }

            public NRCPStatisticInfo()
            {
                this.Rows = new Dictionary<string, NRCPStatisticShapeFileHelper.NRCPStatisticFeatureClassRow>();
            }

            public void Dispose()
            {
                foreach (var a_RowInfo in Rows.Values)
                    a_RowInfo.Dispose();
                Rows.Clear();
            }
        }

        #endregion Types

        public static SpeciesParametersWindow Instance { get; private set; }

        private SavingParameters Parameters { get; set; }

        #region variables for working with ArcGIS

        /// <summary>
        /// Host object of the dockable window
        /// </summary>
        private object Hook { get; set; }

        private static readonly UIDClass WindowUid = new UIDClass { Value = ThisAddIn.IDs.SpeciesParametersWindow };

        #endregion variables for working with ArcGIS

        #region System

        private void MakeBindingFor(CalculationTypeEnum a_CalculationType)
        {
            if (nudLocal.DataBindings.Count > 0)
            {
                //nudLocal.DataBindings[0].WriteValue();
                nudLocal.DataBindings.Clear();
            }
            if (nudWide.DataBindings.Count > 0)
            {
                //nudWide.DataBindings[0].WriteValue();
                nudWide.DataBindings.Clear();
            }

            string a_Prefix = string.Empty;
            switch (a_CalculationType)
            {
                case CalculationTypeEnum.ByBiomesArea:
                case CalculationTypeEnum.ByBiomes:
                    a_Prefix = "ByBiomes";
                    nudWide.Increment = nudLocal.Increment = 1;
                    break;
                default:
                    a_Prefix = "ByArea";
                    nudWide.Increment = nudLocal.Increment = 1000;
                    break;
            }

            nudLocal.DataBindings.Add(
                "Value", Parameters, a_Prefix + "LowValue", false, DataSourceUpdateMode.OnPropertyChanged, 0);
            nudWide.DataBindings.Add(
                "Value", Parameters, a_Prefix + "HighValue", false, DataSourceUpdateMode.OnPropertyChanged, 0);
        }

        public SpeciesParametersWindow(object a_Hook)
        {
            InitializeComponent();
            this.Hook = a_Hook;
            try
            {
                nudLocal.Minimum = 0;
                nudLocal.Maximum = decimal.MaxValue;

                nudWide.Minimum = 0;
                nudWide.Maximum = decimal.MaxValue;

                cbCalculationType.Items.Clear();
                foreach (
                    CalculationTypeEnum a_CalculationType in
                        Enum.GetValues(typeof(CalculationTypeEnum))
                            .OfType<CalculationTypeEnum>()
                            .OrderBy(a_Type => (int)a_Type)) cbCalculationType.Items.Add(a_CalculationType.GetEnumDescription());

                nudWide_ValueChanged(null, null);
                Instance = this;

                Parameters = SavingParameters.Load();
                nudDistrCoeff.DataBindings.Add(
                    "Value", Parameters, "Coefficient", false, DataSourceUpdateMode.OnPropertyChanged, 0);
                cbCalculationType.DataBindings.Add(
                    "SelectedIndex", Parameters, "CalculationType", false, DataSourceUpdateMode.OnPropertyChanged, 0);
                tbIUCNCategoriesPath.DataBindings.Add(
                    "Text", Parameters, "IUCNCategoriesPath", false, DataSourceUpdateMode.OnPropertyChanged, 0);
                chbAddThematicLayers.DataBindings.Add(
                    "Checked", Parameters, "AddThematicLayers", false, DataSourceUpdateMode.OnPropertyChanged, 0);
                tbResultPath.DataBindings.Add(
                    "Text", Parameters, "ResultsPath", false, DataSourceUpdateMode.OnPropertyChanged, 0);
                chbOverwriteResult.DataBindings.Add(
                    "Checked", Parameters, "OverwriteResult", false, DataSourceUpdateMode.OnPropertyChanged, 0);

                this.cbCalculationType_SelectedIndexChanged(null, null);

                rbUseAllLayer.Checked = true;

                UpdateCheckBoxImage(tsbtFilterLayers, Parameters.LayersFiltered);

                InitializeData();

                ArcMap.Events.OpenDocument += EventsOnOpenOrNewDocument;
                ArcMap.Events.NewDocument += EventsOnOpenOrNewDocument;
            }
            catch (Exception a_Exception)
            {
                ExceptionDialog.Show(a_Exception);
            }
        }

        private void EventsOnOpenOrNewDocument()
        {
            if (Instance == null) return;
            if (!ArcMap.DockableWindowManager.GetDockableWindow(WindowUid).IsVisible()) return;
            this.InitializeData();
        }

        /// <summary>
        /// Implementation class of the dockable window add-in. It is responsible for
        /// creating and disposing the user interface class of the dockable window.
        /// </summary>
        public class AddinImpl : ESRI.ArcGIS.Desktop.AddIns.DockableWindow
        {
            private SpeciesParametersWindow m_windowUI;

            public AddinImpl()
            {
            }

            protected override IntPtr OnCreateChild()
            {
                m_windowUI = new SpeciesParametersWindow(this.Hook);
                return m_windowUI.Handle;
            }

            protected override void Dispose(bool a_Disposing)
            {
                if (m_windowUI != null)
                {
                    if (m_windowUI.Parameters != null) m_windowUI.Parameters.Save();
                    m_windowUI.Dispose(a_Disposing);
                    m_windowUI = null;
                }

                base.Dispose(a_Disposing);
            }

        }

        #endregion System

        private DateTime m_LastInitializeDataTime = new DateTime(0);

        public void InitializeData(bool a_ForceRefresh = false)
        {
            if (((DateTime.Now - m_LastInitializeDataTime).TotalSeconds <= 3) && !a_ForceRefresh) return;
            try
            {
                libSpeciesLayers.DataSource = null;
                libSpeciesLayers.DataSource =
                    Utils.GetFeatureLayersByGeometryAndPrefix(esriGeometryType.esriGeometryPolygon,
                                                              Parameters.LayersFiltered ? "s-" : string.Empty);
                libSpeciesLayers.DisplayMember = "DisplayName";

                cbBiomesLayer.DataSource = null;
                cbBiomesLayer.DataSource =
                    Utils.GetFeatureLayersByGeometryAndPrefix(esriGeometryType.esriGeometryPolygon,
                                                              Parameters.LayersFiltered ? "g-" : string.Empty);
                cbBiomesLayer.DisplayMember = "DisplayName";

                cbRegionsLayer.DataSource = null;
                cbRegionsLayer.DataSource =
                    Utils.GetFeatureLayersByGeometryAndPrefix(esriGeometryType.esriGeometryPolygon,
                                                              Parameters.LayersFiltered ? "g-" : string.Empty);
                cbRegionsLayer.DisplayMember = "DisplayName";

                cbFocalRegionLayer.DataSource = null;
                cbFocalRegionLayer.DataSource =
                    Utils.GetFeatureLayersByGeometryAndPrefix(esriGeometryType.esriGeometryPolygon,
                                                              Parameters.LayersFiltered ? "f-" : string.Empty);
                cbFocalRegionLayer.DisplayMember = "DisplayName";
            }
            catch (Exception a_Exception)
            {
                ExceptionDialog.Show(a_Exception);
            }
            m_LastInitializeDataTime = DateTime.Now;
        }

        #region UI

        private void nudWide_ValueChanged(object a_Sender, EventArgs a_EventArgs)
        {
            tbRegional.Text = string.Format("{0}-{1}", nudLocal.Value.ToString("N"), nudWide.Value.ToString("N"));
        }

        private void miRefresh_Click(object a_Sender, EventArgs a_EventArgs)
        {
            InitializeData();
        }

        private void RefreshFocalRegionValues()
        {
            libFocalRegionValues.DataSource = null;
            if (cbFocalRegionLayer.SelectedItem != null)
            {
                var a_FeatureLayer = Utils.GetLayerByName(((Utils.LayerDisplayInfo) cbFocalRegionLayer.SelectedItem).LayerName);
                libFocalRegionValues.DataSource = a_FeatureLayer == null
                                                      ? null
                                                      : Utils.GetNamedRows(a_FeatureLayer, cbFocRegionsNameField.Text);
                if (libFocalRegionValues.DataSource != null) libFocalRegionValues.DisplayMember = "DisplayValue";
            }
        }

        private void RefreshComboboxWithFields(Utils.LayerDisplayInfo a_LayerDisplayInfo, ComboBox a_ComboBox)
        {
            a_ComboBox.BeginUpdate();
            a_ComboBox.Items.Clear();
            try
            {
                if (a_LayerDisplayInfo == null) return;
                var a_Layer = Utils.GetLayerByName(a_LayerDisplayInfo.LayerName);
                if (a_Layer == null) return;
                var a_Fields = a_Layer.GetFields();
                var a_SelectedFieldIndex = -1;
                for (var a_I = 0; a_I < a_Fields.Count; a_I++)
                {
                    a_ComboBox.Items.Add(a_Fields[a_I]);
                    if (a_SelectedFieldIndex == -1) if (Utils.IsDefaultNameField(a_Fields[a_I])) a_SelectedFieldIndex = a_I;
                }
                if (a_SelectedFieldIndex == -1) a_SelectedFieldIndex = 0;
                a_ComboBox.SelectedIndex = a_SelectedFieldIndex;
            }
            catch (Exception a_Exception)
            {
                ExceptionDialog.Show(a_Exception);
            }
            finally
            {
                a_ComboBox.EndUpdate();
            }
        }

        private void cbBiomesLayer_SelectedIndexChanged(object a_Sender, EventArgs a_EventArgs)
        {
            RefreshComboboxWithFields(cbBiomesLayer.SelectedItem as Utils.LayerDisplayInfo, cbBiomesNameField);
        }

        private void cbRegionsLayer_SelectedIndexChanged(object a_Sender, EventArgs a_EventArgs)
        {
            RefreshComboboxWithFields(cbRegionsLayer.SelectedItem as Utils.LayerDisplayInfo, cbRegionsNameField);
        }

        private void cbFocalRegionLayer_SelectedIndexChanged(object a_Sender, EventArgs a_EventArgs)
        {
            RefreshComboboxWithFields(cbFocalRegionLayer.SelectedItem as Utils.LayerDisplayInfo, cbFocRegionsNameField);
        }

        private void cbRefRegionsNameField_SelectedIndexChanged(object a_Sender, EventArgs a_EventArgs)
        {
            this.RefreshFocalRegionValues();
        }


        #endregion UI

        #region Calculation

        private LayerInfo GetLayerInfo(
            string a_LayerName,
            string a_LayerAlias,
            string a_FieldName, IEnumerable<Utils.RowInfo> a_SelectedValues)
        {
            var a_LayerInfo = new LayerInfo { LayerName = a_LayerName, LayerAlias = a_LayerName};
            if (!string.IsNullOrEmpty(a_LayerAlias))
                a_LayerInfo.LayerAlias = a_LayerAlias;
            a_LayerInfo.Layer = Utils.GetLayerByName(a_LayerInfo.LayerName);
            if (a_LayerInfo.Layer != null)
            {
                a_LayerInfo.NameFieldName = a_FieldName;
                a_LayerInfo.NameFieldIndex = ((ILayerFields)a_LayerInfo.Layer).FindField(a_LayerInfo.NameFieldName);
            }
            if (a_SelectedValues != null) a_LayerInfo.SelectedValues = a_SelectedValues.ToList();
            return a_LayerInfo;
        }

        private Dictionary<LayerTypesEnum, LayerInfo> PrepareParameters()
        {
            var a_LayerInfos = new Dictionary<LayerTypesEnum, LayerInfo>();

            WriteDelimiters('*');
            WritelnInLog(Color.LightSeaGreen, "Parameters");
            WriteDelimiters('-');

            var a_MultipleLayerInfo = new MultipleLayerInfo();
            if (libSpeciesLayers.SelectedItems.Count > 0)
            {
                foreach (var a_SpeciesLayer in libSpeciesLayers.SelectedItems.Cast<Utils.LayerDisplayInfo>())
                    a_MultipleLayerInfo.LayerInfos.Add(this.GetLayerInfo(a_SpeciesLayer.LayerName,
                                                                         a_SpeciesLayer.DisplayName, string.Empty, null));
                a_MultipleLayerInfo.ActiveLayerInfo = a_MultipleLayerInfo.LayerInfos.FirstOrDefault();
            }
            a_LayerInfos.Add(LayerTypesEnum.Species, a_MultipleLayerInfo);
            a_LayerInfos.Add(LayerTypesEnum.Biomes,
                             this.GetLayerInfo(((Utils.LayerDisplayInfo) cbBiomesLayer.SelectedItem).LayerName,
                                               ((Utils.LayerDisplayInfo) cbBiomesLayer.SelectedItem).DisplayName,
                                               cbBiomesNameField.Text, null));
            a_LayerInfos.Add(LayerTypesEnum.Regions,
                             this.GetLayerInfo(((Utils.LayerDisplayInfo) cbRegionsLayer.SelectedItem).LayerName,
                                               ((Utils.LayerDisplayInfo) cbRegionsLayer.SelectedItem).DisplayName,
                                               cbRegionsNameField.Text, null));
            a_LayerInfos.Add(
                LayerTypesEnum.FocalRegions,
                this.GetLayerInfo(
                    ((Utils.LayerDisplayInfo) cbFocalRegionLayer.SelectedItem).LayerName,
                    ((Utils.LayerDisplayInfo) cbFocalRegionLayer.SelectedItem).DisplayName,
                    cbFocRegionsNameField.Text,
                    rbUseAllLayer.Checked ? null : libFocalRegionValues.SelectedItems.OfType<Utils.RowInfo>()));

            return a_LayerInfos;
        }


        private bool CheckInputParameters(Dictionary<LayerTypesEnum, LayerInfo> a_Parameters)
        {
            var a_HaveErrors = false;
            foreach (var a_LayerInfoPair in a_Parameters)
            {
                string a_ErrorMessage = string.Empty;
                switch (a_LayerInfoPair.Key)
                {
                    case LayerTypesEnum.Species:
                        this.WriteInLog(Color.Blue, "{0}: ", lbSpeciesLayers.Text);
                        this.WritelnInLog(
                            Color.Black,
                            string.Join(
                                ",",
                                ((MultipleLayerInfo)a_Parameters[LayerTypesEnum.Species]).LayerInfos.Select(
                                    a_Info => a_Info.LayerName).ToArray()));
                        if (!a_LayerInfoPair.Value.Validate(out a_ErrorMessage, false, false))
                        {
                            this.WritelnInLog(Color.Red, a_ErrorMessage);
                            a_HaveErrors = true;
                        }
                        break;
                    case LayerTypesEnum.Biomes:
                        if (this.Parameters.CalculationType.IsOneOf((int)CalculationTypeEnum.ByBiomes, (int)CalculationTypeEnum.ByBiomesArea))
                        {
                            this.WriteInLog(Color.Blue, "{0}: ", lbBiomesLayer.Text);
                            this.WritelnInLog(Color.Black, a_LayerInfoPair.Value.LayerName);
                            if (!a_LayerInfoPair.Value.Validate(out a_ErrorMessage, true, false))
                            {
                                this.WritelnInLog(Color.Red, a_ErrorMessage);
                                a_HaveErrors = true;
                            }
                        }
                        break;
                    case LayerTypesEnum.Regions:
                        this.WriteInLog(Color.Blue, "{0}: ", lbRegionsLayer.Text);
                        this.WritelnInLog(Color.Black, a_LayerInfoPair.Value.LayerName);
                        if (!a_LayerInfoPair.Value.Validate(out a_ErrorMessage, false, false))
                        {
                            this.WritelnInLog(Color.Red, a_ErrorMessage);
                            a_HaveErrors = true;
                        }
                        break;
                    case LayerTypesEnum.FocalRegions:
                        this.WriteInLog(Color.Blue, "{0}: ", lbFocalRegionLayer.Text);
                        this.WritelnInLog(Color.Black, a_LayerInfoPair.Value.LayerName);
                        if (!a_LayerInfoPair.Value.Validate(out a_ErrorMessage, true, true))
                        {
                            this.WritelnInLog(Color.Red, a_ErrorMessage);
                            a_HaveErrors = true;
                        }
                        else
                        {
                            if (a_LayerInfoPair.Value.SelectedValues != null)
                            {
                                WritelnInLog(Color.Blue, "Selected values:");
                                a_LayerInfoPair.Value.SelectedValues.ForEach(
                                    a_Row => this.WritelnInLog(Color.Black, a_Row.DisplayValue));
                            }
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            return !a_HaveErrors;
        }


        private void btAnalyze_Click(object a_Sender, EventArgs a_EventArgs)
        {
            var a_Parameters = this.PrepareParameters();
            if (!this.CheckInputParameters(a_Parameters)) return;

            tcParameters.SelectedTab = tpResults;

            ResultInfo a_BaseResultInfo = null;
            if (this.Parameters.CalculationType == (int) CalculationTypeEnum.ByArea)
            {
                a_BaseResultInfo = new ResultInfo(CalculationTypeEnum.ByArea,
                    new Ranges(Convert.ToDouble(this.Parameters.ByAreaLowValue),
                        Convert.ToDouble(this.Parameters.ByAreaHighValue)),
                    Convert.ToDouble(this.Parameters.Coefficient));
            }
            else
            {
                a_BaseResultInfo = new ResultInfo((CalculationTypeEnum) this.Parameters.CalculationType,
                    new Ranges(Convert.ToDouble(this.Parameters.ByBiomesLowValue),
                        Convert.ToDouble(this.Parameters.ByBiomesHighValue)),
                    Convert.ToDouble(this.Parameters.Coefficient));
            }


            a_BaseResultInfo.BiomesTableName = a_BaseResultInfo.CalculationType == CalculationTypeEnum.ByArea
                                                    ? string.Empty
                                                    : a_Parameters[LayerTypesEnum.Biomes].LayerName;
            a_BaseResultInfo.RegionTableName = a_Parameters[LayerTypesEnum.Regions].LayerName;
            a_BaseResultInfo.FocalRegionTableName = a_Parameters[LayerTypesEnum.FocalRegions].LayerName;

            //Reading other parameters
            var a_UnitsName = string.Empty;

            WriteInLog(Color.Blue, "Calculation type: ");
            WritelnInLog(Color.Black, a_BaseResultInfo.CalculationType.GetEnumDescription());

            WriteInLog(Color.Blue, "Global distribution. Max. Local value: ");
            WritelnInLog(
                Color.Black,
                "{0:G}",
                Math.Round(a_BaseResultInfo.GlobalDistributionRanges.MaxLocalValue, 2, MidpointRounding.AwayFromZero));

            WriteInLog(Color.Blue, "Global distribution. Min. Wide value: ");
            WritelnInLog(
                Color.Black,
                "{0:G}",
                Math.Round(a_BaseResultInfo.GlobalDistributionRanges.MinWideValue, 2, MidpointRounding.AwayFromZero));

            WriteInLog(Color.Blue, "Distribution Coefficient:");
            WritelnInLog(Color.Black, a_BaseResultInfo.DistributionCoefficient.ToString());

            WriteInLog(Color.Blue, "{0}: ", lbIUCNCategoriesPath.Text);
            WritelnInLog(Color.Black, this.Parameters.IUCNCategoriesPath);

            Enabled = false;
            var a_QueryFilter = new QueryFilterClass();

            ResultsShapeFileHelper a_ResultsHelperClass = null;
            if (!string.IsNullOrEmpty(this.Parameters.ResultsPath)) a_ResultsHelperClass = new ResultsShapeFileHelper(this.Parameters.ResultsPath);
            try
            {
                using (var a_ProgressDialog = new ProgressDialog(false, 0, 0, ThisAddIn.Name, "Calculating data"))
                {
                    #region General

                    #region reading IUCN

                    a_ProgressDialog.Message = "Reading IUCN categories";
                    WritelnInLog(Color.Blue, "Reading IUCN categories:");

                    var a_IUCNDictionary = ReadIUCNSpeciesCategories(this.Parameters.IUCNCategoriesPath);
                    if (a_IUCNDictionary == null)
                    {
                        WritelnInLog(Color.Black, "IUCN categories file not specified");
                        return;
                    }
                    if (a_IUCNDictionary.Count == 0)
                    {
                        WritelnInLog(Color.Black, "IUCN categories list is empty");
                        return;
                    }
                    WritelnInLog(Color.Black, "Done");

                    #endregion reading IUCN

                    #endregion General

                    #region Specific

                    WriteDelimiters('-');
                    WritelnInLog(Color.LightSeaGreen, "Calculated data");
                    WriteDelimiters('-');

                    var a_GeometriesList = new Dictionary<int, IGeometry>();
                    var a_Results = new List<ResultInfo>();
                    var a_SpeciesLayers = (MultipleLayerInfo)a_Parameters[LayerTypesEnum.Species];
                    foreach (var a_CurrentSpeciesLayerInfo in a_SpeciesLayers.LayerInfos)
                    {
                        a_SpeciesLayers.ActiveLayerInfo = a_CurrentSpeciesLayerInfo;
                        a_ProgressDialog.Message = "Processing data for " + a_SpeciesLayers.LayerName;
                        WriteDelimiters('#');
                        WritelnInLog(Color.Blue, "Processing data for " + a_SpeciesLayers.LayerName);
                        try
                        {
                            var a_ResultInfo = new ResultInfo(a_BaseResultInfo);
                            a_ResultInfo.SpeciesTableName = a_SpeciesLayers.LayerName;
                            a_Results.Add(a_ResultInfo);

                            #region Determining category

                            a_ProgressDialog.Description = "Determining category";

                            var a_SpeciesCategoryName = string.Empty;
                            if (a_IUCNDictionary.ContainsKey(a_SpeciesLayers.LayerAlias)) a_SpeciesCategoryName = a_SpeciesLayers.LayerAlias;
                            else if (a_IUCNDictionary.ContainsKey(a_SpeciesLayers.LayerName)) a_SpeciesCategoryName = a_SpeciesLayers.LayerName;
                            else
                            {
                                using (var a_CategorySelectionForm = new CategorySelectionForm())
                                {
                                    var a_Ds = a_IUCNDictionary.Keys.ToList();
                                    a_Ds.Sort();
                                    a_CategorySelectionForm.cbCategories.DataSource = a_Ds;
                                    a_CategorySelectionForm.cbCategories.SelectedIndex = 0;
                                    if (a_CategorySelectionForm.ShowDialog() == DialogResult.Cancel)
                                    {
                                        WritelnInLog(Color.Black, "Canceled by user");
                                        return;
                                    }
                                    a_SpeciesCategoryName = a_CategorySelectionForm.cbCategories.Text;
                                }
                            }
                            a_ResultInfo.Category =
                                Utils.GetEnumValueByCode<IUCNCategoryEnum>(a_IUCNDictionary[a_SpeciesCategoryName]);
                            WriteInLog(Color.Blue, "IUCN category: ");
                            WritelnInLog(Color.Black, a_ResultInfo.Category.GetEnumDescription());


                            #endregion Determining category

                            #region reading species data

                            a_ProgressDialog.Description = "Calculating species data";

                            //species
                            a_Parameters[LayerTypesEnum.Species].CalculateArea(null, true, null);
                            a_ResultInfo.SpeciesArea = a_Parameters[LayerTypesEnum.Species].CalculatedArea;
                            WriteInLog(Color.Blue, "Species area: ");
                            WritelnInLog(Color.Black, "{0} {1}", a_ResultInfo.SpeciesArea, a_UnitsName);

                            #endregion reading species data

                            #region Reading biomes data

                            BiomesInfoEx a_BiomesInfoEx = null;

                            if (a_ResultInfo.CalculationType.IsOneOf(CalculationTypeEnum.ByBiomes, CalculationTypeEnum.ByBiomesArea))
                            {
                                a_ProgressDialog.Description = "Calculating biomes data";

                                a_BiomesInfoEx = new BiomesInfoEx(a_Parameters[LayerTypesEnum.Biomes]);

                                a_BiomesInfoEx.CalculateIntersectionsArea(
                                    LayerTypesEnum.Species,
                                    a_Parameters[LayerTypesEnum.Species].CalculatedUnitedGeometry);

                                a_ResultInfo.BiomesCount = a_BiomesInfoEx.IntersectedInfos[LayerTypesEnum.Species].Ids.Count;

                                WriteInLog(Color.Blue, "Biomes count: ");
                                WritelnInLog(Color.Black, "{0}", a_ResultInfo.BiomesCount);
                            }

                            #endregion Reading biomes data

                            #region reading reference region data

                            a_ProgressDialog.Description = "Calculating reference region data";

                            a_Parameters[LayerTypesEnum.Regions].CalculateArea(null, true, null);

                            a_ResultInfo.DistributionInReferenceArea =
                                a_Parameters[LayerTypesEnum.Regions].CalculatedUnitedGeometry.GetAreaOfIntersection(
                                    a_Parameters[LayerTypesEnum.Species].CalculatedUnitedGeometry);
                            if (a_ResultInfo.CalculationType == CalculationTypeEnum.ByBiomes)
                            {
                                if (a_BiomesInfoEx == null) throw new NullReferenceException("a_BiomesInfoEx");
                                a_ResultInfo.TotalReferenceArea =
                                    a_BiomesInfoEx.CalculateIntersectionsArea(
                                        LayerTypesEnum.Regions,
                                        a_Parameters[LayerTypesEnum.Regions].CalculatedUnitedGeometry, LayerTypesEnum.Species);
                            }
                            else
                            {
                                a_ResultInfo.TotalReferenceArea = a_Parameters[LayerTypesEnum.Regions].CalculatedArea;
                            }

                            a_ResultInfo.CalculateGeneralProperties();

                            WriteInLog(Color.Blue, "Total reference area: ");
                            WritelnInLog(Color.Black, "{0} {1}", a_ResultInfo.TotalReferenceArea, a_UnitsName);

                            WriteInLog(Color.Blue, "Distribution in reference area: ");
                            WritelnInLog(Color.Black, "{0} {1}", a_ResultInfo.DistributionInReferenceArea, a_UnitsName);

                            WriteInLog(Color.Blue, "Global distribution: ");
                            WritelnInLog(Color.Black, a_ResultInfo.GlobalDistribution.GetEnumDescription());

                            WriteInLog(Color.Blue, "DPexp: ");
                            WritelnInLog(Color.Black, a_ResultInfo.ExpectedDistributionProbability.ToString());

                            #endregion reading reference region data

                            #region reading focal region data

                            a_ProgressDialog.Description = "Calculating focal region data";

                            HashSet<int> ObjectIdSet = null;
                            if (a_Parameters[LayerTypesEnum.FocalRegions].SelectedValues != null)
                            {
                                ObjectIdSet =
                                    new HashSet<int>(
                                        a_Parameters[LayerTypesEnum.FocalRegions].SelectedValues.Select(
                                            a_RowInfo => a_RowInfo.ObjectId));
                            }
                            var a_FocalRegionResultInfos = new List<ResultInfo>();
                            var a_FeatureCursor = a_Parameters[LayerTypesEnum.FocalRegions].Layer.Search(null, true);
                            try
                            {
                                var a_SpeciesGeometry = a_Parameters[LayerTypesEnum.Species].CalculatedUnitedGeometry;
                                var a_FocalRegionsNameFieldName =
                                    a_Parameters[LayerTypesEnum.FocalRegions].NameFieldName;
                                a_FeatureCursor.DoForCursor(
                                    a_Feature =>
                                        {
                                            var a_ObjectId = a_Feature.OID;
                                            if (ObjectIdSet != null && !ObjectIdSet.Contains(a_ObjectId)) return;
                                            var a_FocalRegionResultInfo = new ResultInfo(a_ResultInfo);
                                            a_FocalRegionResultInfo.FocalRegionObjectId = a_ObjectId;
                                            a_FocalRegionResultInfo.FocalRegionName =
                                                a_Feature.GetFieldValue(a_FocalRegionsNameFieldName, "[Unknown]");

                                            if (a_FocalRegionResultInfo.CalculationType == CalculationTypeEnum.ByBiomes)
                                            {
                                                if (a_BiomesInfoEx == null) throw new NullReferenceException("a_BiomesInfoEx");
                                                a_FocalRegionResultInfo.TotalFocalArea =
                                                    a_BiomesInfoEx.CalculateIntersectionsArea(
                                                        LayerTypesEnum.FocalRegions, a_Feature.Shape,
                                                        LayerTypesEnum.Species);
                                            }
                                            else
                                            {
                                                a_FocalRegionResultInfo.TotalFocalArea = a_Feature.Shape.GetArea();
                                            }
                                            a_FocalRegionResultInfo.DistributionInFocalArea =
                                                a_Feature.Shape.GetAreaOfIntersection(a_SpeciesGeometry);
                                            a_FocalRegionResultInfo.Calculate();
                                            a_FocalRegionResultInfos.Add(a_FocalRegionResultInfo);
                                            a_GeometriesList.Add(
                                                a_FocalRegionResultInfo.FocalRegionObjectId, a_Feature.ShapeCopy);
                                        });
                            }
                            finally
                            {
                                Marshal.ReleaseComObject(a_FeatureCursor);
                            }

                            #endregion reading focal region data

                            #region displaying calculated data

                            var a_ProcessedObjectIds = string.Empty;
                            foreach (var a_FocalRegionResultInfo in a_FocalRegionResultInfos)
                            {
                                if (!string.IsNullOrEmpty(a_ProcessedObjectIds)) a_ProcessedObjectIds += ",";
                                a_ProcessedObjectIds += "'" + a_FocalRegionResultInfo.FocalRegionObjectId.ToString()
                                                        + "'";

                                WriteInLog(Color.BlueViolet, "Focal region: ");
                                WritelnInLog(Color.Black, "{0}", a_FocalRegionResultInfo.FocalRegionName);

                                WriteInLog(Color.Blue, "Total focal area: ");
                                WritelnInLog(
                                    Color.Black, "{0} {1}", a_FocalRegionResultInfo.TotalFocalArea, a_UnitsName);

                                WriteInLog(Color.Blue, "Distribution in focal area: ");
                                WritelnInLog(
                                    Color.Black, "{0} {1}", a_FocalRegionResultInfo.DistributionInFocalArea, a_UnitsName);

                                WriteInLog(Color.Blue, "DPobs: ");
                                WritelnInLog(
                                    Color.Black, "{0}", a_FocalRegionResultInfo.ObservedDistributionProbability);

                                WriteInLog(Color.Blue, "Regional distribution: ");
                                WritelnInLog(
                                    Color.Black, a_FocalRegionResultInfo.RegionalDistribution.GetEnumDescription());

                                WriteInLog(Color.Blue, "Priority: ");
                                WritelnInLog(Color.Black, a_FocalRegionResultInfo.Priority.GetEnumDescription());

                                WriteInLog(Color.Blue, "Class: ");
                                WritelnInLog(Color.Black, a_FocalRegionResultInfo.Class.GetEnumDescription());

                                WriteInLog(Color.Blue, "Score: ");
                                WritelnInLog(Color.Black, a_FocalRegionResultInfo.Score.ToString());
                            }

                            #endregion displaying calculated data

                            #region saving results

                            if (a_ResultsHelperClass != null)
                            {
                                a_ResultsHelperClass.InitializeWriteSession(this.Parameters.OverwriteResult);
                                try
                                {
                                    foreach (var a_FocalRegionResultInfo in a_FocalRegionResultInfos)
                                        a_ResultsHelperClass.Write(a_FocalRegionResultInfo, a_GeometriesList[a_FocalRegionResultInfo.FocalRegionObjectId], a_ProcessedObjectIds);
                                }
                                finally
                                {
                                    a_ResultsHelperClass.FinalizeWriteSession();
                                }
                            }

                            #endregion saving results
                        }
                        finally
                        {
                            foreach (var a_Geometry in a_GeometriesList.Values) Marshal.ReleaseComObject(a_Geometry);
                            a_GeometriesList.Clear();
                        }
                    }

                    #endregion Specific

                    if (this.Parameters.AddThematicLayers && File.Exists(this.Parameters.ResultsPath)) AddLayersAfterCalculation(a_Parameters, a_Results, this.Parameters.ResultsPath);
                }
            }
            catch (Exception a_Exception)
            {
                ExceptionDialog.Show(a_Exception);
            }
            finally
            {
                foreach (LayerInfo a_LayerInfo in a_Parameters.Values) a_LayerInfo.Dispose();
                a_Parameters.Clear();

                Marshal.ReleaseComObject(a_QueryFilter);
                WriteDelimiters('*');
                Enabled = true;
            }
        }

        #endregion Calculation

        #region Log

        private void WriteDelimiters(char a_Symbol)
        {
            rtbResults.AppendText(a_Symbol.ToString().PadRight(60, a_Symbol) + Environment.NewLine);
        }

        private void WriteInLog(Color a_Color, string a_Text, params object[] a_Parameters)
        {
            rtbResults.SelectionStart = rtbResults.TextLength;
            rtbResults.SelectionLength = 0;
            rtbResults.SelectionColor = a_Color;
            rtbResults.AppendText(string.Format(a_Text, a_Parameters));
            rtbResults.SelectionColor = rtbResults.ForeColor;
        }

        private void WritelnInLog(Color a_Color, string a_Text, params object[] a_Parameters)
        {
            WriteInLog(a_Color, a_Text + Environment.NewLine, a_Parameters);
        }

        #endregion Log

        #region Working with layers

        private static ISymbol CreateFillSimbol(Color a_FillColor, esriSimpleFillStyle a_FillStyle)
        {
            return new SimpleFillSymbolClass
            {
                Outline =
                    new SimpleLineSymbolClass
                    {
                        Color = Color.Black.ToEsriColor(),
                        Style =
                            esriSimpleLineStyle.esriSLSSolid,
                        Width = 1
                    },
                Color = a_FillColor.ToEsriColor(),
                Style = a_FillStyle
            };
        }

        private static IGroupLayer CreateGroupLayer(string a_LayerName, params ILayer[] a_SubLayers)
        {
            var a_GroupLayer = new GroupLayerClass { Name = a_LayerName };
            if (a_SubLayers != null && a_SubLayers.Length > 0)
            {
                foreach (var a_SubLayer in a_SubLayers) a_GroupLayer.Add(a_SubLayer);
            }
            return a_GroupLayer;
        }

        private static ILayer CreateFeatureLayer(
            string a_LayerName,
            IFeatureClass a_FeatureClass,
            string a_DefinitionExpression,
            string a_FieldName,
            IEnumerable<LayerRendererInfo> a_LayerRendererInfos,
            bool a_Visible)
        {
            var a_Layer = new FeatureLayerClass
            {
                Name = a_LayerName,
                FeatureClass = a_FeatureClass,
                DefinitionExpression = a_DefinitionExpression,
                Transparency = 30,
                Visible = a_Visible
            };


            var a_UniqueRenderer = new UniqueValueRendererClass();
            a_UniqueRenderer.FieldCount = 1;
            a_UniqueRenderer.Field[0] = a_FieldName;
            a_UniqueRenderer.DefaultLabel = "[Unknown]";
            a_UniqueRenderer.DefaultSymbol = CreateFillSimbol(Color.GhostWhite, esriSimpleFillStyle.esriSFSCross);
            a_UniqueRenderer.UseDefaultSymbol = true;
            foreach (var a_LayerRendererInfo in a_LayerRendererInfos)
            {
                a_UniqueRenderer.AddValue(
                    a_LayerRendererInfo.FieldValue,
                    string.Empty,
                    CreateFillSimbol(a_LayerRendererInfo.FillColor, a_LayerRendererInfo.FillStyle));
            }

            a_Layer.Renderer = a_UniqueRenderer;
            return a_Layer;
        }

        private static ILayer CreateFeatureLayer(
            string a_LayerName,
            IFeatureClass a_FeatureClass,
            string a_DefinitionExpression,
            string a_FillStyleLabel,
            Color a_FillColor,
            esriSimpleFillStyle a_FillStyle,
            bool a_Visible)
        {
            var a_Layer = new FeatureLayerClass
            {
                Name = a_LayerName,
                FeatureClass = a_FeatureClass,
                DefinitionExpression = a_DefinitionExpression,
                Transparency = 30,
                Renderer =
                    new SimpleRendererClass
                    {
                        Label = a_FillStyleLabel,
                        Symbol =
                            CreateFillSimbol(a_FillColor, a_FillStyle)
                    },
                Visible = a_Visible
            };
            return a_Layer;
        }

        private static ILayer CreateGraduatedFeatureLayer(
                    string a_LayerName,
                    IFeatureClass a_FeatureClass,
                    string a_FieldName,
                    bool a_IntField,
                    bool a_Visible)
        {

            var a_TableHistogram = new BasicTableHistogramClass();
            a_TableHistogram.Field = a_FieldName;
            a_TableHistogram.Table = (ITable)a_FeatureClass;
            object a_Values;
            object a_Frequencies;
            a_TableHistogram.GetHistogram(out a_Values, out a_Frequencies);

            var a_ClassifyGEN = new QuantileClass();
            a_ClassifyGEN.Classify(a_Values, a_Frequencies, 5);

            var a_ClassBreaks = a_ClassifyGEN.ClassBreaks;
            var a_ClassBreaksAsArray = a_ClassBreaks as double[];

            IFeatureRenderer a_FeatureRenderer = null;
            if (a_ClassBreaksAsArray != null)
            {
                var a_BreaksCount = a_ClassBreaksAsArray.Length - 1;
                var a_ColorRamp = new AlgorithmicColorRampClass()
                                  {
                                      Algorithm =
                                          esriColorRampAlgorithm.esriCIELabAlgorithm
                                  };
                a_ColorRamp.FromColor = Color.FromArgb(0xFFFF80).ToEsriColor();
                a_ColorRamp.ToColor = Color.FromArgb(0x6B0000).ToEsriColor();
                a_ColorRamp.Size = Math.Max(a_BreaksCount, 2);
                bool a_Ok;
                a_ColorRamp.CreateRamp(out a_Ok);
                var a_EnumColors = a_ColorRamp.Colors;
                a_EnumColors.Reset();

                var a_Renderer = new ClassBreaksRendererClass() { Field = a_FieldName, ColorRamp = "Custom"};
                a_Renderer.BreakCount = a_BreaksCount;
                a_Renderer.ExclusionClause = string.Empty;
                a_Renderer.ExclusionLabel = string.Empty;
                a_Renderer.ExclusionDescription = string.Empty;
                for (int a_I = 0; a_I < a_BreaksCount; a_I++)
                {
                    a_Renderer.LowBreak[a_I] = a_ClassBreaksAsArray[a_I];
                    a_Renderer.Break[a_I] = a_ClassBreaksAsArray[a_I + 1];
                    var a_LowBreak = a_Renderer.LowBreak[a_I];
                    if (a_IntField && a_I != 0) a_LowBreak++;
                    if (a_LowBreak == a_Renderer.Break[a_I]) a_Renderer.Label[a_I] = a_LowBreak.ToString();
                    else a_Renderer.Label[a_I] = string.Format("{0} - {1}", a_LowBreak, a_Renderer.Break[a_I]);
                    a_Renderer.Symbol[a_I] = new SimpleFillSymbolClass
                                             {
                                                 Outline =
                                                     new SimpleLineSymbolClass
                                                     {
                                                         Color =
                                                             Color
                                                             .Black
                                                             .ToEsriColor
                                                             (),
                                                         Style =
                                                             esriSimpleLineStyle
                                                             .esriSLSSolid,
                                                         Width = 1
                                                     },
                                                 Color = a_EnumColors.Next(),
                                                 Style = esriSimpleFillStyle.esriSFSSolid
                                             };
                }

                a_FeatureRenderer = a_Renderer;
            }
            else
            {
                a_FeatureRenderer = new SimpleRendererClass
                                    {
                                        Label = a_FieldName,
                                        Symbol = CreateFillSimbol(Color.Lime, esriSimpleFillStyle.esriSFSHollow)
                                    };
            }

            var a_Layer = new FeatureLayerClass
                          {
                              Name = a_LayerName,
                              FeatureClass = a_FeatureClass,
                              Transparency = 30,
                              Renderer = a_FeatureRenderer,
                              Visible = a_Visible
                          };
            return a_Layer;
        }

        #endregion Working with layers

        #region working with results

        private static string ConstructLayerName(
            string a_SpeciesName, string a_RegionName, string a_FocalRegionsTableName, string a_CalculationType)
        {
            return string.Format(
                "{0} in {1} against {2}({3})", a_SpeciesName, a_FocalRegionsTableName, a_RegionName, a_CalculationType);
        }

        private void AddLayersFromResults()
        {
            if (!File.Exists(this.Parameters.ResultsPath))
            {
                MessageBox.Show(
                    @"Results shapefile does not exists", ThisAddIn.Name, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using (var a_Helper = new ResultsShapeFileHelper(this.Parameters.ResultsPath))
                {
                    a_Helper.OpenOrCreateResultsShapeFile();
                    var a_AllRows = a_Helper.ReadData(string.Empty, false);
                    var a_Groups =
                        a_AllRows.GroupBy(
                            a_Row =>
                            ConstructLayerName(
                                a_Row.SpeciesName,
                                a_Row.RegionTableName,
                                a_Row.FocalRegionTableName,
                                a_Row.CalculationType))
                              .ToDictionary(a_Grouping => a_Grouping.Key, a_Grouping => a_Grouping);
                    var a_SelectedGroups = SelectionForm.GetSelectedGroups(a_Groups.Keys.ToList());
                    if (a_SelectedGroups != null && a_SelectedGroups.Count > 0)
                    {
                        var a_ResultsFeatureClass = a_Helper.OpenFeatureClassWithoutDisposing();
                        //1. Buiding layers infos
                        foreach (var a_SelectedGroup in a_SelectedGroups)
                        {
                            var a_FirstRow = a_Groups[a_SelectedGroup].First();
                            var a_SpeciesLayer = Utils.GetLayerByName(a_FirstRow.SpeciesName);
                            if (a_SpeciesLayer == null)
                                MessageBox.Show(
                                    string.Format("Species layer '{0}' not found", a_FirstRow.SpeciesName),
                                    ThisAddIn.Name,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                            AddLayer(
                                a_SpeciesLayer != null ? a_SpeciesLayer.FeatureClass : null,
                                a_ResultsFeatureClass,
                                a_FirstRow.ToResultInfo());
                        }
                    }
                }
            }
            catch (Exception a_Exception)
            {
                ExceptionDialog.Show(a_Exception);
            }
        }

        private static void AddLayer(IFeatureClass a_SpeciesFeatureClass, IFeatureClass a_ResultsFeatureClass, ResultInfo a_ResultInfo)
        {
            var a_NewLayerName = ConstructLayerName(
                a_ResultInfo.SpeciesTableName,
                a_ResultInfo.RegionTableName,
                a_ResultInfo.FocalRegionTableName,
                a_ResultInfo.CalculationType.GetEnumDescription());

            var a_BasicWhereClause = ResultsShapeFileHelper.GetBasicWhereClause(a_ResultInfo);

            ILayer a_Layer = null;
            a_Layer = Utils.GetFirstLayerByName<IGroupLayer>(a_NewLayerName);
            if (a_Layer != null)
            {
                ArcMap.Document.FocusMap.DeleteLayer(a_Layer);
                a_Layer = null;
            }
            if (a_Layer == null)
            {
                var a_SubLayers = new List<ILayer>();
                if (a_SpeciesFeatureClass != null)
                    a_SubLayers.Add(
                        CreateFeatureLayer(
                            string.Format("IUCN Category - {0}", a_ResultInfo.SpeciesTableName),
                            a_SpeciesFeatureClass,
                            string.Empty,
                            a_ResultInfo.Category.GetEnumDescription(),
                            a_ResultInfo.Category.GetEnumColor(),
                            esriSimpleFillStyle.esriSFSSolid,
                            false));
                if (a_ResultsFeatureClass != null)
                {
                    a_SubLayers.Add(
                        CreateFeatureLayer(
                            "National Responsibility",
                            a_ResultsFeatureClass,
                            a_BasicWhereClause,
                            ResultsShapeFileHelper.fnPriority,
                            LayerRendererInfo.LayerRendererInfoForEnum<PriorityEnum>(),
                            false));
                    a_SubLayers.Add(
                        CreateFeatureLayer(
                            "Conservation Priority",
                            a_ResultsFeatureClass,
                            a_BasicWhereClause,
                            ResultsShapeFileHelper.fnClass,
                            LayerRendererInfo.LayerRendererInfoForEnum<ClassEnum>(),
                            true));
                }

                a_Layer = CreateGroupLayer(a_NewLayerName, a_SubLayers.ToArray());
                ArcMap.Document.AddLayer(a_Layer);
            }
        }

        private static void AddLayersAfterCalculation(
            Dictionary<LayerTypesEnum, LayerInfo> a_Parameters,
            List<ResultInfo> a_Results,
            string a_ResultsShapeFilePath)
        {
            var a_SpeciesLayerInfo = ((MultipleLayerInfo)a_Parameters[LayerTypesEnum.Species]);
            var a_Species = a_SpeciesLayerInfo.LayerInfos.Select(a_Info => a_Info.LayerName).ToList();
            if (a_Species.Count > 1) a_Species = SelectionForm.GetSelectedLayers(a_Species);
            if (a_Species != null && a_Species.Count > 0)
                using (var a_ResultsShapeFileHelper = new ResultsShapeFileHelper(a_ResultsShapeFilePath))
                {
                    var a_ResultsFeatureClass = a_ResultsShapeFileHelper.OpenFeatureClassWithoutDisposing();
                    for (var a_I = 0; a_I < a_SpeciesLayerInfo.LayerInfos.Count; a_I++)
                    {
                        if (a_Species.Contains(a_SpeciesLayerInfo.LayerInfos[a_I].LayerName))
                        {
                            AddLayer(a_SpeciesLayerInfo.LayerInfos[a_I].Layer.FeatureClass,
                                a_ResultsFeatureClass,
                                a_Results[a_I]);
                        }
                    }
                }
        }

        private void GatherStatistic()
        {
            if (string.IsNullOrEmpty(this.Parameters.ResultsPath) || !File.Exists(this.Parameters.ResultsPath))
                return;
            tcParameters.SelectedTab = tpResults;
            using (new ProgressDialog(false, 0, 0, ThisAddIn.Name, "Gathering statistic"))
            try
            {
                var a_CountriesInfo = new Dictionary<string, CountryStatisticInfo>(StringComparer.InvariantCultureIgnoreCase);
                using (var a_ResultsHelperClass = new ResultsShapeFileHelper(this.Parameters.ResultsPath))
                {
                    a_ResultsHelperClass.OpenOrCreateResultsShapeFile();
                    var a_Fc = a_ResultsHelperClass.ResultsFeatureClass.Search(null, true);
                    try
                    {
                        IFeature a_Feature;
                        while ((a_Feature = a_Fc.NextFeature()) != null)
                        {
                            var a_CName = a_Feature.GetFieldValue(ResultsShapeFileHelper.fnFocalRegionNames,
                                string.Empty);
                            CountryStatisticInfo a_CountryInfo;
                            if (!a_CountriesInfo.TryGetValue(a_CName, out a_CountryInfo))
                            {
                                a_CountryInfo = new CountryStatisticInfo {Name = a_CName};
                                a_CountriesInfo[a_CName] = a_CountryInfo;
                            }
                            a_CountryInfo.ReadData(a_Feature);
                        }
                    }
                    finally
                    {
                        if (a_Fc != null)
                            Marshal.ReleaseComObject(a_Fc);
                    }
                }
                WriteDelimiters('*');
                WritelnInLog(Color.Navy, "Analysis results");
                WriteDelimiters('*');
                if (a_CountriesInfo.Count == 0)
                {
                    WritelnInLog(Color.Black, "No results");
                    return;
                }
                foreach (var a_CountryInfo in a_CountriesInfo)
                {
                    WritelnInLog(Color.Black, a_CountryInfo.Value.GetInfo());
                }
            }
            catch (Exception a_Exception)
            {
                ExceptionDialog.Show(a_Exception);
            }
        }

        private void GatherStatisticNRCP()
        {
            tcParameters.SelectedTab = tpResults;
            WriteDelimiters('*');
            WritelnInLog(Color.Navy, "Starting NR and CP analyze");
            WriteDelimiters('*');
            if (string.IsNullOrEmpty(this.Parameters.ResultsPath) || !File.Exists(this.Parameters.ResultsPath))
            {
                WritelnInLog(Color.Red, "Results shapefile '{0}' not found", this.Parameters.ResultsPath);
                return;
            }
            using (new ProgressDialog(false, 0, 0, ThisAddIn.Name, "Gathering NR and CP statistic"))
            using(var a_NRPCStatistic = new NRCPStatisticInfo())
            try
            {
                using (var a_ResultsHelperClass = new ResultsShapeFileHelper(this.Parameters.ResultsPath))
                {
                    a_ResultsHelperClass.OpenOrCreateResultsShapeFile();
                    var a_Fc = a_ResultsHelperClass.ResultsFeatureClass.Search(null, true);
                    try
                    {
                        IFeature a_Feature;
                        while ((a_Feature = a_Fc.NextFeature()) != null)
                        {
                            a_NRPCStatistic.AnalyzeRow(a_Feature);
                        }
                    }
                    finally
                    {
                        if (a_Fc != null)
                            Marshal.ReleaseComObject(a_Fc);
                    }
                }
                if (a_NRPCStatistic.Rows.Count == 0)
                {
                    WritelnInLog(Color.Black, "No result rows found", this.Parameters.ResultsPath);
                    return;
                }
                var a_OutputFileName = "NRCPStatistic" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".shp";
                a_OutputFileName = Path.Combine(Path.GetDirectoryName(this.Parameters.ResultsPath), a_OutputFileName);
                using (var a_NRCPHelperClass = new NRCPStatisticShapeFileHelper(a_OutputFileName))
                {
                    a_NRCPHelperClass.InitializeWriteSession();
                    foreach (var a_Value in a_NRPCStatistic.Rows.Values)
                    {
                        a_NRCPHelperClass.Write(a_Value);
                    }
                    a_NRCPHelperClass.FinalizeWriteSession();
                    AddNRCPLayers(a_NRPCStatistic, a_NRCPHelperClass);
                }
                WritelnInLog(Color.Black, "Analysis finished and saved to '{0}'", a_OutputFileName);
            }
            catch (Exception a_Exception)
            {
                ExceptionDialog.Show(a_Exception);
            }
        }

        #endregion working with results

        #region Work with IUCN Categories

        private IDictionary<string, string> ReadIUCNSpeciesCategories(string a_Path)
        {
            const string SpeciesNameColumnName = "Species English Name";
            const string SpeciesCategoryColumnName = "IUCN Level";

            if (string.IsNullOrEmpty(a_Path)) return null;
            if (!File.Exists(a_Path)) throw new Exception(string.Format("File '{0}' not found", a_Path));
            using (var a_FileReader = new StreamReader(a_Path, true))
            {
                var a_RowIndex = 0;
                var a_KeyColumnIndex = -1;
                var a_ValueColumnIndex = -1;
                var a_ReturnValue = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
                while (!a_FileReader.EndOfStream)
                {
                    var a_String = a_FileReader.ReadLine();
                    var a_Values = a_String.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (a_RowIndex == 0)
                    {
                        for (var a_I = 0; a_I < a_Values.Length; a_I++)
                        {
                            if (string.Equals(
                                a_Values[a_I], SpeciesNameColumnName, StringComparison.InvariantCultureIgnoreCase)) a_KeyColumnIndex = a_I;
                            if (string.Equals(
                                a_Values[a_I], SpeciesCategoryColumnName, StringComparison.InvariantCultureIgnoreCase)) a_ValueColumnIndex = a_I;
                        }
                        if (a_KeyColumnIndex == -1)
                            throw new Exception(
                                string.Format("Column '{0}' not found in '{1}'", SpeciesNameColumnName, a_Path));
                        if (a_ValueColumnIndex == -1)
                            throw new Exception(
                                string.Format("Column '{0}' not found in '{1}'", SpeciesCategoryColumnName, a_Path));
                    }
                    else
                    {
                        if (a_KeyColumnIndex >= a_Values.Length)
                            throw new Exception(
                                string.Format(
                                    "Row {0} does not have enough columns(<{1})", a_RowIndex, a_KeyColumnIndex));
                        if (a_ValueColumnIndex >= a_Values.Length)
                            throw new Exception(
                                string.Format(
                                    "Row {0} does not have enough columns(<{1})", a_RowIndex, a_ValueColumnIndex));
                        var a_Key = a_Values[a_KeyColumnIndex];
                        var a_Value = a_Values[a_ValueColumnIndex];
                        if (a_ReturnValue.ContainsKey(a_Key))
                            WritelnInLog(
                                Color.Red,
                                "Duplicate entry: '{0}'={1}, '{2}'=({3}, {4})",
                                SpeciesNameColumnName,
                                a_Key,
                                SpeciesCategoryColumnName,
                                a_ReturnValue[a_Key],
                                a_Value);
                        a_ReturnValue[a_Key] = a_Value.ToUpperInvariant();
                    }
                    a_RowIndex++;
                }
                return a_ReturnValue;
            }
        }

        #endregion Work with IUCN Categories

        #region event handlers

        private void btClear_Click(object sender, EventArgs e)
        {
            rtbResults.Clear();
        }

        private void btAddLayerFromResults_Click(object sender, EventArgs e)
        {
            AddLayersFromResults();
        }

        private void lbAllLayer_Click(object sender, EventArgs e)
        {
            rbUseAllLayer.PerformClick();
        }

        private void lbUseSelectedValues_Click(object sender, EventArgs e)
        {
            rbUseSelectedValues.PerformClick();
        }

        private void UsedValuesCheckedChanged(object sender, EventArgs e)
        {
            libFocalRegionValues.Enabled = rbUseSelectedValues.Checked;
        }

        private void btBrowseIUCNCategoriesPath_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbIUCNCategoriesPath.Text))
            {
                ofdCsv.InitialDirectory = Path.GetDirectoryName(tbIUCNCategoriesPath.Text);
                ofdCsv.FileName = Path.GetFileName(tbIUCNCategoriesPath.Text);
            }
            if (ofdCsv.ShowDialog() == DialogResult.OK)
            {
                tbIUCNCategoriesPath.Text = ofdCsv.FileName;
            }
        }

        private void btBrowse_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbResultPath.Text))
            {
                ofdShp.InitialDirectory = Path.GetDirectoryName(tbResultPath.Text);
                ofdShp.FileName = Path.GetFileName(tbResultPath.Text);
            }
            if (ofdShp.ShowDialog() == DialogResult.OK)
            {
                tbResultPath.Text = ofdShp.FileName;
            }
        }

        private void cbCalculationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCalculationType.SelectedIndex == -1) return;
            var a_CalculationType = (CalculationTypeEnum)(short)cbCalculationType.SelectedIndex;
            this.MakeBindingFor(a_CalculationType);
        }

        private void UpdateCheckBoxImage(ToolStripButton a_ToolStripButton, bool a_Checked)
        {
            a_ToolStripButton.Image = a_Checked ? Resources.Checked : Resources.Unchecked;
        }

        private void tsbtFilterLayers_Click(object sender, EventArgs e)
        {
            var a_ToolStripButton = sender as ToolStripButton;
            if (a_ToolStripButton == null) return;

            Parameters.LayersFiltered = !Parameters.LayersFiltered;
            UpdateCheckBoxImage(a_ToolStripButton, Parameters.LayersFiltered);
            this.InitializeData(true);
        }

        private void btAnalizeResultShapeFile_Click(object sender, EventArgs e)
        {
            GatherStatistic();
        }

        private void btAnalyzeNRCP_Click(object sender, EventArgs e)
        {
            GatherStatisticNRCP();
        }

        #endregion event handlers

        #region working with  NR CP statistic

        private void AddNRCPLayers(NRCPStatisticInfo a_NrcpStatisticInfo, NRCPStatisticShapeFileHelper a_ShapeFileHelper)
        {
            var a_NewLayerName = "NR and CP Analyze";

            ILayer a_Layer = null;
            a_Layer = Utils.GetFirstLayerByName<IGroupLayer>(a_NewLayerName);
            if (a_Layer != null)
            {
                ArcMap.Document.FocusMap.DeleteLayer(a_Layer);
                a_Layer = null;
            }

            if (a_Layer == null)
            {
                var a_FeatureClass = a_ShapeFileHelper.OpenFeatureClassWithoutDisposing();

                var a_SubLayers = new List<ILayer>();

                a_SubLayers.Add(
                    CreateGraduatedFeatureLayer(
                        "NP very high and high",
                        a_FeatureClass,
                        NRCPStatisticShapeFileHelper.fnHighAndVeryHigh,
                        true,
                        true));
                a_SubLayers.Add(
                    CreateGraduatedFeatureLayer(
                        "NP median",
                        a_FeatureClass,
                        NRCPStatisticShapeFileHelper.fnMedium,
                        true,
                        false));
                a_SubLayers.Add(
                    CreateGraduatedFeatureLayer(
                        "NP basic",
                        a_FeatureClass,
                        NRCPStatisticShapeFileHelper.fnBasic,
                        true,
                        false));

                a_SubLayers.Add(
                    CreateGraduatedFeatureLayer(
                        "CP Class 1",
                        a_FeatureClass,
                        NRCPStatisticShapeFileHelper.fnClass1,
                        true,
                        false));
                a_SubLayers.Add(
                    CreateGraduatedFeatureLayer(
                        "CP Class 2",
                        a_FeatureClass,
                        NRCPStatisticShapeFileHelper.fnClass2,
                        true,
                        false));
                a_SubLayers.Add(
                    CreateGraduatedFeatureLayer(
                        "CP Class 3",
                        a_FeatureClass,
                        NRCPStatisticShapeFileHelper.fnClass3,
                        true,
                        false));
                a_SubLayers.Add(
                    CreateGraduatedFeatureLayer(
                        "CP Class 4",
                        a_FeatureClass,
                        NRCPStatisticShapeFileHelper.fnClass4,
                        true,
                        false));

                a_Layer = CreateGroupLayer(a_NewLayerName, a_SubLayers.ToArray());
                ArcMap.Document.AddLayer(a_Layer);
            }

        }

        #endregion working with  NR CP statistic
    }
}
