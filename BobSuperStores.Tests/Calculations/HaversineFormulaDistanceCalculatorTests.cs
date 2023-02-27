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

namespace BobSuperStores.Tests.Calculations;

using BobSuperStores.Data.Calculations;
using BobSuperStores.Data.Logging;
using BobSuperStores.Data.Model.Interfaces;

using Moq;

using Shouldly;

/// <summary>
/// Tests the <see cref="HaversineFormulaDistanceCalculator"/>.
/// </summary>
[TestFixture]
public class HaversineFormulaDistanceCalculatorTests
{
    #region Public Methods and Operators

    /// <summary>
    /// Tests that the distance calculation between two coordinates is correct. 
    /// </summary>
    /// <param name="lat1">The latitude of the origin.</param>
    /// <param name="long1">The longitude of the origin.</param>
    /// <param name="lat2">The latitude of the destination.</param>
    /// <param name="long2">The longitude of the destination.</param>
    /// <param name="expectedDistance">The expected distance.</param>
    [TestCase(51.7276919, 8.6983936, 51.1673338, 7.0275946, 131.48)]
    [TestCase(41.8339037, -87.8720449, 51.7276919, 8.6983936, 6884.37)]
    public void TestCorrectCalculation(double lat1, double long1, double lat2, double long2, double expectedDistance)
    {
        var start = Mock.Of<IGeoCoordinate>(c => c.Latitude == lat1 && c.Longitude == long1);
        var destination = Mock.Of<IGeoCoordinate>(c => c.Latitude == lat2 && c.Longitude == long2);
        var calculator = new HaversineFormulaDistanceCalculator(Mock.Of<IOptanoLogger>());
        calculator.CalculateDistance(start, destination)
            .ShouldBe(expectedDistance);
    }

    /// <summary>
    /// Tests that the calculation is commutative.
    /// </summary>
    [Test]
    public void TestCalculationIsCommutative()
    {
        var expectedDistance = 324.85;
        var location1 = Mock.Of<IGeoCoordinate>(c => c.Latitude == 51.7276919 && c.Longitude == 8.6983936);
        var location2 = Mock.Of<IGeoCoordinate>(c => c.Latitude == 52.5069704 && c.Longitude == 13.2846516);
        var calculator = new HaversineFormulaDistanceCalculator(Mock.Of<IOptanoLogger>());
        calculator.CalculateDistance(location1, location2).ShouldBe(expectedDistance);
        calculator.CalculateDistance(location2, location1).ShouldBe(expectedDistance);
    }

    #endregion
}