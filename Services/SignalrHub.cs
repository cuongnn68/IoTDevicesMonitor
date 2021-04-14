using System;
using System.Linq;
using Microsoft.AspNetCore.SignalR;

namespace IoTDevicesMonitor.Services {
    public class SignalrHub : Hub {
        private DeviceState deviceState;

        public SignalrHub (DeviceState deviceState) {
            this.deviceState = deviceState;
        }
        public string GetConnectionId() => Context.ConnectionId;

        public void Test(string mess) {
            Clients.All.SendAsync(method: "test", arg1: "Messege need to send from server " + mess);
        }

        public void GetUpdate() {
            Console.WriteLine($"Signalr: {nameof(GetUpdate)}");
            Clients.Client(Context.ConnectionId)
                .SendAsync("updateState", deviceState.BoolStates, deviceState.StringStates);
            Clients.Client(Context.ConnectionId)
                .SendAsync("test", "just got update state");
        }

        public bool ChangeStateBool(string name, bool state) {
            Console.WriteLine($"Signalr: {nameof(ChangeStateBool)}, {name}, {state}");
            var updated = false;
            if (!deviceState.BoolStates.ContainsKey(name)) return updated;
            deviceState.BoolStates[name] = state;
            foreach(var stateName in deviceState.StringStates.Keys) {
                deviceState.StringStates[stateName] = RandomString(4);
            }
            Clients.AllExcept(Context.ConnectionId) // TODO add to group
                .SendAsync("updateState", deviceState.BoolStates, deviceState.StringStates);
            Console.WriteLine("Done set state");
            return true;
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