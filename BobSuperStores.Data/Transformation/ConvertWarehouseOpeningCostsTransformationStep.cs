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

namespace BobSuperStores.Data.Transformation;

using BobSuperStores.Data.Logging;

using JetBrains.Annotations;

/// <summary>
/// Represents a transformation step that converts the opening costs of a warehouse from cent to euro.
/// </summary>
public class ConvertWarehouseOpeningCostsTransformationStep : IDataTransformationStep
{
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ConvertWarehouseOpeningCostsTransformationStep"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public ConvertWarehouseOpeningCostsTransformationStep([NotNull] IOptanoLogger logger)
    {
        this.Logger = logger;
    }

    #endregion

    #region Properties

    [NotNull]
    private IOptanoLogger Logger { get; }

    #endregion

    #region Public Methods and Operators

    /// <inheritdoc />
    public void Execute(DataContext currentContext)
    {
        this.Logger.Log(LogLevel.Information, "Converting opening costs from cents to euro...");
        Parallel.ForEach(currentContext.Warehouses, warehouse => warehouse.OpeningCosts /= 100);
    }

    #endregion
}