using OsmSharp;
using OsmSharp.Streams;
using OsmToDatabase.Common;

OsmContext db = new OsmContext();
long counter = 0;

using (var fileStream = new FileInfo(args[0]).OpenRead())
{
    var source = new PBFOsmStreamSource(fileStream);
    foreach (var element in source)
    {
        if (element is Node node)
        {
            if (node.TagValueByKey("natural") != "peak")
            {
                continue;
            }

            Peak peak = new Peak
            {
                Id = node.Id.Value,
                Version = node.Version.Value,
                TimeStamp = node.TimeStamp.Value.ToUniversalTime(),
                Latitude = node.Latitude.Value,
                Longitude = node.Longitude.Value,
                Elevation = node.TagDoubleValueByKey("ele"),
                Prominence = node.TagDoubleValueByKey("prominence"),
                Isolation = node.TagDoubleValueByKey("isolation"),
                Name = node.TagValueByKey("name"),
                AlternativeName = node.TagValueByKey("alt_name"),
                EnglishName = node.TagValueByKey("name:en"),
                EnglishAlternativeName = node.TagValueByKey("alt_name:en"),
                ItalianName = node.TagValueByKey("name:it"),
                SlovenianName = node.TagValueByKey("name:sl"),
                GermanName = node.TagValueByKey("name:de"),
                SummitRegisterPresent = node.TagValueByKey("summit:register") == "yes",
                Wikidata = node.TagValueByKey("wikidata")
            };
            await db.AddAsync(peak);
            counter++;

            if ((counter % 50_000) == 0)
            {
                Console.WriteLine($"{counter}: Saving another batch.");
                await db.SaveChangesAsync();
            }
        }
    }
    await db.SaveChangesAsync();
}
