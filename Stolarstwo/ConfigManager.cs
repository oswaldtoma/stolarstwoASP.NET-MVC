using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace Stolarstwo
{
    public class ConfigManager
    {
        private Dictionary<string, dynamic> _configDictionary = new();

        public dynamic ReadValue(string key)
        {
            UpdateDictionary();
            _configDictionary.TryGetValue(key, out dynamic value);
            return value;
        }
        public void ChangeValue(string key, dynamic value)
        {
            UpdateDictionary();

            if (_configDictionary.ContainsKey(key))
            {
                _configDictionary[key] = value;
            }
            else
            {
                _configDictionary.Add(key, value);
            }

            List<string> array = new();

            foreach (var pair in _configDictionary.ToArray())
            {
                array.Add(pair.ToString());
            }

            File.WriteAllLines("config.cfg", array);
        }

        private void UpdateDictionary()
        {
            if (File.Exists("config.cfg"))
            {
                var lines = File.ReadAllLines("config.cfg");

                foreach (var pair in lines)
                {
                    string line = pair.Trim('[', ']');

                    var keystring = line.Substring(0, line.IndexOf(','));
                    var valuestring = line.Substring(line.IndexOf(',') + 2, line.Length - line.IndexOf(',') - 2);

                    if (!_configDictionary.ContainsKey(keystring))
                    {
                        _configDictionary.Add(keystring, valuestring);
                    }
                }
            }
        }
    }
}
