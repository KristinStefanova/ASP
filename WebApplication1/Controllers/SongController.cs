using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class SongController : Controller
    {
        private ApplicationDbContext _context;

        public SongController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ViewResult Index()
        {
            var songs = _context.Songs.ToList();
            return View(songs);
        }

        public ActionResult Details(int id)
        {
            var song = _context.Songs.SingleOrDefault(c => c.songID == id);

            if (song == null)
                return HttpNotFound();

            return View(song);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Song newSong)
        {

            if (ModelState.IsValid)
            {
                _context.Songs.Add(newSong);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View(newSong);
            }
        }

        public static Song XmlToObject(Type type, string xml)
        {
            Song oOut = null;

            if (xml != null && xml.Length > 0)
            {
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(type);

                using (System.IO.StringReader sReader = new System.IO.StringReader(xml))
                {
                    oOut = (Song)serializer.Deserialize(sReader);

                    sReader.Close();
                }
            }

            return oOut;
        }
        public static string ObjectToXML(Type type, object obby)
        {
            XmlSerializer ser = new XmlSerializer(type);
            using (System.IO.MemoryStream stm = new System.IO.MemoryStream())
            {
                ser.Serialize(stm, obby);
                stm.Position = 0;
                using (System.IO.StreamReader stmReader = new System.IO.StreamReader(stm))
                {
                    string xmlData = stmReader.ReadToEnd();
                    return xmlData;
                }
            }

        }

        public bool Validate(string inputUri)
        {
           /* XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            settings.ValidationType = ValidationType.DTD;
            */
            XmlSchemaSet sc = new XmlSchemaSet();

            // Add the schema to the collection.
            sc.Add("urn:song-schema", "song_schema.xsd");

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas = sc;
            //settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);

            try
            {
                XmlReader reader = XmlReader.Create(inputUri, settings);
                while (reader.Read()) { }
                reader.Close();
            }
            catch (XmlSchemaException e)
            {
                Redirect("Error");
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }
            catch (FileNotFoundException e)
            {
                return false;
            }
            catch (UriFormatException e)
            {
                return false;
            }
            catch (XmlException e)
            {
                return false;
            }

            return true;
        }
        
        //WRITE AND READ FROM XML
        //READ AND WRITE TO XML
        [Route("WriteXml")]
        public ContentResult WriteXml()
        {

            /*
                Get all database entries and convert them into xml files using the XDocument, X  
            */

            for (int i = 0; i < _context.Songs.ToList().Count; i++)
            {
                new XDocument(
                   new XElement("song",
                        new XAttribute("song_id", _context.Songs.ToList()[i].songID.ToString()),
                        new XElement("title", _context.Songs.ToList()[i].title),
                        new XElement("releaseYear", _context.Songs.ToList()[i].year.ToString()),
                        new XElement("genre", _context.Songs.ToList()[i].genre),
            // artist
                        new XElement("artist", 
                            new XAttribute("artist_id", _context.Songs.ToList()[i].artist.artistID.ToString()),
                            new XAttribute("age", _context.Songs.ToList()[i].artist.age.ToString()),
                            new XElement("artistStageName", _context.Songs.ToList()[i].artist.stage_name),
                            new XElement("artistName", _context.Songs.ToList()[i].artist.name),
                            new XElement("gender", _context.Songs.ToList()[i].artist.gender),
                            new XElement("birthdate", _context.Songs.ToList()[i].artist.birthdate),
                            new XElement("country", _context.Songs.ToList()[i].artist.country),
                            new XElement("image", _context.Songs.ToList()[i].artist.image)
                        ),
                        new XElement("duration", _context.Songs.ToList()[i].duration.ToString()),
            // album
                        new XElement("album",
                            new XAttribute("album_id", _context.Songs.ToList()[i].album.albumID.ToString()),
                            new XAttribute("releaseYear", _context.Songs.ToList()[i].album.year.ToString()),
                            new XAttribute("awards", _context.Songs.ToList()[i].album.awards),
                            new XElement("albumName", _context.Songs.ToList()[i].album.name),
                            new XElement("albumGenre", _context.Songs.ToList()[i].album.genre),
                            new XElement("numberOfTracks", _context.Songs.ToList()[i].album.tracks.ToString()),
                            new XElement("totalDuration", _context.Songs.ToList()[i].album.duration.ToString())
                        ),
            // producer
                        new XElement("producer",
                            new XAttribute("producer_id", _context.Songs.ToList()[i].producer.producerID.ToString()),
                            new XElement("producerCompany", _context.Songs.ToList()[i].producer.name),
                            new XElement("address", _context.Songs.ToList()[i].producer.address),
                            new XElement("phoneNumber", _context.Songs.ToList()[i].producer.phone)
                        ),
                        new XElement("lyrics", _context.Songs.ToList()[i].lyrics),
                        new XElement("music", _context.Songs.ToList()[i].music),
            // video
                        new XElement("video",
                            new XAttribute("video_id", _context.Songs.ToList()[i].video.videoID.ToString()),
                            new XElement("youTubeLink", _context.Songs.ToList()[i].video.link),
                            new XElement("publishedOn", _context.Songs.ToList()[i].video.date),
                            new XElement("numberOfViews", _context.Songs.ToList()[i].video.views.ToString()),
                            new XElement("numberOfLikes", _context.Songs.ToList()[i].video.likes.ToString()),
                            new XElement("numberOfDislikes", _context.Songs.ToList()[i].video.dislikes.ToString())
                        )
                     )

                )
                .Save("C:\\Users\\Kristin Stefanova\\source\\repos\\WebApplication1\\WebApplication1\\XML\\written\\" + i + ".xml");
            }

            return Content("Data from database writen to xml");
        }

        [Route("ReadXml")]
        public ContentResult ReadXml()
        {
            StringBuilder buildString = new StringBuilder();
            for (int i = 0; i < 21; i++)
            {
                XmlDocument doc = new XmlDocument();
                doc.XmlResolver = new XmlUrlResolver();
                doc.Load("C:\\Users\\Kristin Stefanova\\source\\repos\\WebApplication1\\WebApplication1\\XML\\readed\\song_" + i + ".xml");
                if (Validate("C:\\Users\\Kristin Stefanova\\source\\repos\\WebApplication1\\WebApplication1\\XML\\readed\\" + "song_" + i + ".xml"))
                {
                    Song description = XmlToObject(typeof(Song), doc.InnerXml);
                    buildString.Append(ObjectToXML(typeof(Song), description) + "\n");

                    _context.Songs.Add(description);
                    _context.SaveChanges();
                }

                else
                {
                    buildString.Append((i).ToString() + ", ");
                }
            }

            return Content(buildString.ToString());
        }

    }
}
