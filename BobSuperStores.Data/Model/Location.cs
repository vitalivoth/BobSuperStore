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
/// Represent a named geographic location by its <see cref="Latitude"/> and <see cref="Longitude"/>.
/// </summary>
public abstract class Location : ILocation
{
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Location"/> class.
    /// </summary>
    protected Location()
    {
        this.Name = string.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Location"/> class.
    /// </summary>
    /// <param name="name">The name of the location.</param>
    /// <param name="latitude">The latitude value of the geographic location.</param>
    /// <param name="longitude">The longitude value of the geographic location.</param>
    protected Location([NotNull] string name, double latitude, double longitude)
    {
        this.Name = name ?? throw new ArgumentNullException(nameof(name));
        this.Latitude = latitude;
        this.Longitude = longitude;
    }

    #endregion

    #region Public properties

    /// <summary>
    /// Gets the name of the location.
    /// </summary>
    [NotNull]
    public string Name { get; init; }

    /// <summary>
    /// Gets the latitude value of the geographic location.
    /// </summary>
    public double Latitude { get; init; }

    /// <summary>
    /// Gets the longitude value of the geographic location.
    /// </summary>
    public double Longitude { get; init; }

    #endregion

    #region Public Methods and Operators

    /// <inheritdoc />
    public override string ToString()
    {
        return this.Name;
    }

    #endregion
}