using System;
using System.Linq;
using Microsoft.AspNetCore.SignalR;

namespace IoTDevicesMonitor.Services {
    public class StateManager {
        private IHubContext<SignalrHub> hubContext;
        private DeviceState deviceState;

        public StateManager(
            IHubContext<SignalrHub> hubContext,
            DeviceState deviceState
        ) {
            this.hubContext = hubContext;
            this.deviceState = deviceState;
        }




        // test
        private static Random random = new Random();
        public static string RandomString(int length) {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}