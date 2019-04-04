using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Test_5
    {
        // A Test behaves as an ordinary method
        [Test]
        public void Test_5SimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator Test_5WithEnumeratorPasses()
        {
            Player.person.health = 100f;
            Player.vore_2.kill = 0;
            Player.Upsize();
            Assert.AreEqual(Player.person.health, 100f);
            Player.vore_2.kill = 1;
            Player.Upsize();
            Assert.AreEqual(Player.person.health, 125f);
            Player.vore_2.kill = 10;
            Player.Upsize();
            Assert.AreEqual(Player.person.health, 350f);
            yield return null;
        }
    }
}
