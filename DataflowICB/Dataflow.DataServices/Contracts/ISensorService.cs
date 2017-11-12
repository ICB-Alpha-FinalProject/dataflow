
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
        IEnumerable<SensorDataModel> GetAllSensors(bool IsAdmin);
        Task UpdateSensors();
        SensorDataModel GetSensorById(int id);
        void EditSensor(SensorDataModel editedSensor);
        IEnumerable<SensorDataModel> GetAllSensorsForUser(string name);
        IEnumerable<SensorApiUpdate> HistoryDataForBoolSensorsById(int sensorId);
        IEnumerable<SensorApiUpdate> HistoryDataForValueSensorsById(int sensorId);
    }
}
