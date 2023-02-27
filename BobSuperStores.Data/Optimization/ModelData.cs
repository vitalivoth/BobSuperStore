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

namespace BobSuperStores.Data.Optimization;

using BobSuperStores.Data.Model.Interfaces;

using JetBrains.Annotations;

/// <summary>
/// The model data containing all data needed for the optimization problem.
/// </summary>
public class ModelData : IModelData
{
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ModelData"/> class.
    /// </summary>
    /// <param name="distances">The distances to use.</param>
    /// <param name="clients">The clients given, if null the clients from the distances are used.</param>
    /// <param name="facilities">The facilities given, if null the facilities from the distances are used.</param>
    public ModelData([NotNull] IEnumerable<IDistance> distances, IEnumerable<ILocation> clients = null, IEnumerable<ILocation> facilities = null)
    {
        this.Distances = distances.ToList();
        this.Clients = clients ?? this.Distances.Select(x => x.Source).Distinct().ToList();
        this.Facilities = facilities ?? this.Distances.Select(x => x.Target).Distinct().ToList();
    }

    #endregion

    #region Public properties

    /// <summary>
    /// Gets the distances.
    /// </summary>
    public IEnumerable<IDistance> Distances { get; }

    /// <summary>
    /// Gets the clients.
    /// </summary>
    public IEnumerable<ILocation> Clients { get; }

    /// <summary>
    /// Gets the facilities.
    /// </summary>
    public IEnumerable<ILocation> Facilities { get; }

    #endregion
}