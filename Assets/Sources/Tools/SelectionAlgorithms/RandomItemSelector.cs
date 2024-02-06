using System.Collections.Generic;
using UnityEngine;

namespace Clicker.Tools.SelectionAlgorithms
{
    /// <summary>
    /// Encapsulates an algorithm for sampling one random element from a control collection, every n iterations
    /// </summary>
    public class RandomItemSelector : ISelector
    {
        private int _iterationNumberSelectedForGeneration;
        private int _counter;

        private readonly int _maxIterationCount;

        public RandomItemSelector(int maxIterationCount)
        {
            _maxIterationCount = maxIterationCount;
        }

        /// <summary>
        /// every maxIterationCount from controlCollection select 1 random element
        /// </summary>
        /// <returns>selected element / collection of selected elements.</returns>
        /// <param name="controlCollection">Control collection - collection from which the selection is made</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public IEnumerable<T> SelectItems<T>(IEnumerable<T> controlCollection)
        {
            foreach (var item in controlCollection)
            {
                if (_counter == 0)
                    _iterationNumberSelectedForGeneration = Random.Range(0, _maxIterationCount);

                if (_counter == _iterationNumberSelectedForGeneration)
                    yield return item;

                _counter = (++_counter < _maxIterationCount) ? _counter : 0;
            }
        }
    }
}