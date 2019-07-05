namespace ElectionResults.Core.Models
{
    public class Candidate
    {
        public string Id { get; set; }

        public int Votes { get; set; }

        public string Name { get; set; }

        public decimal Percentage { get; set; }
    }
}