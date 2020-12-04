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

﻿namespace SpeciesTool.NET
{
    partial class SpeciesParametersWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpeciesParametersWindow));
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.btRefreshData = new System.Windows.Forms.ToolStripButton();
            this.tsbtFilterLayers = new System.Windows.Forms.ToolStripButton();
            this.ofdCsv = new System.Windows.Forms.OpenFileDialog();
            this.ofdShp = new System.Windows.Forms.OpenFileDialog();
            this.tcParameters = new System.Windows.Forms.TabControl();
            this.tpLayers = new System.Windows.Forms.TabPage();
            this.tblpanMain = new System.Windows.Forms.TableLayoutPanel();
            this.flwpanButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btAnalyze = new System.Windows.Forms.Button();
            this.btAnalizeResultShapeFile = new System.Windows.Forms.Button();
            this.btAddLayerFromResults = new System.Windows.Forms.Button();
            this.cbRegionsNameField = new System.Windows.Forms.ComboBox();
            this.lbRegionsNameField = new System.Windows.Forms.Label();
            this.lbSpeciesLayers = new System.Windows.Forms.Label();
            this.lbBiomesLayer = new System.Windows.Forms.Label();
            this.cbBiomesLayer = new System.Windows.Forms.ComboBox();
            this.cbFocRegionsNameField = new System.Windows.Forms.ComboBox();
            this.lbFocRegionsNameField = new System.Windows.Forms.Label();
            this.cbBiomesNameField = new System.Windows.Forms.ComboBox();
            this.lbBiomesNamesField = new System.Windows.Forms.Label();
            this.rbUseAllLayer = new System.Windows.Forms.RadioButton();
            this.lbAllLayer = new System.Windows.Forms.Label();
            this.cbFocalRegionLayer = new System.Windows.Forms.ComboBox();
            this.lbFocalRegionLayer = new System.Windows.Forms.Label();
            this.lbUseSelectedValues = new System.Windows.Forms.Label();
            this.cbRegionsLayer = new System.Windows.Forms.ComboBox();
            this.lbRegionsLayer = new System.Windows.Forms.Label();
            this.libFocalRegionValues = new System.Windows.Forms.ListBox();
            this.rbUseSelectedValues = new System.Windows.Forms.RadioButton();
            this.libSpeciesLayers = new System.Windows.Forms.ListBox();
            this.tpParameters = new System.Windows.Forms.TabPage();
            this.tblpanDistributionParameters = new System.Windows.Forms.TableLayoutPanel();
            this.chbOverwriteResult = new System.Windows.Forms.CheckBox();
            this.lbOverwriteExisting = new System.Windows.Forms.Label();
            this.panIUCNCategoriesPath = new System.Windows.Forms.Panel();
            this.tbIUCNCategoriesPath = new System.Windows.Forms.TextBox();
            this.btBrowseIUCNCategoriesPath = new System.Windows.Forms.Button();
            this.lbIUCNCategoriesPath = new System.Windows.Forms.Label();
            this.lbCalculationType = new System.Windows.Forms.Label();
            this.lbResultPath = new System.Windows.Forms.Label();
            this.lbAddThematicLayers = new System.Windows.Forms.Label();
            this.nudDistrCoeff = new System.Windows.Forms.NumericUpDown();
            this.lbDistrCoeff = new System.Windows.Forms.Label();
            this.nudLocal = new System.Windows.Forms.NumericUpDown();
            this.lbWide = new System.Windows.Forms.Label();
            this.lbRegional = new System.Windows.Forms.Label();
            this.lbLocal = new System.Windows.Forms.Label();
            this.nudWide = new System.Windows.Forms.NumericUpDown();
            this.tbRegional = new System.Windows.Forms.TextBox();
            this.chbAddThematicLayers = new System.Windows.Forms.CheckBox();
            this.panResultPath = new System.Windows.Forms.Panel();
            this.tbResultPath = new System.Windows.Forms.TextBox();
            this.btBrowse = new System.Windows.Forms.Button();
            this.cbCalculationType = new System.Windows.Forms.ComboBox();
            this.tpResults = new System.Windows.Forms.TabPage();
            this.tblpanParameters = new System.Windows.Forms.TableLayoutPanel();
            this.rtbResults = new System.Windows.Forms.RichTextBox();
            this.btClear = new System.Windows.Forms.Button();
            this.btAnalyzeNRCP = new System.Windows.Forms.Button();
            this.tsMenu.SuspendLayout();
            this.tcParameters.SuspendLayout();
            this.tpLayers.SuspendLayout();
            this.tblpanMain.SuspendLayout();
            this.flwpanButtons.SuspendLayout();
            this.tpParameters.SuspendLayout();
            this.tblpanDistributionParameters.SuspendLayout();
            this.panIUCNCategoriesPath.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDistrCoeff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLocal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWide)).BeginInit();
            this.panResultPath.SuspendLayout();
            this.tpResults.SuspendLayout();
            this.tblpanParameters.SuspendLayout();
            this.SuspendLayout();
            //
            // tsMenu
            //
            this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btRefreshData,
            this.tsbtFilterLayers});
            this.tsMenu.Location = new System.Drawing.Point(0, 0);
            this.tsMenu.Name = "tsMenu";
            this.tsMenu.Size = new System.Drawing.Size(554, 25);
            this.tsMenu.TabIndex = 1;
            this.tsMenu.Text = "toolStrip1";
            //
            // btRefreshData
            //
            this.btRefreshData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btRefreshData.Image = ((System.Drawing.Image)(resources.GetObject("btRefreshData.Image")));
            this.btRefreshData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btRefreshData.Name = "btRefreshData";
            this.btRefreshData.Size = new System.Drawing.Size(77, 22);
            this.btRefreshData.Text = "Refresh Data";
            this.btRefreshData.Click += new System.EventHandler(this.miRefresh_Click);
            //
            // tsbtFilterLayers
            //
            this.tsbtFilterLayers.Image = ((System.Drawing.Image)(resources.GetObject("tsbtFilterLayers.Image")));
            this.tsbtFilterLayers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtFilterLayers.Name = "tsbtFilterLayers";
            this.tsbtFilterLayers.Size = new System.Drawing.Size(86, 22);
            this.tsbtFilterLayers.Text = "Filter layers";
            this.tsbtFilterLayers.Click += new System.EventHandler(this.tsbtFilterLayers_Click);
            //
            // ofdCsv
            //
            this.ofdCsv.DefaultExt = "csv";
            this.ofdCsv.FileName = "IUCN.csv";
            this.ofdCsv.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            this.ofdCsv.SupportMultiDottedExtensions = true;
            //
            // ofdShp
            //
            this.ofdShp.CheckFileExists = false;
            this.ofdShp.DefaultExt = "shp";
            this.ofdShp.FileName = "NRAResults.shp";
            this.ofdShp.Filter = "Shape files (*.shp)|*.shp|All files (*.*)|*.*";
            this.ofdShp.SupportMultiDottedExtensions = true;
            //
            // tcParameters
            //
            this.tcParameters.Controls.Add(this.tpLayers);
            this.tcParameters.Controls.Add(this.tpParameters);
            this.tcParameters.Controls.Add(this.tpResults);
            this.tcParameters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcParameters.Location = new System.Drawing.Point(0, 25);
            this.tcParameters.Name = "tcParameters";
            this.tcParameters.SelectedIndex = 0;
            this.tcParameters.Size = new System.Drawing.Size(554, 396);
            this.tcParameters.TabIndex = 21;
            //
            // tpLayers
            //
            this.tpLayers.Controls.Add(this.tblpanMain);
            this.tpLayers.Location = new System.Drawing.Point(4, 22);
            this.tpLayers.Name = "tpLayers";
            this.tpLayers.Padding = new System.Windows.Forms.Padding(3);
            this.tpLayers.Size = new System.Drawing.Size(546, 370);
            this.tpLayers.TabIndex = 0;
            this.tpLayers.Text = "Layers";
            this.tpLayers.UseVisualStyleBackColor = true;
            //
            // tblpanMain
            //
            this.tblpanMain.ColumnCount = 3;
            this.tblpanMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblpanMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tblpanMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblpanMain.Controls.Add(this.flwpanButtons, 0, 9);
            this.tblpanMain.Controls.Add(this.cbRegionsNameField, 1, 4);
            this.tblpanMain.Controls.Add(this.lbRegionsNameField, 0, 4);
            this.tblpanMain.Controls.Add(this.lbSpeciesLayers, 0, 0);
            this.tblpanMain.Controls.Add(this.lbBiomesLayer, 0, 1);
            this.tblpanMain.Controls.Add(this.cbBiomesLayer, 1, 1);
            this.tblpanMain.Controls.Add(this.cbFocRegionsNameField, 1, 6);
            this.tblpanMain.Controls.Add(this.lbFocRegionsNameField, 0, 6);
            this.tblpanMain.Controls.Add(this.cbBiomesNameField, 2, 2);
            this.tblpanMain.Controls.Add(this.lbBiomesNamesField, 0, 2);
            this.tblpanMain.Controls.Add(this.rbUseAllLayer, 0, 7);
            this.tblpanMain.Controls.Add(this.lbAllLayer, 1, 7);
            this.tblpanMain.Controls.Add(this.cbFocalRegionLayer, 2, 5);
            this.tblpanMain.Controls.Add(this.lbFocalRegionLayer, 0, 5);
            this.tblpanMain.Controls.Add(this.lbUseSelectedValues, 1, 8);
            this.tblpanMain.Controls.Add(this.cbRegionsLayer, 2, 3);
            this.tblpanMain.Controls.Add(this.lbRegionsLayer, 0, 3);
            this.tblpanMain.Controls.Add(this.libFocalRegionValues, 2, 8);
            this.tblpanMain.Controls.Add(this.rbUseSelectedValues, 0, 8);
            this.tblpanMain.Controls.Add(this.libSpeciesLayers, 2, 0);
            this.tblpanMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblpanMain.Location = new System.Drawing.Point(3, 3);
            this.tblpanMain.Name = "tblpanMain";
            this.tblpanMain.RowCount = 10;
            this.tblpanMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 63F));
            this.tblpanMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tblpanMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tblpanMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tblpanMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tblpanMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tblpanMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tblpanMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tblpanMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblpanMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tblpanMain.Size = new System.Drawing.Size(540, 364);
            this.tblpanMain.TabIndex = 1;
            //
            // flwpanButtons
            //
            this.tblpanMain.SetColumnSpan(this.flwpanButtons, 3);
            this.flwpanButtons.Controls.Add(this.btAnalyze);
            this.flwpanButtons.Controls.Add(this.btAnalizeResultShapeFile);
            this.flwpanButtons.Controls.Add(this.btAnalyzeNRCP);
            this.flwpanButtons.Controls.Add(this.btAddLayerFromResults);
            this.flwpanButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flwpanButtons.Location = new System.Drawing.Point(0, 324);
            this.flwpanButtons.Margin = new System.Windows.Forms.Padding(0);
            this.flwpanButtons.Name = "flwpanButtons";
            this.flwpanButtons.Size = new System.Drawing.Size(540, 40);
            this.flwpanButtons.TabIndex = 20;
            //
            // btAnalyze
            //
            this.btAnalyze.Location = new System.Drawing.Point(1, 1);
            this.btAnalyze.Margin = new System.Windows.Forms.Padding(1);
            this.btAnalyze.Name = "btAnalyze";
            this.btAnalyze.Size = new System.Drawing.Size(100, 24);
            this.btAnalyze.TabIndex = 0;
            this.btAnalyze.Text = "Analyze";
            this.btAnalyze.UseVisualStyleBackColor = true;
            this.btAnalyze.Click += new System.EventHandler(this.btAnalyze_Click);
            //
            // btAnalizeResultShapeFile
            //
            this.btAnalizeResultShapeFile.Location = new System.Drawing.Point(103, 1);
            this.btAnalizeResultShapeFile.Margin = new System.Windows.Forms.Padding(1);
            this.btAnalizeResultShapeFile.Name = "btAnalizeResultShapeFile";
            this.btAnalizeResultShapeFile.Size = new System.Drawing.Size(100, 24);
            this.btAnalizeResultShapeFile.TabIndex = 1;
            this.btAnalizeResultShapeFile.Text = "Analyze results";
            this.btAnalizeResultShapeFile.UseVisualStyleBackColor = true;
            this.btAnalizeResultShapeFile.Click += new System.EventHandler(this.btAnalizeResultShapeFile_Click);
            //
            // btAddLayerFromResults
            //
            this.btAddLayerFromResults.Location = new System.Drawing.Point(351, 1);
            this.btAddLayerFromResults.Margin = new System.Windows.Forms.Padding(1);
            this.btAddLayerFromResults.Name = "btAddLayerFromResults";
            this.btAddLayerFromResults.Size = new System.Drawing.Size(140, 24);
            this.btAddLayerFromResults.TabIndex = 3;
            this.btAddLayerFromResults.Text = "Add layers from results";
            this.btAddLayerFromResults.UseVisualStyleBackColor = true;
            this.btAddLayerFromResults.Click += new System.EventHandler(this.btAddLayerFromResults_Click);
            //
            // cbRegionsNameField
            //
            this.cbRegionsNameField.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbRegionsNameField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRegionsNameField.FormattingEnabled = true;
            this.cbRegionsNameField.Location = new System.Drawing.Point(156, 133);
            this.cbRegionsNameField.Margin = new System.Windows.Forms.Padding(1);
            this.cbRegionsNameField.Name = "cbRegionsNameField";
            this.cbRegionsNameField.Size = new System.Drawing.Size(383, 21);
            this.cbRegionsNameField.TabIndex = 9;
            //
            // lbRegionsNameField
            //
            this.tblpanMain.SetColumnSpan(this.lbRegionsNameField, 2);
            this.lbRegionsNameField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbRegionsNameField.Location = new System.Drawing.Point(3, 135);
            this.lbRegionsNameField.Margin = new System.Windows.Forms.Padding(3);
            this.lbRegionsNameField.Name = "lbRegionsNameField";
            this.lbRegionsNameField.Size = new System.Drawing.Size(149, 17);
            this.lbRegionsNameField.TabIndex = 8;
            this.lbRegionsNameField.Text = "\'Name\' field for ref. regions";
            this.lbRegionsNameField.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lbSpeciesLayers
            //
            this.tblpanMain.SetColumnSpan(this.lbSpeciesLayers, 2);
            this.lbSpeciesLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSpeciesLayers.Location = new System.Drawing.Point(3, 3);
            this.lbSpeciesLayers.Margin = new System.Windows.Forms.Padding(3);
            this.lbSpeciesLayers.Name = "lbSpeciesLayers";
            this.lbSpeciesLayers.Size = new System.Drawing.Size(149, 57);
            this.lbSpeciesLayers.TabIndex = 0;
            this.lbSpeciesLayers.Text = "Species layers";
            this.lbSpeciesLayers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lbBiomesLayer
            //
            this.tblpanMain.SetColumnSpan(this.lbBiomesLayer, 2);
            this.lbBiomesLayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbBiomesLayer.Location = new System.Drawing.Point(3, 66);
            this.lbBiomesLayer.Margin = new System.Windows.Forms.Padding(3);
            this.lbBiomesLayer.Name = "lbBiomesLayer";
            this.lbBiomesLayer.Size = new System.Drawing.Size(149, 17);
            this.lbBiomesLayer.TabIndex = 2;
            this.lbBiomesLayer.Text = "Biomes layer";
            this.lbBiomesLayer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // cbBiomesLayer
            //
            this.cbBiomesLayer.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbBiomesLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBiomesLayer.FormattingEnabled = true;
            this.cbBiomesLayer.Location = new System.Drawing.Point(156, 64);
            this.cbBiomesLayer.Margin = new System.Windows.Forms.Padding(1);
            this.cbBiomesLayer.Name = "cbBiomesLayer";
            this.cbBiomesLayer.Size = new System.Drawing.Size(383, 21);
            this.cbBiomesLayer.TabIndex = 3;
            this.cbBiomesLayer.SelectedIndexChanged += new System.EventHandler(this.cbBiomesLayer_SelectedIndexChanged);
            //
            // cbFocRegionsNameField
            //
            this.cbFocRegionsNameField.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbFocRegionsNameField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFocRegionsNameField.FormattingEnabled = true;
            this.cbFocRegionsNameField.Location = new System.Drawing.Point(156, 179);
            this.cbFocRegionsNameField.Margin = new System.Windows.Forms.Padding(1);
            this.cbFocRegionsNameField.Name = "cbFocRegionsNameField";
            this.cbFocRegionsNameField.Size = new System.Drawing.Size(383, 21);
            this.cbFocRegionsNameField.TabIndex = 13;
            this.cbFocRegionsNameField.SelectedIndexChanged += new System.EventHandler(this.cbRefRegionsNameField_SelectedIndexChanged);
            //
            // lbFocRegionsNameField
            //
            this.tblpanMain.SetColumnSpan(this.lbFocRegionsNameField, 2);
            this.lbFocRegionsNameField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbFocRegionsNameField.Location = new System.Drawing.Point(3, 181);
            this.lbFocRegionsNameField.Margin = new System.Windows.Forms.Padding(3);
            this.lbFocRegionsNameField.Name = "lbFocRegionsNameField";
            this.lbFocRegionsNameField.Size = new System.Drawing.Size(149, 17);
            this.lbFocRegionsNameField.TabIndex = 12;
            this.lbFocRegionsNameField.Text = "\'Name\' field for foc. regions";
            this.lbFocRegionsNameField.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // cbBiomesNameField
            //
            this.cbBiomesNameField.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbBiomesNameField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBiomesNameField.FormattingEnabled = true;
            this.cbBiomesNameField.Location = new System.Drawing.Point(156, 87);
            this.cbBiomesNameField.Margin = new System.Windows.Forms.Padding(1);
            this.cbBiomesNameField.Name = "cbBiomesNameField";
            this.cbBiomesNameField.Size = new System.Drawing.Size(383, 21);
            this.cbBiomesNameField.TabIndex = 5;
            //
            // lbBiomesNamesField
            //
            this.tblpanMain.SetColumnSpan(this.lbBiomesNamesField, 2);
            this.lbBiomesNamesField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbBiomesNamesField.Location = new System.Drawing.Point(3, 89);
            this.lbBiomesNamesField.Margin = new System.Windows.Forms.Padding(3);
            this.lbBiomesNamesField.Name = "lbBiomesNamesField";
            this.lbBiomesNamesField.Size = new System.Drawing.Size(149, 17);
            this.lbBiomesNamesField.TabIndex = 4;
            this.lbBiomesNamesField.Text = "\'Name\' field for biomes";
            this.lbBiomesNamesField.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // rbUseAllLayer
            //
            this.rbUseAllLayer.AutoSize = true;
            this.rbUseAllLayer.Location = new System.Drawing.Point(3, 204);
            this.rbUseAllLayer.Name = "rbUseAllLayer";
            this.rbUseAllLayer.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rbUseAllLayer.Size = new System.Drawing.Size(19, 17);
            this.rbUseAllLayer.TabIndex = 14;
            this.rbUseAllLayer.Text = " ";
            this.rbUseAllLayer.UseVisualStyleBackColor = true;
            this.rbUseAllLayer.CheckedChanged += new System.EventHandler(this.UsedValuesCheckedChanged);
            //
            // lbAllLayer
            //
            this.lbAllLayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbAllLayer.Location = new System.Drawing.Point(28, 204);
            this.lbAllLayer.Margin = new System.Windows.Forms.Padding(3);
            this.lbAllLayer.Name = "lbAllLayer";
            this.lbAllLayer.Size = new System.Drawing.Size(124, 17);
            this.lbAllLayer.TabIndex = 15;
            this.lbAllLayer.Text = "All values";
            this.lbAllLayer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbAllLayer.Click += new System.EventHandler(this.lbAllLayer_Click);
            //
            // cbFocalRegionLayer
            //
            this.cbFocalRegionLayer.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbFocalRegionLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFocalRegionLayer.FormattingEnabled = true;
            this.cbFocalRegionLayer.Location = new System.Drawing.Point(156, 156);
            this.cbFocalRegionLayer.Margin = new System.Windows.Forms.Padding(1);
            this.cbFocalRegionLayer.Name = "cbFocalRegionLayer";
            this.cbFocalRegionLayer.Size = new System.Drawing.Size(383, 21);
            this.cbFocalRegionLayer.TabIndex = 11;
            this.cbFocalRegionLayer.SelectedIndexChanged += new System.EventHandler(this.cbFocalRegionLayer_SelectedIndexChanged);
            //
            // lbFocalRegionLayer
            //
            this.tblpanMain.SetColumnSpan(this.lbFocalRegionLayer, 2);
            this.lbFocalRegionLayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbFocalRegionLayer.Location = new System.Drawing.Point(3, 158);
            this.lbFocalRegionLayer.Margin = new System.Windows.Forms.Padding(3);
            this.lbFocalRegionLayer.Name = "lbFocalRegionLayer";
            this.lbFocalRegionLayer.Size = new System.Drawing.Size(149, 17);
            this.lbFocalRegionLayer.TabIndex = 10;
            this.lbFocalRegionLayer.Text = "Focal region layer";
            this.lbFocalRegionLayer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lbUseSelectedValues
            //
            this.lbUseSelectedValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbUseSelectedValues.Location = new System.Drawing.Point(28, 227);
            this.lbUseSelectedValues.Margin = new System.Windows.Forms.Padding(3);
            this.lbUseSelectedValues.Name = "lbUseSelectedValues";
            this.lbUseSelectedValues.Size = new System.Drawing.Size(124, 94);
            this.lbUseSelectedValues.TabIndex = 17;
            this.lbUseSelectedValues.Text = "Selected values";
            this.lbUseSelectedValues.Click += new System.EventHandler(this.lbUseSelectedValues_Click);
            //
            // cbRegionsLayer
            //
            this.cbRegionsLayer.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbRegionsLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRegionsLayer.FormattingEnabled = true;
            this.cbRegionsLayer.Location = new System.Drawing.Point(156, 110);
            this.cbRegionsLayer.Margin = new System.Windows.Forms.Padding(1);
            this.cbRegionsLayer.Name = "cbRegionsLayer";
            this.cbRegionsLayer.Size = new System.Drawing.Size(383, 21);
            this.cbRegionsLayer.TabIndex = 7;
            this.cbRegionsLayer.SelectedIndexChanged += new System.EventHandler(this.cbRegionsLayer_SelectedIndexChanged);
            //
            // lbRegionsLayer
            //
            this.tblpanMain.SetColumnSpan(this.lbRegionsLayer, 2);
            this.lbRegionsLayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbRegionsLayer.Location = new System.Drawing.Point(3, 112);
            this.lbRegionsLayer.Margin = new System.Windows.Forms.Padding(3);
            this.lbRegionsLayer.Name = "lbRegionsLayer";
            this.lbRegionsLayer.Size = new System.Drawing.Size(149, 17);
            this.lbRegionsLayer.TabIndex = 6;
            this.lbRegionsLayer.Text = "Reference region layer";
            this.lbRegionsLayer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // libFocalRegionValues
            //
            this.libFocalRegionValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.libFocalRegionValues.FormattingEnabled = true;
            this.libFocalRegionValues.IntegralHeight = false;
            this.libFocalRegionValues.Location = new System.Drawing.Point(156, 225);
            this.libFocalRegionValues.Margin = new System.Windows.Forms.Padding(1);
            this.libFocalRegionValues.Name = "libFocalRegionValues";
            this.libFocalRegionValues.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.libFocalRegionValues.Size = new System.Drawing.Size(383, 98);
            this.libFocalRegionValues.TabIndex = 18;
            //
            // rbUseSelectedValues
            //
            this.rbUseSelectedValues.AutoSize = true;
            this.rbUseSelectedValues.Location = new System.Drawing.Point(3, 227);
            this.rbUseSelectedValues.Name = "rbUseSelectedValues";
            this.rbUseSelectedValues.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rbUseSelectedValues.Size = new System.Drawing.Size(19, 17);
            this.rbUseSelectedValues.TabIndex = 16;
            this.rbUseSelectedValues.Text = " ";
            this.rbUseSelectedValues.UseVisualStyleBackColor = true;
            this.rbUseSelectedValues.Click += new System.EventHandler(this.UsedValuesCheckedChanged);
            //
            // libSpeciesLayers
            //
            this.libSpeciesLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.libSpeciesLayers.FormattingEnabled = true;
            this.libSpeciesLayers.IntegralHeight = false;
            this.libSpeciesLayers.Location = new System.Drawing.Point(156, 1);
            this.libSpeciesLayers.Margin = new System.Windows.Forms.Padding(1);
            this.libSpeciesLayers.Name = "libSpeciesLayers";
            this.libSpeciesLayers.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.libSpeciesLayers.Size = new System.Drawing.Size(383, 61);
            this.libSpeciesLayers.TabIndex = 1;
            //
            // tpParameters
            //
            this.tpParameters.Controls.Add(this.tblpanDistributionParameters);
            this.tpParameters.Location = new System.Drawing.Point(4, 22);
            this.tpParameters.Name = "tpParameters";
            this.tpParameters.Padding = new System.Windows.Forms.Padding(3);
            this.tpParameters.Size = new System.Drawing.Size(546, 370);
            this.tpParameters.TabIndex = 1;
            this.tpParameters.Text = "Parameters";
            this.tpParameters.UseVisualStyleBackColor = true;
            //
            // tblpanDistributionParameters
            //
            this.tblpanDistributionParameters.AutoSize = true;
            this.tblpanDistributionParameters.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblpanDistributionParameters.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tblpanDistributionParameters.ColumnCount = 3;
            this.tblpanDistributionParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33332F));
            this.tblpanDistributionParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tblpanDistributionParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tblpanDistributionParameters.Controls.Add(this.chbOverwriteResult, 1, 7);
            this.tblpanDistributionParameters.Controls.Add(this.lbOverwriteExisting, 0, 7);
            this.tblpanDistributionParameters.Controls.Add(this.panIUCNCategoriesPath, 1, 4);
            this.tblpanDistributionParameters.Controls.Add(this.lbIUCNCategoriesPath, 0, 4);
            this.tblpanDistributionParameters.Controls.Add(this.lbCalculationType, 0, 3);
            this.tblpanDistributionParameters.Controls.Add(this.lbResultPath, 0, 6);
            this.tblpanDistributionParameters.Controls.Add(this.lbAddThematicLayers, 0, 5);
            this.tblpanDistributionParameters.Controls.Add(this.nudDistrCoeff, 1, 2);
            this.tblpanDistributionParameters.Controls.Add(this.lbDistrCoeff, 0, 2);
            this.tblpanDistributionParameters.Controls.Add(this.nudLocal, 0, 1);
            this.tblpanDistributionParameters.Controls.Add(this.lbWide, 2, 0);
            this.tblpanDistributionParameters.Controls.Add(this.lbRegional, 1, 0);
            this.tblpanDistributionParameters.Controls.Add(this.lbLocal, 0, 0);
            this.tblpanDistributionParameters.Controls.Add(this.nudWide, 2, 1);
            this.tblpanDistributionParameters.Controls.Add(this.tbRegional, 1, 1);
            this.tblpanDistributionParameters.Controls.Add(this.chbAddThematicLayers, 1, 5);
            this.tblpanDistributionParameters.Controls.Add(this.panResultPath, 1, 6);
            this.tblpanDistributionParameters.Controls.Add(this.cbCalculationType, 1, 3);
            this.tblpanDistributionParameters.Dock = System.Windows.Forms.DockStyle.Top;
            this.tblpanDistributionParameters.ForeColor = System.Drawing.Color.Black;
            this.tblpanDistributionParameters.Location = new System.Drawing.Point(3, 3);
            this.tblpanDistributionParameters.Name = "tblpanDistributionParameters";
            this.tblpanDistributionParameters.RowCount = 8;
            this.tblpanDistributionParameters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tblpanDistributionParameters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tblpanDistributionParameters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tblpanDistributionParameters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tblpanDistributionParameters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tblpanDistributionParameters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tblpanDistributionParameters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tblpanDistributionParameters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tblpanDistributionParameters.Size = new System.Drawing.Size(540, 217);
            this.tblpanDistributionParameters.TabIndex = 1;
            //
            // chbOverwriteResult
            //
            this.chbOverwriteResult.Checked = true;
            this.chbOverwriteResult.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbOverwriteResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chbOverwriteResult.Location = new System.Drawing.Point(183, 193);
            this.chbOverwriteResult.Name = "chbOverwriteResult";
            this.chbOverwriteResult.Size = new System.Drawing.Size(172, 20);
            this.chbOverwriteResult.TabIndex = 17;
            this.chbOverwriteResult.Text = " ";
            this.chbOverwriteResult.UseVisualStyleBackColor = true;
            //
            // lbOverwriteExisting
            //
            this.lbOverwriteExisting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbOverwriteExisting.Location = new System.Drawing.Point(4, 193);
            this.lbOverwriteExisting.Margin = new System.Windows.Forms.Padding(3);
            this.lbOverwriteExisting.Name = "lbOverwriteExisting";
            this.lbOverwriteExisting.Size = new System.Drawing.Size(172, 20);
            this.lbOverwriteExisting.TabIndex = 16;
            this.lbOverwriteExisting.Text = "Overwrite the result";
            this.lbOverwriteExisting.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // panIUCNCategoriesPath
            //
            this.tblpanDistributionParameters.SetColumnSpan(this.panIUCNCategoriesPath, 2);
            this.panIUCNCategoriesPath.Controls.Add(this.tbIUCNCategoriesPath);
            this.panIUCNCategoriesPath.Controls.Add(this.btBrowseIUCNCategoriesPath);
            this.panIUCNCategoriesPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panIUCNCategoriesPath.Location = new System.Drawing.Point(183, 112);
            this.panIUCNCategoriesPath.Name = "panIUCNCategoriesPath";
            this.panIUCNCategoriesPath.Size = new System.Drawing.Size(353, 20);
            this.panIUCNCategoriesPath.TabIndex = 9;
            //
            // tbIUCNCategoriesPath
            //
            this.tbIUCNCategoriesPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tbIUCNCategoriesPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.tbIUCNCategoriesPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbIUCNCategoriesPath.Location = new System.Drawing.Point(0, 0);
            this.tbIUCNCategoriesPath.Name = "tbIUCNCategoriesPath";
            this.tbIUCNCategoriesPath.Size = new System.Drawing.Size(329, 21);
            this.tbIUCNCategoriesPath.TabIndex = 0;
            //
            // btBrowseIUCNCategoriesPath
            //
            this.btBrowseIUCNCategoriesPath.Dock = System.Windows.Forms.DockStyle.Right;
            this.btBrowseIUCNCategoriesPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btBrowseIUCNCategoriesPath.Location = new System.Drawing.Point(329, 0);
            this.btBrowseIUCNCategoriesPath.Margin = new System.Windows.Forms.Padding(0);
            this.btBrowseIUCNCategoriesPath.Name = "btBrowseIUCNCategoriesPath";
            this.btBrowseIUCNCategoriesPath.Size = new System.Drawing.Size(24, 20);
            this.btBrowseIUCNCategoriesPath.TabIndex = 0;
            this.btBrowseIUCNCategoriesPath.Text = "...";
            this.btBrowseIUCNCategoriesPath.UseVisualStyleBackColor = true;
            this.btBrowseIUCNCategoriesPath.Click += new System.EventHandler(this.btBrowseIUCNCategoriesPath_Click);
            //
            // lbIUCNCategoriesPath
            //
            this.lbIUCNCategoriesPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbIUCNCategoriesPath.Location = new System.Drawing.Point(4, 112);
            this.lbIUCNCategoriesPath.Margin = new System.Windows.Forms.Padding(3);
            this.lbIUCNCategoriesPath.Name = "lbIUCNCategoriesPath";
            this.lbIUCNCategoriesPath.Size = new System.Drawing.Size(172, 20);
            this.lbIUCNCategoriesPath.TabIndex = 8;
            this.lbIUCNCategoriesPath.Text = "IUCN categories";
            this.lbIUCNCategoriesPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lbCalculationType
            //
            this.lbCalculationType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbCalculationType.Location = new System.Drawing.Point(4, 85);
            this.lbCalculationType.Margin = new System.Windows.Forms.Padding(3);
            this.lbCalculationType.Name = "lbCalculationType";
            this.lbCalculationType.Size = new System.Drawing.Size(172, 20);
            this.lbCalculationType.TabIndex = 6;
            this.lbCalculationType.Text = "Calculation type";
            this.lbCalculationType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lbResultPath
            //
            this.lbResultPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbResultPath.Location = new System.Drawing.Point(4, 166);
            this.lbResultPath.Margin = new System.Windows.Forms.Padding(3);
            this.lbResultPath.Name = "lbResultPath";
            this.lbResultPath.Size = new System.Drawing.Size(172, 20);
            this.lbResultPath.TabIndex = 12;
            this.lbResultPath.Text = "Save results to";
            this.lbResultPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lbAddThematicLayers
            //
            this.lbAddThematicLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbAddThematicLayers.Location = new System.Drawing.Point(4, 139);
            this.lbAddThematicLayers.Margin = new System.Windows.Forms.Padding(3);
            this.lbAddThematicLayers.Name = "lbAddThematicLayers";
            this.lbAddThematicLayers.Size = new System.Drawing.Size(172, 20);
            this.lbAddThematicLayers.TabIndex = 10;
            this.lbAddThematicLayers.Text = "Add thematic layers";
            this.lbAddThematicLayers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // nudDistrCoeff
            //
            this.nudDistrCoeff.DecimalPlaces = 2;
            this.nudDistrCoeff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudDistrCoeff.Location = new System.Drawing.Point(183, 58);
            this.nudDistrCoeff.Maximum = new decimal(new int[] {
            705032704,
            1,
            0,
            0});
            this.nudDistrCoeff.Name = "nudDistrCoeff";
            this.nudDistrCoeff.Size = new System.Drawing.Size(172, 21);
            this.nudDistrCoeff.TabIndex = 5;
            this.nudDistrCoeff.ThousandsSeparator = true;
            this.nudDistrCoeff.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            //
            // lbDistrCoeff
            //
            this.lbDistrCoeff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbDistrCoeff.Location = new System.Drawing.Point(4, 58);
            this.lbDistrCoeff.Margin = new System.Windows.Forms.Padding(3);
            this.lbDistrCoeff.Name = "lbDistrCoeff";
            this.lbDistrCoeff.Size = new System.Drawing.Size(172, 20);
            this.lbDistrCoeff.TabIndex = 4;
            this.lbDistrCoeff.Text = "Distribution coeff.";
            this.lbDistrCoeff.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // nudLocal
            //
            this.nudLocal.DecimalPlaces = 2;
            this.nudLocal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudLocal.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudLocal.Location = new System.Drawing.Point(4, 31);
            this.nudLocal.Maximum = new decimal(new int[] {
            705032704,
            1,
            0,
            0});
            this.nudLocal.Name = "nudLocal";
            this.nudLocal.Size = new System.Drawing.Size(172, 21);
            this.nudLocal.TabIndex = 1;
            this.nudLocal.ThousandsSeparator = true;
            this.nudLocal.Value = new decimal(new int[] {
            500000,
            0,
            0,
            0});
            this.nudLocal.ValueChanged += new System.EventHandler(this.nudWide_ValueChanged);
            //
            // lbWide
            //
            this.lbWide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbWide.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbWide.Location = new System.Drawing.Point(362, 4);
            this.lbWide.Margin = new System.Windows.Forms.Padding(3);
            this.lbWide.Name = "lbWide";
            this.lbWide.Size = new System.Drawing.Size(174, 20);
            this.lbWide.TabIndex = 0;
            this.lbWide.Text = "Wide(>)";
            this.lbWide.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lbRegional
            //
            this.lbRegional.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbRegional.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbRegional.Location = new System.Drawing.Point(183, 4);
            this.lbRegional.Margin = new System.Windows.Forms.Padding(3);
            this.lbRegional.Name = "lbRegional";
            this.lbRegional.Size = new System.Drawing.Size(172, 20);
            this.lbRegional.TabIndex = 15;
            this.lbRegional.Text = "Regional";
            this.lbRegional.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lbLocal
            //
            this.lbLocal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbLocal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbLocal.Location = new System.Drawing.Point(4, 4);
            this.lbLocal.Margin = new System.Windows.Forms.Padding(3);
            this.lbLocal.Name = "lbLocal";
            this.lbLocal.Size = new System.Drawing.Size(172, 20);
            this.lbLocal.TabIndex = 14;
            this.lbLocal.Text = "Local(<=)";
            this.lbLocal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // nudWide
            //
            this.nudWide.DecimalPlaces = 2;
            this.nudWide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudWide.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudWide.Location = new System.Drawing.Point(362, 31);
            this.nudWide.Maximum = new decimal(new int[] {
            705032704,
            1,
            0,
            0});
            this.nudWide.Name = "nudWide";
            this.nudWide.Size = new System.Drawing.Size(174, 21);
            this.nudWide.TabIndex = 3;
            this.nudWide.ThousandsSeparator = true;
            this.nudWide.Value = new decimal(new int[] {
            50000000,
            0,
            0,
            0});
            this.nudWide.ValueChanged += new System.EventHandler(this.nudWide_ValueChanged);
            //
            // tbRegional
            //
            this.tbRegional.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbRegional.Location = new System.Drawing.Point(183, 31);
            this.tbRegional.Name = "tbRegional";
            this.tbRegional.ReadOnly = true;
            this.tbRegional.Size = new System.Drawing.Size(172, 21);
            this.tbRegional.TabIndex = 2;
            //
            // chbAddThematicLayers
            //
            this.chbAddThematicLayers.Checked = true;
            this.chbAddThematicLayers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbAddThematicLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chbAddThematicLayers.Location = new System.Drawing.Point(183, 139);
            this.chbAddThematicLayers.Name = "chbAddThematicLayers";
            this.chbAddThematicLayers.Size = new System.Drawing.Size(172, 20);
            this.chbAddThematicLayers.TabIndex = 11;
            this.chbAddThematicLayers.Text = " ";
            this.chbAddThematicLayers.UseVisualStyleBackColor = true;
            //
            // panResultPath
            //
            this.tblpanDistributionParameters.SetColumnSpan(this.panResultPath, 2);
            this.panResultPath.Controls.Add(this.tbResultPath);
            this.panResultPath.Controls.Add(this.btBrowse);
            this.panResultPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panResultPath.Location = new System.Drawing.Point(183, 166);
            this.panResultPath.Name = "panResultPath";
            this.panResultPath.Size = new System.Drawing.Size(353, 20);
            this.panResultPath.TabIndex = 13;
            //
            // tbResultPath
            //
            this.tbResultPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tbResultPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.tbResultPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbResultPath.Location = new System.Drawing.Point(0, 0);
            this.tbResultPath.Name = "tbResultPath";
            this.tbResultPath.Size = new System.Drawing.Size(329, 21);
            this.tbResultPath.TabIndex = 0;
            //
            // btBrowse
            //
            this.btBrowse.Dock = System.Windows.Forms.DockStyle.Right;
            this.btBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btBrowse.Location = new System.Drawing.Point(329, 0);
            this.btBrowse.Name = "btBrowse";
            this.btBrowse.Size = new System.Drawing.Size(24, 20);
            this.btBrowse.TabIndex = 1;
            this.btBrowse.Text = "...";
            this.btBrowse.UseVisualStyleBackColor = true;
            this.btBrowse.Click += new System.EventHandler(this.btBrowse_Click);
            //
            // cbCalculationType
            //
            this.tblpanDistributionParameters.SetColumnSpan(this.cbCalculationType, 2);
            this.cbCalculationType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbCalculationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCalculationType.FormattingEnabled = true;
            this.cbCalculationType.Items.AddRange(new object[] {
            "By area",
            "By biomes",
            "By biomes with denominator"});
            this.cbCalculationType.Location = new System.Drawing.Point(183, 85);
            this.cbCalculationType.Name = "cbCalculationType";
            this.cbCalculationType.Size = new System.Drawing.Size(353, 21);
            this.cbCalculationType.TabIndex = 7;
            this.cbCalculationType.SelectedIndexChanged += new System.EventHandler(this.cbCalculationType_SelectedIndexChanged);
            //
            // tpResults
            //
            this.tpResults.Controls.Add(this.tblpanParameters);
            this.tpResults.Location = new System.Drawing.Point(4, 22);
            this.tpResults.Name = "tpResults";
            this.tpResults.Padding = new System.Windows.Forms.Padding(3);
            this.tpResults.Size = new System.Drawing.Size(546, 370);
            this.tpResults.TabIndex = 2;
            this.tpResults.Text = "Results";
            this.tpResults.UseVisualStyleBackColor = true;
            //
            // tblpanParameters
            //
            this.tblpanParameters.ColumnCount = 1;
            this.tblpanParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblpanParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblpanParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblpanParameters.Controls.Add(this.rtbResults, 0, 0);
            this.tblpanParameters.Controls.Add(this.btClear, 0, 1);
            this.tblpanParameters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblpanParameters.Location = new System.Drawing.Point(3, 3);
            this.tblpanParameters.Name = "tblpanParameters";
            this.tblpanParameters.RowCount = 2;
            this.tblpanParameters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblpanParameters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tblpanParameters.Size = new System.Drawing.Size(540, 364);
            this.tblpanParameters.TabIndex = 1;
            //
            // rtbResults
            //
            this.rtbResults.BackColor = System.Drawing.SystemColors.Window;
            this.rtbResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbResults.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rtbResults.Location = new System.Drawing.Point(3, 3);
            this.rtbResults.Name = "rtbResults";
            this.rtbResults.ReadOnly = true;
            this.rtbResults.Size = new System.Drawing.Size(534, 332);
            this.rtbResults.TabIndex = 1;
            this.rtbResults.Text = "";
            //
            // btClear
            //
            this.btClear.Location = new System.Drawing.Point(0, 338);
            this.btClear.Margin = new System.Windows.Forms.Padding(0);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(75, 23);
            this.btClear.TabIndex = 2;
            this.btClear.Text = "Clear";
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            //
            // btAnalyzeNRCP
            //
            this.btAnalyzeNRCP.AutoSize = true;
            this.btAnalyzeNRCP.Location = new System.Drawing.Point(205, 1);
            this.btAnalyzeNRCP.Margin = new System.Windows.Forms.Padding(1);
            this.btAnalyzeNRCP.Name = "btAnalyzeNRCP";
            this.btAnalyzeNRCP.Size = new System.Drawing.Size(144, 24);
            this.btAnalyzeNRCP.TabIndex = 2;
            this.btAnalyzeNRCP.Text = "Analyze NR and CP results";
            this.btAnalyzeNRCP.UseVisualStyleBackColor = true;
            this.btAnalyzeNRCP.Click += new System.EventHandler(this.btAnalyzeNRCP_Click);
            //
            // SpeciesParametersWindow
            //
            this.Controls.Add(this.tcParameters);
            this.Controls.Add(this.tsMenu);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "SpeciesParametersWindow";
            this.Size = new System.Drawing.Size(554, 421);
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.tcParameters.ResumeLayout(false);
            this.tpLayers.ResumeLayout(false);
            this.tblpanMain.ResumeLayout(false);
            this.tblpanMain.PerformLayout();
            this.flwpanButtons.ResumeLayout(false);
            this.flwpanButtons.PerformLayout();
            this.tpParameters.ResumeLayout(false);
            this.tpParameters.PerformLayout();
            this.tblpanDistributionParameters.ResumeLayout(false);
            this.tblpanDistributionParameters.PerformLayout();
            this.panIUCNCategoriesPath.ResumeLayout(false);
            this.panIUCNCategoriesPath.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDistrCoeff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLocal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWide)).EndInit();
            this.panResultPath.ResumeLayout(false);
            this.panResultPath.PerformLayout();
            this.tpResults.ResumeLayout(false);
            this.tblpanParameters.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsMenu;
        private System.Windows.Forms.ToolStripButton btRefreshData;
        private System.Windows.Forms.OpenFileDialog ofdCsv;
        private System.Windows.Forms.OpenFileDialog ofdShp;
        private System.Windows.Forms.ToolStripButton tsbtFilterLayers;
        private System.Windows.Forms.TabControl tcParameters;
        private System.Windows.Forms.TabPage tpLayers;
        private System.Windows.Forms.TabPage tpParameters;
        private System.Windows.Forms.TableLayoutPanel tblpanMain;
        private System.Windows.Forms.ComboBox cbRegionsNameField;
        private System.Windows.Forms.Label lbRegionsNameField;
        private System.Windows.Forms.Label lbSpeciesLayers;
        private System.Windows.Forms.Label lbBiomesLayer;
        private System.Windows.Forms.ComboBox cbBiomesLayer;
        private System.Windows.Forms.ComboBox cbFocRegionsNameField;
        private System.Windows.Forms.Label lbFocRegionsNameField;
        private System.Windows.Forms.ComboBox cbBiomesNameField;
        private System.Windows.Forms.Label lbBiomesNamesField;
        private System.Windows.Forms.RadioButton rbUseAllLayer;
        private System.Windows.Forms.Label lbAllLayer;
        private System.Windows.Forms.ComboBox cbFocalRegionLayer;
        private System.Windows.Forms.Label lbFocalRegionLayer;
        private System.Windows.Forms.Label lbUseSelectedValues;
        private System.Windows.Forms.ComboBox cbRegionsLayer;
        private System.Windows.Forms.Label lbRegionsLayer;
        private System.Windows.Forms.ListBox libFocalRegionValues;
        private System.Windows.Forms.RadioButton rbUseSelectedValues;
        private System.Windows.Forms.ListBox libSpeciesLayers;
        private System.Windows.Forms.FlowLayoutPanel flwpanButtons;
        private System.Windows.Forms.Button btAnalyze;
        private System.Windows.Forms.Button btAddLayerFromResults;
        private System.Windows.Forms.TabPage tpResults;
        private System.Windows.Forms.TableLayoutPanel tblpanParameters;
        private System.Windows.Forms.TableLayoutPanel tblpanDistributionParameters;
        private System.Windows.Forms.CheckBox chbOverwriteResult;
        private System.Windows.Forms.Label lbOverwriteExisting;
        private System.Windows.Forms.Panel panIUCNCategoriesPath;
        private System.Windows.Forms.TextBox tbIUCNCategoriesPath;
        private System.Windows.Forms.Button btBrowseIUCNCategoriesPath;
        private System.Windows.Forms.Label lbIUCNCategoriesPath;
        private System.Windows.Forms.Label lbCalculationType;
        private System.Windows.Forms.Label lbResultPath;
        private System.Windows.Forms.Label lbAddThematicLayers;
        private System.Windows.Forms.NumericUpDown nudDistrCoeff;
        private System.Windows.Forms.Label lbDistrCoeff;
        private System.Windows.Forms.NumericUpDown nudLocal;
        private System.Windows.Forms.Label lbWide;
        private System.Windows.Forms.Label lbRegional;
        private System.Windows.Forms.Label lbLocal;
        private System.Windows.Forms.NumericUpDown nudWide;
        private System.Windows.Forms.TextBox tbRegional;
        private System.Windows.Forms.CheckBox chbAddThematicLayers;
        private System.Windows.Forms.Panel panResultPath;
        private System.Windows.Forms.TextBox tbResultPath;
        private System.Windows.Forms.Button btBrowse;
        private System.Windows.Forms.ComboBox cbCalculationType;
        private System.Windows.Forms.RichTextBox rtbResults;
        private System.Windows.Forms.Button btClear;
        private System.Windows.Forms.Button btAnalizeResultShapeFile;
        private System.Windows.Forms.Button btAnalyzeNRCP;

    }
}
