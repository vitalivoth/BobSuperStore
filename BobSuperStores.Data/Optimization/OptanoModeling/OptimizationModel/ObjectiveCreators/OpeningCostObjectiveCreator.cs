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

namespace BobSuperStores.Data.Optimization.OptanoModeling.OptimizationModel.ObjectiveCreators;

using BobSuperStores.Data.Optimization.OptanoModeling.OptimizationModel.ParameterCalculation;

using JetBrains.Annotations;

using OPTANO.Modeling.Optimization;
using OPTANO.Modeling.Optimization.Enums;

/// <summary>
/// Class to add the cost for opening a connection between a facility and a client.
/// </summary>
[UsedImplicitly]
public class OpeningCostObjectiveCreator : ICreateObjectives
{
    #region Fields

    private readonly IModelData _modelData;

    private readonly IParameterCalculator _parameterCalculator;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="OpeningCostObjectiveCreator"/> class.
    /// </summary>
    /// <param name="modelData">The model data.</param>
    /// <param name="parameterCalculator">The parameter calculator.</param>
    public OpeningCostObjectiveCreator(IModelData modelData, IParameterCalculator parameterCalculator)
    {
        this._modelData = modelData;
        this._parameterCalculator = parameterCalculator;
    }

    #endregion

    #region Public Methods and Operators

    /// <inheritdoc />
    public IEnumerable<Objective> GetObjectivesToAdd(ModelVariables modelVariables)
    {
        var facilityOpeningCosts =
            Expression.Sum(
                this._modelData.Facilities
                    .Select(facility => this._parameterCalculator.GetOpeningCostForFacility(facility) * modelVariables.OpenFacility[facility]));

        yield return new Objective(
            facilityOpeningCosts,
            "fixed costs of opening the facilities",
            ObjectiveSense.Minimize,
            -1);
    }

    #endregion
}