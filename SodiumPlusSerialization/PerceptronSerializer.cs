using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using SodiumPlus.Perceptrons;
using SodiumPlus.Topology;
using SodiumPlus.UnitActivations;
using Newtonsoft.Json;
using System.Reflection;

namespace SodiumPlusSerialization
{
    public class PerceptronSerializer
    {
        /// <summary>
        /// Serializes a perceptron as JSON and returns the result.
        /// </summary>
        /// <typeparam name="TUnit">The declared type of the Unit</typeparam>
        /// <typeparam name="TConnection">The declared type of the connection</typeparam>
        /// <typeparam name="TUnitActivation">The declared type of the unit activation</typeparam>
        /// <param name="perceptron">The perceptron to be serialized</param>
        /// <returns>A string representation of the perceptron that can later be deserialized back into an IPerceptron</returns>
        public string SerializeJson<TUnit, TConnection, TUnitActivation>(IPerceptron<TUnit, TConnection, TUnitActivation> perceptron)
            where TUnit : IUnit
            where TConnection : IConnection
            where TUnitActivation : IUnitActivation<TUnit>
        {
            var unitMapping = new Dictionary<ITraversableUnitReadOnly<TUnit, TConnection, TUnitActivation>, SimpleUnit>();
            var simpleUnits = new List<List<SimpleUnit>>();
            foreach (var layer in perceptron.Network)
            {
                var simpleUnitLayer = new List<SimpleUnit>();
                foreach (var unit in layer)
                {
                    var simpleUnit = new SimpleUnit
                    {
                        UnitActivationType = unit.UnitActivation.GetType(),
                        UnitType = unit.UnitActivation.Properties.GetType(),
                        UnitPropertiesType = unit.UnitActivation.Properties.GetType()
                    };

                    MapPropertiesToDictionary(unit.UnitActivation.Properties, simpleUnit.Properties);

                    simpleUnitLayer.Add(simpleUnit);
                    unitMapping.Add(unit, simpleUnit);
                }
                simpleUnits.Add(simpleUnitLayer);
            }

            var simpleConnections = new List<SimpleConnection>();
            foreach (var connection in unitMapping.SelectMany(u => u.Key.IncomingConnections))
            {
                var inSimple = unitMapping[connection.InputUnit];
                var outSimple = unitMapping[connection.OutputUnit];
                var simpleConnection = new SimpleConnection
                {
                    InputUnit = inSimple,
                    OutputUnit = outSimple,
                    Weight = connection.Properties.Weight,
                    Name = connection.Properties.Name,
                    ConnectionType = connection.Properties.GetType()
                };

                MapPropertiesToDictionary(connection.Properties, simpleConnection.Properties);

                simpleConnections.Add(simpleConnection);
            }

            var data = new PerceptronJson
            {
                SimpleUnits = simpleUnits.Select(layer => layer.ToArray()).ToArray(),
                SimpleConnections = simpleConnections.ToArray(),
                PerceptronType = perceptron.GetType()
            };

            return JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            });
        }

        /// <summary>
        /// Deserialize a perceptron that was of the type IPerceptron&lt;IUnit, IConnection, IUnitActivation&lt;IUnit&gt;&gt;
        /// </summary>
        /// <param name="json">The JSON representation of the perceptron to be deserialized</param>
        /// <returns>An IPerceptron</returns>
        public IPerceptron<IUnit, IConnection, IUnitActivation<IUnit>> DeserializeJson(string json)
        {
            return DeserializeJson<IUnit, IConnection, IUnitActivation<IUnit>>(json);
        }

        /// <summary>
        /// Deserialize a perceptron
        /// </summary>
        /// <typeparam name="TUnit">The declared type of the Unit</typeparam>
        /// <typeparam name="TConnection">The declared type of the connection</typeparam>
        /// <typeparam name="TUnitActivation">The declared type of the unit activation</typeparam>
        /// <param name="json">The JSON representation of the perceptron to be deserialized</param>
        /// <returns>An IPerceptron</returns>
        public IPerceptron<TUnit, TConnection, TUnitActivation> DeserializeJson<TUnit, TConnection, TUnitActivation>(string json)
            where TUnit : IUnit
            where TConnection : IConnection
            where TUnitActivation : IUnitActivation<TUnit>
        {
            var simple = JsonConvert.DeserializeObject<PerceptronJson>(json, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            });

            var unitMapping = new Dictionary<SimpleUnit, ITraversableUnit<TUnit, TConnection, TUnitActivation>>();
            var network = new List<IEnumerable<ITraversableUnit<TUnit, TConnection, TUnitActivation>>>();
            foreach (var simpleLayer in simple.SimpleUnits)
            {
                var realLayer = new List<ITraversableUnit<TUnit, TConnection, TUnitActivation>>();
                foreach(var simpleUnit in simpleLayer)
                {
                    var unitActivation = Activator.CreateInstance(simpleUnit.UnitActivationType);
                    var createUnitMethod = typeof(TraversableUnit<TUnit, TConnection, TUnitActivation>)
                                            .GetMethods()
                                            .Single(m => m.Name == "CreateUnit" && m.GetParameters().Length > 0)
                                            .MakeGenericMethod(simpleUnit.UnitType, simpleUnit.UnitActivationType);
                    var traversableUnit = (ITraversableUnit<TUnit, TConnection, TUnitActivation>)createUnitMethod.Invoke(null, new[] { unitActivation });

                    MapDictionaryToProperties(simpleUnit.Properties, unitActivation);

                    if (unitActivation is IUnitActivationMultiFold<TUnit,TConnection ,TUnitActivation>)
                    {
                        ((IUnitActivationMultiFold<TUnit, TConnection, TUnitActivation>) unitActivation).Network = network;
                    }

                    unitMapping.Add(simpleUnit, traversableUnit);
                    realLayer.Add(traversableUnit);
                }

                network.Add(realLayer);
            }

            foreach (var simpleConnection in simple.SimpleConnections)
            {
                var createConnectionMethod = typeof(TraversableConnection<TUnit, TConnection, TUnitActivation>)
                                            .GetMethods()
                                            .Single(m => m.Name == "CreateConnection")
                                            .MakeGenericMethod(simpleConnection.ConnectionType);
                var inputUnit = unitMapping[simpleConnection.InputUnit];
                var outputUnit = unitMapping[simpleConnection.OutputUnit];

                var realConnection = (ITraversableConnection<TUnit, TConnection, TUnitActivation>)createConnectionMethod.Invoke(null, new object[] { inputUnit, outputUnit });
             
                MapDictionaryToProperties(simpleConnection.Properties, realConnection);
            }

            var perceptron = (IPerceptron<TUnit, TConnection, TUnitActivation>)simple.PerceptronType.GetConstructor(BindingFlags.NonPublic | BindingFlags.CreateInstance | BindingFlags.Instance, null, Type.EmptyTypes, null).Invoke(new object[] { });
            if (perceptron == null)
            {
                throw new SerializationException("Could not deserialize to perceptron. Ensure the perceptron type has a parameterless constructor (does not need to be public).");
            }
            perceptron.Network = network;
            return perceptron;
        }

        private static void MapPropertiesToDictionary(object propertiesProperty, IDictionary<string, object> properties)
        {
            var props = propertiesProperty.GetType().GetProperties();
            foreach (var prop in props.Where(p => p.CanRead && p.CanWrite && (p.PropertyType.IsPrimitive || p.PropertyType == typeof(string))))
            {
                properties[prop.Name] = prop.GetValue(propertiesProperty);
            }
        }

        private static void MapDictionaryToProperties(Dictionary<string, object> propMap, object parent)
        {
            var properties = parent.GetType().GetProperty("Properties").GetMethod.Invoke(parent, new object[] { });
            var props = properties.GetType().GetProperties();
            foreach (var prop in propMap)
            {
                props.Single(p => p.Name == prop.Key).SetMethod.Invoke(properties, new [] { prop.Value });
            }
        }
    }

    public class PerceptronJson
    {
        public SimpleUnit[][] SimpleUnits { get; set; }
        public SimpleConnection[] SimpleConnections { get; set; }
        public Type PerceptronType { get; set; }
    }

    public class SimpleUnit
    {
        public SimpleUnit()
        {
            Properties = new Dictionary<string, object>();
        }
        public Type UnitActivationType { get; set; }
        public Type UnitType { get; set; }
        public Dictionary<string, object> Properties { get; set; }
        public Type UnitPropertiesType { get; set; }
    }

    public class SimpleConnection
    {
        public SimpleConnection()
        {
            Properties = new Dictionary<string, object>();
        }
        public double Weight { get; set; }
        public SimpleUnit InputUnit { get; set; }
        public SimpleUnit OutputUnit { get; set; }
        public string Name { get; set; }
        public Type ConnectionType { get; set; }
        public Dictionary<string, object> Properties { get; set; }
    }
}
