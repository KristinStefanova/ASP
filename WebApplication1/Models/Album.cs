using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebApplication1.Models
{
    [Serializable, XmlRoot("album")]
    public class Album
    {
        [XmlAttribute(AttributeName = "album_id")]
        [Key]
        public int albumID { get; set; }

        [XmlElement(ElementName = "albumName")]
        public String name { get; set; }

        [XmlAttribute(AttributeName = "releaseYear")]
        public int year { get; set; }

        [XmlElement(ElementName = "albumGenre")]
        public string genre { get; set; }

        [XmlElement(ElementName = "totalDuration")] 
        public int duration { get; set; }

        [XmlElement(ElementName = "numberOfTracks")]
        public int tracks { get; set; }

        [XmlAttribute(AttributeName = "awards")]
        public String awards { get; set; }
    }
}
