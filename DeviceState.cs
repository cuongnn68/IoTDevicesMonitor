using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace IoTDevicesMonitor {
    public class DeviceState { // TODO asyn change?? how 
        public Dictionary<string, string> StringStates { get; private set; }
        public Dictionary<string, bool> BoolStates { get; private set; }

        public DeviceState() {
            var stringState = new[] {"thong so 1", "thong so 2", "thong so 3", "thong so 4"};
            var boolState = new[] {"trang thai 1", "trang thai 2", "trang thai 3"};
            StringStates = new Dictionary<string, string>();
            BoolStates = new Dictionary<string, bool>();
            foreach(var state in stringState) {
                StringStates.Add(state, "100");
            }
            foreach(var state in boolState) {
                BoolStates.Add(state, false);
            }
        }
    }
}