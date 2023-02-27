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

namespace BobSuperStores.Tests.Csv;

using BobSuperStores.Data;
using BobSuperStores.Data.Csv;
using BobSuperStores.Data.Logging;
using BobSuperStores.Data.Model;

using Moq;

/// <summary>
/// Tests the <see cref="CsvDataSource{T}"/>.
/// </summary>
[TestFixture]
public class CsvDataSourceTests
{
    #region Public Methods and Operators

    /// <summary>
    /// Test that the data context is correctly loaded from the SuperStore CSV file.
    /// </summary>
    /// <returns>>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Test]
    public async Task TestSuperStoreSource()
    {
        var dataSource = new CsvDataSource<SuperStore>(
            new CsvCommandLineOptions { SourceFileDirectory = @"..\..\..\..\CsvData" },
            Mock.Of<IOptanoLogger>());
        await Verifier.Verify(dataSource.LoadData(new DataContext()));
    }

    /// <summary>
    /// Test that the data context is correctly loaded from the Warehouse CSV file.
    /// </summary>
    /// <returns>>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Test]
    public async Task TestWarehouseSource()
    {
        var dataSource = new CsvDataSource<Warehouse>(
            new CsvCommandLineOptions { SourceFileDirectory = @"..\..\..\..\CsvData" },
            Mock.Of<IOptanoLogger>());
        await Verifier.Verify(dataSource.LoadData(new DataContext()));
    }

    #endregion
}