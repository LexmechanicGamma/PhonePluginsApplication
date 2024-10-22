using Newtonsoft.Json;
using System.Collections.Generic;

namespace EmployeeRestLoader
{
    internal class UserDTO 
    {
        [JsonPropertyAttribute("firstName")]
        public string FirstName { get; set; }
        [JsonPropertyAttribute("lastName")]
        public string LastName { get; set; }
        [JsonPropertyAttribute("phone")]
        public string PhoneNumber { get; set; }
    }

    internal class Users
    {
        [JsonPropertyAttribute("users")]
        public List<UserDTO> users;
    }
}
