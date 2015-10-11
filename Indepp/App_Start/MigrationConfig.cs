using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Indepp.App_Start
{
    public class MigrationConfig
    {
        public static void EnableCodeFirstMigrations()
        {
            if (bool.Parse(ConfigurationManager.AppSettings["MigrateDatabaseToLatestVersion"]))
            {
                var configuration = new Indepp.Migrations.Configuration();
                var migrator = new DbMigrator(configuration);
                migrator.Update();
            }
        }
    }
}