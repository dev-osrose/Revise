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
using Revise.IFO;

namespace Revise.Files.Tests {
    /// <summary>
    /// Provides testing for the <see cref="MapDataFile"/> class.
    /// </summary>
    [TestFixture]
    public class MapDataFileTests {
        private const string TestFile = "Tests/Files/33_32.IFO";

        /// <summary>
        /// Tests the load method.
        /// </summary>
        [Test]
        public void TestLoadMethod() {
            MapDataFile mapDataFile = new MapDataFile();
            mapDataFile.Load(TestFile);
        }

        /// <summary>
        /// Tests the save method.
        /// </summary>
        [Test]
        public void TestSaveMethod() {
            MapDataFile mapDataFile = new MapDataFile();
            mapDataFile.Load(TestFile);

            MemoryStream savedStream = new MemoryStream();
            mapDataFile.Save(savedStream);

            savedStream.Seek(0, SeekOrigin.Begin);

            MapDataFile savedMapDataFile = new MapDataFile();
            savedMapDataFile.Load(savedStream);

            savedStream.Close();

            Assert.That(mapDataFile.MapPosition.Equals(savedMapDataFile.MapPosition), "Map position values do not match");
            Assert.That(mapDataFile.ZonePosition.Equals(savedMapDataFile.ZonePosition), "Zone position values do not match");
            Assert.That(mapDataFile.World.Equals(savedMapDataFile.World), "World matrices do not match");
            Assert.That(mapDataFile.Name.Equals(savedMapDataFile.Name), "Names do not match");

            Assert.That(mapDataFile.Objects.Count.Equals(savedMapDataFile.Objects.Count), "Object counts do not match");

            for (int i = 0; i < mapDataFile.Objects.Count; i++) {
                Assert.That(mapDataFile.Objects[i].Name.Equals(savedMapDataFile.Objects[i].Name), "Object names do not match");
                Assert.That(mapDataFile.Objects[i].WarpID.Equals(savedMapDataFile.Objects[i].WarpID), "Object warp ID values do not match");
                Assert.That(mapDataFile.Objects[i].EventID.Equals(savedMapDataFile.Objects[i].EventID), "Object event ID values do not match");
                Assert.That(mapDataFile.Objects[i].ObjectType.Equals(savedMapDataFile.Objects[i].ObjectType), "Object object type values do not match");
                Assert.That(mapDataFile.Objects[i].ObjectID.Equals(savedMapDataFile.Objects[i].ObjectID), "Object object ID values do not match");
                Assert.That(mapDataFile.Objects[i].MapPosition.Equals(savedMapDataFile.Objects[i].MapPosition), "Object map positions do not match");
                Assert.That(mapDataFile.Objects[i].Rotation.Equals(savedMapDataFile.Objects[i].Rotation), "Object rotations do not match");
                Assert.That(mapDataFile.Objects[i].Position.Equals(savedMapDataFile.Objects[i].Position), "Object positions do not match");
                Assert.That(mapDataFile.Objects[i].Scale.Equals(savedMapDataFile.Objects[i].Scale), "Object scales do not match");
            }

            Assert.That(mapDataFile.NPCs.Count.Equals(savedMapDataFile.NPCs.Count), "NPC counts do not match");

            for (int i = 0; i < mapDataFile.NPCs.Count; i++) {
                Assert.That(mapDataFile.NPCs[i].Name.Equals(savedMapDataFile.NPCs[i].Name), "NPC names do not match");
                Assert.That(mapDataFile.NPCs[i].WarpID.Equals(savedMapDataFile.NPCs[i].WarpID), "NPC warp ID values do not match");
                Assert.That(mapDataFile.NPCs[i].EventID.Equals(savedMapDataFile.NPCs[i].EventID), "NPC event ID values do not match");
                Assert.That(mapDataFile.NPCs[i].ObjectType.Equals(savedMapDataFile.NPCs[i].ObjectType), "NPC object type values do not match");
                Assert.That(mapDataFile.NPCs[i].ObjectID.Equals(savedMapDataFile.NPCs[i].ObjectID), "NPC object ID values do not match");
                Assert.That(mapDataFile.NPCs[i].MapPosition.Equals(savedMapDataFile.NPCs[i].MapPosition), "NPC map positions do not match");
                Assert.That(mapDataFile.NPCs[i].Rotation.Equals(savedMapDataFile.NPCs[i].Rotation), "NPC rotations do not match");
                Assert.That(mapDataFile.NPCs[i].Position.Equals(savedMapDataFile.NPCs[i].Position), "NPC positions do not match");
                Assert.That(mapDataFile.NPCs[i].Scale.Equals(savedMapDataFile.NPCs[i].Scale), "NPC scales do not match");
                Assert.That(mapDataFile.NPCs[i].AI.Equals(savedMapDataFile.NPCs[i].AI), "NPC AI values do not match");
                Assert.That(mapDataFile.NPCs[i].ConversationFile.Equals(savedMapDataFile.NPCs[i].ConversationFile), "NPC conversation file values do not match");
            }

            Assert.That(mapDataFile.Buildings.Count.Equals(savedMapDataFile.Buildings.Count), "Building counts do not match");

            for (int i = 0; i < mapDataFile.Buildings.Count; i++) {
                Assert.That(mapDataFile.Buildings[i].Name.Equals(savedMapDataFile.Buildings[i].Name), "Building names do not match");
                Assert.That(mapDataFile.Buildings[i].WarpID.Equals(savedMapDataFile.Buildings[i].WarpID), "Building warp ID values do not match");
                Assert.That(mapDataFile.Buildings[i].EventID.Equals(savedMapDataFile.Buildings[i].EventID), "Building event ID values do not match");
                Assert.That(mapDataFile.Buildings[i].ObjectType.Equals(savedMapDataFile.Buildings[i].ObjectType), "Building object type values do not match");
                Assert.That(mapDataFile.Buildings[i].ObjectID.Equals(savedMapDataFile.Buildings[i].ObjectID), "Building object ID values do not match");
                Assert.That(mapDataFile.Buildings[i].MapPosition.Equals(savedMapDataFile.Buildings[i].MapPosition), "Building map positions do not match");
                Assert.That(mapDataFile.Buildings[i].Rotation.Equals(savedMapDataFile.Buildings[i].Rotation), "Building rotations do not match");
                Assert.That(mapDataFile.Buildings[i].Position.Equals(savedMapDataFile.Buildings[i].Position), "Building positions do not match");
                Assert.That(mapDataFile.Buildings[i].Scale.Equals(savedMapDataFile.Buildings[i].Scale), "Building scales do not match");
            }

            Assert.That(mapDataFile.Sounds.Count.Equals(savedMapDataFile.Sounds.Count), "Sound counts do not match");

            for (int i = 0; i < mapDataFile.Sounds.Count; i++) {
                Assert.That(mapDataFile.Sounds[i].Name.Equals(savedMapDataFile.Sounds[i].Name), "Sound names do not match");
                Assert.That(mapDataFile.Sounds[i].WarpID.Equals(savedMapDataFile.Sounds[i].WarpID), "Sound warp ID values do not match");
                Assert.That(mapDataFile.Sounds[i].EventID.Equals(savedMapDataFile.Sounds[i].EventID), "Sound event ID values do not match");
                Assert.That(mapDataFile.Sounds[i].ObjectType.Equals(savedMapDataFile.Sounds[i].ObjectType), "Sound object type values do not match");
                Assert.That(mapDataFile.Sounds[i].ObjectID.Equals(savedMapDataFile.Sounds[i].ObjectID), "Sound object ID values do not match");
                Assert.That(mapDataFile.Sounds[i].MapPosition.Equals(savedMapDataFile.Sounds[i].MapPosition), "Sound map positions do not match");
                Assert.That(mapDataFile.Sounds[i].Rotation.Equals(savedMapDataFile.Sounds[i].Rotation), "Sound rotations do not match");
                Assert.That(mapDataFile.Sounds[i].Position.Equals(savedMapDataFile.Sounds[i].Position), "Sound positions do not match");
                Assert.That(mapDataFile.Sounds[i].Scale.Equals(savedMapDataFile.Sounds[i].Scale), "Sound scales do not match");
                Assert.That(mapDataFile.Sounds[i].FilePath.Equals(savedMapDataFile.Sounds[i].FilePath), "Sound file paths do not match");
                Assert.That(mapDataFile.Sounds[i].Range.Equals(savedMapDataFile.Sounds[i].Range), "Sound range values do not match");
                Assert.That(mapDataFile.Sounds[i].Interval.Equals(savedMapDataFile.Sounds[i].Interval), "Sound interval values do not match");
            }

            Assert.That(mapDataFile.Effects.Count.Equals(savedMapDataFile.Effects.Count), "Effect counts do not match");

            for (int i = 0; i < mapDataFile.Effects.Count; i++) {
                Assert.That(mapDataFile.Effects[i].Name.Equals(savedMapDataFile.Effects[i].Name), "Effect names do not match");
                Assert.That(mapDataFile.Effects[i].WarpID.Equals(savedMapDataFile.Effects[i].WarpID), "Effect warp ID values do not match");
                Assert.That(mapDataFile.Effects[i].EventID.Equals(savedMapDataFile.Effects[i].EventID), "Effect event ID values do not match");
                Assert.That(mapDataFile.Effects[i].ObjectType.Equals(savedMapDataFile.Effects[i].ObjectType), "Effect object type values do not match");
                Assert.That(mapDataFile.Effects[i].ObjectID.Equals(savedMapDataFile.Effects[i].ObjectID), "Effect object ID values do not match");
                Assert.That(mapDataFile.Effects[i].MapPosition.Equals(savedMapDataFile.Effects[i].MapPosition), "Effect map positions do not match");
                Assert.That(mapDataFile.Effects[i].Rotation.Equals(savedMapDataFile.Effects[i].Rotation), "Effect rotations do not match");
                Assert.That(mapDataFile.Effects[i].Position.Equals(savedMapDataFile.Effects[i].Position), "Effect positions do not match");
                Assert.That(mapDataFile.Effects[i].Scale.Equals(savedMapDataFile.Effects[i].Scale), "Effect scales do not match");
                Assert.That(mapDataFile.Effects[i].FilePath.Equals(savedMapDataFile.Effects[i].FilePath), "Effect file paths do not match");
            }

            Assert.That(mapDataFile.Animations.Count.Equals(savedMapDataFile.Animations.Count), "Animation counts do not match");

            for (int i = 0; i < mapDataFile.Animations.Count; i++) {
                Assert.That(mapDataFile.Animations[i].Name.Equals(savedMapDataFile.Animations[i].Name), "Animation names do not match");
                Assert.That(mapDataFile.Animations[i].WarpID.Equals(savedMapDataFile.Animations[i].WarpID), "Animation warp ID values do not match");
                Assert.That(mapDataFile.Animations[i].EventID.Equals(savedMapDataFile.Animations[i].EventID), "Animation event ID values do not match");
                Assert.That(mapDataFile.Animations[i].ObjectType.Equals(savedMapDataFile.Animations[i].ObjectType), "Animation object type values do not match");
                Assert.That(mapDataFile.Animations[i].ObjectID.Equals(savedMapDataFile.Animations[i].ObjectID), "Animation object ID values do not match");
                Assert.That(mapDataFile.Animations[i].MapPosition.Equals(savedMapDataFile.Animations[i].MapPosition), "Animation map positions do not match");
                Assert.That(mapDataFile.Animations[i].Rotation.Equals(savedMapDataFile.Animations[i].Rotation), "Animation rotations do not match");
                Assert.That(mapDataFile.Animations[i].Position.Equals(savedMapDataFile.Animations[i].Position), "Animation positions do not match");
                Assert.That(mapDataFile.Animations[i].Scale.Equals(savedMapDataFile.Animations[i].Scale), "Animation scales do not match");
            }

            Assert.That(mapDataFile.WaterPatches.Name.Equals(savedMapDataFile.WaterPatches.Name), "Water patch names do not match");
            Assert.That(mapDataFile.WaterPatches.WarpID.Equals(savedMapDataFile.WaterPatches.WarpID), "Water patch warp ID values do not match");
            Assert.That(mapDataFile.WaterPatches.EventID.Equals(savedMapDataFile.WaterPatches.EventID), "Water patch event ID values do not match");
            Assert.That(mapDataFile.WaterPatches.ObjectType.Equals(savedMapDataFile.WaterPatches.ObjectType), "Water patch object type values do not match");
            Assert.That(mapDataFile.WaterPatches.ObjectID.Equals(savedMapDataFile.WaterPatches.ObjectID), "Water patch object ID values do not match");
            Assert.That(mapDataFile.WaterPatches.MapPosition.Equals(savedMapDataFile.WaterPatches.MapPosition), "Water patch map positions do not match");
            Assert.That(mapDataFile.WaterPatches.Rotation.Equals(savedMapDataFile.WaterPatches.Rotation), "Water patch rotations do not match");
            Assert.That(mapDataFile.WaterPatches.Position.Equals(savedMapDataFile.WaterPatches.Position), "Water patch positions do not match");
            Assert.That(mapDataFile.WaterPatches.Scale.Equals(savedMapDataFile.WaterPatches.Scale), "Water patch scales do not match");
            Assert.That(mapDataFile.WaterPatches.Width.Equals(savedMapDataFile.WaterPatches.Width), "Water patch width values do not match");
            Assert.That(mapDataFile.WaterPatches.Height.Equals(savedMapDataFile.WaterPatches.Height), "Water patch height values do not match");

            for (int h = 0; h < mapDataFile.WaterPatches.Height; h++) {
                for (int w = 0; w < mapDataFile.WaterPatches.Width; w++) {
                    Assert.That(mapDataFile.WaterPatches.Patches[h, w].HasWater.Equals(mapDataFile.WaterPatches.Patches[h, w].HasWater), "Water patch has water values do not match");
                    Assert.That(mapDataFile.WaterPatches.Patches[h, w].Height.Equals(mapDataFile.WaterPatches.Patches[h, w].Height), "Water patch has water values do not match");
                    Assert.That(mapDataFile.WaterPatches.Patches[h, w].Type.Equals(mapDataFile.WaterPatches.Patches[h, w].Type), "Water patch has water values do not match");
                    Assert.That(mapDataFile.WaterPatches.Patches[h, w].ID.Equals(mapDataFile.WaterPatches.Patches[h, w].ID), "Water patch has water values do not match");
                    Assert.That(mapDataFile.WaterPatches.Patches[h, w].Reserved.Equals(mapDataFile.WaterPatches.Patches[h, w].Reserved), "Water patch has water values do not match");
                }
            }

            Assert.That(mapDataFile.MonsterSpawns.Count.Equals(savedMapDataFile.MonsterSpawns.Count), "Monster spawn counts do not match");

            for (int i = 0; i < mapDataFile.MonsterSpawns.Count; i++) {
                Assert.That(mapDataFile.MonsterSpawns[i].Name.Equals(savedMapDataFile.MonsterSpawns[i].Name), "Monster spawn names do not match");
                Assert.That(mapDataFile.MonsterSpawns[i].WarpID.Equals(savedMapDataFile.MonsterSpawns[i].WarpID), "Monster spawn warp ID values do not match");
                Assert.That(mapDataFile.MonsterSpawns[i].EventID.Equals(savedMapDataFile.MonsterSpawns[i].EventID), "Monster spawn event ID values do not match");
                Assert.That(mapDataFile.MonsterSpawns[i].ObjectType.Equals(savedMapDataFile.MonsterSpawns[i].ObjectType), "Monster spawn object type values do not match");
                Assert.That(mapDataFile.MonsterSpawns[i].ObjectID.Equals(savedMapDataFile.MonsterSpawns[i].ObjectID), "Monster spawn object ID values do not match");
                Assert.That(mapDataFile.MonsterSpawns[i].MapPosition.Equals(savedMapDataFile.MonsterSpawns[i].MapPosition), "Monster spawn map positions do not match");
                Assert.That(mapDataFile.MonsterSpawns[i].Rotation.Equals(savedMapDataFile.MonsterSpawns[i].Rotation), "Monster spawn rotations do not match");
                Assert.That(mapDataFile.MonsterSpawns[i].Position.Equals(savedMapDataFile.MonsterSpawns[i].Position), "Monster spawn positions do not match");
                Assert.That(mapDataFile.MonsterSpawns[i].Scale.Equals(savedMapDataFile.MonsterSpawns[i].Scale), "Monster spawn scales do not match");
                Assert.That(mapDataFile.MonsterSpawns[i].SpawnName.Equals(savedMapDataFile.MonsterSpawns[i].SpawnName), "Monster spawn names do not match");

                Assert.That(mapDataFile.MonsterSpawns[i].NormalSpawnPoints.Count.Equals(savedMapDataFile.MonsterSpawns[i].NormalSpawnPoints.Count), "Normal spawn point counts do not match");

                for (int j = 0; j < mapDataFile.MonsterSpawns[i].NormalSpawnPoints.Count; j++) {
                    Assert.That(mapDataFile.MonsterSpawns[i].NormalSpawnPoints[j].Name.Equals(savedMapDataFile.MonsterSpawns[i].NormalSpawnPoints[j].Name), "Normal spawn point names do not match");
                    Assert.That(mapDataFile.MonsterSpawns[i].NormalSpawnPoints[j].Monster.Equals(savedMapDataFile.MonsterSpawns[i].NormalSpawnPoints[j].Monster), "Normal spawn point monster values do not match");
                    Assert.That(mapDataFile.MonsterSpawns[i].NormalSpawnPoints[j].Count.Equals(savedMapDataFile.MonsterSpawns[i].NormalSpawnPoints[j].Count), "Normal spawn point Count values do not match");
                }

                Assert.That(mapDataFile.MonsterSpawns[i].TacticalSpawnPoints.Count.Equals(savedMapDataFile.MonsterSpawns[i].TacticalSpawnPoints.Count), "Tactical spawn point counts do not match");

                for (int j = 0; j < mapDataFile.MonsterSpawns[i].TacticalSpawnPoints.Count; j++) {
                    Assert.That(mapDataFile.MonsterSpawns[i].TacticalSpawnPoints[j].Name.Equals(savedMapDataFile.MonsterSpawns[i].TacticalSpawnPoints[j].Name), "Tactical spawn point names do not match");
                    Assert.That(mapDataFile.MonsterSpawns[i].TacticalSpawnPoints[j].Monster.Equals(savedMapDataFile.MonsterSpawns[i].TacticalSpawnPoints[j].Monster), "Tactical spawn point monster values do not match");
                    Assert.That(mapDataFile.MonsterSpawns[i].TacticalSpawnPoints[j].Count.Equals(savedMapDataFile.MonsterSpawns[i].TacticalSpawnPoints[j].Count), "Tactical spawn point Count values do not match");
                }
                Assert.That(mapDataFile.MonsterSpawns[i].Interval.Equals(savedMapDataFile.MonsterSpawns[i].Interval), "Monster spawn interval values do not match");
                Assert.That(mapDataFile.MonsterSpawns[i].Limit.Equals(savedMapDataFile.MonsterSpawns[i].Limit), "Monster spawn limit values do not match");
                Assert.That(mapDataFile.MonsterSpawns[i].Range.Equals(savedMapDataFile.MonsterSpawns[i].Range), "Monster spawn range values do not match");
                Assert.That(mapDataFile.MonsterSpawns[i].TacticalVariable.Equals(savedMapDataFile.MonsterSpawns[i].TacticalVariable), "Monster spawn tactical variable values do not match");
            }

            Assert.That(mapDataFile.WaterSize.Equals(savedMapDataFile.WaterSize), "Water size values do not match");
            Assert.That(mapDataFile.WaterPlanes.Count.Equals(savedMapDataFile.WaterPlanes.Count), "Water plane counts do not match");

            for (int i = 0; i < mapDataFile.WaterPlanes.Count; i++) {
                Assert.That(mapDataFile.WaterPlanes[i].StartPosition.Equals(savedMapDataFile.WaterPlanes[i].StartPosition), "Water plane start positions do not match");
                Assert.That(mapDataFile.WaterPlanes[i].EndPosition.Equals(savedMapDataFile.WaterPlanes[i].EndPosition), "Water plane end positions do not match");
            }

            Assert.That(mapDataFile.WarpPoints.Count.Equals(savedMapDataFile.WarpPoints.Count), "Warp point counts do not match");

            for (int i = 0; i < mapDataFile.WarpPoints.Count; i++) {
                Assert.That(mapDataFile.WarpPoints[i].Name.Equals(savedMapDataFile.WarpPoints[i].Name), "Warp point names do not match");
                Assert.That(mapDataFile.WarpPoints[i].WarpID.Equals(savedMapDataFile.WarpPoints[i].WarpID), "Warp point warp ID values do not match");
                Assert.That(mapDataFile.WarpPoints[i].EventID.Equals(savedMapDataFile.WarpPoints[i].EventID), "Warp point event ID values do not match");
                Assert.That(mapDataFile.WarpPoints[i].ObjectType.Equals(savedMapDataFile.WarpPoints[i].ObjectType), "Warp point object type values do not match");
                Assert.That(mapDataFile.WarpPoints[i].ObjectID.Equals(savedMapDataFile.WarpPoints[i].ObjectID), "Warp point object ID values do not match");
                Assert.That(mapDataFile.WarpPoints[i].MapPosition.Equals(savedMapDataFile.WarpPoints[i].MapPosition), "Warp point map positions do not match");
                Assert.That(mapDataFile.WarpPoints[i].Rotation.Equals(savedMapDataFile.WarpPoints[i].Rotation), "Warp point rotations do not match");
                Assert.That(mapDataFile.WarpPoints[i].Position.Equals(savedMapDataFile.WarpPoints[i].Position), "Warp point positions do not match");
                Assert.That(mapDataFile.WarpPoints[i].Scale.Equals(savedMapDataFile.WarpPoints[i].Scale), "Warp point scales do not match");
            }

            Assert.That(mapDataFile.CollisionObjects.Count.Equals(savedMapDataFile.CollisionObjects.Count), "Collision object counts do not match");

            for (int i = 0; i < mapDataFile.CollisionObjects.Count; i++) {
                Assert.That(mapDataFile.CollisionObjects[i].Name.Equals(savedMapDataFile.CollisionObjects[i].Name), "Collision object names do not match");
                Assert.That(mapDataFile.CollisionObjects[i].WarpID.Equals(savedMapDataFile.CollisionObjects[i].WarpID), "Collision object warp ID values do not match");
                Assert.That(mapDataFile.CollisionObjects[i].EventID.Equals(savedMapDataFile.CollisionObjects[i].EventID), "Collision object event ID values do not match");
                Assert.That(mapDataFile.CollisionObjects[i].ObjectType.Equals(savedMapDataFile.CollisionObjects[i].ObjectType), "Collision object object type values do not match");
                Assert.That(mapDataFile.CollisionObjects[i].ObjectID.Equals(savedMapDataFile.CollisionObjects[i].ObjectID), "Collision object object ID values do not match");
                Assert.That(mapDataFile.CollisionObjects[i].MapPosition.Equals(savedMapDataFile.CollisionObjects[i].MapPosition), "Collision object map positions do not match");
                Assert.That(mapDataFile.CollisionObjects[i].Rotation.Equals(savedMapDataFile.CollisionObjects[i].Rotation), "Collision object rotations do not match");
                Assert.That(mapDataFile.CollisionObjects[i].Position.Equals(savedMapDataFile.CollisionObjects[i].Position), "Collision object positions do not match");
                Assert.That(mapDataFile.CollisionObjects[i].Scale.Equals(savedMapDataFile.CollisionObjects[i].Scale), "Collision object scales do not match");
            }

            Assert.That(mapDataFile.EventObjects.Count.Equals(savedMapDataFile.EventObjects.Count), "Event object counts do not match");

            for (int i = 0; i < mapDataFile.EventObjects.Count; i++) {
                Assert.That(mapDataFile.EventObjects[i].Name.Equals(savedMapDataFile.EventObjects[i].Name), "Event object names do not match");
                Assert.That(mapDataFile.EventObjects[i].WarpID.Equals(savedMapDataFile.EventObjects[i].WarpID), "Event object warp ID values do not match");
                Assert.That(mapDataFile.EventObjects[i].EventID.Equals(savedMapDataFile.EventObjects[i].EventID), "Event object event ID values do not match");
                Assert.That(mapDataFile.EventObjects[i].ObjectType.Equals(savedMapDataFile.EventObjects[i].ObjectType), "Event object object type values do not match");
                Assert.That(mapDataFile.EventObjects[i].ObjectID.Equals(savedMapDataFile.EventObjects[i].ObjectID), "Event object object ID values do not match");
                Assert.That(mapDataFile.EventObjects[i].MapPosition.Equals(savedMapDataFile.EventObjects[i].MapPosition), "Event object map positions do not match");
                Assert.That(mapDataFile.EventObjects[i].Rotation.Equals(savedMapDataFile.EventObjects[i].Rotation), "Event object rotations do not match");
                Assert.That(mapDataFile.EventObjects[i].Position.Equals(savedMapDataFile.EventObjects[i].Position), "Event object positions do not match");
                Assert.That(mapDataFile.EventObjects[i].Scale.Equals(savedMapDataFile.EventObjects[i].Scale), "Event object scales do not match");
                Assert.That(mapDataFile.EventObjects[i].FunctionName.Equals(savedMapDataFile.EventObjects[i].FunctionName), "Event object function names do not match");
                Assert.That(mapDataFile.EventObjects[i].ConversationFile.Equals(savedMapDataFile.EventObjects[i].ConversationFile), "Event object conversation file values do not match");
            }
        }
    }
}