using CarteAuxTresors.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CarteAuxTresor.TEST
{
    [TestClass]
    public class AdventurerTest
    {
        [TestMethod]
        public void MoveLeftTest()
        {
            var adv = new Adventurer();
            Assert.IsTrue(adv.Orientation == OrientationEnum.N);

            adv.MoveLeft();
            Assert.IsTrue(adv.Orientation == OrientationEnum.W);

            adv.MoveLeft();
            Assert.IsTrue(adv.Orientation == OrientationEnum.S);

            adv.MoveLeft();
            Assert.IsTrue(adv.Orientation == OrientationEnum.E);

            adv.MoveLeft();
            Assert.IsTrue(adv.Orientation == OrientationEnum.N);
        }

        [TestMethod]
        public void MoveRightTest()
        {
            var adv = new Adventurer();
            Assert.IsTrue(adv.Orientation == OrientationEnum.N);

            adv.MoveRight();
            Assert.IsTrue(adv.Orientation == OrientationEnum.E);

            adv.MoveRight();
            Assert.IsTrue(adv.Orientation == OrientationEnum.S);

            adv.MoveRight();
            Assert.IsTrue(adv.Orientation == OrientationEnum.W);

            adv.MoveRight();
            Assert.IsTrue(adv.Orientation == OrientationEnum.N);
        }

        [TestMethod]
        public void MoveToTest()
        {
            var sequence = new List<char> { 'A' };
            var adventurer = new Adventurer(0, 0, "Indiana Johns", OrientationEnum.E, sequence);
            Assert.IsTrue(adventurer.Position == (0, 0));

            adventurer.MoveTo((0, 1));
            Assert.IsTrue(adventurer.Position == (0,1));
        }
    }
}
