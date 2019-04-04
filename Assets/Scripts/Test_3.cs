using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Test_3
    {
        // A Test behaves as an ordinary method
        [Test]
        public void Test_3SimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator Test_3WithEnumeratorPasses()
        {
            Player.vore_1.kill = 1;
            Player.vore_2.kill = -1;
            Player.Compare();
            Assert.AreEqual(Player.vore_1.kill, 1);
            Assert.AreEqual(Player.vore_2.kill, 0);
            Player.vore_1.kill = 0;
            Player.vore_2.kill = 5000;
            Player.Compare();
            Assert.AreEqual(Player.vore_2.kill, 5000);
            Assert.AreEqual(Player.vore_1.kill, 0);
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
