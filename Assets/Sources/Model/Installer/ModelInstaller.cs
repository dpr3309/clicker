using System;
using System.Collections.Generic;
using System.Linq;
using Clicker.Model;
using Clicker.Model.FSMComponents;
using Clicker.Model.FSMComponents.States;
using Clicker.Tools.SelectionAlgorithms;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace Clicker.Installers
{
    internal class ModelInstaller : MonoInstaller
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

        [SerializeField]
        private int maxSelectorIterationCount = 5;


        private GameSettings settings = new GameSettings(TileType.Square, 1, PlayerChipType.Circle, 0.5f);

        public override void InstallBindings()
        {
            ValidateCoordinateModifiers(coordinateModifierTypes);
            InstallGameCoreFSM();

            InstallCrystalPositionGenerator(crystalPositionGeneratorType);
            InstallPlayerChipCoordinateProcessor(settings.PlayerChipType, settings.PlayerChipRadius);
            InstallTileCoordinateProcessor(settings.TileType, settings.TileSize);
            InstallTilePositionGenerator(settings.TileType, settings.TileSize, difficaltyLevel, launchPadSize);

            DependentFromDirectionProductsFactory.InstallCoordinateModifierManager(coordinateModifierTypes, Container);
            DependentFromDirectionProductsFactory.GenerateTilePositionGenerator(difficaltyLevel, settings.TileSize,
                coordinateModifierTypes, Container);
            Vector2Int directionModifier =
                DependentFromDirectionProductsFactory.GetDirectionModifiers(coordinateModifierTypes);
            Container.BindInterfacesTo<FieldModel>().AsSingle().WithArguments(directionModifier);

            Container.BindInterfacesTo<CoordinateProcessor>().AsSingle();
            Container.BindInterfacesTo<PlayerChipModel>().AsSingle();
            Container.BindInterfacesTo<CrystalModel>().AsSingle();
            Container.BindInterfacesTo<GameModel>().AsSingle();
            Container.BindInterfacesTo<GameInfoModel>().AsSingle();
            Container.Bind<ApplicationContext>().AsSingle();
        }

        private void ValidateCoordinateModifiers(List<CoordinateModifierTypes> coordinateModifiers)
        {
            if (coordinateModifiers.Count > 2)
                throw new Exception(
                    "[ModelInstaller.ValidateCoordinateModifiers] coordinateModifiers has lot items. Valid is 2 item");

            int result = 0;
            foreach (var item in coordinateModifiers)
            {
                result += IsVertical(item);
            }

            if (result != 1)
                throw new Exception(
                    $"[ModelInstaller.ValidateCoordinateModifiers] not valid coordinateModifiers." +
                    $" Valid is vertical & horizontal in any order. coordinateModifiers: {JsonConvert.SerializeObject(coordinateModifiers.Select(i => i.ToString()))}");

            int IsVertical(CoordinateModifierTypes coordinateModifierType)
            {
                switch (coordinateModifierType)
                {
                    case CoordinateModifierTypes.Backward:
                    case CoordinateModifierTypes.Forward:
                        return 1;
                    case CoordinateModifierTypes.Left:
                    case CoordinateModifierTypes.Right:
                        return 0;
                    default:
                        throw new Exception(
                            $"[ModelInstaller.ValidateCoordinateModifiers.IsVertical] unhandled coordinateModifierType: {coordinateModifierType.ToString()}");
                }
            }
        }

        private void InstallGameCoreFSM()
        {
            Container.BindInterfacesAndSelfTo<InitGameState>().AsSingle();
            Container.BindInterfacesAndSelfTo<ReadyToStartState>().AsSingle();
            Container.BindInterfacesAndSelfTo<InGameState>().AsSingle();
            Container.BindInterfacesAndSelfTo<EndOfGameState>().AsSingle();
            Container.BindInterfacesAndSelfTo<LostGameState>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
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
                    Container.BindInterfacesTo<RandomItemSelector>().AsSingle()
                        .WithArguments(maxSelectorIterationCount);
                    Container.BindInterfacesTo<RandomCrystalPositionGenerator>().AsSingle();
                    break;
                case CrystalPositionGeneratorType.InOrder:
                    Container.BindInterfacesTo<InOrderItemSelector>().AsSingle()
                        .WithArguments(maxSelectorIterationCount);
                    Container.BindInterfacesTo<InOrderCrystalPositionGenerator>().AsSingle();
                    break;
                default:
                    throw new Exception(
                        $"[ModelInstaller.InstallCrystalPositionGenerator] unhandled CrystalPositionGeneratorType : {type}");
            }
        }

        internal static class DependentFromDirectionProductsFactory
        {
            internal static void InstallCoordinateModifierManager(
                IReadOnlyList<CoordinateModifierTypes> coordinateModifierTypes, DiContainer container)
            {
                if (coordinateModifierTypes.Count == 0)
                    throw new Exception(
                        "[ModelInstaller.DependentFromDirectionProductsFactory.InstallCoordinateModifierManager]" +
                        " settings.coordinateModifierTypes.Count is 0");

                // checking for duplicate items in the collection
                if (coordinateModifierTypes.GroupBy(i => i).Any(i => i.Count() > 1))
                    throw new Exception(
                        "[ModelInstaller.DependentFromDirectionProductsFactory.InstallCoordinateModifierManager]" +
                        " settings.coordinateModifierTypes contains duplicate items");

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
                        case CoordinateModifierTypes.Left:
                            coordinateModifiers.Add(new LeftCoordinateModifier());
                            break;
                        case CoordinateModifierTypes.Backward:
                            coordinateModifiers.Add(new BackwardCoordinateModifier());
                            break;

                        default:
                            throw new Exception(
                                "[ModelInstaller.DependentFromDirectionProductsFactory.InstallCoordinateModifierManager]" +
                                $" unhandled coordinateModifierType : {coordinateModifierType}");
                    }
                }

                container.BindInterfacesTo<MainCoordinateModifierManager>().AsSingle()
                    .WithArguments(coordinateModifiers.ToArray());
            }

            internal static Vector2Int GetDirectionModifiers(IReadOnlyList<CoordinateModifierTypes> coordinateModifiers)
            {
                Vector2Int result = Vector2Int.one;

                if (coordinateModifiers.Contains(CoordinateModifierTypes.Left))
                    result.x = -1;
                if (coordinateModifiers.Contains(CoordinateModifierTypes.Backward))
                    result.y = -1;
                return result;
            }

            /// <summary>
            /// factory method, generates tiles position generator taking into account the current level of complexity
            /// </summary>
            /// <returns>The tile position generator.</returns>
            /// <param name="currentDifficultyLevel">Current difficulty level.</param>
            /// <param name="currentTileSize">Current tile size.</param>
            internal static void GenerateTilePositionGenerator(DifficultyLevel currentDifficultyLevel,
                float currentTileSize, List<CoordinateModifierTypes> coordinateModifiers, DiContainer container)
            {
                IDirectionPositionGenerator horizontalCoordinateGenerator = null;
                IDirectionPositionGenerator verticalCoordinateGenerator = null;
                foreach (var item in coordinateModifiers)
                {
                    switch (item)
                    {
                        case CoordinateModifierTypes.Right:
                            horizontalCoordinateGenerator = new RightPositionGenerator();
                            break;
                        case CoordinateModifierTypes.Forward:
                            verticalCoordinateGenerator = new ForwardPositionGenerator();
                            break;
                        case CoordinateModifierTypes.Left:
                            horizontalCoordinateGenerator = new LeftPositionGenerator();
                            break;
                        case CoordinateModifierTypes.Backward:
                            verticalCoordinateGenerator = new BackwardPositionGenerator();
                            break;

                        default:
                            throw new Exception(
                                "[ModelInstaller.DependentFromDirectionProductsFactory.GenerateTilePositionGenerator]" +
                                $" unhandled CoordinateModifierType: {item.ToString()}");
                    }
                }

                switch (currentDifficultyLevel)
                {
                    case DifficultyLevel.High:
                        container.BindInterfacesTo<HighLevelSquareTilePositionGenerator>().AsSingle()
                            .WithArguments(currentTileSize, verticalCoordinateGenerator, horizontalCoordinateGenerator);
                        break;
                    case DifficultyLevel.Middle:
                        container.BindInterfacesTo<MiddleLevelSquareTilePositionGenerator>().AsSingle()
                            .WithArguments(currentTileSize, verticalCoordinateGenerator, horizontalCoordinateGenerator);
                        break;
                    case DifficultyLevel.Low:
                        container.BindInterfacesTo<LowLevelSquareTilePositionGenerator>().AsSingle()
                            .WithArguments(currentTileSize, verticalCoordinateGenerator, horizontalCoordinateGenerator);
                        break;
                    default:
                        throw new Exception(
                            "[ModelInstaller.DependentFromDirectionProductsFactory.GenerateTilePositionGenerator] " +
                            $"unhandled difficulty level: {currentDifficultyLevel}");
                }
            }
        }
    }
}