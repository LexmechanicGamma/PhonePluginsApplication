using System.Collections.Generic;
using System.Linq;
using PhoneApp.Domain.DTO;
using PhoneApp.Domain.Interfaces;
using System.Net;
using PhoneApp.Domain.Attributes;

namespace EmployeeRestLoader
{
    [Author(Name = "IHopeThisIs AllIntentional")]
    public class Plugin : IPluggable
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public IEnumerable<DataTransferObject> Run(IEnumerable<DataTransferObject> args)
        {
            logger.Info("Loading users");
            var usersList = args.Cast<EmployeesDTO>().ToList();

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                                    | SecurityProtocolType.Tls11
                                    | SecurityProtocolType.Tls
                                    | SecurityProtocolType.Ssl3;

            var json = new WebClient().DownloadString("https://dummyjson.com/users");

            var parsedJson = Newtonsoft.Json.JsonConvert.DeserializeObject<Users>(json);
            


            foreach(UserDTO user in parsedJson.users)
            {
                logger.Info(user);
                EmployeesDTO employee = new EmployeesDTO();
                employee.Name = user.FirstName + " " + user.LastName;
                employee.AddPhone(user.PhoneNumber);
                usersList.Add(employee);
            }
            
            logger.Info($"Loaded {usersList.Count()} employees");
            return usersList.Cast<DataTransferObject>();
        }
    }
}
