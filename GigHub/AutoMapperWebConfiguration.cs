using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using GigHub.Dtos;
using GigHub.Models;

namespace GigHub
{
    public class AutoMapperWebConfiguration
    {
        static readonly AutoMapperWebConfiguration _instance = new AutoMapperWebConfiguration();

        public static AutoMapperWebConfiguration Instance
        {
            get
            {
                return _instance;
            }
        }

        MapperConfiguration _config;

        AutoMapperWebConfiguration()
        {
            // Initialize.
            _config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ApplicationUser, UserDto>();
                cfg.CreateMap<Gig, GigDto>();
                cfg.CreateMap<Notification, NotificationDto>();
            });
        }

        public IMapper CreateMapper()
        {
            return _config.CreateMapper();
        }

        public void LinqExtension()
        {
            //QueryableExtensions.Factories.Configuration = () => config;
        }
    }
}