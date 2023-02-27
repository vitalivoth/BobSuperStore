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

namespace BobSuperStores.Data.Csv;

using CommandLine;

/// <summary>
/// Represents the needed command line arguments, when the data is loaded from CSV files.
/// </summary>
[Verb("csv", isDefault: true, HelpText = "Uses the CSV format to import that data.")]
public class CsvCommandLineOptions : IValidatableCommandLineOptions
{
    #region Constants

    private const string StoresFileName = "SuperStore.csv";

    private const string WarehousesFileName = "Warehouse.csv";

    #endregion

    #region Public properties

    /// <summary>
    /// Gets or sets the directory where the application can expect the CSV files.
    /// </summary>
    [Option('s', "SourceFileDirectory", HelpText = "The directory containing the valid CSV files to import.", Required = true)]
    public string SourceFileDirectory { get; set; }

    #endregion

    #region Public Methods and Operators

    /// <inheritdoc/>
    public void Validate()
    {
        if (!Directory.Exists(this.SourceFileDirectory))
        {
            throw new ArgumentValidationException($"The specified directory '{this.SourceFileDirectory}' does not exists!");
        }

        this.CheckCsvFile(CsvCommandLineOptions.StoresFileName);
        this.CheckCsvFile(CsvCommandLineOptions.WarehousesFileName);
    }

    #endregion

    #region Methods

    private void CheckCsvFile(string fileName)
    {
        if (!File.Exists(Path.Combine(this.SourceFileDirectory, fileName)))
        {
            throw new ArgumentValidationException($"The specified directory '{this.SourceFileDirectory}' does not contain a {fileName}!");
        }
    }

    #endregion
}