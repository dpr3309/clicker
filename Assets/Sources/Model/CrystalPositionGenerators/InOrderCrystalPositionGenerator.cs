using Clicker.Tools.SelectionAlgorithms;
using Zenject;

namespace Clicker.Model
{
    internal sealed class InOrderCrystalPositionGenerator : BaseCrystalPositionGenerator
    {
        [Inject]
        private InOrderCrystalPositionGenerator(ISelector selector)
            : base(selector)
        {
        }
    }
}