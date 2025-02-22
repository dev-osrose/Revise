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
using Revise.HIM;

namespace Revise.Files.Tests {
    /// <summary>
    /// Provides testing for the <see cref="HeightmapFile"/> class.
    /// </summary>
    [TestFixture]
    public class HeightmapFileTests {
        private const string TestFile = "Tests/Files/31_30.HIM";

        /// <summary>
        /// Tests the load method.
        /// </summary>
        [Test]
        public void TestLoadMethod() {
            const int HEIGHT = 65;
            const int WIDTH = 65;

            Stream stream = File.OpenRead(TestFile);

            stream.Seek(0, SeekOrigin.End);
            long fileSize = stream.Position;
            stream.Seek(0, SeekOrigin.Begin);

            HeightmapFile heightmapFile = new HeightmapFile();
            heightmapFile.Load(stream);

            long streamPosition = stream.Position;
            stream.Close();

            Assert.That(fileSize.Equals(streamPosition), "Not all of the file was read");
            Assert.That(WIDTH.Equals(heightmapFile.Width), "Incorrect width");
            Assert.That(HEIGHT.Equals(heightmapFile.Height), "Incorrect height");
        }

        /// <summary>
        /// Tests the save method.
        /// </summary>
        [Test]
        public void TestSaveMethod() {
            HeightmapFile heightmapFile = new HeightmapFile();
            heightmapFile.Load(TestFile);

            MemoryStream savedStream = new MemoryStream();
            heightmapFile.Save(savedStream);
            heightmapFile.Load(TestFile);

            savedStream.Seek(0, SeekOrigin.Begin);

            HeightmapFile savedHeightmapFile = new HeightmapFile();
            savedHeightmapFile.Load(savedStream);

            savedStream.Close();

            for (int x = 0; x < heightmapFile.Height; x++) {
                for (int y = 0; y < heightmapFile.Width; y++) {
                    Assert.That(heightmapFile[x, y].Equals(savedHeightmapFile[x, y]), "Height values do not match");
                }
            }

            for (int x = 0; x < heightmapFile.Patches.GetLength(0); x++) {
                for (int y = 0; y < heightmapFile.Patches.GetLength(1); y++) {
                    Assert.That(heightmapFile.Patches[x, y].Minimum.Equals(savedHeightmapFile.Patches[x, y].Minimum), "Minimum patch values do not match");
                    Assert.That(heightmapFile.Patches[x, y].Maximum.Equals(savedHeightmapFile.Patches[x, y].Maximum), "Maximum patch values do not match");
                }
            }

            for (int i = 0; i < heightmapFile.QuadPatches.Length; i++) {
                Assert.That(heightmapFile.QuadPatches[i].Minimum.Equals(savedHeightmapFile.QuadPatches[i].Minimum), "Minimum quad patch values do not match");
                Assert.That(heightmapFile.QuadPatches[i].Maximum.Equals(savedHeightmapFile.QuadPatches[i].Maximum), "Maximum quad patch values do not match");
            }
        }
    }
}