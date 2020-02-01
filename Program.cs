using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp1

{
    public class Person : IComparable
    {
        //private variable declarations
        public uint id;
        private string lastName;
        private string firstName;
        private string occupation;
        public DateTime birthday;
        private List<uint> residenceIds = new List<uint>();
        private string fullName;

        private string[] personInput;
        private int personIndex;
        protected string stringParser() => personInput[personIndex];
        protected bool boolParser() => (stringParser() == "T");
        protected uint uintParser() => uint.Parse(stringParser());
        protected uint binaryParser() => Convert.ToUInt32(stringParser(), 2);
        protected int intParser() => int.Parse(stringParser());

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

        public int Age
        {
            get
            {
                TimeSpan yearsAlive = DateTime.Now - birthday;
                return (int)(yearsAlive.TotalDays / 365.2422);
            }
        }

        //Constructors
        public Person()
        {
            id = 0;
            lastName = "";
            firstName = "";
            occupation = "";
            birthday = DateTime.Now;
        }
        public Person(uint newId, string newFirstName, string newLastName, string newOccupation, DateTime newBirthday, List<uint> newResidenceID)
        {
            id = newId;
            lastName = newLastName;
            firstName = newFirstName;
            lastName = newLastName;
            occupation = newOccupation;
            birthday = newBirthday;
            residenceIds = newResidenceID;
        }

       
        public Person(string[] input)
        {
           // id = uintParser();
           // lastName = stringParser();
           // firstName = stringParser();
           // occupation = stringParser();
           // int year = intParser();
           // int month = intParser();
           // int day = intParser();
           // uint residenceId = uintParser();

           // birthday = new DateTime(year, month, day);
            //residenceIds.Add(residenceId);
           id = Convert.ToUInt32(input[0], 2);
           lastName = input[1];
           firstName = input[2];
           occupation = input[3];
           int year = int.Parse(input[4]);
           int month = int.Parse(input[5]);
           int day = int.Parse(input[6]);
           //birthday = new DateTime(year, month, day);
           uint residenceId = uint.Parse(input[7]);

            birthday = new DateTime(year, month, day);
            residenceIds.Add(residenceId);
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
            return $"{FullName}, Age: ({Age}), Occupation: {Occupation}";
        }

        class Program
        {
            private static Community community;
            private const string dashes = "----------------------------------------------------------------------------------";
            public static List<Property> PropertyList = new List<Property>();
            public static List<Property> HouseList = new List<Property>();
            public static List<Property> ApartmentList = new List<Property>();
            private static void fileReader(string nameOfFile, Action<string[]> lineProcessor)
            {
                using (FileStream myFile = File.OpenRead("../../" + nameOfFile))
                using (StreamReader streamReader = new StreamReader(myFile))
                {
                    string inputLine;

                    while ((inputLine = streamReader.ReadLine()) != null)
                    {
                        if (inputLine.Length > 0)
                        {
                            string[] splicing = inputLine.Split('\t');
                            lineProcessor(splicing);
                        }
                    }
                }
            }
            private static void Main(string[] args)
            {
                community = new Community(99999, "DeKalb");

                fileReader("p.txt", (inputFile) => { Person personFromInput = new Person(inputFile); community.AddPerson(personFromInput); });
                fileReader("a.txt", (inputFile) => { Apartment apartmentFromInput = new Apartment(inputFile); PropertyList.Add(apartmentFromInput); ApartmentList.Add(apartmentFromInput); });
                fileReader("r.txt", (inputFile) => { House houseFromInput = new House(inputFile); PropertyList.Add(houseFromInput); HouseList.Add(houseFromInput); });

                Console.WriteLine(" 1. Full property list");
                Console.WriteLine(" 2. List addresses of either House or Apartment-type properties");
                Console.WriteLine(" 3. List addresses of all for-sale properties");
                Console.WriteLine(" 4. List all residents of a community");
                Console.WriteLine(" 5. List all residents of a property, by street address");
                Console.WriteLine(" 6. Toggle a property, by street address, as being for sale or not");
                Console.WriteLine(" 7. Buy a for-sale property, by street address");
                Console.WriteLine(" 8. Add yourself as an occupant to a property");
                Console.WriteLine(" 9. Remove yourself as an occupant from a property");
                Console.WriteLine(" 10. Quit\n");

                string input;
                Console.WriteLine("Enter your choice: ");
                input = Console.ReadLine();
                int a = Convert.ToInt32(input);
                int caseSwitch = a;
                Console.WriteLine("You entered: " + a);

                while (a != 10)
                {
                    switch (caseSwitch)
                    {
                        case 1:
                            Console.WriteLine(dashes);
                            PropertyList.ForEach(PropertyList => PropertyList.PropertyInfo(community));      
                            Console.WriteLine();
                            break;
                        case 2:
                            Console.WriteLine("Enter property type (House/Apartment): ");

                            string caseTwoUserInput = Console.ReadLine();
                            Type typeOfVar = null;

                            if (caseTwoUserInput == "House")
                            {
                                typeOfVar = typeof(House);
                            }

                            else if (caseTwoUserInput == "Apartment")
                            {
                                typeOfVar = typeof(Apartment);
                            }

                            if (typeOfVar != null)
                            {
                                Console.WriteLine($"List of addressses of {caseTwoUserInput} properties in the {community.name} community. ");
                                Console.WriteLine(dashes);
                                if (typeOfVar == typeof(House))
                                { foreach (House h in HouseList)
                                    {
                                        Console.WriteLine(h.ToString());
                                    }
                                }
                                else if (typeOfVar == typeof(Apartment))
                                {
                                    foreach (Apartment appy in ApartmentList)
                                    {
                                        Console.WriteLine(appy.ToString());
                                    }
                                }
                                //var addressListing = community.Properties.Where(PropertyList => PropertyList.GetType() == typeOfVar).Select(PropertyList => PropertyList.ToString()).ToList();
                                //addressListing.ForEach(address => Console.WriteLine(address));
                                
                            }
                            break;
                        case 3:
                            Console.WriteLine("Case 3");
                            break;
                        case 4:
                            Console.WriteLine(dashes);
                            foreach (var person in community)
                            { Console.WriteLine(person.ToString()); }
                            Console.WriteLine();
                            break;
                        case 5:
                            Console.WriteLine("Case 5");
                            break;
                        case 6:
                            Console.WriteLine("Case 6");
                            break;
                        case 7:
                            Console.WriteLine("Case 7");
                            break;
                        case 8:
                            Console.WriteLine("Case 8");
                            break;
                        case 9:
                            Console.WriteLine("Case 9");
                            break;
                        case 10:
                            Console.WriteLine("Case 10");
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                    Console.WriteLine("Enter your choice: ");
                    input = Console.ReadLine();
                    a = Convert.ToInt32(input);
                    caseSwitch = a;
                    Console.WriteLine("You entered: " + a);
                }
            }
        }
    }
    public abstract class Property : IComparable
    {

        public uint id;
        public uint ownerID;
        public uint x;
        public uint y;
        public string streetAddr;
        public string city;
        public string state;
        public string zip;
        public bool forSale;
        private string[] propertiesInput;
        private int index;
 
        public string forSaleOrNot => (forSale ? "FOR SALE" : "NOT for sale");

        protected abstract void residenceInfo();
        protected string stringParser() => propertiesInput[index++];
        protected bool boolParser() => (stringParser() == "T");
        protected uint uintParser() => uint.Parse(stringParser());
        protected uint binaryParser() => Convert.ToUInt32(stringParser(), 2);
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

        public Property(string[] input)
        {
            propertiesInput = input;

            id = uintParser();
            ownerID = binaryParser();
            x = uintParser();
            y = uintParser();
            streetAddr = stringParser();
            city = stringParser();
            state = stringParser();
            zip = stringParser();
            forSale = boolParser();

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
                        string compName = streetAddr.Substring(streetAddr.IndexOf(" ") + 1);
                        string compNum = streetAddr.Substring(0, streetAddr.IndexOf(" "));
                        //return streetAddr.CompareTo(rightOperand.streetAddr);
                    }
                    return city.CompareTo(rightOperand.city);

                }
                return state.CompareTo(rightOperand.state);
            }
            else
                throw new ArgumentException("[Property]:CompareTo argument is not a Property");

        }

        public override string ToString()
        { return $"{streetAddr}, {city}, {state}, {zip}"; }

        public void PropertyInfo(Community c)
        {
            Console.WriteLine($"Property Address: {streetAddr} / {city} / {state} / {zip}");
            Person owner = c.Residents.Where(person => person.id == ownerID).FirstOrDefault();
            Console.WriteLine($"\tOwned by {owner}"); 
            Console.Write($"\t({forSaleOrNot}) ");
            residenceInfo();
        }


    }
    public class Residential : Property
    {
        public uint bedrooms;
        public uint baths;
        public uint sqft;

        public Residential() : base()
        {
            bedrooms = 0;
            baths = 0;
            sqft = 0;
        }

        public Residential(string[] fileInput) : base(fileInput)
        {
            bedrooms = uintParser();
            baths = uintParser();
            sqft = uintParser();
        }
        public uint Bedrooms
        { get { return bedrooms; }
          set { Bedrooms = value; }
        }
        public uint Baths
        {
            get { return baths; }
            set { Baths = value; }
        }
        public uint SqFootage
        {
            get { return sqft; }
            set { SqFootage = value; }
        }

        protected override void residenceInfo()
        {
            Console.Write($"{bedrooms} bedrooms \\ {baths} baths \\ {sqft} sq.ft.");
        }
    }

    public class House : Residential
    {
        private bool garage;
        private bool? attachedGarage;
        private uint floors;

        public bool Garage
        {
            get { return garage; }
            set { Garage = value; }
        }

        public bool? AttachedGarage
        {
            get { return attachedGarage; }
            set { AttachedGarage = value; }
        }

        public uint Floors
        {
            get { return floors; }
            set { Floors = value; }
        }

        public House() : base()
        {
            floors = 0;
            garage = false;
            attachedGarage = null;

        }
        protected override void residenceInfo()
        {
            base.residenceInfo();

            string garageOrNot = "no garage";

            if (garage && attachedGarage.Value)
            {
                garageOrNot = "an attached garage";
            }

            else if (garage)
            {
                garageOrNot = "a detached garage";
            }

            string flooring = "floor";

            if (floors > 1)
            {
                flooring += 's';
            }

            Console.WriteLine($"\n\t ...with {garageOrNot} : {floors} {flooring}.");
        }

        public House(string[] input) : base(input)
        {
            //id = Convert.ToUInt32(input[0]);
            //ownerID = Convert.ToUInt32(input[1]);
            //x = Convert.ToUInt32(input[2]);
            // y = Convert.ToUInt32(input[3]);
            //streetAddr = input[4];
            //city = input[5];
            //state = input[6];
            //zip = input[7];
            //forSale = BooleanParser.GetValue(input[8]);
            //bedrooms = Convert.ToUInt32(input[9]);
            //baths = Convert.ToUInt32(input[10]);
            //sqft = Convert.ToUInt32(input[11]);
            garage = boolParser();
            attachedGarage = boolParser();
            floors = uintParser();

            if(!garage)
            {
                attachedGarage = null;
            }
        }
    }

    public static class BooleanParser
    {
        public static bool GetValue(string value)
        {
            return IsTrue(value);
        }

        public static bool IsFalse(string value)
        {
            return !IsTrue(value);
        }

        public static bool IsTrue(string value)
        {
            try
            {
                if (value == null)
                { return false; }
                
                if (value == "T")
                {
                    return true;
                }

                if (value == "F")
                {
                    return false;
                }

                return false;
            }

            catch { return false; }
            
        }
    }
    public class Apartment : Residential
    {
        private string unit;

        public string Unit
        { get { return unit; }
          set { Unit = value; }
        }

        public Apartment() : base()
        {
            unit = "";
        }
        public Apartment(string[] input) : base(input)
        {
            unit = stringParser();
        }

        protected override void residenceInfo()
        {
            base.residenceInfo();
            Console.WriteLine($" Apt.# {Unit}");
        }
    }

    public class Community : IComparable, IEnumerable<Person>
    {
        private SortedSet<Property> props = new SortedSet<Property>();
        private SortedSet<Person> residents = new SortedSet<Person>();
        private readonly uint id;
        public string name;
        private uint mayorId;
        public int Population
        { get { return residents.Count; } }



        public List<Property> Properties => props.ToList();
        public List<Person> Residents => residents.ToList();

        public Community()
        {
            id = 0;
            name = "";
            mayorId = 0;
        }

        public Community(SortedSet<Property> properties, SortedSet<Person> people, uint ID, string communityName, uint MayorID)
        {
            props = properties;
            residents = people;
            ID = 99999;
            MayorID = 0;
            communityName = "DeKalb";
        }

        public Community(uint id, string name, uint mayorId = 0)
        {
            this.id = id;
            this.name = name;
            this.mayorId = mayorId; 
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

        IEnumerator<Person> IEnumerable<Person>.GetEnumerator()
        { return residents.GetEnumerator(); }

        public IEnumerator<Person> GetEnumerator()
        { return residents.GetEnumerator(); }

        IEnumerator IEnumerable.GetEnumerator() { return this.GetEnumerator(); }

        public bool AddProperty(Property property)
        {
            return props.Add(property);
        }

        public bool AddPerson(Person person)
        {
            return residents.Add(person);
        }
    }
}
