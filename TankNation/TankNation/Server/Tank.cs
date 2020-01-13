namespace Server
{
    class Tank
    {
        public int health;
        public int kills;
        public int positionX;
        public int positionY;
        public int angle;
        public int ID;

        public Tank(int ID, int positionX, int positionY, int angle)
        {
            this.ID = ID;
            health = 100;
            kills = 0;
            this.positionX = positionX;
            this.positionY = positionY;
            this.angle = angle;

        }

    }
}