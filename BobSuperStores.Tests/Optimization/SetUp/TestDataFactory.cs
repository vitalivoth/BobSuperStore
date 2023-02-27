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

namespace BobSuperStores.Tests.Optimization.SetUp;

using BobSuperStores.Data.Model;
using BobSuperStores.Data.Model.Interfaces;

using Moq;

/// <summary>
/// Factory class to create test data.
/// </summary>
public static class TestDataFactory
{
    #region Public Methods and Operators

    /// <summary>
    /// Creates the given amount of locations with indices starting at zero.
    /// </summary>
    /// <param name="amountToCreate">The amount of locations to create.</param>
    /// <returns>The created location.</returns>
    public static IEnumerable<ILocation> GetLocations(int amountToCreate)
    {
        for (var i = 0; i < amountToCreate; i++)
        {
            yield return TestDataFactory.GetLocation(i);
        }
    }

    /// <summary>
    /// Gets a location object with the given index.
    /// </summary>
    /// <param name="i">The index to use.</param>
    /// <returns>A location object with the given index.</returns>
    public static ILocation GetLocation(int i = 0)
    {
        var locationMock = new Mock<ILocation>();
        var name = $"Location {i}";
        locationMock.Setup(m => m.Name).Returns(name);
        locationMock.Setup(d => d.ToString()).Returns(name);
        return locationMock.Object;
    }

    /// <summary>
    /// Method to create the distances between newly created clients and facilities.
    /// </summary>
    /// <param name="amountClients">the amount clients to use.</param>
    /// <param name="amountFacilities">the amount facilities to use.</param>
    /// <param name="openingCostFunction">the opening cost function for the facilities to create.</param>
    /// <returns>the distance objects between all created clients and facilities.</returns>
    public static IEnumerable<IDistance> GetDistances(int amountClients, int amountFacilities, Func<int, double> openingCostFunction = null)
    {
        return TestDataFactory.GetDistances(
            TestDataFactory.GetLocations(amountClients),
            TestDataFactory.GetFacilities(amountFacilities, openingCostFunction));
    }

    /// <summary>
    /// Gets the distances for all combinations of the given clients and facilities.
    /// </summary>
    /// <param name="clients">The clients to use.</param>
    /// <param name="facilities">The facilities to use.</param>
    /// <returns>The distances between all given clients and facilities.</returns>
    public static IEnumerable<IDistance> GetDistances(IEnumerable<ILocation> clients, IEnumerable<ILocation> facilities)
    {
        return from client in clients
               from facility in facilities
               select TestDataFactory.GetDistance(client: client, facility: facility);
    }

    /// <summary>
    /// Method to get the given amount of facilities.
    /// </summary>
    /// <param name="amountFacilities">The amount of facilities to be created.</param>
    /// <param name="openingCostFunction">The opening cost function, if none is given cost 0 are used.</param>
    /// <returns>The created facilities.</returns>
    public static IEnumerable<ILocation> GetFacilities(int amountFacilities, Func<int, double> openingCostFunction = null)
    {
        for (var i = 0; i < amountFacilities; i++)
        {
            var facility = TestDataFactory.GetFacility(openingCostFunction?.Invoke(i) ?? 0d, i);
            yield return facility;
        }
    }

    /// <summary>
    /// Gets a facility with the given properties.
    /// </summary>
    /// <param name="openingCost">The opening cost to use.</param>
    /// <param name="number">The optional index of the facility, if none is given zero is used.</param>
    /// <returns>The created facility.</returns>
    public static ILocation GetFacility(double openingCost = 0d, int number = 0)
    {
        var facility = new Warehouse($"Warehouse {number}", 0, 0, openingCost);
        return facility;
    }

    /// <summary>
    /// Gets the distance object with the given parameter.
    /// </summary>
    /// <param name="distanceValue">The distance value in meter to use.</param>
    /// <param name="client">The client to use, if none given a new one is created.</param>
    /// <param name="facility">The facility to use, if none is given, a new one is created.</param>
    /// <returns>The created distance object with the given parameter.</returns>
    public static IDistance GetDistance(double distanceValue = 0d, ILocation client = null, ILocation facility = null)
    {
        var clientToUse = client ?? TestDataFactory.GetLocation();
        var facilityToUse = facility ?? TestDataFactory.GetFacility();

        var distanceMock = new Mock<IDistance>();
        distanceMock.Setup(d => d.Source).Returns(clientToUse);
        distanceMock.Setup(d => d.Target).Returns(facilityToUse);
        distanceMock.Setup(d => d.ToString()).Returns($"{clientToUse} - {facilityToUse}");
        distanceMock.Setup(d => d.DistanceInMeter).Returns(distanceValue);
        return distanceMock.Object;
    }

    #endregion
}