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

namespace BobSuperStores.Data.Calculations;

using BobSuperStores.Data.Logging;
using BobSuperStores.Data.Model;

using JetBrains.Annotations;

/// <summary>
/// Provides a cartesian product of distances between all known <see cref="SuperStore"/> and <see cref="Warehouse"/>.
/// </summary>
public class AllDistancesDataSource : IDataSource<Distance>
{
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="AllDistancesDataSource"/> class.
    /// </summary>
    /// <param name="distanceCalculator">The distance calculator.</param>
    /// <param name="logger">The logger.</param>
    public AllDistancesDataSource([NotNull] IDistanceCalculator distanceCalculator, [NotNull] IOptanoLogger logger)
    {
        this.DistanceCalculator = distanceCalculator ?? throw new ArgumentNullException(nameof(distanceCalculator));
        this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    #endregion

    #region Properties

    [NotNull]
    private IDistanceCalculator DistanceCalculator { get; }

    [NotNull]
    private IOptanoLogger Logger { get; }

    #endregion

    #region Public Methods and Operators

    /// <inheritdoc />
    public IEnumerable<Distance> LoadData(DataContext currentContext)
    {
        this.Logger.Log(LogLevel.Information, "Loading distances by using the cartesian product of all stores and warehouses...");
        return from store in currentContext.SuperStores
               from warehouse in currentContext.Warehouses
               select this.CreateDistance(store, warehouse);
    }

    #endregion

    #region Methods

    private Distance CreateDistance([NotNull] SuperStore store, [NotNull] Warehouse warehouse)
    {
        this.Logger.Log(LogLevel.Debug, "Creating distance from store '{0}' and warehouse '{1}'", store.Name, warehouse.Name);
        return new Distance(store, warehouse, this.DistanceCalculator.CalculateDistance(store, warehouse));
    }

    #endregion
}