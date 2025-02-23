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
using Revise.AIP;

namespace Revise.Files.Tests {
    /// <summary>
    /// Provides testing for the <see cref="ArtificialIntelligenceFile"/> class.
    /// </summary>
    [TestFixture]
    public class ArtificialIntelligenceFileTests {
        private const string TestFile = "Tests/Files/CLAN_BOSS1.AIP";

        /// <summary>
        /// Tests the load method.
        /// </summary>
        [Test]
        public void TestLoadMethod() {
            Stream stream = File.OpenRead(TestFile);

            stream.Seek(0, SeekOrigin.End);
            long fileSize = stream.Position;
            stream.Seek(0, SeekOrigin.Begin);

            ArtificialIntelligenceFile artificialIntelligenceFile = new ArtificialIntelligenceFile();
            artificialIntelligenceFile.Load(stream);

            long streamPosition = stream.Position;
            stream.Close();

            Assert.That(fileSize.Equals(streamPosition), "Not all of the file was read");
        }

        /// <summary>
        /// Tests the save method.
        /// </summary>
        [Test]
        public void TestSaveMethod() {
            ArtificialIntelligenceFile artificialIntelligenceFile = new ArtificialIntelligenceFile();
            artificialIntelligenceFile.Load(TestFile);

            MemoryStream savedStream = new MemoryStream();
            artificialIntelligenceFile.Save(savedStream);

            savedStream.Seek(0, SeekOrigin.Begin);

            ArtificialIntelligenceFile savedArtificialIntelligenceFile = new ArtificialIntelligenceFile();
            savedArtificialIntelligenceFile.Load(savedStream);

            savedStream.Close();

            Assert.That(artificialIntelligenceFile.Name.Equals(savedArtificialIntelligenceFile.Name), "Names do not match");
            Assert.That(artificialIntelligenceFile.IdleInterval.Equals(savedArtificialIntelligenceFile.IdleInterval), "Idle interval values do not match");
            Assert.That(artificialIntelligenceFile.DamageRate.Equals(savedArtificialIntelligenceFile.DamageRate), "Damage rate values do not match");

            Assert.That(artificialIntelligenceFile.Patterns.Count.Equals(savedArtificialIntelligenceFile.Patterns.Count), "Pattern counts do not match");

            for (int i = 0; i < artificialIntelligenceFile.Patterns.Count; i++) {
                Assert.That(artificialIntelligenceFile.Patterns[i].Name.Equals(savedArtificialIntelligenceFile.Patterns[i].Name), "Pattern names do not match");

                Assert.That(artificialIntelligenceFile.Patterns[i].Events.Count.Equals(savedArtificialIntelligenceFile.Patterns[i].Events.Count), "Event counts do not match");

                for (int j = 0; j < artificialIntelligenceFile.Patterns[i].Events.Count; j++) {
                    Assert.That(artificialIntelligenceFile.Patterns[i].Events[j].Name.Equals(savedArtificialIntelligenceFile.Patterns[i].Events[j].Name), "Event names do not match");

                    Assert.That(artificialIntelligenceFile.Patterns[i].Events[j].Conditions.Count.Equals(savedArtificialIntelligenceFile.Patterns[i].Events[j].Conditions.Count), "Condition counts do not match");
                    Assert.That(artificialIntelligenceFile.Patterns[i].Events[j].Actions.Count.Equals(savedArtificialIntelligenceFile.Patterns[i].Events[j].Actions.Count), "Action counts do not match");

                    for (int k = 0; k < artificialIntelligenceFile.Patterns[i].Events[j].Conditions.Count; k++) {
                        Assert.That(artificialIntelligenceFile.Patterns[i].Events[j].Conditions[k].GetType().Equals(savedArtificialIntelligenceFile.Patterns[i].Events[j].Conditions[k].GetType()), "Condition types do not match");
                    }

                    for (int k = 0; k < artificialIntelligenceFile.Patterns[i].Events[j].Actions.Count; k++) {
                        Assert.That(artificialIntelligenceFile.Patterns[i].Events[j].Actions[k].GetType().Equals(savedArtificialIntelligenceFile.Patterns[i].Events[j].Actions[k].GetType()), "Action types do not match");
                    }
                }
            }
        }
    }
}