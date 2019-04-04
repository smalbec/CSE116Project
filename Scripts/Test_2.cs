using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;

namespace Tests
{
    public class Test_2
    {
        static public Player test2 = new Player();
        // A Test behaves as an ordinary method
        [Test]
        public void Test_2SimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator Test_2WithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.

            Player.person.health = 100.0f;
            Player.person.rate = 0.1f;
            Player.person.size = 1.0f; 
            Player.Degen();
            Assert.AreEqual(Player.person.health, 99.9f);
            Console.WriteLine(Player.person.health);

            Player.person.health = 66.2f;
            Player.Degen();
            Assert.AreEqual(Player.person.health, 66.1f);

            Player.person.health = 0f;
            Player.Degen();
            Assert.AreEqual(Player.person.health, 0f);

            Player.person.health = -1111f;
            Player.Degen();
            Assert.AreEqual(Player.person.health, 0f);


            yield return null;
        }
    }
}
