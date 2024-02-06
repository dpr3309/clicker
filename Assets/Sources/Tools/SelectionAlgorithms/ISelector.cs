using System.Collections.Generic;

namespace Clicker.Tools.SelectionAlgorithms
{
    public interface ISelector
    {
        IEnumerable<T> SelectItems<T>(IEnumerable<T> controlCollection);
    }
}