using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SDating.Models
{
    public class SessionResult
    {
        public int ID { get; private set; }
        public DateTime DT { get; private set; }        
        public List<PersonalResult> Boys { get; set; }
        public List<PersonalResult> Girls { get; set; }
        public int ImageWidth = 350;
        public Statisticks Statisticks { get; set; }
        public SessionResult(int id,  DateTime date)
        {
            ID = id;
            DT = date;
            Boys = new List<PersonalResult>();
            Girls = new List<PersonalResult>();
            Statisticks = new Statisticks();
        }
    }

    public class PersonalResult : IComparable<PersonalResult>
    {
        public int tableNumber { get; private set; }
        public string Name { get; private set; }
        public string Picture { get; private set; }
        public int Age { get; private set; }
        public string Phone { get; private set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; private set; }

        /// <summary>
        /// Совпадения
        /// </summary>
        public List<PersonalResult> Matching { get; set; }

        public PersonalResult (int tableNumber, string Name, string Picture, int Age, string Phone, string Email)
        {
            this.Name = Name;            
            this.Picture = Picture;
            this.Age = Age;
            this.Phone = Phone;
            this.tableNumber = tableNumber;
            this.Email = Email;

            Matching = new List<PersonalResult>();
        }

        public int CompareTo(PersonalResult obj)
        {
            var currentValue = this.Matching.Count();
            var objValue = obj.Matching.Count();

            if (currentValue < objValue) return -1;
            else if (currentValue == objValue) return 0;
            else return 1;
        }
    }

    public class Statisticks
    {
        public int participantsBoys { get; set; }
        public int participantsGirls { get; set; }
        public int matchingFound { get; set; }
        public string mostPopularBoy { get; set; } 
        public string mostPopularGirl { get; set; }
        public string mostDontLuckBoy { get; set; } 
        public string mostDontLuckyGirl { get; set; }
    }

}
