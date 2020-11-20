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
using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Path = System.IO.Path;

namespace SpeciesTool.NET
{
    public class NRCPStatisticShapeFileHelper : IDisposable
    {
        #region Constants

        //fields constants
        // ReSharper disable InconsistentNaming
        public const string fnCountryName = "CNTRY_NAME";
        public const string fnWide = "Wide";
        public const string fnLocal = "Local";
        public const string fnRegional = "Regional";
        public const string fnLow = "Low";
        public const string fnHigh = "High";
        public const string fnHighAndVeryHigh = "Vhigh_High";
        public const string fnMedium = "medium";
        public const string fnBasic = "basic";
        public const string fnClass1 = "Class1";
        public const string fnClass2 = "Class2";
        public const string fnClass3 = "Class3";
        public const string fnClass4 = "Class4";
        public const string fnScore = "SCORE";
        public const string fnExtinctInWild = "Extinct_in";
        public const string fnCriticallyEndangered = "Critically";
        public const string fnEndangered = "Endangered";
        public const string fnVulnerable = "Vulnerable";
        public const string fnNearThreatened = "Near_Threa";
        public const string fnLeastConcern = "Least_Conc";
        public const string fnDataDeficient = "Data_Defic";
        public const string fnNotEvaluated = "Not_Evalua";
        // ReSharper restore InconsistentNaming

        #endregion Constants

        #region Types

        public class NRCPStatisticFeatureClassRow: IDisposable
        {
            [DefaultValue(null)]
            [Browsable(false)]
            public IGeometry Shape { get; set; }

            [ColumnAttribute(Name = fnCountryName)]
            [DefaultValue("")]
            [DisplayName(@"Country Name")]
            public string CountryName { get; set; }

            [ColumnAttribute(Name = fnWide)]
            [DefaultValue(0)]
            [DisplayName(@"Global Distribution: Wide")]
            public int Wide { get; set; }

            [ColumnAttribute(Name = fnRegional)]
            [DefaultValue(0)]
            [DisplayName(@"Global Distribution: Regional")]
            public int Regional { get; set; }

            [ColumnAttribute(Name = fnLocal)]
            [DefaultValue(0)]
            [DisplayName(@"Global Distribution: Local")]
            public int Local { get; set; }

            [ColumnAttribute(Name = fnHigh)]
            [DefaultValue(0)]
            [DisplayName(@"Regional Distribution: High")]
            public int High { get; set; }

            [ColumnAttribute(Name = fnLow)]
            [DefaultValue(0)]
            [DisplayName(@"Regional Distribution: Low")]
            public int Low { get; set; }

            [ColumnAttribute(Name = fnHighAndVeryHigh)]
            [DefaultValue(0)]
            [DisplayName(@"Priority: High and Very High")]
            public int HighAndVeryHigh { get; set; }

            [ColumnAttribute(Name = fnMedium)]
            [DefaultValue(0)]
            [DisplayName(@"Priority: Medium")]
            public int Medium { get; set; }

            [ColumnAttribute(Name = fnBasic)]
            [DefaultValue(0)]
            [DisplayName(@"Priority: Basic")]
            public int Basic { get; set; }

            [ColumnAttribute(Name = fnClass1)]
            [DefaultValue(0)]
            [DisplayName(@"Class: 1")]
            public int Class1 { get; set; }

            [ColumnAttribute(Name = fnClass2)]
            [DefaultValue(0)]
            [DisplayName(@"Class: 2")]
            public int Class2 { get; set; }

            [ColumnAttribute(Name = fnClass3)]
            [DefaultValue(0)]
            [DisplayName(@"Class: 3")]
            public int Class3 { get; set; }

            [ColumnAttribute(Name = fnClass4)]
            [DefaultValue(0)]
            [DisplayName(@"Class: 4")]
            public int Class4 { get; set; }

            [ColumnAttribute(Name = fnScore)]
            [DefaultValue(0)]
            [DisplayName(@"Score")]
            public int Score { get; set; }

            [ColumnAttribute(Name = fnExtinctInWild)]
            [DefaultValue(0)]
            [DisplayName(@"IUCN Category: Extinct In Wild")]
            public int ExtinctInWild { get; set; }

            [ColumnAttribute(Name = fnCriticallyEndangered)]
            [DefaultValue(0)]
            [DisplayName(@"IUCN Category: Critically Endangered")]
            public int CriticallyEndangered { get; set; }

            [ColumnAttribute(Name = fnEndangered)]
            [DefaultValue(0)]
            [DisplayName(@"IUCN Category: Endangered")]
            public int Endangered { get; set; }

            [ColumnAttribute(Name = fnVulnerable)]
            [DefaultValue(0)]
            [DisplayName(@"IUCN Category: Vulnerable")]
            public int Vulnerable { get; set; }

            [ColumnAttribute(Name = fnNearThreatened)]
            [DefaultValue(0)]
            [DisplayName(@"IUCN Category: Near Threatened")]
            public int NearThreatened { get; set; }

            [ColumnAttribute(Name = fnLeastConcern)]
            [DefaultValue(0)]
            [DisplayName(@"IUCN Category: Least Concern")]
            public int LeastConcern { get; set; }

            [ColumnAttribute(Name = fnDataDeficient)]
            [DefaultValue(0)]
            [DisplayName(@"IUCN Category: Data Deficient")]
            public int DataDeficient { get; set; }

            [ColumnAttribute(Name = fnNotEvaluated)]
            [DefaultValue(0)]
            [DisplayName(@"IUCN Category: NotE valuated")]
            public int NotEvaluated { get; set; }

            public void Write(IFeatureBuffer a_FeatureBuffer)
            {
                foreach (
                    var a_PropertyInfo in
                        this.GetType()
                            .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty))
                {
                    var a_ColumnAttribute =
                        (ColumnAttribute)
                            a_PropertyInfo.GetCustomAttributes(typeof (ColumnAttribute), false).FirstOrDefault();
                    if (a_ColumnAttribute == null) continue;
                    var a_FieldIndex = a_FeatureBuffer.Fields.FindField(a_ColumnAttribute.Name);
                    if (a_FieldIndex == -1)
                        throw new ArgumentException(
                            string.Format(
                                "Field {0}(property {1}) not found", a_ColumnAttribute.Name, a_PropertyInfo.Name));
                    if (a_FeatureBuffer.Fields.Field[a_FieldIndex].Editable)
                        a_FeatureBuffer.Value[a_FieldIndex] = a_PropertyInfo.GetValue(this, null);
                }
                if (Shape != null) a_FeatureBuffer.Shape = Shape;
            }

            public NRCPStatisticFeatureClassRow()
            {
                Shape = null;
                CountryName = string.Empty;
                Wide = Regional = Local = 0;
                High = Low = 0;
                HighAndVeryHigh = Medium = Basic = 0;
                Class1 = Class2 = Class3 = Class4 = 0;
                Score = 0;
                ExtinctInWild =
                    CriticallyEndangered =
                        Endangered = Vulnerable = NearThreatened = LeastConcern = DataDeficient = NotEvaluated = 0;
            }

            public void Dispose()
            {
                if (Shape != null)
                {
                    Marshal.ReleaseComObject(Shape);
                    Shape = null;
                }
            }
        }

        private class WriteSession : IDisposable
        {
            private IFeatureClass m_FeatureClass = null;
            private IFeatureCursor m_FeatureCursor = null;
            private IFeatureBuffer m_FeatureBuffer = null;

            #region Creating\Destroying

            public WriteSession(IFeatureClass a_FeatureClass)
            {
                m_FeatureClass = a_FeatureClass;
            }

            public void Write(NRCPStatisticFeatureClassRow a_Row)
            {
                if (m_FeatureCursor == null)
                {
                    m_FeatureCursor = m_FeatureClass.Insert(true);
                    m_FeatureBuffer = m_FeatureClass.CreateFeatureBuffer();
                }

                a_Row.Write(m_FeatureBuffer);
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

        public IFeatureClass FeatureClass { get; private set; }

        private WriteSession m_WriteSession = null;

        #endregion Properties

        public NRCPStatisticShapeFileHelper(string a_FilePath)
        {
            FilePath = a_FilePath;
        }

        public void Dispose()
        {
            this.FinalizeWriteSession();
            if (this.FeatureClass != null)
            {
                Marshal.ReleaseComObject(this.FeatureClass);
                this.FeatureClass = null;
            }
        }

        #region Creating\Reading\Writing shape file

        private static IFeatureClass OpenOrCreateShapeFile(string a_ShapeFilePath, bool a_CreateIfNotExists)
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
                    (IFeatureWorkspace) a_WorkspaceFactory.OpenFromString(string.Format("Database={0}", a_Directory), 0);
                IFeatureClass a_FeatureClass = null;
                if (((IWorkspace2) a_Workspace).NameExists[esriDatasetType.esriDTFeatureClass, a_TableName])
                {
                    a_FeatureClass = a_Workspace.OpenFeatureClass(a_TableName);
                }
                else if (a_CreateIfNotExists)
                {
                    IFeatureClassDescription a_FeatureClassDescription = new FeatureClassDescriptionClass();
                    var a_ObjectClassDescription = (IObjectClassDescription) a_FeatureClassDescription;
                    var a_Fields = a_ObjectClassDescription.RequiredFields;
                    var a_FieldsEdit = (IFieldsEdit) a_Fields;

                    // Find the shape field in the required fields and modify its GeometryDef to
                    // use point geometry and to set the spatial reference.
                    var a_ShapeFieldIndex = a_Fields.FindField(a_FeatureClassDescription.ShapeFieldName);
                    var a_Field = a_Fields.Field[a_ShapeFieldIndex];
                    var a_GeometryDef = a_Field.GeometryDef;
                    var a_GeometryDefEdit = (IGeometryDefEdit) a_GeometryDef;
                    a_GeometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolygon;

                    #region fields

                    a_Field = new FieldClass
                    {
                        IFieldEdit_Name_2 = fnCountryName,
                        IFieldEdit_Type_2 = esriFieldType.esriFieldTypeString,
                        IFieldEdit_Length_2 = 255,
                        IFieldEdit_DefaultValue_2 = string.Empty,
                        IFieldEdit_Editable_2 = true,
                    };
                    a_FieldsEdit.AddField(a_Field);

                    var a_AddIntField = new Action<string>(a_FieldName =>
                    {
                        a_Field = new FieldClass
                        {
                            IFieldEdit_Name_2 = a_FieldName,
                            IFieldEdit_Type_2 = esriFieldType.esriFieldTypeInteger,
                            IFieldEdit_DefaultValue_2 = 0,
                            IFieldEdit_Editable_2 = true,
                        };
                        a_FieldsEdit.AddField(a_Field);
                    });

                    a_AddIntField(fnWide);
                    a_AddIntField(fnLocal);
                    a_AddIntField(fnRegional);
                    a_AddIntField(fnLow);
                    a_AddIntField(fnHigh);
                    a_AddIntField(fnHighAndVeryHigh);
                    a_AddIntField(fnMedium);
                    a_AddIntField(fnBasic);
                    a_AddIntField(fnClass1);
                    a_AddIntField(fnClass2);
                    a_AddIntField(fnClass3);
                    a_AddIntField(fnClass4);
                    a_AddIntField(fnScore);
                    a_AddIntField(fnExtinctInWild);
                    a_AddIntField(fnCriticallyEndangered);
                    a_AddIntField(fnEndangered);
                    a_AddIntField(fnVulnerable);
                    a_AddIntField(fnNearThreatened);
                    a_AddIntField(fnLeastConcern);
                    a_AddIntField(fnDataDeficient);
                    a_AddIntField(fnNotEvaluated);

                    #endregion fields

                    IFieldChecker a_FieldChecker = new FieldCheckerClass();
                    IEnumFieldError a_EnumFieldError = null;
                    IFields a_ValidatedFields = null;
                    a_FieldChecker.ValidateWorkspace = (IWorkspace) a_Workspace;
                    a_FieldChecker.Validate(a_Fields, out a_EnumFieldError, out a_ValidatedFields);

                    a_FeatureClass = a_Workspace.CreateFeatureClass(
                        a_TableName,
                        a_ValidatedFields,
                        a_ObjectClassDescription.InstanceCLSID,
                        a_ObjectClassDescription.ClassExtensionCLSID,
                        esriFeatureType.esriFTSimple,
                        ((IFeatureClassDescription) a_ObjectClassDescription).ShapeFieldName,
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
            return OpenOrCreateShapeFile(FilePath, false);
        }

        public void OpenOrCreateShapeFile()
        {
            if (this.FeatureClass != null)
            {
                Marshal.ReleaseComObject(this.FeatureClass);
                this.FeatureClass = null;
            }
            this.FeatureClass = OpenOrCreateShapeFile(FilePath, true);
        }

        public void InitializeWriteSession()
        {
            if (m_WriteSession != null)
                this.FinalizeWriteSession();
            this.OpenOrCreateShapeFile();
            m_WriteSession = new WriteSession(FeatureClass);
        }

        public void Write(NRCPStatisticFeatureClassRow a_Row)
        {
            m_WriteSession.Write(a_Row);
        }

        public void FinalizeWriteSession()
        {
            if (m_WriteSession != null)
            {
                m_WriteSession.Dispose();
                m_WriteSession = null;
            }
        }

        #endregion Creating\Reading\Writing shape file
    }
}
