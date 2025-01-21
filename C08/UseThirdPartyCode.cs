namespace CleanCodeProject.C08;

public class UseThirdPartyCode
{
    public void UseDictionary()
    {
        var sensors = new Sensors();
        sensors.Add("1", new Sensor { Id = "1", Name = "Sensor 1" });
        var sensor = sensors.GetById("1");
        Console.WriteLine(sensor.Name);
    }

    // 封裝 Map 的使用範例
    public class Sensors
    {
        private readonly Dictionary<string, Sensor> sensors = new Dictionary<string, Sensor>();

        public Sensor GetById(string id)
        {
            return sensors.TryGetValue(id, out var sensor) ? sensor : new Sensor();
        }

        public void Add(string id, Sensor sensor)
        {
            sensors[id] = sensor;
        }

        public void Remove(string id)
        {
            sensors.Remove(id);
        }

        // 只暴露必要的操作
        public int Count => sensors.Count;

        public bool Contains(string id)
        {
            return sensors.ContainsKey(id);
        }
    }

    public class Sensor
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
    }
}