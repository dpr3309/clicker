using System.Collections.Generic;

namespace Clicker.Tools.SelectionAlgorithms
{
    /// <summary>
    /// Encapsulates the algorithm for sampling 1 element from the control collection, every n iterations, in order, with counter increment
    /// </summary>
    public sealed class InOrderItemSelector : ISelector
    {
        private int _iterationNumberSelectedForGeneration;
        private int _counter;

        private readonly int _maxIterationCount;

        public InOrderItemSelector(int maxIterationCount)
        {
            _maxIterationCount = maxIterationCount;
            _iterationNumberSelectedForGeneration = -1;
        }

        /// <summary>
        /// every maxIterationCount from controlCollection select 1 element the index of which is in the group from maxIterationCount elements,
        /// and equal iterationNumberSelectedForGeneration
        /// in other words, from the first group of size in maxIterationCount, select first element, from second group - second element, ...
        /// </summary>
        /// <returns>selected element / collection of selected elements.</returns>
        /// <param name="controlCollection">Control collection - collection from which the selection is made</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public IEnumerable<T> SelectItems<T>(IEnumerable<T> controlCollection)
        {
            foreach (var item in controlCollection)
            {
                if (_counter == 0)
                    _iterationNumberSelectedForGeneration =
                        (++_iterationNumberSelectedForGeneration < _maxIterationCount)
                            ? _iterationNumberSelectedForGeneration
                            : 0;

                if (_counter == _iterationNumberSelectedForGeneration)
                    yield return item;

                _counter = (++_counter < _maxIterationCount) ? _counter : 0;
            }
        }
    }
}