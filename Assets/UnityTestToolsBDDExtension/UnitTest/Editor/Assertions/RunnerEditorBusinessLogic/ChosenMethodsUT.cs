﻿//-----------------------------------------------------------------------
// <copyright file="ChosenMethodsUT.cs" company="Hud Dimesion">
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
using NUnit.Framework;

namespace HudDimension.UnityTestBDD
{
    [TestFixture]
    public class ChosenMethodsUT
    {
        [Test]
        public void ChosenMethodsClone()
        {
            ChosenMethods original = new ChosenMethods();
            original.ChosenMethodsNames = new string[3] { "Name1", "Name3", "Name2" };
            original.ChosenMethodsParametersIndex = new string[3] { "indexParameter1", "indexParameter3", "indexParameter2" };
            ChosenMethods clone = (ChosenMethods)original.Clone();
            Assert.AreEqual(original.ChosenMethodsNames, clone.ChosenMethodsNames, "the clone method for the class ChosenMethods returns a different names list");
            Assert.AreEqual(original.ChosenMethodsParametersIndex, clone.ChosenMethodsParametersIndex, "the clone method for the class ChosenMethods returns a different indexes list");
        }
    }
}