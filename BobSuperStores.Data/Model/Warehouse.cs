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

using JetBrains.Annotations;

/// <summary>
/// Represents a single warehouse.
/// </summary>
public class Warehouse : Location
{
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Warehouse"/> class.
    /// </summary>
    public Warehouse()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Warehouse"/> class.
    /// </summary>
    /// <param name="name">The name of the warehouse.</param>
    /// <param name="latitude">The latitude value of the geographic location.</param>
    /// <param name="longitude">The longitude value of the geographic location.</param>
    /// <param name="openingCosts">The opening costs.</param>
    [PublicAPI]
    public Warehouse([NotNull] string name, double latitude, double longitude, double openingCosts)
        : base(name, latitude, longitude)
    {
        this.OpeningCosts = openingCosts;
    }

    #endregion

    #region Public properties

    /// <summary>
    /// Gets or sets the costs that are due when the warehouse is opened.
    /// </summary>
    public double OpeningCosts { get; set; }

    #endregion
}