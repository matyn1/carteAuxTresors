namespace CarteAuxTresors.Models
{
    public class Treasure : ModelWithPosition
    {
        public Treasure(int x, int y, int nb)
        {
            Position = new(x, y);
            Count = nb;
        }

        public int Count { get; set; }

        public override string ToString()
        {
            return $"T - {Position.X} - {Position.Y} - {Count}";
        }
    }
}
