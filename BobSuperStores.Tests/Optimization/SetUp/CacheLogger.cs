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

namespace BobSuperStores.Tests.Optimization.SetUp;

using BobSuperStores.Data.Logging;

/// <summary>
/// Logging class which caches the logged entries and yield methods to check if certain entries are contained.
/// </summary>
public class CacheLogger : IOptanoLogger
{
    #region Fields

    private readonly List<string> _logs;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="CacheLogger"/> class.
    /// </summary>
    public CacheLogger()
    {
        this._logs = new List<string>();
    }

    #endregion

    #region Public Methods and Operators

    /// <inheritdoc />
    public void Log(LogLevel logLevel, string format, params object[] parameter)
    {
        var text = string.Format(format, parameter);
        this._logs.Add($"{logLevel}: {text}");
    }

    /// <summary>
    /// Method to check if an entry fitting the given predicate has been logged.
    /// </summary>
    /// <param name="stringCheck">The predicate to check the logged entries.</param>
    /// <returns>If an entry fitting the predicate is contained.</returns>
    public bool ContainsFittingEntry(Predicate<string> stringCheck)
    {
        return this._logs.Any(stringCheck.Invoke);
    }

    /// <inheritdoc />
    public void LogException(Exception exception)
    {
        this.Log(LogLevel.Error, exception.ToString());
    }

    #endregion
}