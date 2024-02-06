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

        [SerializeField]
        private Vector2Int launchPadSize = Vector2Int.one;

        [SerializeField]
        private DifficultyLevel difficaltyLevel = (DifficultyLevel)(-1);

        [SerializeField]
        private CrystalPositionGeneratorType crystalPositionGeneratorType = (CrystalPositionGeneratorType)(-1);


        private GameSettings settings = new GameSettings(TileType.Square, 1, PlayerChipType.Circle, 0.5f);

        public override void InstallBindings()
        {
            InstallCrystalPositionGenerator(crystalPositionGeneratorType);
            InstallPlayerChipCoordinateProcessor(settings.PlayerChipType, settings.PlayerChipRadius);
            InstallTileCoordinateProcessor(settings.TileType, settings.TileSize);
            InstallCoordinateModifierManager(coordinateModifierTypes);

            InstallTilePositionGenerator(settings.TileType, settings.TileSize, difficaltyLevel, launchPadSize);

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

        private void InstallTilePositionGenerator(TileType tileType, float tileSize, DifficultyLevel difficultyLevel,
            Vector2Int launchPadSize)
        {
            if (tileSize <= 0)
                throw new Exception($"[ModelInstaller.InstallTilePositionGenerator] tile size <= 0");

            if (launchPadSize.x <= 0 || launchPadSize.y <= 0)
                throw new Exception($"[ModelInstaller.InstallTilePositionGenerator] incorrect launch pad dimensions");

            switch (tileType)
            {
                case TileType.Square:
                    Container.BindInterfacesTo<SquareTilePositionGenerator>().AsSingle()
                        .WithArguments(tileSize, difficultyLevel, launchPadSize);
                    break;
                default:
                    throw new Exception(
                        $"[ModelInstaller.InstallTilePositionGenerator] unhandled TileType : {tileType}");
            }
        }

        private void InstallCrystalPositionGenerator(CrystalPositionGeneratorType type)
        {
            switch (type)
            {
                case CrystalPositionGeneratorType.Random:
                    Container.BindInterfacesTo<RandomCrystalPositionGenerator>().AsSingle();
                    break;
                case CrystalPositionGeneratorType.InOrder:
                    Container.BindInterfacesTo<InOrderCrystalPositionGenerator>().AsSingle();
                    break;
                default:
                    throw new Exception($"[ModelInstaller.InstallCrystalPositionGenerator] unhandled CrystalPositionGeneratorType : {type}");
            }
        }
    }
}