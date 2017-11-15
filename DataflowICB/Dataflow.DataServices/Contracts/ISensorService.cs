
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
        void EditSensor(ISensorDataModel editedSensor);
        void ShareSensorWithUser(int id, string username);
        void DeleteSensor(int id);
        SensorDataModel GetUsersSharedSensor(int id);
        IEnumerable<SensorDataModel> GetAllSensors(bool IsAdmin);
        IEnumerable<SensorDataModel> GetAllPublicSensors();
        Task UpdateSensors();
        SensorDataModel GetAdminSensorById(int id);
        SensorDataModel GetUserSensorById(int id);
        IEnumerable<SensorDataModel> GetSharedWithUserSensors(string username);
        IEnumerable<SensorDataModel> GetAllSensorsForUser(string name);
        IEnumerable<SensorApiUpdate> HistoryDataForBoolSensorsById(int sensorId);
        IEnumerable<SensorApiUpdate> HistoryDataForValueSensorsById(int sensorId);
        void SendEmailIfNeeded(Sensor sensor);
        bool ValueHasAlreadyBeenChangedForThisInvalidValue { get; }
    }
}
