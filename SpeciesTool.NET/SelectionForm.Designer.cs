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

﻿namespace SpeciesTool.NET
{
    partial class SelectionForm
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
            this.flwpanButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOk = new System.Windows.Forms.Button();
            this.lbCaption = new System.Windows.Forms.Label();
            this.chlbItems = new System.Windows.Forms.CheckedListBox();
            this.flwpanButtons.SuspendLayout();
            this.SuspendLayout();
            //
            // flwpanButtons
            //
            this.flwpanButtons.Controls.Add(this.btCancel);
            this.flwpanButtons.Controls.Add(this.btOk);
            this.flwpanButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flwpanButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flwpanButtons.Location = new System.Drawing.Point(0, 131);
            this.flwpanButtons.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.flwpanButtons.Name = "flwpanButtons";
            this.flwpanButtons.Size = new System.Drawing.Size(361, 31);
            this.flwpanButtons.TabIndex = 1;
            //
            // btCancel
            //
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(283, 3);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 2;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            //
            // btOk
            //
            this.btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOk.Location = new System.Drawing.Point(202, 3);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 0;
            this.btOk.Text = "Create";
            this.btOk.UseVisualStyleBackColor = true;
            //
            // lbCaption
            //
            this.lbCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbCaption.Location = new System.Drawing.Point(0, 0);
            this.lbCaption.Margin = new System.Windows.Forms.Padding(0);
            this.lbCaption.Name = "lbCaption";
            this.lbCaption.Size = new System.Drawing.Size(361, 25);
            this.lbCaption.TabIndex = 2;
            this.lbCaption.Text = "Select species layers";
            this.lbCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // chlbItems
            //
            this.chlbItems.CheckOnClick = true;
            this.chlbItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chlbItems.FormattingEnabled = true;
            this.chlbItems.IntegralHeight = false;
            this.chlbItems.Location = new System.Drawing.Point(0, 25);
            this.chlbItems.Name = "chlbItems";
            this.chlbItems.Size = new System.Drawing.Size(361, 106);
            this.chlbItems.TabIndex = 3;
            //
            // SelectionForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 162);
            this.Controls.Add(this.chlbItems);
            this.Controls.Add(this.lbCaption);
            this.Controls.Add(this.flwpanButtons);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectionForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Creating thematic layers";
            this.flwpanButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flwpanButtons;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Label lbCaption;
        private System.Windows.Forms.CheckedListBox chlbItems;
    }
}
