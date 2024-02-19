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

namespace BobSuperStores.DataSource;

using BobSuperStores.Data;
using BobSuperStores.DataSource;
using BobSuperStores.Data.Logging;
using JetBrains.Annotations;
using Newtonsoft.Json;

/// <summary>
/// The data source implementation that loads the data from a JSON file.
/// </summary>
/// <typeparam name="T">The type that can be loaded by the data source.</typeparam>
public class JsonDataSource<T> : IDataSource<T>
{
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonDataSource{T}"/> class.
    /// </summary>
    /// <param name="options">The options that were provided from the command line.</param>
    /// <param name="logger">The logger.</param>
    public JsonDataSource([NotNull] string options, [NotNull] IOptanoLogger logger)
    {
        Options = options ?? throw new ArgumentNullException(nameof(options));
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    #endregion

    #region Properties

    [NotNull]
    private string Options { get; }

    [NotNull]
    private IOptanoLogger Logger { get; }

    #endregion

    #region Public Methods and Operators

    /// <inheritdoc />
    public IEnumerable<T> LoadData(DataContext currentContext)
    {
        var path = Path.Combine(Options, $"{typeof(T).Name}.json");
        Logger.Log(LogLevel.Information, "Loading {0} from JSON source {1}...", typeof(T).Name, path);
        var foo = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(path));
        return foo;
    }

    #endregion
}