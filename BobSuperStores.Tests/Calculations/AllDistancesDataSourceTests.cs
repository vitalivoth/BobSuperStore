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

namespace BobSuperStores.Tests.Calculations;

using BobSuperStores.Data;
using BobSuperStores.Data.Calculations;
using BobSuperStores.Data.Logging;
using BobSuperStores.Data.Model;

using Moq;

using Newtonsoft.Json;

/// <summary>
/// Tests the <see cref="AllDistancesDataSource"/>.
/// </summary>
[TestFixture]
public class AllDistancesDataSourceTests
{
    #region Public Methods and Operators

    /// <summary>
    /// Tests that all distances are calculated.
    /// </summary>
    /// <returns>>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Test]
    public async Task TestCalculation()
    {
        var currentAssembly = typeof(AllDistancesDataSourceTests).Assembly;
        var dataNamespace = $"{typeof(AllDistancesDataSourceTests).Namespace}.Data";
        var context = new DataContext();

        using var storeReader = new StreamReader(currentAssembly.GetManifestResourceStream($"{dataNamespace}.SuperStores.json") !);
        context.SuperStores.AddRange(JsonConvert.DeserializeObject<List<SuperStore>>(await storeReader.ReadToEndAsync()));
        using var warehouseReader = new StreamReader(currentAssembly.GetManifestResourceStream($"{dataNamespace}.Warehouses.json") !);
        context.Warehouses.AddRange(JsonConvert.DeserializeObject<List<Warehouse>>(await warehouseReader.ReadToEndAsync()));

        var logger = Mock.Of<IOptanoLogger>();
        await Verifier.Verify(new AllDistancesDataSource(new HaversineFormulaDistanceCalculator(logger), logger).LoadData(context));
    }

    #endregion
}