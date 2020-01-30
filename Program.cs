using System;
using System.Collections.Generic;

namespace ConsoleApp1

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
        {
            get { return residenceIds.ToArray(); }
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
            { return FullName.CompareTo(rightOperand.FullName); }
            // Making use of what's already available!
            else
            { throw new ArgumentException("[Person]:CompareTo argument is not a Person"); }
        }



        public override string ToString()
        {
            return String.Format("Resident ID: {0}\nName: {1}\nOccupation: {2}\nBirthday: {3}\n",
                                 id, FullName, occupation, birthday.ToString());
        }

        static void Main(string[] args)
        {
            Person me = new Person();
            Console.WriteLine(me.ToString());

            DateTime yourBirthday = new DateTime();

            Person you = new Person(1, "Emily", "Ducatte", "Teacher", yourBirthday);
            Console.WriteLine(you.ToString());
            Console.WriteLine(me.ToString());

            DateTime yourbirthday = new DateTime();
            Person Heather = new Person(4, "Ritchey", "Heather", "child", yourbirthday);
            Person Poppie = new Person(1, "Ritchey", "Poppie", "cat", yourbirthday);

            Console.WriteLine(Heather.ToString());


            int comp = Poppie.CompareTo(Heather);
            Console.WriteLine("comp:" + comp);


            Property Mine = new Property();
            Property Other = new Property(8, 0, 7, 8, "1234 MyStreet", "Sycamore", "IL", "60178", false);
            Property Another = new Property(8, 0, 7, 8, "22 MyStreet", "Sycamore", "IL", "60178", false);
            int prop = Another.CompareTo(Other);
            Console.WriteLine("comp:" + prop);
        }
    }
public class Property : IComparable
{

    private readonly uint id;
    private uint ownerID;
    private readonly uint x;
    private readonly uint y;
    private string streetAddr;
    private string city;
    private string state;
    private string zip;
    private bool forSale;

    public Property()
    {
        id = 0;
        ownerID = 0;
        x = 0;
        y = 0;
        streetAddr = "";
        city = "";
        state = "";
        zip = "";
        forSale = false;
    }

    public Property(uint Id, uint OwnerID, uint X, uint Y, string StreetAddr, string City, string State, string Zip, bool ForSale)
    {
        id = Id;
        ownerID = OwnerID;
        x = X;
        y = Y;
        streetAddr = StreetAddr;
        city = City;
        state = State;
        zip = Zip;
        forSale = ForSale;
    }

    public int CompareTo(Object alpha)
    {
        if (alpha == null) throw new ArgumentNullException();
        Property rightOperand = alpha as Property;
        if (rightOperand != null)
        {
            if (state.CompareTo(rightOperand.state) == 0)
            {
                if (city.CompareTo(rightOperand.city) == 0)
                {
                    string fulladdress = rightOperand.streetAddr;
                    string streetNum = fulladdress.Substring(0, fulladdress.IndexOf(" "));
                    string nameStreet = fulladdress.Substring(fulladdress.IndexOf(" ") + 1);
                    Console.WriteLine(streetNum);
                    Console.WriteLine(nameStreet);

                    string compName = streetAddr.Substring(streetAddr.IndexOf(" ") + 1);
                    string compNum = streetAddr.Substring(0, streetAddr.IndexOf(" "));

                    Console.WriteLine("compNum " + compNum);
                    Console.WriteLine("compName " + compName);
                    //return streetAddr.CompareTo(rightOperand.streetAddr);
                }
                return city.CompareTo(rightOperand.city);

            }
            return state.CompareTo(rightOperand.state);
        }
        else
            throw new ArgumentException("[Property]:CompareTo argument is not a Property");

    }



}
    public class Residential : Property
    { 
        private uint bedrooms;
        private uint baths;
        private uint sqft;
    }

    public class House : Residential
    {
        private bool garage;
        private bool? attachedGarage;
        private uint floors;

        public void garageIsNull()
        {
            if (garage == false)
            { attachedGarage = null; }
        }    
        }

    public class Apartment : Residential
    {
        private string unit; 
    }

    public class Community : IComparable, IEnumerable<Person>
    {
        private SortedSet<Property> props;
        private SortedSet<Person> residents;
        private readonly uint id;
        private readonly string name;
        private uint mayorID;
        public int Population
        { get { return residents.Count; } }

        public Community()
        {
            id = 0;
            name = "";
            mayorID = 0;
        }

        public Community(SortedSet<Property> properties, SortedSet<Person> people, uint ID, string communityName, uint MayorID)
        {
            props = properties;
            residents = people;
            ID = 99999;
            MayorID = 0;
            communityName = "DeKalb";
          }

        public int CompareTo(object alpha)
        {
            if (alpha == null)
            { throw new ArgumentNullException(); }

            Community rightOperand = alpha as Community; //typecast our object as a Community object

            if (rightOperand != null)
            { return name.CompareTo(rightOperand.name); }

            else
            { throw new ArgumentException("[Community]:CompareTo argument is not a Community"); }

        }
      
        public CommEnum GetEnumerator()
        { return new CommEnum(residents); }
    }

    public class CommEnum : IEnumerator<Person>
    {
        public SortedSet<Person> sortedSetOfPeople;

        int enumPosition;

        public CommEnum(SortedSet<Person> setOfPeople)
        {
            sortedSetOfPeople = setOfPeople;
        }

        public bool MoveNext()
        {
            enumPosition++;
            return (enumPosition < sortedSetOfPeople.Count)
        }
    }

}
