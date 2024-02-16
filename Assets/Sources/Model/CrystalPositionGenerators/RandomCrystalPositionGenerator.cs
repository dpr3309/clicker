using Clicker.Tools.SelectionAlgorithms;
using Zenject;

namespace Clicker.Model
{
    internal sealed class RandomCrystalPositionGenerator : BaseCrystalPositionGenerator
    {
        [Inject]
        private RandomCrystalPositionGenerator(ISelector selector)
            : base(selector)
        {
        }
    }
}