using AutoMapper;
using Dbmodel;
using Entities;

namespace CuelogicResourceManagement
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Employee, EmployeeModel>();
                config.CreateMap<EmployeeModel, Employee>();
                config.CreateMap<RegistrationModel, Registration>();
                config.CreateMap<Registration, RegistrationModel>();
            });
            return mappingConfig;
        }
    }
}
