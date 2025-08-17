namespace WebChattingServer.Rooms
{
    public class Room
    {
        public int index;
        public int playerCount;
        public int maxPlayerCount;
        public string roomName;
        public string hostName;
        public Room(int idx, int maxCount, string roomName, string hostName)
        {
            index = idx;
            maxPlayerCount = maxCount;
            this.roomName = roomName;
            this.hostName = hostName;
        }
        public bool CanEnter => playerCount < maxPlayerCount;
    }
}
