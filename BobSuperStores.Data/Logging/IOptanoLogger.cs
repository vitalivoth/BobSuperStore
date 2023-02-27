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

namespace BobSuperStores.Data.Logging
{
    /// <summary>
    /// Logging interface to use customizable logging for an optimization job.
    /// </summary>
    public interface IOptanoLogger
    {
        #region Public Methods and Operators

        /// <summary>
        /// Logs the given message at the specified log level.
        /// </summary>
        /// <param name="logLevel">The log level.</param>
        /// <param name="format">The string format.</param>
        /// <param name="parameter">The parameter for string format.</param>
        void Log(LogLevel logLevel, string format, params object[] parameter);

        /// <summary>
        /// Logs the given exception.
        /// </summary>
        /// <param name="exception">the exception to log.</param>
        void LogException(Exception exception);

        #endregion
    }
}