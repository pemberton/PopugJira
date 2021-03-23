using System;
using Mapster;
using TasksService.BO;

namespace TasksService.Db
{
    public class EntityToDbTypeMappingConfiguration
    {
        public static void InitConfig()
        {
            TypeAdapterConfig<PopugTask, PopugTaskDb>.NewConfig()
                //.TwoWays()
                //.Map(d => d.Creator, s => s.Creator.Id)
                //.Map(d => d.Assignee, s => s.Assignee.Id)
                ;
        }
    }
}