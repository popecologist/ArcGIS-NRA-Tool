# ArcGIS-NRA-Tool
ArcGIS Tool for the Assessment of National Responsibilities for Endangered Species and Habitats

The aim of the source code is to develop a National Responsibility Assessment Tool that can be used as plug-in for ArcGIS (https://github.com/popecologist/ArcGIS-NRA-Tool or http://popecologist.github.io).

There are two binary versions of the add-in:
SpeciesTool.NET.20140619.esriAddIn - Version from 2014-06-14 compiled with Visual Studio 12, Microsoft .NET 3.4, and the matching (older) SDKs ArcGISRuntime and ArcObjects from ESRI.
SpeciesTool.NET.20200228.esriAddIn -  Version from 2020-02-28 compiled with Visual Studio 17, Microsoft .NET 4.6.1, and the and the matching (newer) SDKs ArcGISRuntime 100.2.0 and ArcObjects for ArcGIS 10.6.1 from ESRI.

Microsoft .NET, the SDK for ESRI ArcGISRuntime, and ESRI ArcObjects are not provided with the code. They have to be obtained from Microsoft and ESRI and to be installed before compilation.

Copyright (c) 2020 Yu-Pin Lin / 林裕彬
Department of Bioenvironmental Systems Engineering
National Taiwan University
No. 1, Sec. 4, Roosevelt Road
Taipei
10617 Taiwan
R.O.C.
Office Phone: 886-2-33663467; Fax: 886-2-2368-6980
E-mail: yplin@ntu.edu.tw
http://homepage.ntu.edu.tw/~yplin/Scales-Taiwan.htm

Credits:
   Academia Sinica (programming)
   Reinhard A. Klenke (revising, updating)

The development of the ArcGIS-NRA-Tool was mainly funded by Ministry of Science and Technology of Taiwan (former National Science Council of Taiwan, code NSC101-2923-I-002-001-MY2), and a contribution from the EU FP7 project SCALES: Securing the Conservation of biodiversity across Administrative Levels and spatial, temporal, and Ecological Scales, under the European Union’s Framework Program 7 (Code: 226852 FP7-ENVIRONMENT ENV.2008.2.1.4.4., www. scales-project.net.

This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, version 3. This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with this program. If not, see <http://www.gnu.org/licenses/>. Please cite and refer to the tool in any report, scientific paper or other type of publication either in print or electronically with the reference mentioned below.

Unfortunately we still need proprietary libraries from third parties such as ESRI (e.g. ArcGIS SDK, ArcObjects SDK) and from Microsoft to be linked again this source code. The need for non-free libraries is a drawback. Therefore, we suggest to anyone who thinks of doing substantial further work on the program to give highest priority to tasks changing the program in a way that it can do the same job without the non-free libraries.

Installation of the ArcGIS-add-in:
There might be a "No GUI components found in this Add-In. Add-In version does not match." error during installation. This error only occurs when a user tries to install the add-in using the <Add From File button> in the Customize dialog. When the add-in is double-clicked from Windows Explorer, the add-in is installed successfully. This is a defect (NIM095435 http://support.esri.com/bugs/nimbus/TklNMDk1NDM1. A potential workaround is to either: <Double click> the Add-In in Windows Explorer to install OR add folder through <options> in Add-In Manager (see more at: https://community.esri.com/thread/162324).

References:
Lin Y-P, Schmeller D S, Ding T S, Wang Y Ch,  Lien W-Y, Henle K, Klenke R A (2020): A GIS-based policy support tool to determine national responsibilities and priorities for biodiversity conservation. PLoS ONE. doi: xxxxxx
