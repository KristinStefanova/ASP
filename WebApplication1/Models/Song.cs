using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebApplication1.Models
{
    [Serializable, XmlRoot("song")]
    public class Song
    {
        [XmlAttribute(AttributeName = "song_id")]
        [Key]
        public int songID { get; set; }

        [XmlElement(ElementName = "title")]
        public String title { get; set; }

        [XmlElement(ElementName = "releaseYear")]
        public int year { get; set; }

        [XmlElement(ElementName = "genre")]
        public String genre { get; set; }

        [XmlElement(ElementName = "artist")]
        public virtual Artist artist { get; set; }

        [XmlElement(ElementName = "duration")]
        public int duration { get; set; }

        [XmlElement(ElementName = "lyrics")]
        public String lyrics { get; set; }

        [XmlElement(ElementName = "music")]
        public String music { get; set; }

        [XmlElement(ElementName = "album")]
        public virtual Album album { get; set; }

        [XmlElement(ElementName = "producer")]
        public virtual Producer producer { get; set; }

        [XmlElement(ElementName = "video")]
        public virtual Video video { get; set; }

    }
}