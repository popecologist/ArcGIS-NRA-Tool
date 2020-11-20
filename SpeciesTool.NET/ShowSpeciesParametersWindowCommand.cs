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
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.esriSystem;

namespace SpeciesTool.NET
{
    public class ShowSpeciesParametersWindowCommand : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        private static readonly UIDClass WindowUid = new UIDClass { Value = ThisAddIn.IDs.SpeciesParametersWindow };
        private IDockableWindow m_ParametersWindow = null;
        private IDockableWindow ParametersWindow
        {
            get
            {
                return m_ParametersWindow ??
                       (m_ParametersWindow = ArcMap.DockableWindowManager.GetDockableWindow(WindowUid));
            }
        }


        public ShowSpeciesParametersWindowCommand()
        {
        }

        protected override void OnClick()
        {
            try
            {
                ParametersWindow.Show(!ParametersWindow.IsVisible());
                if (ParametersWindow.IsVisible() && SpeciesParametersWindow.Instance != null)
                {
                    SpeciesParametersWindow.Instance.InitializeData();
                }
            }
            catch (Exception a_Exception)
            {
                ExceptionDialog.Show(a_Exception);
            }
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
            try
            {
                Checked = ParametersWindow != null && ParametersWindow.IsVisible();
            }
            catch
            {
                Checked = false;
            }
        }
    }

}
