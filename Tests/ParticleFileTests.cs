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
 * MERCHANTABILITY or FITNESS FOR AD:\Code\Revise\Revise.Files Tests\EFTTests.cs PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

#endregion

using NUnit.Framework;
using Revise.PTL;
using System.IO;

namespace Revise.Files.Tests
{
    /// <summary>
    /// Provides testing for the <see cref="ParticleFile"/> class.
    /// </summary>
    [TestFixture]
    public class ParticleFileTests {
        private const string TestFile = "Tests/Files/LEVELUP_01.PTL";

        /// <summary>
        /// Tests the load method.
        /// </summary>
        [Test]
        public void TestLoadMethod() {
            Stream stream = File.OpenRead(TestFile);

            stream.Seek(0, SeekOrigin.End);
            long fileSize = stream.Position;
            stream.Seek(0, SeekOrigin.Begin);

            ParticleFile particleFile = new ParticleFile();
            particleFile.Load(stream);

            long streamPosition = stream.Position;
            stream.Close();

            Assert.That(fileSize.Equals(streamPosition), "Not all of the file was read");
        }

        /// <summary>
        /// Tests the save method.
        /// </summary>
        [Test]
        public void TestSaveMethod() {
            ParticleFile particleFile = new ParticleFile();
            particleFile.Load(TestFile);

            MemoryStream savedStream = new MemoryStream();
            particleFile.Save(savedStream);

            savedStream.Seek(0, SeekOrigin.Begin);

            ParticleFile savedParticleFile = new ParticleFile();
            savedParticleFile.Load(savedStream);

            savedStream.Close();

            Assert.That(particleFile.Sequences.Count.Equals(savedParticleFile.Sequences.Count), "Sequence counts do not match");

            for (int i = 0; i < particleFile.Sequences.Count; i++) {
                Assert.That(particleFile.Sequences[i].Name.Equals(savedParticleFile.Sequences[i].Name), "Sequence name values do not match");
                Assert.That(particleFile.Sequences[i].Lifetime.Minimum.Equals(savedParticleFile.Sequences[i].Lifetime.Minimum), "Sequence minimum lifetime values do not match");
                Assert.That(particleFile.Sequences[i].Lifetime.Maximum.Equals(savedParticleFile.Sequences[i].Lifetime.Maximum), "Sequence maximum lifetime values do not match");
                Assert.That(particleFile.Sequences[i].EmitRate.Minimum.Equals(savedParticleFile.Sequences[i].EmitRate.Minimum), "Sequence minimum emit rate values do not match");
                Assert.That(particleFile.Sequences[i].EmitRate.Maximum.Equals(savedParticleFile.Sequences[i].EmitRate.Maximum), "Sequence maximum emit rate values do not match");
                Assert.That(particleFile.Sequences[i].LoopCount.Equals(savedParticleFile.Sequences[i].LoopCount), "Sequence loop count values do not match");
                Assert.That(particleFile.Sequences[i].SpawnDirection.Minimum.Equals(savedParticleFile.Sequences[i].SpawnDirection.Minimum), "Sequence minimum spawn direction values do not match");
                Assert.That(particleFile.Sequences[i].SpawnDirection.Maximum.Equals(savedParticleFile.Sequences[i].SpawnDirection.Maximum), "Sequence maximum spawn direction values do not match");
                Assert.That(particleFile.Sequences[i].EmitRadius.Minimum.Equals(savedParticleFile.Sequences[i].EmitRadius.Minimum), "Sequence minimum emit radius values do not match");
                Assert.That(particleFile.Sequences[i].EmitRadius.Maximum.Equals(savedParticleFile.Sequences[i].EmitRadius.Maximum), "Sequence maximum emit radius values do not match");
                Assert.That(particleFile.Sequences[i].Gravity.Minimum.Equals(savedParticleFile.Sequences[i].Gravity.Minimum), "Sequence minimum gravity values do not match");
                Assert.That(particleFile.Sequences[i].Gravity.Maximum.Equals(savedParticleFile.Sequences[i].Gravity.Maximum), "Sequence maximum gravity values do not match");
                Assert.That(particleFile.Sequences[i].TextureFileName.Equals(savedParticleFile.Sequences[i].TextureFileName), "Sequence texture file names do not match");
                Assert.That(particleFile.Sequences[i].ParticleCount.Equals(savedParticleFile.Sequences[i].ParticleCount), "Sequence particle counts do not match");
                Assert.That(particleFile.Sequences[i].Alignment.Equals(savedParticleFile.Sequences[i].Alignment), "Sequence alignment values do not match");
                Assert.That(particleFile.Sequences[i].UpdateCoordinate.Equals(savedParticleFile.Sequences[i].UpdateCoordinate), "Sequence update coordinate values do not match");
                Assert.That(particleFile.Sequences[i].TextureWidth.Equals(savedParticleFile.Sequences[i].TextureWidth), "Sequence texture width values do not match");
                Assert.That(particleFile.Sequences[i].TextureHeight.Equals(savedParticleFile.Sequences[i].TextureHeight), "Sequence texture height values do not match");
                Assert.That(particleFile.Sequences[i].Implementation.Equals(savedParticleFile.Sequences[i].Implementation), "Sequence implementation values do not match");
                Assert.That(particleFile.Sequences[i].DestinationBlendMode.Equals(savedParticleFile.Sequences[i].DestinationBlendMode), "Sequence destination blend mode values do not match");
                Assert.That(particleFile.Sequences[i].SourceBlendMode.Equals(savedParticleFile.Sequences[i].SourceBlendMode), "Sequence source blend mode values do not match");
                Assert.That(particleFile.Sequences[i].BlendOperation.Equals(savedParticleFile.Sequences[i].BlendOperation), "Sequence blend operation values do not match");

                Assert.That(particleFile.Sequences[i].Events.Count.Equals(savedParticleFile.Sequences[i].Events.Count), "Event counts do not match");

                for (int j = 0; j < particleFile.Sequences[i].Events.Count; j++) {
                    Assert.That(particleFile.Sequences[i].Events[j].Type.Equals(savedParticleFile.Sequences[i].Events[j].Type), "Event type values do not match");
                    Assert.That(particleFile.Sequences[i].Events[j].Fade.Equals(savedParticleFile.Sequences[i].Events[j].Fade), "Event fade values do not match");
                    Assert.That(particleFile.Sequences[i].Events[j].TimeRange.Minimum.Equals(savedParticleFile.Sequences[i].Events[j].TimeRange.Minimum), "Event minimum time range values do not match");
                    Assert.That(particleFile.Sequences[i].Events[j].TimeRange.Maximum.Equals(savedParticleFile.Sequences[i].Events[j].TimeRange.Maximum), "Event maximum time range values do not match");
                }
            }
        }
    }
}