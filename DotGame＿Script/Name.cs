namespace Game
{
    public class Weapon
    {
        public int NAME { get; set; }
        public int HP { get; set; }
        public int ATK{ get; set; }
        public Weapon (int name,int hp, int atk)
        {
            HP = hp;
            ATK = atk;
            NAME = name;

        }

    }
    public class Enemies
    {
        public int name;
        public int hp;
        public int atk;
    }
   public static class Price
    {
        public const int TrapMoney = 200;
        public const int Trap1Money = 200;
        public const int Trap2Money = 400;
        public const int Trap3Money = 250;

    }
    public class Coin
    {
        public int coinnumber;

    }
}