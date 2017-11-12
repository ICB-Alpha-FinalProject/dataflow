
using Dataflow.DataServices.Models;
using DataflowICB.Database.Models;
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
        SensorDataModel GetSensorById(int Id);
        void EditSensor(SensorDataModel editedSensor);
    }
}
