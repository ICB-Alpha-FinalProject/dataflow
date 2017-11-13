﻿
using Dataflow.DataServices.Models;
using DataflowICB.Database.Models;
using DataflowICB.Models.DataApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dataflow.DataServices.Contracts
{
    public interface ISensorService
    {
        void AddSensor(Sensor sensor);
        void EditSensor(SensorDataModel editedSensor);
        void ShareSensorWithUser(int id, string username);
        SensorDataModel GetUsersSharedSensor(int id);
        IEnumerable<SensorDataModel> GetAllSensors(bool IsAdmin);
        IEnumerable<SensorDataModel> GetAllPublicSensors();
        Task UpdateSensors();
        SensorDataModel GetSensorById(int id);
        SensorDataModel GetUserSensorById(int id);
        IEnumerable<SensorDataModel> GetAllSensorsForUser(string name);
        IEnumerable<SensorApiUpdate> HistoryDataForBoolSensorsById(int sensorId);
        IEnumerable<SensorApiUpdate> HistoryDataForValueSensorsById(int sensorId);
    }
}
