namespace WarlamovGleb_CatFramework
{
    public class CatException : ArgumentException
    {
        public CatException(string message) : base(message)
        {
        }
    }
    public abstract class Cat
    {
        public abstract int Fluffiness { get; protected set; }
        public abstract string FluffinessCheck();
        public override string ToString()
        {
            return "A cat with fluffness: " + Fluffiness;
        }
    }
    public class Tiger : Cat
    {
        public override int Fluffiness { get; protected set; }
        public double Weight { get; protected set; }

        public override string FluffinessCheck()
        {
            return "Kycb";
        }
        public Tiger()
        {
            Fluffiness = 50;
            Weight = 50;
        }
        public Tiger(int fluffiness, double weight)
        {
            string toThrowNext = "";
            if (weight > 140 || weight < 0)
                toThrowNext = ("Unable to create a tiger with weight: " + weight);
            if (fluffiness < 0 || fluffiness > 100)
            {
                if (toThrowNext != "")
                {
                    throw new CatException("Unable to create a tiger with fluffiness: " + fluffiness + ".\n" + toThrowNext);
                }
                else
                {
                    throw new CatException("Unable to create a tiger with fluffiness: " + fluffiness);
                }
            }
            if (toThrowNext != "")
            {
                throw new CatException(toThrowNext);
            }
            Fluffiness = fluffiness;
            Weight = weight;
        }
        public override string ToString()
        {
            return "A tiger with weight: " + Weight + " fluffness: " + Fluffiness;
        }
    }
    public class CuteCat : Cat
    {
        public override int Fluffiness { get; protected set; }

        public override string FluffinessCheck()
        {
            if (Fluffiness > 75) return "OwO";
            else if (Fluffiness <= 75 && Fluffiness >= 51) return "Heavy";
            else if (Fluffiness <= 50 && Fluffiness >= 21) return "Medium";
            else if (Fluffiness <= 20 && Fluffiness >= 1) return "Slightly";
            else return "Sphynx";
        }
        public override string ToString()
        {
            return "A cute cat with fluffiness: " + Fluffiness;
        }
        public CuteCat()
        {
            Fluffiness = 50;
        }
        public CuteCat(int fluffiness)
        {
            if (fluffiness < 0 || fluffiness > 140)
                throw new CatException("Unable to create a cute cat with fluffiness: " + fluffiness);
            Fluffiness = fluffiness;
        }
    }
}
