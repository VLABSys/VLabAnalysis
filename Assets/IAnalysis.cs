﻿/*
IAnalysis.cs is part of the VLAB project.
Copyright (c) 2016 Li Alex Zhang and Contributors

Permission is hereby granted, free of charge, to any person obtaining a 
copy of this software and associated documentation files (the "Software"),
to deal in the Software without restriction, including without limitation
the rights to use, copy, modify, merge, publish, distribute, sublicense,
and/or sell copies of the Software, and to permit persons to whom the 
Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included 
in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF 
OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Concurrent;
using VLab;

namespace VLabAnalysis
{
    public enum AnalysisSystem
    {
        ConditionTest
    }

    public enum AnalysisInterface
    {
        IAnalyzer,
        IVisualizer,
        IController
    }

    public enum VisualizeMode
    {
        First,
        Last,
        All
    }

    public struct AnalysisEvent
    {
        public readonly int Index;
        public readonly bool IsClear;
        public readonly double Time;

        public AnalysisEvent(int index = 0, bool isclear = false, double time = 0)
        {
            Index = index;
            IsClear = isclear;
            Time = time;
        }
    }

    /// <summary>
    /// Implementation should be thread safe
    /// </summary>
    public interface IAnalysis : IDisposable
    {
        ISignal Signal { get; set; }
        void CondTestEnqueue(CONDTESTPARAM name, object value);
        void CondTestEndEnqueue(double time);
        void ExperimentEndEnqueue();
        VLADataSet DataSet { get; }
        int ClearDataPerAnalysis { get; set; }
        int RetainAnalysisPerClear { get; set; }
        ConcurrentDictionary<int, IAnalyzer> Analyzers { get; }
        void AddAnalyzer(IAnalyzer analyzer);
        void RemoveAnalyzer(int analyzerid);
        void Start();
        void Stop();
        void VisualizeResults(VisualizeMode mode);
        bool IsExperimentAnalysisDone { get; set; }
        void SaveVisualization(int width, int height, int dpi);
        void Restart();
        bool IsAnalyzing { get; }
        int AnalysisEventIndex { get; }
        int AnalysisDone { get; }
        int VisualizationDone { get; }
    }
}