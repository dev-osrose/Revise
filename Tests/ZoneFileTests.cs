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
using Revise.ZON;

namespace Revise.Files.Tests {
    /// <summary>
    /// Provides testing for the <see cref="ZoneFile"/> class.
    /// </summary>
    [TestFixture]
    public class ZoneFileTests {
        private const string TestFile = "Tests/Files/JPT01.ZON";

        /// <summary>
        /// Tests the load method.
        /// </summary>
        [Test]
        public void TestLoadMethod() {
            Stream stream = File.OpenRead(TestFile);

            stream.Seek(0, SeekOrigin.End);
            long fileSize = stream.Position;
            stream.Seek(0, SeekOrigin.Begin);

            ZoneFile zoneFile = new ZoneFile();
            zoneFile.Load(stream);

            long streamPosition = stream.Position;
            stream.Close();

            Assert.That(fileSize.Equals(streamPosition), "Not all of the file was read");
        }

        /// <summary>
        /// Tests the save method.
        /// </summary>
        [Test]
        public void TestSaveMethod() {
            ZoneFile zoneFile = new ZoneFile();
            zoneFile.Load(TestFile);

            MemoryStream savedStream = new MemoryStream();
            zoneFile.Save(savedStream);

            savedStream.Seek(0, SeekOrigin.Begin);

            ZoneFile savedZoneFile = new ZoneFile();
            savedZoneFile.Load(savedStream);

            savedStream.Close();

            Assert.That(zoneFile.Type.Equals(savedZoneFile.Type), "Type values do not match");
            Assert.That(zoneFile.Width.Equals(savedZoneFile.Width), "Width values do not match");
            Assert.That(zoneFile.Height.Equals(savedZoneFile.Height), "Height values do not match");
            Assert.That(zoneFile.GridCount.Equals(savedZoneFile.GridCount), "Grid count values do not match");
            Assert.That(zoneFile.GridSize.Equals(savedZoneFile.GridSize), "Grid size values do not match");
            Assert.That(zoneFile.StartPosition.Equals(savedZoneFile.StartPosition), "Start position values do not match");

            for (int x = 0; x < zoneFile.Width; x++) {
                for (int y = 0; y < zoneFile.Height; y++) {
                    Assert.That(zoneFile.Positions[x, y].IsUsed.Equals(savedZoneFile.Positions[x, y].IsUsed), "Is used values do not match");
                    Assert.That(zoneFile.Positions[x, y].Position.Equals(savedZoneFile.Positions[x, y].Position), "Positions do not match");
                }
            }

            Assert.That(zoneFile.SpawnPoints.Count.Equals(savedZoneFile.SpawnPoints.Count), "Spawn counts do not match");

            for (int i = 0; i < zoneFile.SpawnPoints.Count; i++) {
                Assert.That(zoneFile.SpawnPoints[i].Position.Equals(savedZoneFile.SpawnPoints[i].Position), "Spawn point positions do not match");
                Assert.That(zoneFile.SpawnPoints[i].Name.Equals(savedZoneFile.SpawnPoints[i].Name), "Spawn point names do not match");
            }

            Assert.That(zoneFile.Textures.Count.Equals(savedZoneFile.Textures.Count), "Texture counts do not match");

            for (int i = 0; i < zoneFile.Textures.Count; i++) {
                Assert.That(zoneFile.Textures[i].Equals(savedZoneFile.Textures[i]), "Texture file paths do not match");
            }

            Assert.That(zoneFile.Tiles.Count.Equals(savedZoneFile.Tiles.Count), "Tile counts do not match");

            for (int i = 0; i < zoneFile.Tiles.Count; i++) {
                Assert.That(zoneFile.Tiles[i].Layer1.Equals(savedZoneFile.Tiles[i].Layer1), "Tile layer 1 values do not match");
                Assert.That(zoneFile.Tiles[i].Layer2.Equals(savedZoneFile.Tiles[i].Layer2), "Tile layer 2 values do not match");
                Assert.That(zoneFile.Tiles[i].Offset1.Equals(savedZoneFile.Tiles[i].Offset1), "Tile offset 1 values do not match");
                Assert.That(zoneFile.Tiles[i].Offset2.Equals(savedZoneFile.Tiles[i].Offset2), "Tile offset 2 values do not match");
                Assert.That(zoneFile.Tiles[i].BlendingEnabled.Equals(savedZoneFile.Tiles[i].BlendingEnabled), "Tile blending enabled values do not match");
                Assert.That(zoneFile.Tiles[i].Rotation.Equals(savedZoneFile.Tiles[i].Rotation), "Tile rotation values do not match");
                Assert.That(zoneFile.Tiles[i].TileType.Equals(savedZoneFile.Tiles[i].TileType), "Tile type values do not match");
            }

            Assert.That(zoneFile.Name.Equals(savedZoneFile.Name), "Name values do not match");
            Assert.That(zoneFile.IsUnderground.Equals(savedZoneFile.IsUnderground), "Is underground values do not match");
            Assert.That(zoneFile.BackgroundMusicFilePath.Equals(savedZoneFile.BackgroundMusicFilePath), "Background music file paths do not match");
            Assert.That(zoneFile.SkyFilePath.Equals(savedZoneFile.SkyFilePath), "Sky file paths do not match");
            Assert.That(zoneFile.EconomyCheckRate.Equals(savedZoneFile.EconomyCheckRate), "Economy check rate values do not match");
            Assert.That(zoneFile.PopulationBase.Equals(savedZoneFile.PopulationBase), "Population base values do not match");
            Assert.That(zoneFile.PopulationGrowthRate.Equals(savedZoneFile.PopulationGrowthRate), "Population growth rate values do not match");
            Assert.That(zoneFile.MetalConsumption.Equals(savedZoneFile.MetalConsumption), "Metal consumption values do not match");
            Assert.That(zoneFile.StoneConsumption.Equals(savedZoneFile.StoneConsumption), "Stone consumption values do not match");
            Assert.That(zoneFile.WoodConsumption.Equals(savedZoneFile.WoodConsumption), "Wood consumption values do not match");
            Assert.That(zoneFile.LeatherConsumption.Equals(savedZoneFile.LeatherConsumption), "Leather consumption values do not match");
            Assert.That(zoneFile.ClothConsumption.Equals(savedZoneFile.ClothConsumption), "Cloth consumption values do not match");
            Assert.That(zoneFile.AlchemyConsumption.Equals(savedZoneFile.AlchemyConsumption), "Alchemy consumption values do not match");
            Assert.That(zoneFile.ChemicalConsumption.Equals(savedZoneFile.ChemicalConsumption), "Chemical consumption values do not match");
            Assert.That(zoneFile.IndustrialConsumption.Equals(savedZoneFile.IndustrialConsumption), "Industrial consumption values do not match");
            Assert.That(zoneFile.MedicineConsumption.Equals(savedZoneFile.MedicineConsumption), "Medicine consumption values do not match");
            Assert.That(zoneFile.FoodConsumption.Equals(savedZoneFile.FoodConsumption), "Food consumption values do not match");
        }
    }
}