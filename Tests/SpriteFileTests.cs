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
using Revise.TSI;

namespace Revise.Files.Tests {
    /// <summary>
    /// Provides testing for the <see cref="SpriteFile"/> class.
    /// </summary>
    [TestFixture]
    public class SpriteFileTests {
        private const string TestFile = "Tests/Files/UI.TSI";

        /// <summary>
        /// Tests the load method.
        /// </summary>
        [Test]
        public void TestLoadMethod() {
            const int TEXTURE_COUNT = 38;
            const int SPRITE_COUNT = 648;

            Stream stream = File.OpenRead(TestFile);

            stream.Seek(0, SeekOrigin.End);
            long fileSize = stream.Position;
            stream.Seek(0, SeekOrigin.Begin);

            SpriteFile spriteFile = new SpriteFile();
            spriteFile.Load(stream);

            long streamPosition = stream.Position;
            stream.Close();

            Assert.That(fileSize.Equals(streamPosition), "Not all of the file was read");
            Assert.That(TEXTURE_COUNT.Equals(spriteFile.Textures.Count), "Incorrect texture count");
            Assert.That(SPRITE_COUNT.Equals(spriteFile.Sprites.Count), "Incorrect sprite count");
        }

        /// <summary>
        /// Tests the save method.
        /// </summary>
        [Test]
        public void TestSaveMethod() {
            SpriteFile spriteFile = new SpriteFile();
            spriteFile.Load(TestFile);

            MemoryStream savedStream = new MemoryStream();
            spriteFile.Save(savedStream);

            savedStream.Seek(0, SeekOrigin.Begin);

            SpriteFile savedSpriteFile = new SpriteFile();
            savedSpriteFile.Load(savedStream);

            savedStream.Close();

            Assert.That(spriteFile.Textures.Count.Equals(savedSpriteFile.Textures.Count), "Texture counts do not match");
            Assert.That(spriteFile.Sprites.Count.Equals(savedSpriteFile.Sprites.Count), "Sprite counts do not match");

            for (int i = 0; i < spriteFile.Textures.Count; i++) {
                Assert.That(spriteFile.Textures[i].FileName.Equals(savedSpriteFile.Textures[i].FileName), "Texture file names values do not match");
                Assert.That(spriteFile.Textures[i].ColourKey.Equals(savedSpriteFile.Textures[i].ColourKey), "Texture colour key values do not match");
            }

            for (int i = 0; i < spriteFile.Sprites.Count; i++) {
                Assert.That(spriteFile.Sprites[i].Texture.Equals(savedSpriteFile.Sprites[i].Texture), "Sprite texture values do not match");
                Assert.That(spriteFile.Sprites[i].X1.Equals(savedSpriteFile.Sprites[i].X1), "Sprite X1 values do not match");
                Assert.That(spriteFile.Sprites[i].Y1.Equals(savedSpriteFile.Sprites[i].Y1), "Sprite Y1 values do not match");
                Assert.That(spriteFile.Sprites[i].X2.Equals(savedSpriteFile.Sprites[i].X2), "Sprite X2 values do not match");
                Assert.That(spriteFile.Sprites[i].Y2.Equals(savedSpriteFile.Sprites[i].Y2), "Sprite Y2 values do not match");
                Assert.That(spriteFile.Sprites[i].ID.Equals(savedSpriteFile.Sprites[i].ID), "Sprite ID values do not match");
            }
        }
    }
}