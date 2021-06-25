namespace Json.Converters.Hierarchy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Common.DataAnnotations.Hierarchy;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class HierarchyJsonConverter : JsonConverter
    {
        private readonly IDictionary<Type, HierarchyDescriptor> _hierarchyDescriptors = new Dictionary<Type, HierarchyDescriptor>();



        public override bool CanWrite => false;



        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);

            if (token is not JObject jObject)
                return null;

            return Read(jObject, objectType, serializer);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.GetProperties().Any(x => x.GetCustomAttribute<HierarchyDiscriminatorAttribute>() != null);
        }



        private object Read(JObject jObject, Type objectType, JsonSerializer serializer)
        {
            if (!_hierarchyDescriptors.ContainsKey(objectType))
                _hierarchyDescriptors[objectType] = new HierarchyDescriptor(objectType);

            HierarchyDescriptor descriptor = _hierarchyDescriptors[objectType];

            var discriminatorValue = jObject
                .GetValue(descriptor.DiscriminatorProperty.Name, StringComparison.InvariantCultureIgnoreCase)
                .ToObject(descriptor.DiscriminatorProperty.PropertyType, serializer);

            if (descriptor.NestedTypes[discriminatorValue].GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly).Any(x => x.GetCustomAttribute<HierarchyDiscriminatorAttribute>() != null))
                return Read(jObject, descriptor.NestedTypes[discriminatorValue], serializer);

            object instance = Activator.CreateInstance(descriptor.NestedTypes[discriminatorValue]);

            serializer.Populate(jObject.CreateReader(), instance);

            return instance;
        }
    }

}
