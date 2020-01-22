using System;
using System.Collections.Generic;

namespace Assignment1 {
    public class Person implements IComparable
    {
        readonly uint id;
        string lastName;
        string firstName;
        string occupation;
        readonly DateTime birthday;
        List<int> residenceIds;

        public Person()
        {
            id = 0;
            lastName = "";
            firstName = "";
            occupation = "";
            birthday = DateTime.Now;
        }

    public interface CompareTo(object alpha) {
        }
    }
