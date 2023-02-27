#region Copyright (c) OPTANO GmbH

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

namespace BobSuperStores.Tests.Optimization.OptanoModeling.OptimizationModel.ParameterCalculation;

using BobSuperStores.Data.Optimization.OptanoModeling.OptimizationModel.ParameterCalculation;
using BobSuperStores.Tests.Optimization.SetUp;

using Shouldly;

/// <summary>
/// Tests for the parameter calculator.
/// </summary>
[TestFixture]
public class ParameterCalculatorTests
{
    #region Public Methods and Operators

    /// <summary>
    /// Tests that the opening costs given in the facility object are returned by the parameter calculator.
    /// </summary>
    [Test]
    public void TestThatOpeningCostsAreCorrectlyReturned()
    {
        var openingCost = 10.3;
        var facility = TestDataFactory.GetFacility(openingCost);

        var cost = new ParameterCalculator().GetOpeningCostForFacility(facility);

        cost.ShouldBe(openingCost);
    }

    /// <summary>
    /// Tests that for a given distance the connection cost are calculated correctly.
    /// </summary>
    [Test]
    public void TestThatConnectionCostAreCalculatedCorrectly()
    {
        var distanceValue = 42d;
        var costPerMeter = .3 / 1000d;
        var distance = TestDataFactory.GetDistance(distanceValue);

        var cost = new ParameterCalculator(costPerMeter).GetConnectionCost(distance);

        cost.ShouldBe(distanceValue * costPerMeter);
    }

    #endregion
}