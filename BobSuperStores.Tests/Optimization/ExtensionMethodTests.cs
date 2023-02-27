﻿#region Copyright (c) OPTANO GmbH

// ////////////////////////////////////////////////////////////////////////////////
// 
//        OPTANO GmbH Source Code
//        Copyright (c) 2010-2023 OPTANO GmbH
//        ALL RIGHTS RESERVED.
// 
//    The entire contents of this file is protected by German and
//    International Copyright Laws. Unauthorized reproduction,
//    reverse-engineering, and distribution of all or any portion of
//    the code contained in this file is strictly prohibited and may
//    result in severe civil and criminal penalties and will be
//    prosecuted to the maximum extent possible under the law.
// 
//    RESTRICTIONS
// 
//    THIS SOURCE CODE AND ALL RESULTING INTERMEDIATE FILES
//    ARE CONFIDENTIAL AND PROPRIETARY TRADE SECRETS OF
//    OPTANO GMBH.
// 
//    THE SOURCE CODE CONTAINED WITHIN THIS FILE AND ALL RELATED
//    FILES OR ANY PORTION OF ITS CONTENTS SHALL AT NO TIME BE
//    COPIED, TRANSFERRED, SOLD, DISTRIBUTED, OR OTHERWISE MADE
//    AVAILABLE TO OTHER INDIVIDUALS WITHOUT WRITTEN CONSENT
//    AND PERMISSION FROM OPTANO GMBH.
// 
// ////////////////////////////////////////////////////////////////////////////////

#endregion

namespace BobSuperStores.Tests.Optimization;

using BobSuperStores.Data.Optimization;

using Shouldly;

/// <summary>
/// Tests for the extension methods.
/// </summary>
[TestFixture]
public class ExtensionMethodTests
{
    #region Interfaces

    /// <summary>
    /// Interface for testing the extension methods. 
    /// </summary>
    private interface ITestInterface
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Test that the correct result is yielded if the interface is not implemented.
    /// </summary>
    [Test]
    public void TestThatImplementsInterfaceIsFalseIfInterfaceIsNotImplemented()
    {
        typeof(TestClassNotImplementingInterface).ImplementsInterface<ITestInterface>().ShouldBeFalse();
    }

    /// <summary>
    /// Test that the correct result is yielded if the interface is implemented.
    /// </summary>
    [Test]
    public void TestThatImplementsInterfaceIsTrueIfInterfaceIsImplemented()
    {
        typeof(TestClassImplementingInterface).ImplementsInterface<ITestInterface>().ShouldBeTrue();
    }

    #endregion

    private class TestClassNotImplementingInterface
    {
    }

    private class TestClassImplementingInterface : ITestInterface
    {
    }
}