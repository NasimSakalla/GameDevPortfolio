using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;

public class TestGravity
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestGravitySanityTest()
    {
        // Set up the StartPracticeForTest object
        var testObject = new GameObject("StartPracticeForTest");
        var startPractice = testObject.AddComponent<StartPracticeForTest>();

        // Set up the player GameObject with a Rigidbody
        var playerObject = new GameObject("Player");
        var rb = playerObject.AddComponent<Rigidbody>(); // Add Rigidbody
        rb.useGravity = true; // Ensure gravity is initially enabled

        // Assign the player to the StartPracticeForTest script
        startPractice.player = playerObject;

        // Call StartPracticeFunction to turn off gravity
        startPractice.StartPracticeFunction();

        // Assert that gravity is off
        Assert.IsFalse(rb.useGravity, "Gravity should be disabled.");
        Assert.IsTrue(startPractice.gravIsOff, "gravIsOff should be true.");
    }

}
