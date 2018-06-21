using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebApplication1.Models
{
    [Serializable, XmlRoot("artist")]
    public class Artist
    {
        [XmlAttribute(AttributeName = "artist_id")]
        [Key]
        public int artistID { get; set; }

        [XmlElement(ElementName = "artistName")]
        public String name { get; set; }

        [XmlElement(ElementName = "artistStageName")]
        public String stage_name { get; set; }

        [XmlAttribute(AttributeName = "age")]
        public int age { get; set; }

        [XmlElement(ElementName = "gender")]
        public String gender { get; set; }

        [XmlElement(ElementName = "birthdate")]
        public String birthdate { get; set; }

        [XmlElement(ElementName = "country")]
        public String country { get; set; }

        [XmlElement(ElementName = "image")]
        public String image { get; set; }
    }
}