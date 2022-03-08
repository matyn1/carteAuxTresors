using CarteAuxTresors.Helpers;
using CarteAuxTresors.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CarteAuxTresor.TEST
{
    [TestClass]
    public class GameTest
    {

        [TestMethod]
        public void InitializeGameTest()
        {
            var directoryfolder = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));

            string filePath = Path.Combine(directoryfolder, "EntryTestNoMap.txt");
            Assert.IsFalse(GameHelper.InitializeGame(filePath));

            GameHelper.ResetGame();
            filePath = Path.Combine(directoryfolder, "EntryTestNoAdventurer.txt");
            Assert.IsFalse(GameHelper.InitializeGame(filePath));

            GameHelper.ResetGame();
            filePath = Path.Combine(directoryfolder, "EntryTestAdventurerInMountain.txt");
            Assert.IsFalse(GameHelper.InitializeGame(filePath));

            GameHelper.ResetGame();
            filePath = Path.Combine(directoryfolder, "EntryTest.txt");
            Assert.IsTrue(GameHelper.InitializeGame(filePath));
        }

        [TestMethod]
        public void CalculateNextPositionTest()
        {
            GameHelper.ResetGame();

            var sequence = new List<char> { 'A' };
            var adventurer = new Adventurer(1, 1, "Indiana Johns", OrientationEnum.N, sequence);

            var nextPosition = GameHelper.CalculateNextPosition(adventurer);
            Assert.IsTrue(nextPosition == (1, 0));

            adventurer = new Adventurer(1, 1, "Indiana Johns", OrientationEnum.W, sequence);
            nextPosition = GameHelper.CalculateNextPosition(adventurer);
            Assert.IsTrue(nextPosition == (0, 1));

            adventurer = new Adventurer(1, 1, "Indiana Johns", OrientationEnum.S, sequence);
            nextPosition = GameHelper.CalculateNextPosition(adventurer);
            Assert.IsTrue(nextPosition == (1, 2));

            adventurer = new Adventurer(1, 1, "Indiana Johns", OrientationEnum.E, sequence);
            nextPosition = GameHelper.CalculateNextPosition(adventurer);
            Assert.IsTrue(nextPosition == (2, 1));
        }

        [TestMethod]
        public void IsPositionAuthorizedTest()
        {
            GameHelper.ResetGame();

            GameHelper.Map = new Map(3, 3);

            var sequence = new List<char> { 'A' };
            var adventurer = new Adventurer(0, 0, "Indiana Johns", OrientationEnum.N, sequence);

            var nextPosition = GameHelper.CalculateNextPosition(adventurer);
            Assert.IsFalse(GameHelper.IsPositionAuthorized(nextPosition));

            GameHelper.Mountains.Add(new Mountain(0, 1));
            adventurer.Orientation = OrientationEnum.S;

            nextPosition = GameHelper.CalculateNextPosition(adventurer);
            Assert.IsFalse(GameHelper.IsPositionAuthorized(nextPosition));

            adventurer.Orientation = OrientationEnum.E;

            nextPosition = GameHelper.CalculateNextPosition(adventurer);
            Assert.IsTrue(GameHelper.IsPositionAuthorized(nextPosition));

            adventurer.Position = (2, 0);
            adventurer.Orientation = OrientationEnum.E;

            nextPosition = GameHelper.CalculateNextPosition(adventurer);
            Assert.IsFalse(GameHelper.IsPositionAuthorized(nextPosition));

        }

        [TestMethod]
        public void RetrieveTreasureTest()
        {
            GameHelper.ResetGame();

            GameHelper.Map = new Map(3, 3);

            var sequence = new List<char> { 'A' };
            var adventurer = new Adventurer(0, 0, "Indiana Johns", OrientationEnum.E, sequence);

            GameHelper.Treasures.Add(new Treasure(0, 1, 1));
            Assert.IsTrue(GameHelper.Treasures.First().Count == 1);

            adventurer.MoveTo((0, 1));
            GameHelper.RetrieveTreasure(adventurer);

            Assert.IsTrue(adventurer.NbTreasures == 1);
            Assert.IsTrue(GameHelper.Treasures.First().Count == 0);
        }
    }
}
