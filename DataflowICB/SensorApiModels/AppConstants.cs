using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SensorApiModels
{
    public static class AppConstants
    {
        public const int MaxPollingInterval = 86400;
        public const string AllSensorsUrl = "http://telerikacademy.icb.bg/api/sensor/all";
    }
}