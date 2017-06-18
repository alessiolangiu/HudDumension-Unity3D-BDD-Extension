﻿//-----------------------------------------------------------------------
// <copyright file="SerializedPropertyWrapper.cs" company="Hud Dimesion">
//     Copyright (c) Hud Dimension. All rights reserved.
// </copyright>
//
// <disclaimer>
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </disclaimer>
//
// <author>Alessio Langiu</author>
// <email>alessio.langiu@huddimension.co.uk</email>
//-----------------------------------------------------------------------
using UnityEditor;

namespace HudDimension.UnityTestBDD
{
    public class SerializedPropertyWrapper : ISerializedPropertyWrapper
    {
        public SerializedPropertyWrapper(SerializedProperty property)
        {
            this.Property = property;
        }

        private SerializedProperty Property { get; set; }

        public SerializedProperty GetProperty()
        {
            return this.Property;
        }
    }
}