using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace YachtClub.Model
{
    [Serializable]
    public class Member : Boat, IXmlSerializable
    {
        private int _id;
        private string _name;
        private string _personalNumber;
        private List<Boat> _boats;

        public Member() { }

        public Member(int id)
        {
            _id = id;
            _boats = new List<Boat>();
        }

        public int ID { get { return _id; } }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value.Length == 0)
                    throw new ArgumentException("Name can not be empty!");
                _name = value;
            }
        }

        public string PersonalNumber
        {
            get
            {
                return _personalNumber;
            }
            set
            {
                Match match = Regex.Match(value, @"(\d{2})?(\d{6})-?(\d{4})");

                if (match.Success)
                    _personalNumber = $"{match.Groups[2]}-{match.Groups[3]}";
                else
                    throw new ArgumentException("Personal number format is invalid!");
            }
        }

        public int GetBoatCount() { return _boats.Count; }

        public IEnumerable<Boat> GetBoats() { return _boats.AsEnumerable(); }

        public void AddBoat(Boat boat) { _boats.Add(boat); }

        public void DeleteBoat(int boatId)
        {
            foreach (Boat boat in _boats)
            {
                if (boat.ID == boatId)
                {
                    _boats.Remove(boat);
                    return;
                }
            }

            throw new ArgumentException($"There is no boat with id = {boatId}");
        }

        public Boat GetBoat(int boatId)
        {
            foreach (Boat boat in _boats)
            {
                if (boat.ID == boatId)
                    return boat;
            }
            throw new ArgumentException($"There is no boat with id = {boatId}");
        }

        public XmlSchema GetSchema() { return null; }

        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            reader.ReadStartElement();

            _id = reader.ReadElementContentAsInt("Id", reader.NamespaceURI);
            _name = reader.ReadElementContentAsString("Name", reader.NamespaceURI);
            _personalNumber = reader.ReadElementContentAsString("PersonalNumber", reader.NamespaceURI);

            XmlSerializer serializer = new XmlSerializer(typeof(List<Boat>));
            _boats = (List<Boat>)serializer.Deserialize(reader);

            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteElementString("Id", _id.ToString());
            writer.WriteElementString("Name", _name);
            writer.WriteElementString("PersonalNumber", _personalNumber);

            new XmlSerializer(typeof(List<Boat>)).Serialize(writer, this._boats);
        }
    }
}