using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Test_4
    {
        // A Test behaves as an ordinary method
        [Test]
        public void Test_4SimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator Test_4WithEnumeratorPasses()
        {
            Player.person.base_damage = 25f;
            Player.vore_2.kill = 0;
            Player.Damage();
            Assert.AreEqual(Player.person.damage, 25f);
            Player.vore_2.kill = -1;
            Player.Damage();
            Assert.AreEqual(Player.person.damage, 25f);
            Player.vore_2.kill = 10;
            Player.Damage();
            Assert.AreEqual(Player.person.damage, 275f);
            yield return null;
        }
    }
}
