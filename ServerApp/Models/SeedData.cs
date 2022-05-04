using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace ServerApp.Models
{
    public class SeedData
    {
        static string baseDir = @"D:\Programming\Angular\Angular_Dotnet\QuranAnalysis\ServerApp\Models\SeedData";
        static List<string> files = new List<string> { "quran-uthmani-xml.xml", "ar.muyassar.xml", "en.hilali.xml",  
                                                        "quran-simple-clean-xml.xml", "quran-meta.xml" };
        public static void SeedDatabase(DataContext context)
        {
            context.Database.Migrate();
            if (context.Quran.Count() == 0)
            {
                SeedQuran(context);
                SeedMeta(context);
                context.SaveChanges();
            }
        }

        public static void SeedQuran(DataContext ctx)
        {
            string QuranPath = baseDir + @"\" + files[0];
            string TafseerPath = baseDir + @"\" + files[1];
            string TranslationPath = baseDir + @"\" + files[2];
            string QuranCleanPath = baseDir + @"\" + files[3];

            SeedQuran(QuranPath, ctx);
            SeedTafseer(TafseerPath, ctx);
            SeedTranslation(TranslationPath, ctx);
            SeedQuranClean(QuranCleanPath, ctx);
        }

        public static void SeedQuran(string path, DataContext ctx)
        {
            DbSet<Quran> data = ctx.Quran;
            XElement quran = XElement.Load(path);
            int globIdx = 1;
            int suraIndex = 1;

            foreach (XElement sura in quran.Elements())
            {
                foreach (XElement aya in sura.Elements())
                {
                    data.Add(new Quran
                    {
                        Index = globIdx,
                        Sura = suraIndex,
                        Aya = int.Parse(aya.Attribute("index").Value),
                        Text = aya.Attribute("text").Value
                    });
                    ++globIdx;

                }
                suraIndex++;
            }
        }

        public static void SeedTafseer(string path, DataContext ctx)
        {
            DbSet<Tafseer> data = ctx.Tafseer;
            XElement quran = XElement.Load(path);
            int globIdx = 1;
            int suraIndex = 1;
            foreach (XElement sura in quran.Elements())
            {
                foreach (XElement aya in sura.Elements())
                {
                    data.Add(new Tafseer
                    {
                        Index = globIdx,
                        Sura = suraIndex,
                        Aya = int.Parse(aya.Attribute("index").Value),
                        Text = aya.Attribute("text").Value
                    });
                    globIdx++;
                }
                suraIndex++;
            }
        }

        public static void SeedTranslation(string path, DataContext ctx)
        {
            DbSet<Translation> data = ctx.Translation;
            XElement quran = XElement.Load(path);
            int globIndx = 1;
            int suraIndex = 1;
            foreach (XElement sura in quran.Elements())
            {
                foreach (XElement aya in sura.Elements())
                {
                    data.Add(new Translation
                    {
                        Index = globIndx,
                        Sura = suraIndex,
                        Aya = int.Parse(aya.Attribute("index").Value),
                        Text = aya.Attribute("text").Value
                    });
                    globIndx++;
                }
                suraIndex++;
            }
        }
        public static void SeedQuranClean(string path, DataContext ctx)
        {
            DbSet<QuranClean> data = ctx.QuranClean;
            XElement quran = XElement.Load(path);
            int globIdx = 1;
            int suraIndex = 1;

            foreach (XElement sura in quran.Elements())
            {
                foreach (XElement aya in sura.Elements())
                {
                    data.Add(new QuranClean
                    {
                        Index = globIdx,
                        Sura = suraIndex,
                        Aya = int.Parse(aya.Attribute("index").Value),
                        Text = aya.Attribute("text").Value
                    });
                    ++globIdx;

                }
                suraIndex++;
            }
        }

        public static void SeedMeta(DataContext ctx)
        {
            string metaPath = baseDir + @"\" + files[4];
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
            foreach (XElement elm in elmnts)
            {
                data.Add(new Juz
                {
                    Index = int.Parse(elm.Attribute("index").Value),
                    Sura = int.Parse(elm.Attribute("sura").Value),
                    Aya = int.Parse(elm.Attribute("aya").Value),
                });
            }
        }

        public static void SeedHizbs(IEnumerable<XElement> elmnts, DataContext ctx)
        {
            DbSet<Hizb> data = ctx.Hizbs;
            foreach (XElement elm in elmnts)
            {
                data.Add(new Hizb
                {
                    Index = int.Parse(elm.Attribute("index").Value),
                    Sura = int.Parse(elm.Attribute("sura").Value),
                    Aya = int.Parse(elm.Attribute("aya").Value),
                });
            }
        }

        public static void SeedManzils(IEnumerable<XElement> elmnts, DataContext ctx)
        {
            DbSet<Manzil> data = ctx.Manzils;
            foreach (XElement elm in elmnts)
            {
                data.Add(new Manzil
                {
                    Index = int.Parse(elm.Attribute("index").Value),
                    Sura = int.Parse(elm.Attribute("sura").Value),
                    Aya = int.Parse(elm.Attribute("aya").Value),
                });
            }
        }

        public static void SeedRukus(IEnumerable<XElement> elmnts, DataContext ctx)
        {
            DbSet<Ruku> data = ctx.Rukus;
            foreach (XElement elm in elmnts)
            {
                data.Add(new Ruku
                {
                    Index = int.Parse(elm.Attribute("index").Value),
                    Sura = int.Parse(elm.Attribute("sura").Value),
                    Aya = int.Parse(elm.Attribute("aya").Value),
                });
            }
        }

        public static void SeedPages(IEnumerable<XElement> elmnts, DataContext ctx)
        {
            DbSet<Page> data = ctx.Pages;
            foreach (XElement elm in elmnts)
            {
                data.Add(new Page
                {
                    Index = int.Parse(elm.Attribute("index").Value),
                    Sura = int.Parse(elm.Attribute("sura").Value),
                    Aya = int.Parse(elm.Attribute("aya").Value),
                });
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
    }
}
