
using QuranHub.Domain.Models;
using System.Xml.Linq;


namespace QuranHub.DAL.Database;

public class QuranSeedData
{
    static string baseDir = Directory.GetCurrentDirectory();

    static string solutionDir = Directory.GetParent(baseDir) + @"\QuranHub.DAL\Database\SeedData";

    static List<string> files = new List<string> 
    { 
        "quran-uthmani-xml.xml", "ar.muyassar.xml",
        "ar.jalalayn.xml", "katheerWithHTML.xml",
        "tabaryWithHTML.xml","qortobiWithHTML.xml",  
        "en.hilali.xml","quran-simple-clean-xml.xml",
        "quran-meta.xml",
        "sahih-elbukhary.xml"
    };

    public static async Task SeedDatabaseAsync(IServiceProvider provider)
    {
        try
        {
            QuranContext context = provider.GetRequiredService<QuranContext>();

            context.Database.Migrate();

            if (context.Quran.Count() == 0)
            {
                SeedQuran(context);

                SeedMeta(context);

                SeedMindMaps(context);

                await SeedSahihElbokhary(context);

                context.SaveChanges();            
            }
        }
        catch (Exception ex)
        {
            return;
        }
    }

    public static void SeedQuran(QuranContext ctx)
    {
        try
        {
            string QuranPath = solutionDir + @"\" + files[0];
            string TafseerPath = solutionDir + @"\" + files[1];
            string JalalynPath = solutionDir + @"\" + files[2];
            string IbnKatheerPath = solutionDir + @"\" + files[3];
            string TabaryPath = solutionDir + @"\" + files[4];
            string QortobiPath = solutionDir + @"\" + files[5];
            string TranslationPath = solutionDir + @"\" + files[6];
            string QuranCleanPath = solutionDir + @"\" + files[7];

            SeedQuranData(ctx.Quran, XElement.Load(QuranPath));
            SeedQuranData(ctx.Muyassar, XElement.Load(TafseerPath));
            SeedQuranData(ctx.Jalalayn, XElement.Load(JalalynPath));
            SeedQuranData(ctx.IbnKatheer, XElement.Load(IbnKatheerPath));
            SeedQuranData(ctx.Tabary, XElement.Load(TabaryPath));
            SeedQuranData(ctx.Qortobi, XElement.Load(QortobiPath));
            SeedQuranData(ctx.Translation, XElement.Load(TranslationPath));
            SeedQuranData(ctx.QuranClean, XElement.Load(QuranCleanPath));
            SeedWeightVector(ctx.WeightVectorDimentions, XElement.Load(QuranCleanPath));
        }
        catch (Exception ex)
        {
            return;
        }

    }

    public static void SeedQuranData<T>(DbSet<T> data, XElement quran) where T : class, new()
    {
        try
        {
            int globIdx = 1;
            int suraIndex = 1;

            foreach (XElement sura in quran.Elements())
            {
                foreach (XElement aya in sura.Elements())
                {
                    T cur = new T();

                    typeof(T).GetProperty("Id").SetValue(cur, globIdx);

                    typeof(T).GetProperty("Index").SetValue(cur, globIdx);

                    typeof(T).GetProperty("Sura").SetValue(cur, suraIndex);

                    typeof(T).GetProperty("Aya").SetValue(cur, int.Parse(aya.Attribute("index").Value));

                    typeof(T).GetProperty("Text").SetValue(cur, aya.Attribute("text").Value);

                    data.Add(cur);

                    ++globIdx;
                }
                suraIndex++;
            }
        }
        catch (Exception ex)
        {
            return;
        }
    }

    public static void SeedMeta(QuranContext ctx)
    {
        try
        {
            string metaPath = solutionDir + @"\" + files[8];

            XElement meta = XElement.Load(metaPath);

            SeedQuranMeta<Sura>(ctx.Suras, meta.Element("suras").Elements("sura"));
            SeedQuranMeta<Juz>(ctx.Juzs, meta.Element("juzs").Elements("juz"));
            SeedQuranMeta<Hizb>(ctx.Hizbs, meta.Element("hizbs").Elements("quarter"));
            SeedQuranMeta<Manzil>(ctx.Manzils, meta.Element("manzils").Elements("manzil"));
            SeedQuranMeta<Ruku>(ctx.Rukus, meta.Element("rukus").Elements("ruku"));
            SeedQuranMeta<Page>(ctx.Pages, meta.Element("pages").Elements("page"));
            SeedQuranMeta<Sajda>(ctx.Sajdas, meta.Element("sajdas").Elements("sajda"));
        }
        catch (Exception ex)
        {
            return;
        }
    }

   

     public static void SeedQuranMeta<T>(DbSet<T> data, IEnumerable<XElement> elmnts) where T : class, new()
     {
        try
        {
            foreach (XElement elm in elmnts)
            {
                T cur = new T();

                foreach (PropertyInfo prop in typeof(T).GetProperties())
                {
                    if (prop.Name.Contains("Id") )
                    {
                        typeof(T).GetProperty(prop.Name).SetValue(cur, int.Parse(elm.Attribute("index").Value));
                    }
                    else
                    {
                        typeof(T).GetProperty(prop.Name).SetValue(cur, ParsePropValue(prop, elm));
                    }
                }

                data.Add(cur);
            }
        }
        catch (Exception ex)
        {
            return;
        }
    }

    public static dynamic ParsePropValue(PropertyInfo prop, XElement elm )
    {
        try
        {
            string propName = prop.Name.ToLower();

            string propType = prop.PropertyType.ToString();

            if (propType == "System.Int32")
            {
                return int.Parse(elm.Attribute(propName).Value);
            }
            else
            {
                return elm.Attribute(propName).Value;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }
        
    public static void SeedMindMaps(QuranContext ctx)
    {
        try
        {
           string MindMapsPath = solutionDir + @"\MindMaps";

           DbSet<MindMap> data = ctx.MindMaps;

           var txtFiles = Directory.EnumerateFiles(MindMapsPath, "*.JPG");

           foreach (string currentFile in txtFiles)
           {
                data.Add(new MindMap
                {
                    Index = int.Parse(Path.GetFileNameWithoutExtension(currentFile)),
                    MapImage =  File.ReadAllBytes(currentFile)
                });
           }
        }
        catch (Exception ex)
        {
            return;
        }
    }

    private static void SeedWeightVector(DbSet<WeightVectorDimention> data, XElement quran)
    {
        try
        {
            var weightVector = new Dictionary<string, double>();

            foreach (XElement sura in quran.Elements())
            {
                foreach (XElement aya in sura.Elements())
                {
                    string ayaText = aya.Attribute("text").Value;

                    string[] words = ayaText.Split(" ");

                    foreach (string word in words)
                    {
                        if (weightVector.ContainsKey(word))
                        {
                            weightVector[word]++;
                        }
                        else
                        {
                            weightVector.Add(word, 1);
                        }
                    }


                }
           
            }

            foreach(var keyValue in weightVector)
            {
                data.Add(new WeightVectorDimention
                {
                    Word = keyValue.Key,
                    Value = keyValue.Value
                });
            }

        }
        catch (Exception ex)
        {
             return;
        }

    }

    public static async Task SeedSahihElbokhary(QuranContext ctx)
    {
        try
        {
            string metaPath = solutionDir + @"\" + files[9];

            XElement data = XElement.Load(metaPath);

            await SeedSections(ctx, data);

        }
        catch (Exception ex)
        {
            return;
        }
    }

    public static async Task SeedSections(QuranContext ctx, XElement xmlData)
    {
        try
        {
            IEnumerable<XElement> sections = xmlData.Element("metadata").Element("sections").Elements("element");

            int sectionNumber = 1;

            foreach (XElement section in sections)
            {

                    Section curSection = new Section()
                    {
                        Name = section.Value
                    };

                    GetSectionDetails(xmlData, ref curSection, sectionNumber);

                    ctx.Sections.Add(curSection);

                    await ctx.SaveChangesAsync();

                    SeedHadiths(ctx, xmlData, curSection.HadithNumberStart, curSection.HadithNumberEnd, curSection.SectionId);

                sectionNumber++;
            }

        }
        catch (Exception ex)
        {
            return;
        }
    }

    public static void GetSectionDetails(XElement xmlData, ref Section section,  int sectionNumber)
    {
        try
        {
            var curSection = xmlData.Element("metadata").Element("section_details").Elements("section_detail").ToList()[sectionNumber - 1];
            section.HadithNumberStart = int.Parse(curSection.Element("hadithnumber_first").Value);
            section.HadithNumberEnd = int.Parse(curSection.Element("hadithnumber_last").Value);

        }
        catch (Exception ex)
        {
            return;
        }
    }

    public static void SeedHadiths(QuranContext ctx, XElement xmlData, int start, int end, int sectionId)
    {
        try
        {
            List<XElement> hadiths = xmlData.Elements("hadiths").ToList();

            while(start <= end)
            {

                var hadith = hadiths[start];
                try
                {

                    Hadith curHadith = new Hadith()
                    {
                        Text = hadith.Element("text").Value,
                        HadithNumber = int.Parse(hadith.Element("hadithnumber").Value),
                        HadithNumberInSection = int.Parse(hadith.Element("reference").Element("hadith").Value),
                        SectionId = sectionId
                    };

                    if (int.Parse(hadith.Element("reference").Element("book").Value) > 97)
                    {
                        throw new Exception();

                    }
                    ctx.Hadiths.Add(curHadith);
                    
                }
                catch (Exception ex)
                {
                    start++;
                    continue;
                }
                start++;

            }

        }
        catch (Exception ex)
        {
            return;
        }
    }
}

