using System.Collections.Concurrent;

namespace WebChattingServer.Rooms
{
    public class RoomManager
    {
        public ConcurrentDictionary<int, Room> roomDict;
        private int _roomGenerator = 0;
        public RoomManager()
        {
            roomDict = new();
        }
        public void MakeRoom(int playerId, int maxCount, string roomName, string hostName)
        {
            Room room = new(++_roomGenerator, maxCount, roomName, hostName);
            roomDict.TryAdd(_roomGenerator, room);
        }
        public bool CheckCanEnter(int roomId)
        {
            if (roomDict.TryGetValue(roomId, out var room) && room.CanEnter)
                return true;
            return false;
        }
    }
}
