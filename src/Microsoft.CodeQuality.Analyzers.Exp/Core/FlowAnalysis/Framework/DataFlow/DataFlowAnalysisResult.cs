﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis.Operations.ControlFlow;

namespace Microsoft.CodeAnalysis.Operations.DataFlow
{
    /// <summary>
    /// Result from execution of a <see cref="DataFlowAnalysis"/> on a control flow graph.
    /// It stores:
    ///  (1) Analysis values for all operations in the graph and
    ///  (2) <see cref="AbstractBlockAnalysisResult{TAnalysisData, TAbstractAnalysisValue}"/> for every basic block in the graph.
    /// </summary>
    internal class DataFlowAnalysisResult<TAnalysisResult, TAbstractAnalysisValue>
        where TAnalysisResult: class
    {
        private readonly ImmutableDictionary<BasicBlock, TAnalysisResult> _basicBlockStateMap;
        private readonly ImmutableDictionary<IOperation, TAbstractAnalysisValue> _operationStateMap;
        public DataFlowAnalysisResult(ImmutableDictionary<BasicBlock, TAnalysisResult> basicBlockStateMap, ImmutableDictionary<IOperation, TAbstractAnalysisValue> operationStateMap)
        {
            _basicBlockStateMap = basicBlockStateMap;
            _operationStateMap = operationStateMap;
        }

        public TAnalysisResult this[BasicBlock block] => _basicBlockStateMap[block];
        public TAbstractAnalysisValue this[IOperation operation] => _operationStateMap[operation];
    }
}
