using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace ServerApp.Models
{
    public class SeedData
    {
        static string baseDir = @"D:\Programming\Angular\Angular_Dotnet\QuranAnalysis\ServerApp\Models\SeedData";
        static List<string> files = new List<string> {  "quran-uthmani-xml.xml",
                                                        "ar.muyassar.xml",
                                                        "ar.jalalayn.xml",
                                                        "ibn-katheer.xml",
                                                        "tabary.xml",  
                                                        "en.hilali.xml",
                                                        "quran-simple-clean-xml.xml",
                                                        "quran-meta.xml" };

        public static void SeedDatabase(DataContext context)
        {
            context.Database.Migrate();
            if (context.Quran.Count() == 0)
            {
                SeedQuran(context);
                SeedMeta(context);
                SeedMindMaps(context);
                context.SaveChanges();
            }
        }

        public static void SeedQuran(DataContext ctx)
        {
            string QuranPath = baseDir + @"\" + files[0];
            string TafseerPath = baseDir + @"\" + files[1];
            string JalalynPath = baseDir + @"\" + files[2];
            string IbnKatheerPath = baseDir + @"\" + files[3];
            string TabaryPath = baseDir + @"\" + files[4];
            string TranslationPath = baseDir + @"\" + files[5];
            string QuranCleanPath = baseDir + @"\" + files[6];

            SeedQuran(QuranPath, ctx);
            SeedTafseer(TafseerPath, ctx);
            SeedJalalyn(JalalynPath, ctx);
            SeedIbnKatheer(IbnKatheerPath, ctx);
            SeedTabary(TabaryPath, ctx);
            SeedTranslation(TranslationPath, ctx);
            SeedQuranClean(QuranCleanPath, ctx);
        }

        public static void SeedQuran(string path, DataContext ctx)
        {
            DbSet<Quran> data = ctx.Quran;
            XElement quran = XElement.Load(path);
            SeedQuranData<Quran>(data, quran);

        }

        public static void SeedTafseer(string path, DataContext ctx)
        {
            DbSet<Tafseer> data = ctx.Tafseer;
            XElement quran = XElement.Load(path);
            SeedQuranData<Tafseer>(data, quran);
        }

        public static void SeedJalalyn(string path, DataContext ctx)
        {
            DbSet<Jalalayn> data = ctx.Jalalayn;
            XElement quran = XElement.Load(path);
            SeedQuranData<Jalalayn>(data, quran);
        }

        public static void SeedIbnKatheer(string path, DataContext ctx)
        {
            DbSet<IbnKatheer> data = ctx.IbnKatheer;
            XElement quran = XElement.Load(path);
            SeedQuranData<IbnKatheer>(data, quran);
        }

        public static void SeedTabary(string path, DataContext ctx)
        {
            DbSet<Tabary> data = ctx.Tabary;
            XElement quran = XElement.Load(path);
            SeedQuranData<Tabary>(data, quran);
        }

        public static void SeedTranslation(string path, DataContext ctx)
        {
            DbSet<Translation> data = ctx.Translation;
            XElement quran = XElement.Load(path);
            SeedQuranData<Translation>(data, quran);
        }

        public static void SeedQuranClean(string path, DataContext ctx)
        {
            DbSet<QuranClean> data = ctx.QuranClean;
            XElement quran = XElement.Load(path);
            SeedQuranData<QuranClean>(data, quran);
        }

        public static void SeedQuranData<T>(DbSet<T> data,  XElement quran) where T : class, new(){
            int globIdx = 1;
            int suraIndex = 1;

            foreach (XElement sura in quran.Elements())
            {
                foreach (XElement aya in sura.Elements())
                {
                    T cur = new T();

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

        public static void SeedMeta(DataContext ctx)
        {
            string metaPath = baseDir + @"\" + files[7];
            XElement meta = XElement.Load(metaPath);

            SeedSura(meta.Element("suras").Elements("sura"), ctx);
            SeedJuzs(meta.Element("juzs").Elements("juz"), ctx);
            SeedHizbs(meta.Element("hizbs").Elements("quarter"), ctx);
            SeedManzils(meta.Element("manzils").Elements("manzil"), ctx);
            SeedRukus(meta.Element("rukus").Elements("ruku"), ctx);
            SeedPages(meta.Element("pages").Elements("page"), ctx);
            SeedSajda(meta.Element("sajdas").Elements("sajda"), ctx);
        }

        public static void SeedSura(IEnumerable<XElement> elmnts, DataContext ctx)
        {
            DbSet<Sura> data = ctx.Suras;
            foreach (XElement elm in elmnts)
            {
                data.Add(new Sura
                {
                    Index = int.Parse(elm.Attribute("index").Value),
                    Ayas = int.Parse(elm.Attribute("ayas").Value),
                    Start = int.Parse(elm.Attribute("start").Value),
                    Name = elm.Attribute("name").Value,
                    Tname = elm.Attribute("tname").Value,
                    Ename = elm.Attribute("ename").Value,
                    Type = elm.Attribute("type").Value,
                    Order = int.Parse(elm.Attribute("order").Value),
                    Rukus = int.Parse(elm.Attribute("rukus").Value)
                });
            }
        }

        public static void SeedJuzs(IEnumerable<XElement> elmnts, DataContext ctx)
        {
            DbSet<Juz> data = ctx.Juzs;
            SeedQuranMeta<Juz>(data, elmnts);
        }

        public static void SeedHizbs(IEnumerable<XElement> elmnts, DataContext ctx)
        {
            DbSet<Hizb> data = ctx.Hizbs;
            SeedQuranMeta<Hizb>(data, elmnts);
        }

        public static void SeedManzils(IEnumerable<XElement> elmnts, DataContext ctx)
        {
            DbSet<Manzil> data = ctx.Manzils;
            SeedQuranMeta<Manzil>(data, elmnts);
        }

        public static void SeedRukus(IEnumerable<XElement> elmnts, DataContext ctx)
        {
            DbSet<Ruku> data = ctx.Rukus;
            SeedQuranMeta<Ruku>(data, elmnts);
        }

        public static void SeedPages(IEnumerable<XElement> elmnts, DataContext ctx)
        {
            DbSet<Page> data = ctx.Pages;
            SeedQuranMeta<Page>(data, elmnts);
        }

         public static void SeedQuranMeta<T>(DbSet<T> data, IEnumerable<XElement> elmnts) where T : class, new() 
        {
           foreach (XElement elm in elmnts)
            {
                 T cur = new T();

                    typeof(T).GetProperty("Index").SetValue(cur, int.Parse(elm.Attribute("index").Value));
                    typeof(T).GetProperty("Sura").SetValue(cur, int.Parse(elm.Attribute("sura").Value));
                    typeof(T).GetProperty("Aya").SetValue(cur, int.Parse(elm.Attribute("aya").Value));
                    data.Add(cur);
            }
        }

        public static void SeedSajda(IEnumerable<XElement> elmnts, DataContext ctx)
        {
            DbSet<Sajda> data = ctx.Sajdas;
            foreach (XElement elm in elmnts)
            {
                data.Add(new Sajda
                {
                    Index = int.Parse(elm.Attribute("index").Value),
                    Sura = int.Parse(elm.Attribute("sura").Value),
                    Aya = int.Parse(elm.Attribute("aya").Value),
                    Type = elm.Attribute("type").Value
                });
            }
        }
        public static void SeedMindMaps(DataContext ctx)
        {
           string MindMapsPath = baseDir + @"\MindMaps";
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
    }
}
