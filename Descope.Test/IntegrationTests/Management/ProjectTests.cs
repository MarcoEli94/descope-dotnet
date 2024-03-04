using Xunit;

namespace Descope.Test.Integration
{
    public class ProjectTests
    {
        private readonly DescopeClient _descopeClient = IntegrationTestSetup.InitDescopeClient();

        [Fact]
        public async Task Project_ExportImport()
        {
            var imported_project = await _descopeClient.Management.Project.Export();
            await _descopeClient.Management.Project.Import(imported_project);
        }

        [Fact(Skip = "Test fails due to theme import")]
        public async Task Project_CloneRenameDelete()
        {
            // Clone the current project
            var name = Guid.NewGuid().ToString().Split("-").First();
            var result = await _descopeClient.Management.Project.Clone(name, "");
            Assert.NotNull(result);

            // Delete cloned project
            await _descopeClient.Management.Project.Delete(result.ProjectId);

            // Rename
            var new_name = Guid.NewGuid().ToString().Split("-").First();
            await _descopeClient.Management.Project.Rename(new_name);

            // Rename again so we will have original name
            await _descopeClient.Management.Project.Rename("dotnet");
        }

    }
}
