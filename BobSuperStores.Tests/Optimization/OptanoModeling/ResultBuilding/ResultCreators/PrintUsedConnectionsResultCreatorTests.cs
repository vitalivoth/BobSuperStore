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

namespace BobSuperStores.Tests.Optimization.OptanoModeling.ResultBuilding.ResultCreators;

using Autofac;

using BobSuperStores.Data.Logging;
using BobSuperStores.Data.Optimization;
using BobSuperStores.Tests.Optimization.SetUp;

using Shouldly;

/// <summary>
/// Tests for the result creator printing the connections chosen by the optimization problem.
/// </summary>
[TestFixture]
public class PrintUsedConnectionsResultCreatorTests
{
    #region Public Methods and Operators

    /// <summary>
    /// The model data to test contain two factories with different costs that are connected to all clients with distance 0.
    /// The optimal solution only opens the cheaper facility and connects it to every client.
    /// In the output we expect an entry for each client to the opened facility and none to the other facility.
    /// </summary>
    [Test]
    public void TestThatOpenedFacilityWithFittingOpeningCostsIsContainedInResults()
    {
        var cacheLogger = new CacheLogger();
        var modelData = new ModelData(TestDataFactory.GetDistances(5, 2, i => i));

        OptimizationTestRunner.RunOptimizationForModelData(
            modelData,
            additionalRegistrations: cb => { cb.RegisterInstance(cacheLogger).As<IOptanoLogger>().SingleInstance(); });

        var cheapestFacility = modelData.Facilities.First();
        var notChosenFacility = modelData.Facilities.Last();

        foreach (var client in modelData.Clients)
        {
            cacheLogger.ContainsFittingEntry(s => s.Contains($"Connection '{client} - {cheapestFacility}'")).ShouldBeTrue();
            cacheLogger.ContainsFittingEntry(s => s.Contains($"Connection '{client} - {notChosenFacility}'")).ShouldBeFalse();
        }
    }

    #endregion
}