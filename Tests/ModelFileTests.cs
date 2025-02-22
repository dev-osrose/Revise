#region License

/**
 * Copyright (C) 2011 Jack Wakefield
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

#endregion

using System.IO;
using NUnit.Framework;
using Revise.ZMS;

namespace Revise.Files.Tests {
    /// <summary>
    /// Provides testing for the <see cref="ModelFile"/> class.
    /// </summary>
    [TestFixture]
    public class ModelFileTests {
        private const string TestFile1 = "Tests/Files/CART01_ABILITY01.ZMS";
        private const string TestFile2 = "Tests/Files/HEADBAD01.ZMS";
        private const string TestFile3 = "Tests/Files/STONE014.ZMS";

        /// <summary>
        /// Tests the load method.
        /// </summary>
        private void TestLoadMethod(string filePath) {
            Stream stream = File.OpenRead(filePath);

            stream.Seek(0, SeekOrigin.End);
            long fileSize = stream.Position;
            stream.Seek(0, SeekOrigin.Begin);

            ModelFile modelFile = new ModelFile();
            modelFile.Load(stream);

            long streamPosition = stream.Position;
            stream.Close();

            Assert.That(fileSize.Equals(streamPosition), "Not all of the file was read");
        }

        /// <summary>
        /// Tests the save method.
        /// </summary>
        private void TestSaveMethod(string filePath) {
            ModelFile modelFile = new ModelFile();
            modelFile.Load(filePath);

            MemoryStream savedStream = new MemoryStream();
            modelFile.Save(savedStream);

            savedStream.Seek(0, SeekOrigin.Begin);

            ModelFile savedModelFile = new ModelFile();
            savedModelFile.Load(savedStream);

            savedStream.Close();

            Assert.That(modelFile.Pool.Equals(savedModelFile.Pool), "Pool values do not match");
            Assert.That(modelFile.BoneTable.Count.Equals(savedModelFile.BoneTable.Count), "Bone table counts do not match");

            for (int i = 0; i < modelFile.BoneTable.Count; i++) {
                Assert.That(modelFile.BoneTable[i].Equals(savedModelFile.BoneTable[i]), "Bone table values do not match");
            }

            Assert.That(modelFile.Vertices.Count.Equals(savedModelFile.Vertices.Count), "Vertex counts do not match");

            for (int i = 0; i < modelFile.Vertices.Count; i++) {
                Assert.That(modelFile.Vertices[i].Position.Equals(savedModelFile.Vertices[i].Position), "Vertex position values do not match");
                Assert.That(modelFile.Vertices[i].Normal.Equals(savedModelFile.Vertices[i].Normal), "Vertex normal values do not match");
                Assert.That(modelFile.Vertices[i].Colour.Equals(savedModelFile.Vertices[i].Colour), "Vertex colour values do not match");
                Assert.That(modelFile.Vertices[i].BoneWeights.Equals(savedModelFile.Vertices[i].BoneWeights), "Vertex bone weight values do not match");
                Assert.That(modelFile.Vertices[i].BoneIndices.Equals(savedModelFile.Vertices[i].BoneIndices), "Vertex bone index values do not match");
                Assert.That(modelFile.Vertices[i].TextureCoordinates[0].Equals(savedModelFile.Vertices[i].TextureCoordinates[0]), "Vertex texture coordinate values do not match");
                Assert.That(modelFile.Vertices[i].TextureCoordinates[1].Equals(savedModelFile.Vertices[i].TextureCoordinates[1]), "Vertex texture coordinate values do not match");
                Assert.That(modelFile.Vertices[i].TextureCoordinates[2].Equals(savedModelFile.Vertices[i].TextureCoordinates[2]), "Vertex texture coordinate values do not match");
                Assert.That(modelFile.Vertices[i].TextureCoordinates[3].Equals(savedModelFile.Vertices[i].TextureCoordinates[3]), "Vertex texture coordinate values do not match");
                Assert.That(modelFile.Vertices[i].Tangent.Equals(savedModelFile.Vertices[i].Tangent), "Vertex tangent values do not match");
            }

            Assert.That(modelFile.Indices.Count.Equals(savedModelFile.Indices.Count), "Index counts do not match");

            for (int i = 0; i < modelFile.Indices.Count; i++) {
                Assert.That(modelFile.Indices[i].Equals(savedModelFile.Indices[i]), "Index values do not match");
            }

            Assert.That(modelFile.Materials.Count.Equals(savedModelFile.Materials.Count), "Material counts do not match");

            for (int i = 0; i < modelFile.Materials.Count; i++) {
                Assert.That(modelFile.Materials[i].Equals(savedModelFile.Materials[i]), "Material values do not match");
            }

            Assert.That(modelFile.Strips.Count.Equals(savedModelFile.Strips.Count), "Strip counts do not match");

            for (int i = 0; i < modelFile.Strips.Count; i++) {
                Assert.That(modelFile.Strips[i].Equals(savedModelFile.Strips[i]), "Strip values do not match");
            }
        }

        [Test]
        public void TestLoadMethod1() {
            TestLoadMethod(TestFile1);
        }

        [Test]
        public void TestLoadMethod2() {
            TestLoadMethod(TestFile2);
        }

        [Test]
        public void TestLoadMethod3() {
            TestLoadMethod(TestFile3);
        }

        [Test]
        public void TestSaveMethod1() {
            TestSaveMethod(TestFile1);
        }

        [Test]
        public void TestSaveMethod2() {
            TestSaveMethod(TestFile2);
        }

        [Test]
        public void TestSaveMethod3() {
            TestSaveMethod(TestFile3);
        }
    }
}