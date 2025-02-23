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
using Revise.CHR;

namespace Revise.Files.Tests {
    /// <summary>
    /// Provides testing for the <see cref="CharacterFile"/> class.
    /// </summary>
    [TestFixture]
    public class CharacterFileTests {
        private const string TestFile = "Tests/Files/LIST_NPC.CHR";

        /// <summary>
        /// Tests the load method.
        /// </summary>
        [Test]
        public void TestLoadMethod() {
            Stream stream = File.OpenRead(TestFile);

            stream.Seek(0, SeekOrigin.End);
            long fileSize = stream.Position;
            stream.Seek(0, SeekOrigin.Begin);

            CharacterFile characterFile = new CharacterFile();
            characterFile.Load(stream);

            long streamPosition = stream.Position;
            stream.Close();

            Assert.That(fileSize.Equals(streamPosition), "Not all of the file was read");
        }

        /// <summary>
        /// Tests the save method.
        /// </summary>
        [Test]
        public void TestSaveMethod() {
            CharacterFile characterFile = new CharacterFile();
            characterFile.Load(TestFile);

            MemoryStream savedStream = new MemoryStream();
            characterFile.Save(savedStream);

            savedStream.Seek(0, SeekOrigin.Begin);

            CharacterFile savedCharacterFile = new CharacterFile();
            savedCharacterFile.Load(savedStream);

            savedStream.Close();

            Assert.That(characterFile.SkeletonFiles.Count.Equals(savedCharacterFile.SkeletonFiles.Count), "Skeleton file counts do not match");

            for (int i = 0; i < characterFile.SkeletonFiles.Count; i++) {
                Assert.That(characterFile.SkeletonFiles[i].Equals(savedCharacterFile.SkeletonFiles[i]), "Skeleton file names do not match");
            }

            Assert.That(characterFile.MotionFiles.Count.Equals(savedCharacterFile.MotionFiles.Count), "Motion file counts do not match");

            for (int i = 0; i < characterFile.MotionFiles.Count; i++) {
                Assert.That(characterFile.MotionFiles[i].Equals(savedCharacterFile.MotionFiles[i]), "Motion file names do not match");
            }

            Assert.That(characterFile.EffectFiles.Count.Equals(savedCharacterFile.EffectFiles.Count), "Effect file counts do not match");

            for (int i = 0; i < characterFile.EffectFiles.Count; i++) {
                Assert.That(characterFile.EffectFiles[i].Equals(savedCharacterFile.EffectFiles[i]), "Effect file names do not match");
            }

            Assert.That(characterFile.Characters.Count.Equals(savedCharacterFile.Characters.Count), "Character counts do not match");

            for (int i = 0; i < characterFile.Characters.Count; i++) {
                Assert.That(characterFile.Characters[i].IsEnabled.Equals(savedCharacterFile.Characters[i].IsEnabled), "Character enabled values do not match");

                if (characterFile.Characters[i].IsEnabled) {
                    Assert.That(characterFile.Characters[i].ID.Equals(savedCharacterFile.Characters[i].ID), "Character ID values do not match");
                    Assert.That(characterFile.Characters[i].Name.Equals(savedCharacterFile.Characters[i].Name), "Character name values do not match");

                    Assert.That(characterFile.Characters[i].Objects.Count.Equals(savedCharacterFile.Characters[i].Objects.Count), "Character object counts do not match");

                    for (int j = 0; j < characterFile.Characters[i].Objects.Count; j++) {
                        Assert.That(characterFile.Characters[i].Objects[j].Object.Equals(savedCharacterFile.Characters[i].Objects[j].Object), "Character object values do not match");
                    }

                    Assert.That(characterFile.Characters[i].Animations.Count.Equals(savedCharacterFile.Characters[i].Animations.Count), "Character animation counts do not match");

                    for (int j = 0; j < characterFile.Characters[i].Animations.Count; j++) {
                        Assert.That(characterFile.Characters[i].Animations[j].Type.Equals(savedCharacterFile.Characters[i].Animations[j].Type), "Character animation type values do not match");
                        Assert.That(characterFile.Characters[i].Animations[j].Animation.Equals(savedCharacterFile.Characters[i].Animations[j].Animation), "Character animation values do not match");
                    }

                    Assert.That(characterFile.Characters[i].Effects.Count.Equals(savedCharacterFile.Characters[i].Effects.Count), "Character effect counts do not match");

                    for (int j = 0; j < characterFile.Characters[i].Effects.Count; j++) {
                        Assert.That(characterFile.Characters[i].Effects[j].Bone.Equals(savedCharacterFile.Characters[i].Effects[j].Bone), "Character effect bone values do not match");
                        Assert.That(characterFile.Characters[i].Effects[j].Effect.Equals(savedCharacterFile.Characters[i].Effects[j].Effect), "Character effect values do not match");
                    }
                }
            }
        }
    }
}