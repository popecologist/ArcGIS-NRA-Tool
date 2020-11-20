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
    partial class ExceptionDialog
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
            this.tblpan = new System.Windows.Forms.TableLayoutPanel();
            this.tbExceptionDetails = new System.Windows.Forms.TextBox();
            this.lbErrorMessage = new System.Windows.Forms.Label();
            this.lbErrorMessageText = new System.Windows.Forms.Label();
            this.flwpan = new System.Windows.Forms.FlowLayoutPanel();
            this.btClose = new System.Windows.Forms.Button();
            this.btDetails = new System.Windows.Forms.Button();
            this.pb = new System.Windows.Forms.PictureBox();
            this.tblpan.SuspendLayout();
            this.flwpan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).BeginInit();
            this.SuspendLayout();
            //
            // tblpan
            //
            this.tblpan.ColumnCount = 2;
            this.tblpan.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tblpan.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblpan.Controls.Add(this.tbExceptionDetails, 0, 3);
            this.tblpan.Controls.Add(this.lbErrorMessage, 1, 0);
            this.tblpan.Controls.Add(this.lbErrorMessageText, 1, 1);
            this.tblpan.Controls.Add(this.flwpan, 0, 2);
            this.tblpan.Controls.Add(this.pb, 0, 0);
            this.tblpan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblpan.Location = new System.Drawing.Point(0, 0);
            this.tblpan.Name = "tblpan";
            this.tblpan.RowCount = 4;
            this.tblpan.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tblpan.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblpan.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tblpan.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tblpan.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblpan.Size = new System.Drawing.Size(588, 388);
            this.tblpan.TabIndex = 0;
            //
            // tbExceptionDetails
            //
            this.tblpan.SetColumnSpan(this.tbExceptionDetails, 2);
            this.tbExceptionDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbExceptionDetails.Location = new System.Drawing.Point(3, 87);
            this.tbExceptionDetails.Multiline = true;
            this.tbExceptionDetails.Name = "tbExceptionDetails";
            this.tbExceptionDetails.ReadOnly = true;
            this.tbExceptionDetails.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbExceptionDetails.Size = new System.Drawing.Size(582, 298);
            this.tbExceptionDetails.TabIndex = 2;
            //
            // lbErrorMessage
            //
            this.lbErrorMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbErrorMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbErrorMessage.Location = new System.Drawing.Point(63, 3);
            this.lbErrorMessage.Margin = new System.Windows.Forms.Padding(3);
            this.lbErrorMessage.Name = "lbErrorMessage";
            this.lbErrorMessage.Size = new System.Drawing.Size(522, 22);
            this.lbErrorMessage.TabIndex = 0;
            this.lbErrorMessage.Text = "Unexpected error";
            this.lbErrorMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lbErrorMessageText
            //
            this.lbErrorMessageText.AutoSize = true;
            this.lbErrorMessageText.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbErrorMessageText.Location = new System.Drawing.Point(65, 33);
            this.lbErrorMessageText.Margin = new System.Windows.Forms.Padding(5);
            this.lbErrorMessageText.Name = "lbErrorMessageText";
            this.lbErrorMessageText.Size = new System.Drawing.Size(518, 13);
            this.lbErrorMessageText.TabIndex = 1;
            this.lbErrorMessageText.Text = "label1";
            //
            // flwpan
            //
            this.tblpan.SetColumnSpan(this.flwpan, 2);
            this.flwpan.Controls.Add(this.btClose);
            this.flwpan.Controls.Add(this.btDetails);
            this.flwpan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flwpan.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flwpan.Location = new System.Drawing.Point(0, 56);
            this.flwpan.Margin = new System.Windows.Forms.Padding(0);
            this.flwpan.Name = "flwpan";
            this.flwpan.Padding = new System.Windows.Forms.Padding(2);
            this.flwpan.Size = new System.Drawing.Size(588, 28);
            this.flwpan.TabIndex = 3;
            //
            // btClose
            //
            this.btClose.Location = new System.Drawing.Point(481, 2);
            this.btClose.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(100, 24);
            this.btClose.TabIndex = 1;
            this.btClose.Text = "Close";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            //
            // btDetails
            //
            this.btDetails.Location = new System.Drawing.Point(375, 2);
            this.btDetails.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btDetails.Name = "btDetails";
            this.btDetails.Size = new System.Drawing.Size(100, 24);
            this.btDetails.TabIndex = 0;
            this.btDetails.Text = "Show details";
            this.btDetails.UseVisualStyleBackColor = true;
            this.btDetails.Click += new System.EventHandler(this.btDetails_Click);
            //
            // pb
            //
            this.pb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb.Image = global::SpeciesTool.NET.Properties.Resources.dialog_no_3;
            this.pb.Location = new System.Drawing.Point(3, 3);
            this.pb.Name = "pb";
            this.tblpan.SetRowSpan(this.pb, 2);
            this.pb.Size = new System.Drawing.Size(54, 50);
            this.pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pb.TabIndex = 4;
            this.pb.TabStop = false;
            //
            // ExceptionDialog
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 388);
            this.Controls.Add(this.tblpan);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExceptionDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Error dialog";
            this.tblpan.ResumeLayout(false);
            this.tblpan.PerformLayout();
            this.flwpan.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblpan;
        private System.Windows.Forms.TextBox tbExceptionDetails;
        private System.Windows.Forms.Label lbErrorMessage;
        private System.Windows.Forms.Label lbErrorMessageText;
        private System.Windows.Forms.FlowLayoutPanel flwpan;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btDetails;
        private System.Windows.Forms.PictureBox pb;
    }
}
