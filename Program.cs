using System;
using System.Collections.Generic;

namespace Assignment1
{
    public class Person : IComparable
    {
        //private variable declarations
        private readonly uint id;
        private string lastName;
        private string firstName;
        private string occupation;
        private readonly DateTime birthday;
        private List<uint> residenceIds;
        private string fullName;

        //public get and set methods to access the private variables
        public uint ID
        {
            get { return id; }
            set { }
        }
        public string lastname
        {
            get { return lastName; }
            set { lastName = value; }
        }
        public string firstname
        {
            get { return firstName; }
            set { firstName = value; }
        }
        public string Occupation
        {
            get { return occupation; }
            set { occupation = value; }
        }
        public DateTime Birthday
        {
            get { return DateTime.Now; }
            set { }
        }
        public uint[] ResidenceIDs
        { get { return residenceIds.ToArray(); }
            set { }
        }

        public string FullName
        { get { return lastName + ", " + firstName; } }

        //Constructors
        public Person()
        {
            id = 0;
            lastName = "";
            firstName = "";
            occupation = "";
            birthday = DateTime.Now;
        }
        public Person(uint newId, string newFirstName, string newLastName, string newOccupation, DateTime newBirthday)
            { 
              id = newId;
              lastName = newLastName;
              firstName = newFirstName;
              lastName = newLastName;
              occupation = newOccupation;
              birthday = newBirthday;
            }
        public int CompareTo(object alpha)
        {
            if (alpha == null)
            { throw new ArgumentNullException(); }// Always... check for null values

            Person rightOperand = alpha as Person; // Oo, typecasting using English! I like it

            if (rightOperand != null) // Protect against a failed typecasting
            { return id.CompareTo(rightOperand.id); }
            // Making use of what's already available!
            else
            { throw new ArgumentException("[Student]:CompareTo argument is not a Student"); }
        }



            public override string ToString()
    {
        return String.Format("Resident ID: {0}\nName: {1}\nOccupation: {2}\nBirthday: {3}\n",
                             id, FullName, occupation, birthday.ToString());
    }

        static void Main(string[] args)
        { Person me = new Person();
            Console.WriteLine(me.ToString());

            DateTime yourBirthday = new DateTime();

            Person you = new Person(1,"Emily", "Ducatte", "Teacher", yourBirthday);
            Console.WriteLine(you.ToString());
        }
}
    }
