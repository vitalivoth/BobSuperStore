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

namespace BobSuperStores.Data.Optimization.OptanoModeling.OptimizationModel.ConstraintCreators;

using JetBrains.Annotations;

using OPTANO.Modeling.Optimization;

/// <summary>
/// Class to add the constraints to ensure that clients are only connected to open facilities.
/// </summary>
[UsedImplicitly]
public class OnlyConnectToOpenFacilitiesConstraintCreator : ICreateConstraints
{
    #region Fields

    private readonly IModelData _modelData;

    private readonly ConstraintNameGenerator _constraintNameGenerator;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="OnlyConnectToOpenFacilitiesConstraintCreator"/> class.
    /// </summary>
    /// <param name="modelData">The model data.</param>
    /// <param name="constraintNameGenerator">The constraint name generator.</param>
    public OnlyConnectToOpenFacilitiesConstraintCreator(IModelData modelData, ConstraintNameGenerator constraintNameGenerator)
    {
        this._modelData = modelData;
        this._constraintNameGenerator = constraintNameGenerator;
    }

    #endregion

    #region Public Methods and Operators

    /// <inheritdoc />
    public IEnumerable<Constraint> GetConstraints(ModelVariables modelVariables)
    {
        foreach (var distance in this._modelData.Distances)
        {
            // Define Constraint:
            var constraint = modelVariables.OpenFacility[distance.Target] >= modelVariables.UseConnection[distance];
            yield return
                this._constraintNameGenerator.AddNameToConstraint(
                    constraint,
                    $"ConnectToOpen_{distance.Source}_{distance.Target}");
        }
    }

    #endregion
}