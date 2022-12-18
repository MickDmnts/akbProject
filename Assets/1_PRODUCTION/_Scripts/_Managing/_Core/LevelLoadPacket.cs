using UnityEngine;

namespace akb.Core.Managing.LevelLoading
{
    [CreateAssetMenu(fileName = "Level Load Packet", menuName = "Level Load Packet/Packet")]
    public class LevelLoadPacket : ScriptableObject
    {
        //The packet index the LevelSceneManager list.
        public int PacketIndex;

        //The scene this packet represents (mainly the one that it unloads first.)
        public GameScenes PacketMainScene;

        //The scenes to load
        public GameScenes[] ScenesToLoad;

        //The scenes to unload
        public GameScenes[] ScenesToUnload;
    }
}