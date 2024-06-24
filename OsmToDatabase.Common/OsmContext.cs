using Microsoft.EntityFrameworkCore;

namespace OsmToDatabase.Common
{
    public class OsmContext : DbContext
    {
        public DbSet<Peak> Peaks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=127.0.0.1;Username=postgres;Database=peakdata");
        }
    }

    public class Peak
    {
        public long Id { get; set; }
        public int Version { get; set; }
        public DateTime TimeStamp { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? Elevation { get; set; }
        public double? Prominence { get; set; }
        public double? Isolation { get; set; }
        public string? Name { get; set; }
        public string? AlternativeName { get; set; }
        public string? EnglishName { get; set; }
        public string? EnglishAlternativeName { get; set; }
        public string? ItalianName { get; set; }
        public string? SlovenianName { get; set; }
        public string? GermanName { get; set; }
        public bool SummitRegisterPresent { get; set; }
        public string? Wikidata { get; set; }
    }
}
