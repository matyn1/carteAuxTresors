namespace CarteAuxTresors.Models
{
    public class Mountain: ModelWithPosition
    {
        public Mountain(int x, int y)
        {
            Position = new (x, y);
        }

        public override string ToString()
        {
            return $"M - {Position.X} - {Position.Y}";
        }
    }
}
