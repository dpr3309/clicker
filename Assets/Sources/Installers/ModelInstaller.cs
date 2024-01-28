using System;
using System.Collections.Generic;
using System.Linq;
using Clicker.Model;
using UnityEngine;
using Zenject;

namespace Clicker.Installers
{
    public class ModelInstaller : MonoInstaller
    {
        [SerializeField]
        private List<CoordinateModifierTypes> coordinateModifierTypes =
            new List<CoordinateModifierTypes>() { CoordinateModifierTypes.Forward, CoordinateModifierTypes.Right };

        private GameSettings settings = new GameSettings(TileType.Square, 1, PlayerChipType.Circle, 0.5f);

        public override void InstallBindings()
        {
            InstallPlayerChipCoordinateProcessor(settings.PlayerChipType, settings.PlayerChipRadius);
            InstallTileCoordinateProcessor(settings.TileType, settings.TileSize);
            InstallCoordinateModifierManager(coordinateModifierTypes);

            Container.BindInterfacesTo<CoordinateProcessor>().AsSingle();
            Container.BindInterfacesTo<FieldModel>().AsSingle();
            Container.BindInterfacesTo<PlayerChipModel>().AsSingle();
            Container.BindInterfacesTo<CrystalModel>().AsSingle();
            Container.BindInterfacesTo<GameModel>().AsSingle();
        }

        private void InstallPlayerChipCoordinateProcessor(PlayerChipType playerChipType, float playerChipRadius)
        {
            switch (playerChipType)
            {
                case PlayerChipType.Circle:
                    Container.BindInterfacesTo<PlayerBallCoordinateProcessor>().AsSingle()
                        .WithArguments(playerChipRadius);
                    break;
                default:
                    throw new Exception(
                        $"[ModelInstaller.InstallPlayerChipCoordinateProcessor] unhandled TileType : {playerChipType}");
            }
        }

        private void InstallTileCoordinateProcessor(TileType tileType, float tileSize)
        {
            switch (tileType)
            {
                case TileType.Square:
                    Container.BindInterfacesTo<SquareTileCoordinateProcessor>().AsSingle().WithArguments(tileSize);
                    break;
                default:
                    throw new Exception(
                        $"[ModelInstaller.InstallTileCoordinateProcessor] unhandled TileType : {tileType}");
            }
        }

        private void InstallCoordinateModifierManager(List<CoordinateModifierTypes> coordinateModifierTypes)
        {
            if (coordinateModifierTypes.Count == 0)
                throw new Exception(
                    "[ModelInstaller.InstallCoordinateModifierManager] settings.coordinateModifierTypes.Count ==0");

            // checking for duplicate items in the collection
            if (coordinateModifierTypes.GroupBy(i => i).Any(i => i.Count() > 1))
                throw new Exception(
                    "[ModelInstaller.InstallCoordinateModifierManager] settings.coordinateModifierTypes contains duplicate items");

            List<ICoordinateModifier> coordinateModifiers = new List<ICoordinateModifier>();
            foreach (var coordinateModifierType in coordinateModifierTypes)
            {
                switch (coordinateModifierType)
                {
                    case CoordinateModifierTypes.Forward:
                        coordinateModifiers.Add(new ForwardCoordinateModifier());
                        break;
                    case CoordinateModifierTypes.Right:
                        coordinateModifiers.Add(new RightCoordinateModifier());
                        break;
                    default:
                        throw new Exception(
                            $"[ModelInstaller.InstallCoordinateModifierManager] unhandled coordinateModifierType : {coordinateModifierType}");
                }
            }

            Container.BindInterfacesTo<MainCoordinateModifierManager>().AsSingle()
                .WithArguments(coordinateModifiers.ToArray());
        }
    }
}