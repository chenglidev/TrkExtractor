using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TrkExtract
{
    public class Extractor
    {
        public void Extract(string xmlFilePath, string csvFilePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFilePath);

            var elTrkpts = doc.GetElementsByTagName("trkpt");

            var csvConfig = new CsvConfiguration(System.Globalization.CultureInfo.CurrentCulture);
            using (StringWriter writer = new StringWriter())
            {
                writer.WriteLine("Lat,Lon,Depth");
                using (CsvWriter csvWriter = new CsvWriter(writer, csvConfig))
                {
                    foreach (XmlNode elTrkpt in elTrkpts)
                    {
                        var lat = elTrkpt.Attributes["lat"].Value;
                        var lon = elTrkpt.Attributes["lon"].Value;

                        var exts = elTrkpt.FirstChild;
                        var depth = "";
                        foreach (XmlNode hNode in exts.ChildNodes)
                        {
                            if (hNode.Name != "h:depth") continue;
                            depth = hNode.InnerText;
                            break;
                        }


                        csvWriter.WriteField(lat);
                        csvWriter.WriteField(lon);
                        csvWriter.WriteField(depth);
                        csvWriter.NextRecord();
                    }
                }
                File.WriteAllText(csvFilePath, writer.ToString());
            }
        }
    }
}
