using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace YachtClub.Model
{
    class MemberRegistry : Member
    {
        private List<Member> _memberList;

        public MemberRegistry()
        {
            _memberList = new List<Member>();
            Load();
        }

        public void Load()
        {
            TextReader reader = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Member>));
                reader = new StreamReader("SystemRegistry.xml");
                _memberList = (List<Member>)serializer.Deserialize(reader);
            }
            catch (Exception) { }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        public void Save()
        {
            TextWriter writer = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Member>));
                writer = new StreamWriter("SystemRegistry.xml", false);
                serializer.Serialize(writer, _memberList);
            }
            catch (Exception) { }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        public void AddMember(Member member) { _memberList.Add(member); }

        public void DeleteMember(Member member) { _memberList.Remove(member); }

        public Member GetMember(int memberId)
        {
            Member memb;
            for (int i = 0; i < _memberList.Count(); i++)
            {
                memb = _memberList[i];
                if (memb.ID == memberId)
                {
                    return memb;
                }
            }

            throw new ArgumentException($"There exists no member with id = {memberId}");
        }

        public int GetNextMemberId()
        {
            int max = 0;

            foreach (Member memb in _memberList)
            {
                if (memb.ID > max)
                    max = memb.ID;
            }
            return max + 1;
        }

        public int GetNextBoatIdFor(Member memb)
        {
            if (memb == null)
                throw new ArgumentException("Member can not be empty!");

            int max = 0;
            foreach (Boat boat in memb.GetBoats())
            {
                if (boat.ID > max)
                    max = boat.ID;
            }
            return max + 1;
        }

        public IEnumerable<Member> GetMemberList() { return _memberList.AsEnumerable(); }
    }
}
