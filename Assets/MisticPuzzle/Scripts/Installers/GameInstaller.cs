using System.Collections.Generic;
using Zenject;

namespace Lonely
{
    public class GameInstaller : MonoInstaller<GameInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindAllInterfacesAndSelf<MisticPuzzleInput>().To<MisticPuzzleInput>().AsSingle();

            Container.BindAllInterfacesAndSelf<GameManager>().To<GameManager>().AsSingle();

            Container.BindFactory<List<IState>, FSM, FSM.Factory>();
        }
    }
}