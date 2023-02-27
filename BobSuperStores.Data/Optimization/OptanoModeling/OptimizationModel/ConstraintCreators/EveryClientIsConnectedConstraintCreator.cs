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

using BobSuperStores.Data.Model.Interfaces;

using JetBrains.Annotations;

using OPTANO.Modeling.Optimization;

/// <summary>
/// Constraints ensuring that every client is connected to exactly one facility.
/// </summary>
[UsedImplicitly]
public class EveryClientIsConnectedConstraintCreator : ICreateConstraints
{
    #region Fields

    private readonly IModelData _modelData;

    private readonly ConstraintNameGenerator _constraintNameGenerator;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="EveryClientIsConnectedConstraintCreator"/> class.
    /// </summary>
    /// <param name="modelData">the model data.</param>
    /// <param name="constraintNameGenerator">the name generator for the constraints.</param>
    public EveryClientIsConnectedConstraintCreator(IModelData modelData, ConstraintNameGenerator constraintNameGenerator)
    {
        this._modelData = modelData;
        this._constraintNameGenerator = constraintNameGenerator;
    }

    #endregion

    #region Public Methods and Operators

    /// <inheritdoc />
    public IEnumerable<Constraint> GetConstraints(ModelVariables modelVariables)
    {
        var distancesForClients = this._modelData.Distances
            .GroupBy(x => x.Source)
            .ToDictionary(k => k.Key, v => v.Select(x => x).ToList());

        foreach (var client in this._modelData.Clients)
        {
            distancesForClients.TryGetValue(client, out var distances);
            distances ??= new List<IDistance>();

            var constraint = Expression.Sum(distances.Select(d => modelVariables.UseConnection[d])) == 1;
            yield return
                this._constraintNameGenerator.AddNameToConstraint(
                    constraint,
                    $"ClientConnected_{client.Name}");
        }
    }

    #endregion
}