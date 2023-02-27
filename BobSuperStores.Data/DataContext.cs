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

namespace BobSuperStores.Data;

using BobSuperStores.Data.Model;

/// <summary>
/// Represents the complete set of data that is used by this application.
/// </summary>
public class DataContext
{
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="DataContext"/> class.
    /// </summary>
    internal DataContext()
    {
        this.SuperStores = new List<SuperStore>();
        this.Warehouses = new List<Warehouse>();
        this.Distances = new List<Distance>();
    }

    #endregion

    #region Public properties

    /// <summary>
    /// Gets all <see cref="SuperStore"/> on the current context.
    /// </summary>
    public List<SuperStore> SuperStores { get; }

    /// <summary>
    /// Gets all <see cref="Warehouse"/> on the current context.
    /// </summary>
    public List<Warehouse> Warehouses { get; }

    /// <summary>
    /// Gets all <see cref="Distance"/> on the current context.
    /// </summary>
    public List<Distance> Distances { get; }

    #endregion
}