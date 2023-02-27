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

namespace BobSuperStores.Data.Optimization.OptanoModeling.OptimizationModel.VariableCreators;

using BobSuperStores.Data.Model.Interfaces;

using JetBrains.Annotations;

using OPTANO.Modeling.Optimization;
using OPTANO.Modeling.Optimization.Enums;

/// <summary>
/// Class for creating the variables for opening a facility.
/// </summary>
[UsedImplicitly]
public class OpenFacilityVariableCreator : IVariableInitializer
{
    #region Fields

    private readonly IModelData _modelData;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="OpenFacilityVariableCreator"/> class.
    /// </summary>
    /// <param name="modelData">The model data.</param>
    public OpenFacilityVariableCreator(IModelData modelData)
    {
        this._modelData = modelData;
    }

    #endregion

    #region Public Methods and Operators

    /// <inheritdoc />
    public void CreateVariables(ModelVariables modelVariables, Model model)
    {
        modelVariables.OpenFacility = new VariableCollection<ILocation>(
            model,
            this._modelData.Facilities,
            "open",
            f => $"openFacility_{f.Name}",
            variableTypeGenerator: facility => VariableType.Binary);
    }

    #endregion
}