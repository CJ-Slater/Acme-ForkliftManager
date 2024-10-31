using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        public User(string firstName, string lastName)
        {
            if (String.IsNullOrWhiteSpace(firstName))
                throw new ArgumentNullException("A first name must be provided.");

            if (String.IsNullOrWhiteSpace(lastName))
                throw new ArgumentNullException("A last name must be provided.");


            FirstName = firstName;
            LastName = lastName;
        }
        [Key]
        public int Id { get; set; }
        public int AccountId { get; private init; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
    }
}
