﻿#region License

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
using Revise.LOD;

namespace Revise.Files.Tests {
    /// <summary>
    /// Provides testing for the <see cref="LevelOfDetailFile"/> class.
    /// </summary>
    [TestFixture]
    public class LevelOfDetailFileTests {
        private const string TestFile = "Tests/Files/PATCHTYPE.LOD";

        /// <summary>
        /// Tests the load method.
        /// </summary>
        [Test]
        public void TestLoadMethod() {
            const string NAME = "block_height";

            Stream stream = File.OpenRead(TestFile);

            stream.Seek(0, SeekOrigin.End);
            long fileSize = stream.Position;
            stream.Seek(0, SeekOrigin.Begin);

            LevelOfDetailFile levelOfDetailFile = new LevelOfDetailFile();
            levelOfDetailFile.Load(stream);

            long streamPosition = stream.Position;
            stream.Close();

            Assert.That(fileSize.Equals(streamPosition), "Not all of the file was read");
            Assert.That(NAME.Equals(levelOfDetailFile.Name), "Incorrect name");
        }

        /// <summary>
        /// Tests the save method.
        /// </summary>
        [Test]
        public void TestSaveMethod() {
            LevelOfDetailFile levelOfDetailFile = new LevelOfDetailFile();
            levelOfDetailFile.Load(TestFile);

            MemoryStream savedStream = new MemoryStream();
            levelOfDetailFile.Save(savedStream);

            savedStream.Seek(0, SeekOrigin.Begin);

            LevelOfDetailFile savedLevelOfDetailFile = new LevelOfDetailFile();
            savedLevelOfDetailFile.Load(savedStream);

            savedStream.Close();

            Assert.That(levelOfDetailFile.Name.Equals(savedLevelOfDetailFile.Name), "Names do not match");

            for (int x = 0; x < levelOfDetailFile.Height; x++) {
                for (int y = 0; y < levelOfDetailFile.Width; y++) {
                    Assert.That(levelOfDetailFile[x, y].Equals(savedLevelOfDetailFile[x, y]), "Values do not match");
                }
            }
        }
    }
}