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
 * Lin YP, Schmeller DS, Ding TS, Wang YC, Lien WY, Henle K, 
 * Klenke RA (2020) A GIS-based policy support tool to determine national 
 * responsibilities and priorities for biodiversity conservation. 
 * PLOS ONE 15(12): e0243135. https://doi.org/10.1371/journal.pone.0243135
 */

﻿using System;

namespace SpeciesTool.NET
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;

    using System.Data.Linq.Mapping;

    using ESRI.ArcGIS.DataSourcesFile;
    using ESRI.ArcGIS.Geodatabase;
    using ESRI.ArcGIS.Geometry;

    using Path = System.IO.Path;

    public class ResultsShapeFileHelper: IDisposable
    {
        #region Constants
        //fields constants
        // ReSharper disable InconsistentNaming
        public const string fnSpeciesName = "SPECIES";

        public const string fnBiomesTableName = "BTABLE";

        public const string fnRegionTableName = "RTABLE";

        public const string fnFocalRegionTableName = "FRTABLE";

        public const string fnFocalRegionFieldName = "FRFIELD";

        public const string fnFocalRegionFieldValues = "FRVALUES";

        public const string fnFocalRegionNames = "CNAMES";

        public const string fnCalculationType = "CTYPE";

        public const string fnTitle = "TITLE";

        public const string fnSpeciesArea = "SAREA";

        public const string fnRegionsArea = "RAREA";

        public const string fnFocalRegionsArea = "FRAREA";

        public const string fnIntersectionArea = "INTAREA";

        public const string fnGlobalDistribution = "GDISTR";

        public const string fnDPobs = "DPOBS";

        public const string fnDPexp = "DPEXP";

        public const string fnRegionalDistribution = "RDISTR";

        public const string fnPriority = "PRIORITY";

        public const string fnCategory = "IUCNCAT";

        public const string fnClass = "CLASS";

        public const string fnScore = "SCORE";

        // ReSharper restore InconsistentNaming

        #endregion Constants

        #region Types

        public class ResultFeatureClassRow
        {
            [ColumnAttribute(Name = "FID")]
            [DefaultValue(-1)]
            [Browsable(false)]
            public int ObjectId { get; private set; }

            [DefaultValue(null)]
            [Browsable(false)]
            public IGeometry Shape { get; private set; }

            [ColumnAttribute(Name = fnSpeciesName)]
            [DefaultValue("")]
            [DisplayName(@"Species name")]
            public string SpeciesName { get; set; }

            [ColumnAttribute(Name = fnCategory)]
            [DefaultValue("")]
            [DisplayName(@"Category")]
            public string Category { get; set; }

            [ColumnAttribute(Name = fnBiomesTableName)]
            [DefaultValue("")]
            [DisplayName(@"Biomes layer name")]
            public string BiomesTableName { get; set; }

            [ColumnAttribute(Name = fnRegionTableName)]
            [DefaultValue("")]
            [DisplayName(@"Region layer name")]
            public string RegionTableName { get; set; }

            [ColumnAttribute(Name = fnFocalRegionTableName)]
            [DefaultValue("")]
            [DisplayName(@"Focal regions layer name")]
            public string FocalRegionTableName { get; set; }

            [ColumnAttribute(Name = fnFocalRegionFieldName)]
            [DefaultValue("")]
            [DisplayName(@"Focal region field name")]
            public string FocalRegionFieldName { get; set; }

            [ColumnAttribute(Name = fnFocalRegionFieldValues)]
            [DefaultValue("")]
            [DisplayName(@"Focal region field value")]
            public string FocalRegionFieldValues { get; set; }

            [ColumnAttribute(Name = fnFocalRegionNames)]
            [DefaultValue("")]
            [DisplayName(@"Focal region name")]
            public string FocalRegionNames { get; set; }

            [ColumnAttribute(Name = fnCalculationType)]
            [DefaultValue("")]
            [DisplayName(@"Calculation type")]
            public string CalculationType { get; set; }

            [ColumnAttribute(Name = fnTitle)]
            [DefaultValue("")]
            [DisplayName(@"Title")]
            [Browsable(false)]
            public string Title { get; set; }

            [ColumnAttribute(Name = fnSpeciesArea)]
            [DefaultValue(0.0)]
            [DisplayName(@"Species area")]
            public double SpeciesArea { get; set; }

            [ColumnAttribute(Name = fnRegionsArea)]
            [DefaultValue(0.0)]
            [DisplayName(@"Regions area")]
            public double RegionsArea { get; set; }

            [ColumnAttribute(Name = fnFocalRegionsArea)]
            [DefaultValue(0.0)]
            [DisplayName(@"Focal region area")]
            public double FocalRegionsArea { get; set; }

            [ColumnAttribute(Name = fnIntersectionArea)]
            [DefaultValue(0.0)]
            [DisplayName(@"Intersection area")]
            public double IntersectionArea { get; set; }

            [ColumnAttribute(Name = fnGlobalDistribution)]
            [DefaultValue("")]
            [DisplayName(@"Global distribution")]
            public string GlobalDistribution { get; set; }

            [ColumnAttribute(Name = fnDPobs)]
            [DefaultValue(0.0)]
            [DisplayName(@"DPobs")]
            public double? DPobs { get; set; }

            [ColumnAttribute(Name = fnDPexp)]
            [DefaultValue(0.0)]
            [DisplayName(@"DPexp")]
            public double? DPexp { get; set; }

            [ColumnAttribute(Name = fnRegionalDistribution)]
            [DefaultValue("")]
            [DisplayName(@"Regional distribution")]
            public string RegionalDistribution { get; set; }

            [ColumnAttribute(Name = fnPriority)]
            [DefaultValue("")]
            [DisplayName(@"Priority")]
            public string Priority { get; set; }

            [ColumnAttribute(Name = fnClass)]
            [DefaultValue("")]
            [DisplayName(@"Class")]
            public string Class { get; set; }

            [ColumnAttribute(Name = fnScore)]
            [DefaultValue(0)]
            [DisplayName(@"Score")]
            public byte? Score { get; set; }

            public void SetValues(ResultInfo a_ResultInfo, IGeometry a_Geometry)
            {
                ObjectId = -1;
                Shape = a_Geometry;

                CalculationType = a_ResultInfo.CalculationType.GetEnumDescription();
                Title = "?";

                SpeciesName = a_ResultInfo.SpeciesTableName;
                BiomesTableName = a_ResultInfo.BiomesTableName;
                RegionTableName = a_ResultInfo.RegionTableName;
                FocalRegionTableName = a_ResultInfo.FocalRegionTableName;

                Category = a_ResultInfo.Category.GetEnumDescription();
                SpeciesArea = a_ResultInfo.SpeciesArea;
                RegionsArea = a_ResultInfo.TotalReferenceArea;

                FocalRegionFieldName = "OID";
                FocalRegionFieldValues = a_ResultInfo.FocalRegionObjectId.ToString();
                FocalRegionNames = a_ResultInfo.FocalRegionName;

                FocalRegionsArea = a_ResultInfo.TotalFocalArea;
                IntersectionArea = a_ResultInfo.DistributionInFocalArea;

                /*if (IntersectionArea > 0)
                {
                    GlobalDistribution = a_ResultInfo.GlobalDistribution.GetEnumDescription();
                    DPexp = a_ResultInfo.ExpectedDistributionProbability;
                    DPobs = a_ResultInfo.ObservedDistributionProbability;
                    RegionalDistribution = a_ResultInfo.RegionalDistribution.GetEnumDescription();
                    Priority = a_ResultInfo.Priority.GetEnumDescription();
                    Class = a_ResultInfo.Class.GetEnumDescription();
                    Score = a_ResultInfo.Score;
                }
                else
                {
                    GlobalDistribution = null;
                    DPexp = null;
                    DPobs = null;
                    RegionalDistribution = null;
                    Priority = null;
                    Class = null;
                    Score = null;
                }*/
                GlobalDistribution = a_ResultInfo.GlobalDistribution.GetEnumDescription();
                DPexp = a_ResultInfo.ExpectedDistributionProbability;
                DPobs = a_ResultInfo.ObservedDistributionProbability;
                RegionalDistribution = a_ResultInfo.RegionalDistribution.GetEnumDescription();
                Priority = a_ResultInfo.Priority.GetEnumDescription();
                Class = a_ResultInfo.Class.GetEnumDescription();
                Score = a_ResultInfo.Score;
            }

            public ResultInfo ToResultInfo()
            {
                var a_ResultInfo = new ResultInfo(Utils.GetEnumValueByDescription<CalculationTypeEnum>(CalculationType), new Ranges(double.MinValue, double.MaxValue), 2);
                a_ResultInfo.SpeciesTableName = SpeciesName;
                a_ResultInfo.BiomesTableName = BiomesTableName;
                a_ResultInfo.RegionTableName = RegionTableName;
                a_ResultInfo.FocalRegionTableName = FocalRegionTableName;
                a_ResultInfo.Category = Utils.GetEnumValueByDescription<IUCNCategoryEnum>(Category);

                a_ResultInfo.SpeciesArea = SpeciesArea;
                a_ResultInfo.TotalReferenceArea = RegionsArea;

                a_ResultInfo.FocalRegionObjectId = Convert.ToInt32(FocalRegionFieldValues);
                a_ResultInfo.FocalRegionName = FocalRegionNames;

                a_ResultInfo.TotalFocalArea = FocalRegionsArea;
                a_ResultInfo.DistributionInFocalArea = IntersectionArea;

                //these values not needed for creating thematic layers
                /*a_ResultInfo.GlobalDistribution = Utils.GetEnumValueByDescription<GlobalDistributionEnum>(GlobalDistribution);
                a_ResultInfo.ExpectedDistributionProbability = DPexp;
                a_ResultInfo.ObservedDistributionProbability = DPobs;
                a_ResultInfo.RegionalDistribution = RegionalDistribution.GetEnumDescription();
                a_ResultInfo.Priority = Priority.GetEnumDescription() ;
                a_ResultInfo.Class = Class.GetEnumDescription();*/
                return a_ResultInfo;
            }

            public void Read(IFeature a_Feature, bool a_ReadShape)
            {
                foreach (
                    var a_PropertyInfo in
                        this.GetType()
                            .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty))
                {
                    var a_ColumnAttribute =
                        (ColumnAttribute)
                        a_PropertyInfo.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault();
                    if (a_ColumnAttribute == null) continue;
                    var a_FieldIndex = a_Feature.Fields.FindField(a_ColumnAttribute.Name);
                    if (a_FieldIndex == -1)
                        throw new ArgumentException(
                            string.Format(
                                "Field {0}(property {1}) not found", a_ColumnAttribute.Name, a_PropertyInfo.Name));
                    try
                    {
                        var a_Value = a_Feature.Value[a_FieldIndex];
                        if (a_Value == null) a_PropertyInfo.SetValue(this, null, null);
                        else
                        {
                            var a_CorrectedPropertyType = a_PropertyInfo.PropertyType;
                            if (a_CorrectedPropertyType.IsGenericType
                                && a_CorrectedPropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                            {
                                a_CorrectedPropertyType = Nullable.GetUnderlyingType(a_CorrectedPropertyType);
                            }
                            a_PropertyInfo.SetValue(
                                this,
                                Convert.ChangeType(a_Feature.Value[a_FieldIndex], a_CorrectedPropertyType),
                                null);
                        }
                    }
                    catch (Exception)
                    {
                        var a_DefaultValueAttribute =
                            (DefaultValueAttribute)
                            a_PropertyInfo.GetCustomAttributes(typeof(DefaultValueAttribute), false).FirstOrDefault();
                        if (a_DefaultValueAttribute != null)
                        {
                            a_PropertyInfo.SetValue(this, a_DefaultValueAttribute.Value, null);
                        }
                    }
                }
                if (a_ReadShape)
                {
                    Shape = a_Feature.ShapeCopy;
                }
            }

            public void Write(IFeatureBuffer a_FeatureBuffer)
            {
                foreach (
                    var a_PropertyInfo in
                        this.GetType()
                            .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty))
                {
                    var a_ColumnAttribute =
                        (ColumnAttribute)
                        a_PropertyInfo.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault();
                    if (a_ColumnAttribute == null) continue;
                    var a_FieldIndex = a_FeatureBuffer.Fields.FindField(a_ColumnAttribute.Name);
                    if (a_FieldIndex == -1)
                        throw new ArgumentException(
                            string.Format(
                                "Field {0}(property {1}) not found", a_ColumnAttribute.Name, a_PropertyInfo.Name));
                    if (a_FeatureBuffer.Fields.Field[a_FieldIndex].Editable) a_FeatureBuffer.Value[a_FieldIndex] = a_PropertyInfo.GetValue(this, null);
                }
                if (Shape != null) a_FeatureBuffer.Shape = Shape;
            }
        }

        private class WriteSession: IDisposable
        {
            private IFeatureClass m_FeatureClass = null;
            private IFeatureCursor m_FeatureCursor = null;
            private IFeatureBuffer m_FeatureBuffer = null;

            private bool m_OverwriteResults = false;

            private ResultFeatureClassRow m_ResultRow;

            #region Creating\Destroying

            public WriteSession(IFeatureClass a_FeatureClass, bool a_OverwriteResults)
            {
                m_FeatureClass = a_FeatureClass;
                m_OverwriteResults = a_OverwriteResults;
                m_ResultRow = new ResultFeatureClassRow();
            }

            public void Write(ResultInfo a_ResultInfo, IGeometry a_Geometry, string a_ProcessedObjectIds)
            {
                if (m_FeatureCursor == null)
                {
                    if (m_OverwriteResults)
                    {
                        IQueryFilter a_QueryFilter = new QueryFilterClass();
                        try
                        {
                            var a_BasicWhereClause = GetBasicWhereClause(a_ResultInfo);
                            a_QueryFilter.WhereClause = string.Format(
                                "{0} and {1}='{2}' and {3} in ({4})",
                                a_BasicWhereClause,
                                fnFocalRegionFieldName,
                                "OID",
                                fnFocalRegionFieldValues,
                                a_ProcessedObjectIds);

                            if (a_ResultInfo.CalculationType.IsOneOf(CalculationTypeEnum.ByBiomes, CalculationTypeEnum.ByBiomesArea))
                            {
                                a_QueryFilter.WhereClause += string.Format(
                                    " and {0}='{1}'", fnBiomesTableName, a_ResultInfo.BiomesTableName);
                            }

                            if (m_FeatureClass is ITable) (m_FeatureClass as ITable).DeleteSearchedRows(a_QueryFilter);
                        }
                        finally
                        {
                            Marshal.ReleaseComObject(a_QueryFilter);
                        }
                    }
                    m_FeatureCursor = m_FeatureClass.Insert(true);
                    m_FeatureBuffer = m_FeatureClass.CreateFeatureBuffer();
                }

                m_ResultRow.SetValues(a_ResultInfo, a_Geometry);
                m_ResultRow.Write(m_FeatureBuffer);
                m_FeatureCursor.InsertFeature(m_FeatureBuffer);
            }

            public void Dispose()
            {
                m_FeatureCursor.Flush();
                Marshal.ReleaseComObject(m_FeatureBuffer);
                Marshal.ReleaseComObject(m_FeatureCursor);
            }

            #endregion Creating\Destroying
        }

        #endregion Types

        #region Properties

        public string FilePath { get; private set; }

        public IFeatureClass ResultsFeatureClass { get; private set; }

        private WriteSession m_WriteSession = null;

        #endregion Properties

        public ResultsShapeFileHelper(string a_FilePath)
        {
            FilePath = a_FilePath;
        }

        public void Dispose()
        {
            this.FinalizeWriteSession();
            if (this.ResultsFeatureClass != null)
            {
                Marshal.ReleaseComObject(this.ResultsFeatureClass);
                this.ResultsFeatureClass = null;
            }
        }

        #region Creating\Reading\Writing shape file

        private static IFeatureClass OpenOrCreateResultsShapeFile(string a_ShapeFilePath, bool a_CreateIfNotExists)
        {
            var a_WorkspaceFactory = new ShapefileWorkspaceFactoryClass();
            IFeatureWorkspace a_Workspace = null;
            try
            {
                var a_Directory = Path.GetDirectoryName(a_ShapeFilePath);
                if (string.IsNullOrEmpty(a_Directory)) throw new Exception("Directory name is empty");
                if (!Directory.Exists(a_Directory)) Directory.CreateDirectory(a_Directory);
                var a_TableName = Path.GetFileNameWithoutExtension(a_ShapeFilePath);
                if (string.IsNullOrEmpty(a_TableName)) throw new Exception("Table name is empty");
                a_Workspace =
                    (IFeatureWorkspace)a_WorkspaceFactory.OpenFromString(string.Format("Database={0}", a_Directory), 0);
                IFeatureClass a_FeatureClass = null;
                if (((IWorkspace2)a_Workspace).NameExists[esriDatasetType.esriDTFeatureClass, a_TableName])
                {
                    a_FeatureClass = a_Workspace.OpenFeatureClass(a_TableName);
                }
                else if (a_CreateIfNotExists)
                {
                    IFeatureClassDescription a_FeatureClassDescription = new FeatureClassDescriptionClass();
                    var a_ObjectClassDescription = (IObjectClassDescription)a_FeatureClassDescription;
                    var a_Fields = a_ObjectClassDescription.RequiredFields;
                    var a_FieldsEdit = (IFieldsEdit)a_Fields;

                    // Find the shape field in the required fields and modify its GeometryDef to
                    // use point geometry and to set the spatial reference.
                    var a_ShapeFieldIndex = a_Fields.FindField(a_FeatureClassDescription.ShapeFieldName);
                    var a_Field = a_Fields.Field[a_ShapeFieldIndex];
                    var a_GeometryDef = a_Field.GeometryDef;
                    var a_GeometryDefEdit = (IGeometryDefEdit)a_GeometryDef;
                    a_GeometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolygon;

                    a_Field = new FieldClass
                                  {
                                      IFieldEdit_Name_2 = fnSpeciesName,
                                      IFieldEdit_Type_2 = esriFieldType.esriFieldTypeString,
                                      IFieldEdit_Length_2 = 255,
                                      IFieldEdit_DefaultValue_2 = string.Empty,
                                      IFieldEdit_Editable_2 = true,
                                  };
                    a_FieldsEdit.AddField(a_Field);

                    a_Field = new FieldClass
                                  {
                                      IFieldEdit_Name_2 = fnCategory,
                                      IFieldEdit_Type_2 = esriFieldType.esriFieldTypeString,
                                      IFieldEdit_Length_2 = 30,
                                      IFieldEdit_DefaultValue_2 = string.Empty,
                                      IFieldEdit_Editable_2 = true,
                                  };
                    a_FieldsEdit.AddField(a_Field);

                    a_Field = new FieldClass
                                  {
                                      IFieldEdit_Name_2 = fnBiomesTableName,
                                      IFieldEdit_Type_2 = esriFieldType.esriFieldTypeString,
                                      IFieldEdit_Length_2 = 255,
                                      IFieldEdit_DefaultValue_2 = string.Empty,
                                      IFieldEdit_Editable_2 = true,
                                  };
                    a_FieldsEdit.AddField(a_Field);

                    a_Field = new FieldClass
                                  {
                                      IFieldEdit_Name_2 = fnRegionTableName,
                                      IFieldEdit_Type_2 = esriFieldType.esriFieldTypeString,
                                      IFieldEdit_Length_2 = 255,
                                      IFieldEdit_DefaultValue_2 = string.Empty,
                                      IFieldEdit_Editable_2 = true,
                                  };
                    a_FieldsEdit.AddField(a_Field);

                    a_Field = new FieldClass
                                  {
                                      IFieldEdit_Name_2 = fnFocalRegionTableName,
                                      IFieldEdit_Type_2 = esriFieldType.esriFieldTypeString,
                                      IFieldEdit_Length_2 = 255,
                                      IFieldEdit_DefaultValue_2 = string.Empty,
                                      IFieldEdit_Editable_2 = true,
                                  };
                    a_FieldsEdit.AddField(a_Field);

                    a_Field = new FieldClass
                                  {
                                      IFieldEdit_Name_2 = fnFocalRegionFieldName,
                                      IFieldEdit_Type_2 = esriFieldType.esriFieldTypeString,
                                      IFieldEdit_Length_2 = 255,
                                      IFieldEdit_DefaultValue_2 = string.Empty,
                                      IFieldEdit_Editable_2 = true,
                                  };
                    a_FieldsEdit.AddField(a_Field);

                    a_Field = new FieldClass
                                  {
                                      IFieldEdit_Name_2 = fnFocalRegionFieldValues,
                                      IFieldEdit_Type_2 = esriFieldType.esriFieldTypeString,
                                      IFieldEdit_Length_2 = 255,
                                      IFieldEdit_DefaultValue_2 = string.Empty,
                                      IFieldEdit_Editable_2 = true,
                                  };
                    a_FieldsEdit.AddField(a_Field);

                    a_Field = new FieldClass
                                  {
                                      IFieldEdit_Name_2 = fnFocalRegionNames,
                                      IFieldEdit_Type_2 = esriFieldType.esriFieldTypeString,
                                      IFieldEdit_Length_2 = 255,
                                      IFieldEdit_DefaultValue_2 = string.Empty,
                                      IFieldEdit_Editable_2 = true,
                                  };
                    a_FieldsEdit.AddField(a_Field);

                    a_Field = new FieldClass
                                  {
                                      IFieldEdit_Name_2 = fnCalculationType,
                                      IFieldEdit_Type_2 = esriFieldType.esriFieldTypeString,
                                      IFieldEdit_Length_2 = 40,
                                      IFieldEdit_DefaultValue_2 = string.Empty,
                                      IFieldEdit_Editable_2 = true,
                                  };
                    a_FieldsEdit.AddField(a_Field);

                    a_Field = new FieldClass
                                  {
                                      IFieldEdit_Name_2 = fnTitle,
                                      IFieldEdit_Type_2 = esriFieldType.esriFieldTypeString,
                                      IFieldEdit_Length_2 = 255,
                                      IFieldEdit_DefaultValue_2 = string.Empty,
                                      IFieldEdit_Editable_2 = true,
                                  };
                    a_FieldsEdit.AddField(a_Field);

                    a_Field = new FieldClass
                                  {
                                      IFieldEdit_Name_2 = fnSpeciesArea,
                                      IFieldEdit_Type_2 = esriFieldType.esriFieldTypeDouble,
                                      IFieldEdit_DefaultValue_2 = null,
                                      IFieldEdit_Editable_2 = true,
                                  };
                    a_FieldsEdit.AddField(a_Field);

                    a_Field = new FieldClass
                                  {
                                      IFieldEdit_Name_2 = fnRegionsArea,
                                      IFieldEdit_Type_2 = esriFieldType.esriFieldTypeDouble,
                                      IFieldEdit_DefaultValue_2 = null,
                                      IFieldEdit_Editable_2 = true,
                                  };
                    a_FieldsEdit.AddField(a_Field);

                    a_Field = new FieldClass
                                  {
                                      IFieldEdit_Name_2 = fnFocalRegionsArea,
                                      IFieldEdit_Type_2 = esriFieldType.esriFieldTypeDouble,
                                      IFieldEdit_DefaultValue_2 = null,
                                      IFieldEdit_Editable_2 = true,
                                  };
                    a_FieldsEdit.AddField(a_Field);

                    a_Field = new FieldClass
                                  {
                                      IFieldEdit_Name_2 = fnIntersectionArea,
                                      IFieldEdit_Type_2 = esriFieldType.esriFieldTypeDouble,
                                      IFieldEdit_DefaultValue_2 = null,
                                      IFieldEdit_Editable_2 = true,
                                  };
                    a_FieldsEdit.AddField(a_Field);

                    //caculated data
                    //can be null
                    a_Field = new FieldClass
                                  {
                                      IFieldEdit_Name_2 = fnGlobalDistribution,
                                      IFieldEdit_Type_2 = esriFieldType.esriFieldTypeString,
                                      IFieldEdit_Length_2 = 50,
                                      IFieldEdit_DefaultValue_2 = string.Empty,
                                      IFieldEdit_Editable_2 = true,
                                      IFieldEdit2_IsNullable_2 = true,
                                  };
                    a_FieldsEdit.AddField(a_Field);

                    a_Field = new FieldClass
                                  {
                                      IFieldEdit_Name_2 = fnDPobs,
                                      IFieldEdit_Type_2 = esriFieldType.esriFieldTypeDouble,
                                      IFieldEdit_Editable_2 = true,
                                      IFieldEdit2_IsNullable_2 = true,
                                  };
                    a_FieldsEdit.AddField(a_Field);

                    a_Field = new FieldClass
                                  {
                                      IFieldEdit_Name_2 = fnDPexp,
                                      IFieldEdit_Type_2 = esriFieldType.esriFieldTypeDouble,
                                      IFieldEdit_Editable_2 = true,
                                      IFieldEdit2_IsNullable_2 = true,
                                  };
                    a_FieldsEdit.AddField(a_Field);

                    a_Field = new FieldClass
                                  {
                                      IFieldEdit_Name_2 = fnRegionalDistribution,
                                      IFieldEdit_Type_2 = esriFieldType.esriFieldTypeString,
                                      IFieldEdit_Length_2 = 50,
                                      IFieldEdit_DefaultValue_2 = string.Empty,
                                      IFieldEdit_Editable_2 = true,
                                      IFieldEdit2_IsNullable_2 = true,
                                  };
                    a_FieldsEdit.AddField(a_Field);

                    a_Field = new FieldClass
                                  {
                                      IFieldEdit_Name_2 = fnPriority,
                                      IFieldEdit_Type_2 = esriFieldType.esriFieldTypeString,
                                      IFieldEdit_Length_2 = 50,
                                      IFieldEdit_DefaultValue_2 = string.Empty,
                                      IFieldEdit_Editable_2 = true,
                                      IFieldEdit2_IsNullable_2 = true,
                                  };
                    a_FieldsEdit.AddField(a_Field);

                    a_Field = new FieldClass
                                  {
                                      IFieldEdit_Name_2 = fnClass,
                                      IFieldEdit_Type_2 = esriFieldType.esriFieldTypeString,
                                      IFieldEdit_Length_2 = 40,
                                      IFieldEdit_DefaultValue_2 = string.Empty,
                                      IFieldEdit_Editable_2 = true,
                                      IFieldEdit2_IsNullable_2 = true,
                                  };
                    a_FieldsEdit.AddField(a_Field);

                    a_Field = new FieldClass
                                  {
                                      IFieldEdit_Name_2 = fnScore,
                                      IFieldEdit_Type_2 = esriFieldType.esriFieldTypeSmallInteger,
                                      IFieldEdit_Editable_2 = true,
                                      IFieldEdit2_IsNullable_2 = true,
                                  };
                    a_FieldsEdit.AddField(a_Field);

                    IFieldChecker a_FieldChecker = new FieldCheckerClass();
                    IEnumFieldError a_EnumFieldError = null;
                    IFields a_ValidatedFields = null;
                    a_FieldChecker.ValidateWorkspace = (IWorkspace)a_Workspace;
                    a_FieldChecker.Validate(a_Fields, out a_EnumFieldError, out a_ValidatedFields);

                    a_FeatureClass = a_Workspace.CreateFeatureClass(
                        a_TableName,
                        a_ValidatedFields,
                        a_ObjectClassDescription.InstanceCLSID,
                        a_ObjectClassDescription.ClassExtensionCLSID,
                        esriFeatureType.esriFTSimple,
                        ((IFeatureClassDescription)a_ObjectClassDescription).ShapeFieldName,
                        string.Empty);
                }
                return a_FeatureClass;
            }
            finally
            {
                if (a_Workspace != null) Marshal.ReleaseComObject(a_Workspace);
                Marshal.ReleaseComObject(a_WorkspaceFactory);
            }
        }

        public IFeatureClass OpenFeatureClassWithoutDisposing()
        {
            return OpenOrCreateResultsShapeFile(FilePath, false);
        }

        public void OpenOrCreateResultsShapeFile()
        {
            if (this.ResultsFeatureClass != null)
            {
                Marshal.ReleaseComObject(this.ResultsFeatureClass);
                this.ResultsFeatureClass = null;
            }
            this.ResultsFeatureClass = OpenOrCreateResultsShapeFile(FilePath, true);
        }

        public List<ResultFeatureClassRow> ReadData(string a_WhereClause, bool a_ReadShape)
        {
            var a_Results = new List<ResultFeatureClassRow>();
            if (ResultsFeatureClass == null) throw new NullReferenceException("Results feature class not open");
            IQueryFilter a_QueryFilter = string.IsNullOrEmpty(a_WhereClause) ? null : new QueryFilterClass();
            IFeatureCursor a_FeatureCursor = null;
            try
            {
                IFeature a_Feature;
                a_FeatureCursor = ResultsFeatureClass.Search(a_QueryFilter, true);
                while ((a_Feature = a_FeatureCursor.NextFeature()) != null)
                {
                    var a_Row = new ResultFeatureClassRow();
                    a_Row.Read(a_Feature, a_ReadShape);
                    a_Results.Add(a_Row);
                }
            }
            finally
            {
                if (a_FeatureCursor != null)
                    Marshal.ReleaseComObject(a_FeatureCursor);
                if (a_QueryFilter != null) Marshal.ReleaseComObject(a_QueryFilter);
            }
            return a_Results;
        }

        public void InitializeWriteSession(bool a_Overwrite)
        {
            if  (m_WriteSession != null)
                this.FinalizeWriteSession();
            this.OpenOrCreateResultsShapeFile();
            m_WriteSession = new WriteSession(ResultsFeatureClass, a_Overwrite);
        }

        public void Write(ResultInfo a_ResultInfo, IGeometry a_Geometry, string a_ProcessedObjectIds)
        {
            m_WriteSession.Write(a_ResultInfo, a_Geometry, a_ProcessedObjectIds);
        }

        public void FinalizeWriteSession()
        {
            if (m_WriteSession != null)
            {
                m_WriteSession.Dispose();
                m_WriteSession = null;
            }
        }

        public static string GetBasicWhereClause(ResultInfo a_ResultInfo)
        {
            return string.Format(
                "{0}='{1}' and {2}='{3}' and {4}='{5}' and {6}='{7}'",
                fnCalculationType,
                a_ResultInfo.CalculationType.GetEnumDescription(),
                fnSpeciesName,
                a_ResultInfo.SpeciesTableName,
                fnRegionTableName,
                a_ResultInfo.RegionTableName,
                fnFocalRegionTableName,
                a_ResultInfo.FocalRegionTableName);
        }

        #endregion Creating\Reading\Writing shape file
    }
}
