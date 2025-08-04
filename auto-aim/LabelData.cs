namespace auto_aim
{
    internal class LabelData
    {
        public int id { get; set; }
        public double centerX { get; set; }
        public double centerY { get; set; }
        public double width { get; set; }
        public double height { get; set; }

        public LabelData() { }

        public LabelData(string line)
        {
            var parts = line.Split(' ');
            if (parts.Length < 5)
            {
                throw new ArgumentException("Invalid label format");
            }
            id = int.Parse(parts[0]);
            centerX = double.Parse(parts[1]);
            centerY = double.Parse(parts[2]);
            width = double.Parse(parts[3]);
            height = double.Parse(parts[4]);
        }
    }
}
