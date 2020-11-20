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
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;

namespace SpeciesTool.NET
{
    using System.ComponentModel;

    using ESRI.ArcGIS.Geometry;

    public static class Utils
    {
        public class RowInfo
        {
            public int ObjectId { get; set; }
            public string DisplayValue { get; set; }

            public RowInfo()
            {
                ObjectId = -1;
                DisplayValue = string.Empty;
            }
        }

        public class LayerDisplayInfo
        {
            public string LayerName { get; set; }
            public string DisplayName { get; set; }

            public LayerDisplayInfo(string a_LayerName, string a_Prefix)
            {
                LayerName = a_LayerName;
                DisplayName = a_LayerName;
                if (!string.IsNullOrEmpty(a_Prefix) &&
                    LayerName.StartsWith(a_Prefix, StringComparison.InvariantCultureIgnoreCase))
                    DisplayName = LayerName.Substring(a_Prefix.Length);
            }
        }

        public static T GetFirstLayerByName<T>(string a_LayerName) where T: class, ILayer
        {
            var a_EnumLayers = ArcMap.Document.FocusMap.Layers[null, true];
            a_EnumLayers.Reset();
            ILayer a_Layer;
            while ((a_Layer = a_EnumLayers.Next()) != null)
            {
                if (a_Layer.Name == a_LayerName && a_Layer is T)
                    return a_Layer as T;
            }
            return null;
        }

        public static IFeatureLayer GetLayerByName(string a_LayerName)
        {
            return GetFirstLayerByName<IFeatureLayer>(a_LayerName);
        }

        public static List<string> GetFeatureLayers(Func<IFeatureLayer, bool> a_Selector)
        {
            var a_Layers = new List<string>();
            if (ArcMap.Document != null && ArcMap.Document.FocusMap != null)
            {
                var a_EnumLayers = ArcMap.Document.FocusMap.Layers[null, true];
                a_EnumLayers.Reset();
                ILayer a_Layer;
                while ((a_Layer = a_EnumLayers.Next()) != null)
                {
                    if (a_Layer is IFeatureLayer && (a_Layer as IFeatureLayer).FeatureClass != null &&  (a_Selector == null || a_Selector(a_Layer as IFeatureLayer)))
                    {
                        a_Layers.Add(a_Layer.Name);
                    }
                }
            }
            return a_Layers;
        }

        public static List<LayerDisplayInfo> GetFeatureLayersByGeometryAndPrefix(esriGeometryType a_GeometryType,
                                                                                 string a_Prefix)
        {
            var a_Layers = GetFeatureLayers(
                a_FeatureLayer =>
                a_FeatureLayer.FeatureClass.ShapeType == a_GeometryType &&
                (string.IsNullOrEmpty(a_Prefix) ||
                 ((a_FeatureLayer.FeatureClass as IDataset) != null &&
                  (a_FeatureLayer.FeatureClass as IDataset).Name.StartsWith(a_Prefix)))
                );
            return a_Layers.Select(a_LayerName => new LayerDisplayInfo(a_LayerName, a_Prefix)).ToList();
        }

        private static readonly HashSet<string> mrs_DefaultNameFields =
            new HashSet<string>(new[] { "CNTRY_NAME", "ECO_NAME" }, StringComparer.InvariantCultureIgnoreCase);


        public static bool IsDefaultNameField(string a_FieldName)
        {
            return mrs_DefaultNameFields.Contains(a_FieldName);
        }

        public static List<RowInfo> GetNamedRows(IFeatureLayer a_FeatureLayer, string a_FieldName)
        {
            var a_ReturnValues = new List<RowInfo>();
            {
                var a_FeatureCursor = a_FeatureLayer.Search(null, true);
                try
                {
                    var a_DisplayNameFieldIndex = a_FeatureCursor.FindField(a_FieldName);
                    if (a_DisplayNameFieldIndex != -1)
                    {
                        IFeature a_Feature;
                        while ((a_Feature = a_FeatureCursor.NextFeature()) != null)
                        {
                            a_ReturnValues.Add(
                                new RowInfo()
                                    {
                                        ObjectId = a_Feature.OID,
                                        DisplayValue = a_Feature.Value[a_DisplayNameFieldIndex].ToString()
                                    });
                        }
                    }
                }
                finally
                {
                    Marshal.ReleaseComObject(a_FeatureCursor);
                }
            }
            a_ReturnValues.Sort(
                (a_First, a_Second) =>
                String.Compare(
                    a_First.DisplayValue,
                    a_Second.DisplayValue,
                    StringComparison.InvariantCultureIgnoreCase));
            return a_ReturnValues;
        }

        public static IColor ToEsriColor(this Color a_Color)
        {
            return new RgbColorClass()
            {
                Red = a_Color.R,
                Green = a_Color.G,
                Blue = a_Color.B,
                Transparency = a_Color.A
            };
        }

        public static List<string> GetFields(this ILayer a_Layer)
        {
            if (a_Layer == null)
                throw new NullReferenceException("a_Layer");
            var a_ReturnValues = new List<string>();
            if (a_Layer is ILayerFields)
            {
                var a_Fields = (a_Layer as ILayerFields);
                for (var a_I = 0; a_I < a_Fields.FieldCount; a_I++)
                {
                    var a_Field = a_Fields.Field[a_I];
                    if (a_Field.Type != esriFieldType.esriFieldTypeGeometry) a_ReturnValues.Add(a_Field.AliasName);
                }
            }
            return a_ReturnValues;
        }

        public static T GetFieldValue<T>(this IFeature a_Feature, string a_FieldName, T a_DefaultValue)
        {
            var a_FieldIndex = a_Feature.Fields.FindField(a_FieldName);
            if (a_FieldIndex == -1) return a_DefaultValue;
            try
            {
                return (T)Convert.ChangeType(a_Feature.Value[a_FieldIndex], typeof(T));
            }
            catch
            {
                return a_DefaultValue;
            }
        }

        public static void SetFieldValue(this IFeatureBuffer a_Feature, string a_FieldName, object a_Value)
        {
            var a_FieldIndex = a_Feature.Fields.FindField(a_FieldName);
            if (a_FieldIndex == -1)
                throw new Exception(string.Format("Field '{0}' not found", a_FieldName));
            a_Feature.Value[a_FieldIndex] = a_Value;
        }

        public static void DoForCursor(this IFeatureCursor a_FeatureCursor, Action<IFeature> a_FeatureAction)
        {
            try
            {
                IFeature a_Feature;
                while ((a_Feature = a_FeatureCursor.NextFeature()) != null)
                {
                    a_FeatureAction(a_Feature);
                    Application.DoEvents();
                }
            }
            finally
            {
                Marshal.ReleaseComObject(a_FeatureCursor);
            }
        }

        public static double GetArea(this IGeometry a_Geometry)
        {
            /*if (a_Geometry is IAreaGeodetic)
            {
                if (m_SquareLinearUnit == null)
                {
                    var a_Environment = new SpatialReferenceEnvironmentClass() as ISpatialReferenceFactory3;
                    m_SquareLinearUnit = a_Environment.CreateUnit((int)esriSRUnitType.esriSRUnit_Kilometer) as ILinearUnit;
                }
                try
                {
                    return (a_Geometry as IAreaGeodetic).AreaGeodetic[esriGeodeticType.esriGeodeticTypeGeodesic, m_SquareLinearUnit];
                }
                catch
                {
                }
            }
            return 0;*/
            return (a_Geometry is IArea) ? (a_Geometry as IArea).Area : 0;
        }

        public static double GetAreaOfIntersection(this IGeometry a_PrimaryGeometry, IGeometry a_SecondaryGeometry)
        {
            if (a_PrimaryGeometry == null || a_PrimaryGeometry.IsEmpty) return 0;
            if (a_SecondaryGeometry == null || a_SecondaryGeometry.IsEmpty) return 0;
            var a_IntersectedGeometry = ((ITopologicalOperator)a_PrimaryGeometry).Intersect(a_SecondaryGeometry, esriGeometryDimension.esriGeometry2Dimension);
            try
            {
                if (a_IntersectedGeometry == null || a_IntersectedGeometry.IsEmpty) return 0;
                return a_IntersectedGeometry.GetArea();
            }
            finally
            {
                Marshal.ReleaseComObject(a_IntersectedGeometry);
            }
        }


        public static bool IsOneOf<T>(this T a_CheckValue, params T[] a_Values)
        {
            if (a_Values == null) return false;
            return a_Values.Any(a_Arg => a_Arg.Equals(a_CheckValue));
        }

        public static string GetEnumDescription<TEnumType>(this TEnumType a_Value) where TEnumType : struct
        {
            var a_Attribute =
                a_Value.GetType()
                       .GetField(a_Value.ToString())
                       .GetCustomAttributes(typeof(DescriptionAttribute), false)
                       .SingleOrDefault() as DescriptionAttribute;
            return a_Attribute == null ? a_Value.ToString() : a_Attribute.Description;
        }

        public static Color GetEnumColor<TEnumType>(this TEnumType a_Value) where TEnumType : struct
        {
            var a_Attribute =
                a_Value.GetType()
                       .GetField(a_Value.ToString())
                       .GetCustomAttributes(typeof(ColorAttribute), false)
                       .SingleOrDefault() as ColorAttribute;
            return a_Attribute == null ? Color.LimeGreen : a_Attribute.Value;
        }

        public static string GetEnumCode<TEnumType>(this TEnumType a_Value) where TEnumType : struct
        {
            var a_Attribute =
                a_Value.GetType()
                       .GetField(a_Value.ToString())
                       .GetCustomAttributes(typeof(CodeAttribute), false)
                       .SingleOrDefault() as CodeAttribute;
            return a_Attribute == null ? string.Empty : a_Attribute.Value;
        }

        public static TEnumType GetEnumValueByCode<TEnumType>(string a_Code) where TEnumType : struct
        {
            foreach (TEnumType a_Value in Enum.GetValues(typeof(TEnumType)))
            {
                if (GetEnumCode(a_Value) == a_Code)
                    return a_Value;
            }
            throw new ArgumentOutOfRangeException("a_Code");
        }

        public static TEnumType GetEnumValueByDescription<TEnumType>(string a_Description) where TEnumType : struct
        {
            foreach (TEnumType a_Value in Enum.GetValues(typeof(TEnumType)))
            {
                if (GetEnumDescription(a_Value) == a_Description)
                    return a_Value;
            }
            throw new ArgumentOutOfRangeException("a_Description");
        }

        public static byte GetEnumScore<TEnumType>(this TEnumType a_Value) where TEnumType : struct
        {
            var a_Attribute =
                a_Value.GetType()
                       .GetField(a_Value.ToString())
                       .GetCustomAttributes(typeof(ScoreAttribute), false)
                       .SingleOrDefault() as ScoreAttribute;
            return a_Attribute == null ? (byte)0 : a_Attribute.Value;
        }

    }

    public class ProgressDialog : IDisposable
    {
        private readonly ITrackCancel m_TrackCancel = null;
        private readonly IStepProgressor m_StepProgressor = null;
        private readonly IProgressDialog2 m_ProgressDialog = null;

        public string Description
        {
            set
            {
                m_StepProgressor.Message = value;
                Application.DoEvents();
            }
        }

        public string Message
        {
            set
            {
                m_ProgressDialog.Description = value;
                Application.DoEvents();
            }
        }


        public ProgressDialog(bool a_EnableCancel, int a_MinValue, int a_MaxValue, string a_Title,
                              string a_Description)
        {
            var a_ProgressDialogFactory = new ProgressDialogFactoryClass();

            m_TrackCancel = new CancelTracker();
            m_StepProgressor = a_ProgressDialogFactory.Create(m_TrackCancel, 0);
            m_ProgressDialog = (IProgressDialog2)m_StepProgressor;
            m_TrackCancel.CancelOnClick = a_EnableCancel;
            m_TrackCancel.CancelOnKeyPress = a_EnableCancel;

            m_StepProgressor.MinRange = a_MinValue;
            m_StepProgressor.MaxRange = a_MaxValue;
            m_StepProgressor.StepValue = 1;
            m_StepProgressor.Position = m_StepProgressor.MinRange;
            if (a_MinValue == a_MaxValue)
                m_StepProgressor.Hide();

            m_ProgressDialog.CancelEnabled = a_EnableCancel;
            m_ProgressDialog.Title = a_Title;
            m_ProgressDialog.Description = a_Description;
            m_ProgressDialog.Animation = esriProgressAnimationTypes.esriProgressSpiral;
        }

        public bool Step()
        {
            m_StepProgressor.Step();
            Application.DoEvents();
            return m_TrackCancel.Continue();
        }

        public bool Step(string a_Message)
        {
            m_StepProgressor.Message = a_Message;
            return Step();
        }

        public void Dispose()
        {
            Marshal.ReleaseComObject(m_TrackCancel);
            Marshal.ReleaseComObject(m_ProgressDialog);
        }
    }

}
