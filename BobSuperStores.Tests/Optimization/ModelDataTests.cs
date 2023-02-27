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

namespace BobSuperStores.Tests.Optimization;

using BobSuperStores.Data.Optimization;
using BobSuperStores.Tests.Optimization.SetUp;

/// <summary>
/// Tests for composing and using model data.
/// </summary>
[TestFixture]
public class ModelDataTests
{
    #region Public Methods and Operators

    /// <summary>
    /// Test to validate, that if the model data are created using only distances, the correct clients and facilities are set.
    /// </summary>
    [Test]
    public void IfOnlyDistancesAreGivenClientsAndFacilitiesCorrespondToThoseFromDistances()
    {
        var clients = TestDataFactory.GetLocations(10).ToList();
        var facilities = TestDataFactory.GetLocations(5).ToList();
        var distances = TestDataFactory.GetDistances(clients, facilities).ToList();

        var modelData = new ModelData(distances);

        CollectionAssert.AreEquivalent(modelData.Clients, clients);
        CollectionAssert.AreEquivalent(modelData.Facilities, facilities);
        CollectionAssert.AreEquivalent(modelData.Distances, distances);
    }

    /// <summary>
    /// If all data are given to the model data constructor, they are set correctly.
    /// </summary>
    [Test]
    public void IfAllDataAreGivenTheValuesAreUsedCorrectly()
    {
        var clients = TestDataFactory.GetLocations(10).ToList();
        var facilities = TestDataFactory.GetLocations(5).ToList();
        var distances = TestDataFactory.GetDistances(clients, facilities).ToList();

        var modelData = new ModelData(distances, clients, facilities);

        CollectionAssert.AreEquivalent(modelData.Clients, clients);
        CollectionAssert.AreEquivalent(modelData.Facilities, facilities);
        CollectionAssert.AreEquivalent(modelData.Distances, distances);
    }

    #endregion
}