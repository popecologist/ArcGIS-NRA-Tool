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
 * Klenke RA (2020) A GIS-based policy support tool to determine 
 * national responsibilities and priorities for biodiversity conservation. 
 * PLOS ONE 15(12): e0243135. https://doi.org/10.1371/journal.pone.0243135
 */

﻿namespace SpeciesTool.NET
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;

    [AttributeUsage(AttributeTargets.Field)]
    public class ColorAttribute : Attribute
    {
        public Color Value { get; private set; }

        public ColorAttribute(Color a_Value)
        {
            Value = a_Value;
        }
        public ColorAttribute(int a_Red, int a_Green, int a_Blue)
        {
            Value = Color.FromArgb(a_Red, a_Green, a_Blue);
        }

    }

    [AttributeUsage(AttributeTargets.Field)]
    public class CodeAttribute : Attribute
    {
        public string Value { get; private set; }

        public CodeAttribute(string a_Value)
        {
            Value = a_Value;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class ScoreAttribute : Attribute
    {
        public byte Value { get; private set; }

        public ScoreAttribute(byte a_Value)
        {
            Value = a_Value;
        }
    }

    public enum GlobalDistributionEnum : short
    {
        [Description("Local")]
        Local = 0,

        [Description("Regional")]
        Regional = 1,

        [Description("Wide")]
        Wide = 2,
    }

    public enum RegionalDistributionEnum : short
    {
        [Description("High")]
        High = 0,

        [Description("Low")]
        Low = 1
    }

    public enum PriorityEnum : short
    {
        [Description("Data Deficient/Not Evaluated")]
        [Color(196, 196, 196)]
        [Score(0)]
        DataDeficientNotEvaluated = -1,

        [Description("very high")]
        [Color(255, 80, 80)]
        [Score(10)]
        VeryHighPriority = 0,

        [Description("high")]
        [Color(255, 153, 51)]
        [Score(05)]
        HighPriority = 1,

        [Description("medium")]
        [Color(255, 204, 0)]
        [Score(02)]
        MediumPriority = 2,

        [Description("basic")]
        [Color(255, 255, 204)]
        [Score(01)]
        BasicPriority = 3,
    }

    public enum IUCNCategoryEnum : int
    {
        [Description("Extinct")]
        [Code("EX")]
        //by ppt color was not defined
        [Color(255, 0, 0)]
        [Score(0)]
        Extinct,

        [Description("Extinct in the Wild")]
        [Code("EW")]
        [Color(0xD6, 0x00, 0x93)]
        [Score(15)]
        ExtinctInTheWild,

        [Description("Critically Endangered")]
        [Code("CR")]
        [Color(0xFF, 0x00, 0xFF)]
        [Score(12)]
        CriticallyEndangered,

        [Description("Endangered")]
        [Code("EN")]
        [Color(0xFF, 0x99, 0xCC)]
        [Score(10)]
        Endangered,

        [Description("Vulnerable")]
        [Code("VU")]
        [Color(0xFF, 0xCC, 0x66)]
        [Score(08)]
        Vulnerable,

        [Description("Near Threatened")]
        [Code("NT")]
        [Color(0xFF, 0xFF, 0x66)]
        [Score(06)]
        NearThreatened,

        [Description("Least Concern")]
        [Code("LC")]
        [Color(0xCC, 0xFF, 0x33)]
        [Score(01)]
        LeastConcern,

        [Description("Data Deficient")]
        [Code("DD")]
        [Color(0xD9, 0xD9, 0xD9)]
        [Score(0)]
        DataDeficient,

        [Description("Not Evaluated")]
        [Code("NE")]
        [Color(0xD9, 0xD9, 0xD9)]
        [Score(0)]
        NotEvaluated,
    }

    public enum ClassEnum : short
    {
        [Description("Data Deficient/Not Evaluated")]
        [Color(196, 196, 196)]
        DataDeficientNotEvaluated = -1,

        [Description("Class 1")]
        [Color(255, 0, 0)]
        Class1 = 0,

        [Description("Class 2")]
        [Color(255, 102, 0)]
        Class2 = 1,

        [Description("Class 3")]
        [Color(255, 153, 102)]
        Class3 = 2,

        [Description("Class 4")]
        [Color(102, 204, 255)]
        Class4 = 3,
    }

    public enum CalculationTypeEnum : int
    {
        [Description("By area")]
        ByArea = 0,

        [Description("By biomes")]
        ByBiomes = 1,

        [Description("By biomes-area")]
        ByBiomesArea = 2
    }

    public class Ranges
    {
        public double MaxLocalValue { get; private set; }

        public double MinWideValue { get; private set; }

        public Ranges(double a_MaxLocalValue, double a_MinWideValue)
        {
            MaxLocalValue = a_MaxLocalValue;
            MinWideValue = a_MinWideValue;
        }

        public GlobalDistributionEnum GetGlobalDistribution(double a_Value)
        {
            if (a_Value < 0)
                throw new ArgumentException("a_Value");
            if (a_Value <= MaxLocalValue)
                return GlobalDistributionEnum.Local;
            if (a_Value > MinWideValue)
                return GlobalDistributionEnum.Wide;
            return GlobalDistributionEnum.Regional;
        }
    }

    public class ResultInfo
    {
        #region Tuning properties

        public CalculationTypeEnum CalculationType { get; private set; }

        public Ranges GlobalDistributionRanges { get; private set; }

        public double DistributionCoefficient { get; private set; }

        public IUCNCategoryEnum Category { get; set; }

        #endregion Tuning properties

        #region Calculated by an outside properties

        public double SpeciesArea { get; set; }

        public int BiomesCount { get; set; }

        public double DistributionInReferenceArea { get; set; }

        public double TotalReferenceArea { get; set; }

        public double DistributionInFocalArea { get; set; }

        public double TotalFocalArea { get; set; }

        #endregion Calculated by an outside properties

        #region Display properties

        public string SpeciesTableName { get; set; }

        public string BiomesTableName { get; set; }

        public string RegionTableName { get; set; }

        public string FocalRegionTableName { get; set; }

        public int FocalRegionObjectId { get; set; }

        public string FocalRegionName { get; set; }

        #endregion Display properties

        public GlobalDistributionEnum GlobalDistribution { get; private set; }

        /// <summary>
        /// DPexp
        /// </summary>
        public double ExpectedDistributionProbability { get; private set; }

        /// <summary>
        /// DPobs
        /// </summary>
        public double ObservedDistributionProbability { get; private set; }

        public RegionalDistributionEnum RegionalDistribution { get; private set; }

        public PriorityEnum Priority { get; private set; }

        public ClassEnum Class { get; private set; }

        public byte Score { get; private set; }

        private static ClassEnum GetClassByScore(IUCNCategoryEnum a_Category, PriorityEnum a_Priority, int a_Score)
        {
            if (a_Category.IsOneOf(IUCNCategoryEnum.NotEvaluated, IUCNCategoryEnum.DataDeficient) || a_Priority == PriorityEnum.DataDeficientNotEvaluated)
                return ClassEnum.DataDeficientNotEvaluated;
            if (a_Score.IsOneOf(2, 3, 6, 7, 8, 9))
                return ClassEnum.Class4;
            if (a_Score.IsOneOf(10, 11, 12))
                return ClassEnum.Class3;
            if (a_Score.IsOneOf(13, 14, 15, 16, 17, 18))
                return ClassEnum.Class2;
            if (a_Score.IsOneOf(20, 22, 25))
                return ClassEnum.Class1;
            return ClassEnum.DataDeficientNotEvaluated;
        }

        private static ClassEnum GetClassByCategoryAndPriority(IUCNCategoryEnum a_Category, PriorityEnum a_Priority)
        {
            if (a_Priority == PriorityEnum.DataDeficientNotEvaluated)
                return ClassEnum.DataDeficientNotEvaluated;
            if (a_Category == IUCNCategoryEnum.ExtinctInTheWild)
            {
                if (a_Priority.IsOneOf(PriorityEnum.BasicPriority, PriorityEnum.MediumPriority)) return ClassEnum.Class2;
                return ClassEnum.Class1;
            }
            if (a_Category == IUCNCategoryEnum.CriticallyEndangered)
            {
                if (a_Priority == PriorityEnum.VeryHighPriority) return ClassEnum.Class1;
                return ClassEnum.Class2;
            }
            if (a_Category == IUCNCategoryEnum.Endangered)
            {
                if (a_Priority == PriorityEnum.VeryHighPriority) return ClassEnum.Class1;
                if (a_Priority == PriorityEnum.HighPriority) return ClassEnum.Class2;
                return ClassEnum.Class3;
            }
            if (a_Category == IUCNCategoryEnum.Vulnerable)
            {
                if (a_Priority.IsOneOf(PriorityEnum.VeryHighPriority, PriorityEnum.HighPriority)) return ClassEnum.Class2;
                if (a_Priority == PriorityEnum.MediumPriority) return ClassEnum.Class3;
                return ClassEnum.Class4;
            }
            if (a_Category == IUCNCategoryEnum.NearThreatened)
            {
                if (a_Priority == PriorityEnum.VeryHighPriority) return ClassEnum.Class2;
                if (a_Priority == PriorityEnum.HighPriority) return ClassEnum.Class3;
                return ClassEnum.Class4;
            }
            if (a_Category == IUCNCategoryEnum.LeastConcern)
            {
                if (a_Priority == PriorityEnum.VeryHighPriority) return ClassEnum.Class3;
                return ClassEnum.Class4;
            }
            if (a_Category == IUCNCategoryEnum.DataDeficient || a_Category == IUCNCategoryEnum.NotEvaluated)
            {
                if (a_Priority == PriorityEnum.VeryHighPriority) return ClassEnum.Class1;
                if (a_Priority == PriorityEnum.HighPriority) return ClassEnum.Class2;
                if (a_Priority == PriorityEnum.MediumPriority) return ClassEnum.Class3;
                if (a_Priority == PriorityEnum.BasicPriority) return ClassEnum.Class4;
            }
            throw new ArgumentException(string.Format("Unknown IUCN category: {0}", a_Category));
        }

        private static byte GetScoreByCategoryAndPriority(IUCNCategoryEnum a_Category, PriorityEnum a_Priority)
        {
            return (byte)(a_Category.GetEnumScore() + a_Priority.GetEnumScore());
        }

        private ResultInfo()
        {
            this.CalculationType = CalculationTypeEnum.ByArea;
            this.GlobalDistributionRanges = null;
            this.DistributionCoefficient = 0;
            this.Category = IUCNCategoryEnum.NotEvaluated;

            this.SpeciesTableName = string.Empty;
            this.BiomesTableName = string.Empty;
            this.RegionTableName = string.Empty;
            this.FocalRegionTableName = string.Empty;

            this.FocalRegionObjectId = -1;
            this.FocalRegionName = string.Empty;

            this.SpeciesArea = 0;
            this.BiomesCount = 0;

            this.Score = 0;
        }

        public ResultInfo(
            CalculationTypeEnum a_CalculationType, Ranges a_GlobalDistributionRanges, double a_DistributionCoefficient)
            : this()
        {
            Debug.Assert(a_GlobalDistributionRanges != null, "a_GlobalDistributionRanges == null");
            Debug.Assert(a_DistributionCoefficient > 0, "a_DistributionCoefficient <= 0");
            CalculationType = a_CalculationType;
            GlobalDistributionRanges = a_GlobalDistributionRanges;
            DistributionCoefficient = a_DistributionCoefficient;
        }

        public ResultInfo(ResultInfo a_ResultInfo)
            : this()
        {
            Debug.Assert(a_ResultInfo != null, "a_ResultInfo == null");

            this.CalculationType = a_ResultInfo.CalculationType;
            this.GlobalDistributionRanges = new Ranges(
                a_ResultInfo.GlobalDistributionRanges.MaxLocalValue, a_ResultInfo.GlobalDistributionRanges.MinWideValue);
            this.DistributionCoefficient = a_ResultInfo.DistributionCoefficient;
            this.Category = a_ResultInfo.Category;

            this.SpeciesArea = a_ResultInfo.SpeciesArea;
            this.DistributionInReferenceArea = a_ResultInfo.DistributionInReferenceArea;
            this.TotalReferenceArea = a_ResultInfo.TotalReferenceArea;

            this.DistributionInFocalArea = a_ResultInfo.DistributionInFocalArea;
            this.TotalFocalArea = a_ResultInfo.TotalFocalArea;

            this.SpeciesTableName = a_ResultInfo.SpeciesTableName;
            this.BiomesTableName = a_ResultInfo.BiomesTableName;
            this.RegionTableName = a_ResultInfo.RegionTableName;
            this.FocalRegionTableName = a_ResultInfo.FocalRegionTableName;

            this.FocalRegionObjectId = a_ResultInfo.FocalRegionObjectId;
            this.FocalRegionName = a_ResultInfo.FocalRegionName;

            this.GlobalDistribution = a_ResultInfo.GlobalDistribution;
            this.ExpectedDistributionProbability = a_ResultInfo.ExpectedDistributionProbability;
            this.ObservedDistributionProbability = a_ResultInfo.ObservedDistributionProbability;
            this.RegionalDistribution = a_ResultInfo.RegionalDistribution;
            this.Priority = a_ResultInfo.Priority;
            this.Class = a_ResultInfo.Class;
            this.Score = a_ResultInfo.Score;
        }

        public void CalculateGeneralProperties()
        {
            GlobalDistribution =
                GlobalDistributionRanges.GetGlobalDistribution(
                    CalculationType == CalculationTypeEnum.ByArea ? SpeciesArea : BiomesCount);

            try
            {
                ExpectedDistributionProbability = DistributionInReferenceArea / TotalReferenceArea;
            }
            catch (DivideByZeroException)
            {
                ExpectedDistributionProbability = 0;
            }
        }

        public void Calculate()
        {
            try
            {
                ObservedDistributionProbability = DistributionInFocalArea / TotalFocalArea;
            }
            catch (DivideByZeroException)
            {
                ObservedDistributionProbability = 0;
            }

            this.RegionalDistribution = this.ObservedDistributionProbability
                                        > this.DistributionCoefficient * this.ExpectedDistributionProbability
                                            ? RegionalDistributionEnum.High
                                            : RegionalDistributionEnum.Low;

            Priority =
                ((ObservedDistributionProbability > 0) ? (PriorityEnum)((short)GlobalDistribution + (short)RegionalDistribution) : PriorityEnum.DataDeficientNotEvaluated);

            Score = GetScoreByCategoryAndPriority(Category, Priority);
            Class = GetClassByCategoryAndPriority(Category, Priority);
            //Class = GetClassByScore(Category, Priority, Score);
        }
    }
}
