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

namespace BobSuperStores.Data.Optimization.OptanoModeling.OptimizationModel
{
    using BobSuperStores.Data.Logging;
    using BobSuperStores.Data.Optimization.OptanoModeling.OptimizationModel.SolutionExtractor;

    using JetBrains.Annotations;

    using OPTANO.Modeling.Common;
    using OPTANO.Modeling.Optimization;
    using OPTANO.Modeling.Optimization.Configuration;

    /// <summary>
    /// Class to solve an optimization problem using modeling and extracting the solution from the solver solution.
    /// </summary>
    /// <seealso cref="IOptimizationProblemSolver" />
    public class OptimizationProblemSolver : IOptimizationProblemSolver
    {
        #region Fields

        private readonly IOptimizationModelDefinition _modelDefinition;

        private readonly ISolutionExtractor _solutionExtractor;

        private readonly IOptanoLogger _logger;

        private Model _model;

        private ModelVariables _modelVariables;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OptimizationProblemSolver"/> class.
        /// </summary>
        /// <param name="modelDefinition">The model definition.</param>
        /// <param name="solutionExtractor">The solution extractor.</param>
        /// <param name="logger">The logger.</param>
        public OptimizationProblemSolver(
            IOptimizationModelDefinition modelDefinition,
            ISolutionExtractor solutionExtractor,
            IOptanoLogger logger)
        {
            this._modelDefinition = modelDefinition;
            this._solutionExtractor = solutionExtractor;
            this._logger = logger;
            this._model = null;
            this._modelVariables = null;
        }

        #endregion

        #region Public Methods and Operators

        /// <inheritdoc />
        [NotNull]
        public ProblemSolution SolveProblem()
        {
            ProblemSolution solution = null;
            using (var scope = this.GetModelScope())
            {
                this._logger.Log(LogLevel.Information, "Building Optimization Model");
                this.FillModel();

                this._logger.Log(LogLevel.Information, "Solving Optimization Model");
                solution = this.SolveModel();
            }

            return solution;
        }

        #endregion

        #region Methods

        [NotNull]
        private ProblemSolution SolveModel()
        {
            var problemSolution = new ProblemSolution();
            var startValuesVariables = this.CalculateStartValues();

            using (var solver = this._modelDefinition.SolverCreationFunction())
            {
                this._logger.Log(
                    LogLevel.Information,
                    "Start solving the optimization problem with {0} variables, {1} constraints, {2} start values",
                    this._model.VariablesCount,
                    this._model.ConstraintsCount,
                    startValuesVariables.Count);

                var solution = solver.Solve(this._model, startValuesVariables);
                this._logger.Log(LogLevel.Information, "Optimization Problem solved with solution status {0}", solution?.Status);

                if (solution != null)
                {
                    this._logger.Log(LogLevel.Information, "Creating optimization result");
                    problemSolution = this._solutionExtractor.GetSolution(solution, this._model, this._modelVariables);
                }
            }

            return problemSolution;
        }

        private void FillModel()
        {
            this._logger.Log(LogLevel.Information, "Starting to fill the model");
            this.InitializeModelCreation();
            this._logger.Log(LogLevel.Information, "Creating variables");
            this.CreateVariables();
            this._logger.Log(LogLevel.Information, "Creating constraints");
            this.CreateConstraints();
            this._logger.Log(LogLevel.Information, "Creating objectives");
            this.CreateObjectives();
        }

        private void InitializeModelCreation()
        {
            this._modelVariables = new ModelVariables();
            this._model = new Model();
        }

        private void CreateObjectives()
        {
            var objectivesToAdd = this._modelDefinition
                .ObjectiveFunctionCreators
                .SelectMany(ocf => ocf.GetObjectivesToAdd(this._modelVariables));

            objectivesToAdd.ForEach(o => this._model.AddObjective(o));
        }

        private void CreateConstraints()
        {
            this._modelDefinition
                .ConstraintCreators
                .ForEach(
                    cc =>
                        {
                            var name = cc.GetType().Name;
                            this._logger.Log(LogLevel.Debug, $"Start calculating constraints of type {name}");
                            var constraints = cc.GetConstraints(this._modelVariables).ToList();

                            this._logger.Log(LogLevel.Debug, $"Start adding {constraints.Count} constraints of type {name}");
                            this.AddConstraints(constraints);

                            this._logger.Log(LogLevel.Debug, $"End adding constraints of type {name}");
                        });
        }

        private void AddConstraints([NotNull] IEnumerable<Constraint> constraints)
        {
            this._model.AddConstraints(constraints.Where(c => c.IsRelevant()));
        }

        private void CreateVariables()
        {
            this._modelDefinition
                .VariableCreators
                .ForEach(vi => vi.CreateVariables(this._modelVariables, this._model));
        }

        [NotNull]
        private Dictionary<Variable, double> CalculateStartValues()
        {
            this._logger.Log(LogLevel.Information, "Calculating start values");
            var startValues = this._modelDefinition.StartValueCalculator.CalculateStartValues(this._model);
            return startValues;
        }

        [NotNull]
        private IDisposable GetModelScope()
        {
            var scopeSettings = new Configuration
                                    {
                                        NameHandling = NameHandlingStyle.UniqueLongNames,
                                        IndexValidationStyle = IndexValidationStyle.Disabled,
                                        ComputeRemovedVariables = false,
                                    };
            var scope = new ModelScope(scopeSettings);
            return scope;
        }

        #endregion
    }
}