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

namespace BobSuperStores.Data.Model;

using BobSuperStores.Data.Model.Interfaces;

using JetBrains.Annotations;

/// <summary>
/// The distance data class that connects a <see cref="Store"/> and a <see cref="Warehouse"/> and holds their <see cref="Distance"/>.
/// </summary>
public sealed class Distance : IDistance
{
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Distance"/> class.
    /// </summary>
    [PublicAPI]
    public Distance()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Distance"/> class.
    /// </summary>
    /// <param name="store">The store.</param>
    /// <param name="warehouse">The warehouse.</param>
    /// <param name="value">The distance between both locations.</param>
    public Distance([NotNull] SuperStore store, [NotNull] Warehouse warehouse, double value)
    {
        this.Store = store ?? throw new ArgumentNullException(nameof(store));
        this.Warehouse = warehouse ?? throw new ArgumentNullException(nameof(warehouse));
        this.Value = value;
    }

    #endregion

    #region Public properties

    /// <summary>
    /// Gets the store.
    /// </summary>
    [NotNull]
    [PublicAPI]
    public SuperStore Store { get; init; }

    /// <summary>
    /// Gets the warehouse.
    /// </summary>
    [NotNull]
    [PublicAPI]
    public Warehouse Warehouse { get; init; }

    /// <summary>
    /// Gets or sets the distance between the <see cref="Store"/> and the <see cref="Warehouse"/>.
    /// </summary>
    [PublicAPI]
    public double Value { get; set; }

    #endregion

    #region Explicit Interface properties

    /// <inheritdoc />
    ILocation IDistance.Source => this.Store;

    /// <inheritdoc />
    ILocation IDistance.Target => this.Warehouse;

    /// <inheritdoc />
    double IDistance.DistanceInMeter => this.Value;

    #endregion

    #region Public Methods and Operators

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{this.Store} - {this.Warehouse}";
    }

    #endregion
}