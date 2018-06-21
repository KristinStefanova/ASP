using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebApplication1.Models
{
    [Serializable, XmlRoot("producer")]
    public class Producer
    {
        [XmlAttribute(AttributeName = "producer_id")]
        [Key]
        public int producerID { get; set; }

        [XmlElement(ElementName = "producerCompany")]
        public String name { get; set; }

        [XmlElement(ElementName = "address")]
        public String address { get; set; }

        [XmlElement(ElementName = "phoneNumber")]
        public String phone { get; set; }
    }
}