using FakeXrmEasy.Abstractions.Plugins;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FakeXrmEasy.Abstractions
{
    public interface IXrmFakedContext: IXrmBaseContext
    {
        ICallerProperties CallerProperties { get; set; }

        /// <summary>
        /// Receives a strong-typed entity type and returns a Queryable of that type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IQueryable<T> CreateQuery<T>() where T : Entity;
        IQueryable<Entity> CreateQuery(string logicalName);
        T GetEntityById<T>(Guid id) where T: Entity;
        Entity GetEntityById(string sLogicalName, Guid id);
        bool ContainsEntity(string sLogicalName, Guid id);

        /// <summary>
        /// Receives a list of entities, that are used to initialize the context with those
        /// </summary>
        /// <param name="entities"></param>
        void Initialize(IEnumerable<Entity> entities);
        
        /// Initializes the context with a single entity when only one is needed
        void Initialize(Entity entity);

        IXrmFakedPluginContextProperties PluginContextProperties { get; set; }

        void AddEntity(Entity e);
        void AddEntityWithDefaults(Entity e, bool clone = false, bool usePluginPipeline = false);
        Guid CreateEntity(Entity e);
        void UpdateEntity(Entity e);
        void DeleteEntity(EntityReference er);
        
        
        Type FindReflectedType(string logicalName);
        Type FindReflectedAttributeType(Type earlyBoundType, string sEntityName, string attributeName);

        void EnableProxyTypes(Assembly assembly);
        IEnumerable<Assembly> ProxyTypesAssemblies { get; }
        void InitializeMetadata(IEnumerable<EntityMetadata> entityMetadataList);
        void InitializeMetadata(EntityMetadata entityMetadata);
        void InitializeMetadata(Assembly earlyBoundEntitiesAssembly);
        IQueryable<EntityMetadata> CreateMetadataQuery();
        EntityMetadata GetEntityMetadataByName(string sLogicalName);
        void SetEntityMetadata(EntityMetadata em);
        void AddRelationship(string schemaname, XrmFakedRelationship relationship);
        void RemoveRelationship(string schemaname);
        XrmFakedRelationship GetRelationship(string schemaName);
        IEnumerable<XrmFakedRelationship> Relationships { get; }

        Guid GetRecordUniqueId(EntityReference record, bool validate = true);
    }
}