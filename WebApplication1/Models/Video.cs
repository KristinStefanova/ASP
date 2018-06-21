using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebApplication1.Models
{
    [Serializable, XmlRoot("video")]
    public class Video
    {
        [XmlAttribute(AttributeName = "video_id")]
        [Key]
        public int videoID { get; set; }

        [XmlElement(ElementName = "youTubeLink")]
        public string link { get; set; }

        [XmlElement(ElementName = "publishedOn")]
        public string date { get; set; }

        [XmlElement(ElementName = "numberOfViews")]
        public int views { get; set; }

        [XmlElement(ElementName = "numberOfLikes")]
        public int likes { get; set; }

        [XmlElement(ElementName = "numberOfDislikes")]
        public int dislikes { get; set; }

    }
}