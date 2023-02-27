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

namespace BobSuperStores.Data.Builder;

using BobSuperStores.Data.Logging;
using BobSuperStores.Data.Model;
using BobSuperStores.Data.Transformation;

using JetBrains.Annotations;

/// <summary>
/// The default implementation of the <see cref="IDataContextBuilder"/>.
/// </summary>
public class DefaultDataContextBuilder : IDataContextBuilder
{
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultDataContextBuilder"/> class.
    /// </summary>
    /// <param name="superStoreSource">The data source to load <see cref="SuperStore"/> from.</param>
    /// <param name="warehouseSource">The data source to load <see cref="Warehouse"/> from.</param>
    /// <param name="distanceSource">The data source to load <see cref="Distance"/> from.</param>
    /// <param name="transformation">The transformation that is executed after loading the initial data.</param>
    /// <param name="logger">The logger.</param>
    public DefaultDataContextBuilder(
        [NotNull] IDataSource<SuperStore> superStoreSource,
        [NotNull] IDataSource<Warehouse> warehouseSource,
        [NotNull] IDataSource<Distance> distanceSource,
        [NotNull] IDataTransformation transformation,
        [NotNull] IOptanoLogger logger)
    {
        this.SuperStoreSource = superStoreSource ?? throw new ArgumentNullException(nameof(superStoreSource));
        this.WarehouseSource = warehouseSource ?? throw new ArgumentNullException(nameof(warehouseSource));
        this.DistanceSource = distanceSource ?? throw new ArgumentNullException(nameof(distanceSource));
        this.Transformation = transformation ?? throw new ArgumentNullException(nameof(transformation));
        this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    #endregion

    #region Properties

    [NotNull]
    private IDataSource<SuperStore> SuperStoreSource { get; }

    [NotNull]
    private IDataSource<Warehouse> WarehouseSource { get; }

    [NotNull]
    private IDataSource<Distance> DistanceSource { get; }

    [NotNull]
    private IDataTransformation Transformation { get; }

    [NotNull]
    private IOptanoLogger Logger { get; }

    #endregion

    #region Public Methods and Operators

    /// <inheritdoc/>
    public DataContext Build()
    {
        this.Logger.Log(LogLevel.Information, "Loading data...");
        var context = new DataContext();
        this.LoadStores(context);
        this.LoadWarehouses(context);
        this.LoadDistances(context);
        this.Transformation.Transform(context);
        this.PrintResult(context);

        return context;
    }

    #endregion

    #region Methods

    private void PrintResult(DataContext context)
    {
        this.Logger.Log(LogLevel.Information, "#Stores: {0}", context.SuperStores.Count);
        this.Logger.Log(LogLevel.Information, "#Warehouses: {0}", context.Warehouses.Count);
        this.Logger.Log(LogLevel.Information, "#Distances: {0}", context.Distances.Count);
    }

    private void LoadStores([NotNull] DataContext currentContext)
    {
        this.LoadSource(currentContext, currentContext.SuperStores, this.SuperStoreSource);
    }

    private void LoadWarehouses([NotNull] DataContext currentContext)
    {
        this.LoadSource(currentContext, currentContext.Warehouses, this.WarehouseSource);
    }

    private void LoadDistances([NotNull] DataContext currentContext)
    {
        this.LoadSource(currentContext, currentContext.Distances, this.DistanceSource);
    }

    private void LoadSource<T>([NotNull] DataContext currentContext, [NotNull] List<T> collection, [NotNull] IDataSource<T> dataSource)
    {
        var typeName = typeof(T).Name;
        this.Logger.Log(LogLevel.Information, "Loading {0}...", typeName);
        collection.AddRange(dataSource.LoadData(currentContext));
        this.Logger.Log(LogLevel.Information, "Successfully loaded {0} {1}", collection.Count, typeName);
    }

    #endregion
}