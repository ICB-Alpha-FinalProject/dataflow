using Dataflow.DataServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace DataflowICB.Areas.Admin.Models
{
    public class AdminSensorViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string MeasurementType { get; set; }

        public string Description { get; set; }

        public string URL { get; set; }

        public int PollingInterval { get; set; }

        public double MaxValue { get; set; }

        public double MinValue { get; set; }

        public bool IsBoolType { get; set; }

        public bool IsPublic { get; set; }

        public bool IsShared { get; set; }

        public string CurrentValue { get; set; }

        public string OwnerId { get; set; }

        public string Owner { get; set; }

        public ICollection<string> SharedWithUsers { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime LastUpdate { get; set; }

        public static Expression<Func<SensorDataModel, AdminSensorViewModel>> Create
        {
            get
            {
                return s => new AdminSensorViewModel()
                {
                    Id = s.Id,
                    Name = s.Name,
                    MeasurementType = s.MeasurementType,
                    Description = s.Description,
                    URL = s.URL,
                    PollingInterval = s.PollingInterval,
                    CurrentValue = s.CurrentValue,
                    MinValue = s.MinValue,
                    MaxValue = s.MaxValue,
                    IsBoolType = s.IsBoolType,
                    IsPublic = s.IsPublic,
                    IsShared = s.IsShared,
                    OwnerId = s.OwnerId,
                    Owner = s.Owner,
                    SharedWithUsers = s.SharedWithUsers,
                    LastUpdate = s.LastUpdate,
                    IsDeleted = s.IsDeleted
                };
            }
        }

        public static ICollection<AdminSensorViewModel> Convert(IEnumerable<SensorDataModel> sensors)
        {
            ICollection<AdminSensorViewModel> usersViewModel = new List<AdminSensorViewModel>();
            foreach (var sensor in sensors)
            {
                usersViewModel.Add(new AdminSensorViewModel
                {
                    Id = sensor.Id,
                    Name = sensor.Name,
                    MeasurementType = sensor.MeasurementType,
                    Description = sensor.Description,
                    URL = sensor.URL,
                    PollingInterval = sensor.PollingInterval,
                    CurrentValue = sensor.CurrentValue,
                    MinValue = sensor.MinValue,
                    MaxValue = sensor.MaxValue,
                    IsBoolType = sensor.IsBoolType,
                    IsPublic = sensor.IsPublic,
                    IsShared = sensor.IsShared,
                    OwnerId = sensor.OwnerId,
                    Owner = sensor.Owner,
                    SharedWithUsers = sensor.SharedWithUsers,
                    LastUpdate = sensor.LastUpdate,
                    IsDeleted = sensor.IsDeleted
                });
            }

            return usersViewModel;
        }

        public static AdminSensorViewModel Convert(SensorDataModel sensor)
        {
            AdminSensorViewModel sensorViewModel = new AdminSensorViewModel()
            {
                Id = sensor.Id,
                Name = sensor.Name,
                MeasurementType = sensor.MeasurementType,
                Description = sensor.Description,
                URL = sensor.URL,
                PollingInterval = sensor.PollingInterval,
                CurrentValue = sensor.CurrentValue,
                MinValue = sensor.MinValue,
                MaxValue = sensor.MaxValue,
                IsBoolType = sensor.IsBoolType,
                IsPublic = sensor.IsPublic,
                IsShared = sensor.IsShared,
                OwnerId = sensor.OwnerId,
                Owner = sensor.Owner,
                SharedWithUsers = sensor.SharedWithUsers,
                LastUpdate = sensor.LastUpdate,
                IsDeleted = sensor.IsDeleted
            };           

            return sensorViewModel;
        }
    }
}