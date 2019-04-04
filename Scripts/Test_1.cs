using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Test_1
    {
        // A Test behaves as an ordinary method
        [Test]
        public void Test_1SimplePasses()
        {
       
            
                // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator Test_1WithEnumeratorPasses()
        {
            Player.person.size = 1.0f;
            Player.vore_2.kill = 1;
            Assert.AreNotEqual(Player.vore_2.kill, Player.vore_1.kill);
            Assert.AreEqual(Player.vore_1.kill, 0);
            Assert.AreEqual(Player.person.size, 1.0f);
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            Player.Upsize();
            Assert.AreEqual(Player.vore_1.kill, Player.vore_2.kill);
            Assert.AreNotEqual(Player.person.size, 1.0f);
            yield return null;
        }
    }
}
