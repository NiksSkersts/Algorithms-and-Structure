using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RESTAPI.Model
{
    public class Storage : DbContext
    {
        public Storage()
        {
            Persons = new List<Person>
            {
                //populate the list so it doesn't feel so empty.
                new("a", "a", 0, "a", 0, "a",
                    new Flat(0, 0, 0, 0, 0, 0,
                        new House(0, "a", "a", "a", "a"))),
                new("a", "a", 1, "a", 0, "a",
                    new Flat(0, 0, 0, 0, 0, 0,
                        new House(0, "a", "a", "a", "a"))),
                new("a", "a", 2, "a", 0, "a",
                    new Flat(0, 0, 0, 0, 0, 0,
                        new House(0, "a", "a", "a", "a"))),
                new("a", "a", 3, "a", 0, "a",
                    new Flat(0, 0, 0, 0, 0, 0,
                        new House(0, "a", "a", "a", "a"))),
                new("a", "a", 4, "a", 0, "a",
                    new Flat(0, 0, 0, 0, 0, 0,
                        new House(0, "a", "a", "a", "a")))
            };
        }
        public List<Person> Persons { get; set; }
    }
}