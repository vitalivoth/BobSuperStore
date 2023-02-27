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
/// The default implementation of a <see cref="IDataTransformation"/>. Takes all given <see cref="IDataTransformationStep"/> and executes them.
/// </summary>
public class DefaultDataTransformation : IDataTransformation
{
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultDataTransformation"/> class.
    /// </summary>
    /// <param name="transformationSteps">The single steps to execute.</param>
    /// <param name="logger">The logger.</param>
    public DefaultDataTransformation([NotNull] [ItemNotNull] IEnumerable<IDataTransformationStep> transformationSteps, [NotNull] IOptanoLogger logger)
    {
        this.TransformationSteps = transformationSteps;
        this.Logger = logger;
    }

    #endregion

    #region Properties

    [NotNull]
    [ItemNotNull]
    private IEnumerable<IDataTransformationStep> TransformationSteps { get; }

    [NotNull]
    private IOptanoLogger Logger { get; }

    #endregion

    #region Public Methods and Operators

    /// <inheritdoc />
    public void Transform(DataContext currentContext)
    {
        this.Logger.Log(LogLevel.Information, "Starting transformation...");
        foreach (var step in this.TransformationSteps)
        {
            step.Execute(currentContext);
        }
    }

    #endregion
}