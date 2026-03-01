using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using Andrey04o.Chess;

namespace Andrey04o.Chess.Tests
{
    public class TestChess
    {
        GridGenerator gridGenerator;
        [SetUp]
        public void Setup() {
            gridGenerator = AssetDatabase.LoadAssetAtPath<GridGenerator>("Assets/UdonChess/Runtime/Prefabs/GridGenerator.prefab");
            if (gridGenerator != null) {
                Debug.Log("fine");
            }
            gridGenerator = Object.Instantiate(gridGenerator, Vector3.zero, Quaternion.identity);
        }
        [TearDown]
        public void Teardown()
        {
            Object.DestroyImmediate(gridGenerator);
        }
        [Test]
        public void SimpleTest()
        {
            Assert.IsNotNull(gridGenerator);
        }
    }
}
