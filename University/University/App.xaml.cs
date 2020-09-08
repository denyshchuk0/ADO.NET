using Autofac;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Entity.Core.Metadata.Edm;
using University.DAL.Repository;
using AutoMapper;
using University.BLL.Services;
using University.BLL.Utils;
using University.DAL;

namespace University
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<AplicationContext>().As<DbContext>().SingleInstance();
            builder.RegisterGeneric(typeof(EFRepository<>)).As(typeof(IGenericRepository<>));
            builder.RegisterType<UniversityService>().As<IUniversityService>();
            builder.RegisterType<MainWindow>().AsSelf();

            var config = new MapperConfiguration(cgf => cgf.AddProfile(new MapperConfig()));
            builder.RegisterInstance(config.CreateMapper());

            using (var scope = builder.Build().BeginLifetimeScope())
            {
                var window = scope.Resolve<MainWindow>();
                window.ShowDialog();
            }
        }
        }
}
