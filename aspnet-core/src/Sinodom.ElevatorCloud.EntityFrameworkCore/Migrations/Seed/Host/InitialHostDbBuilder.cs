using Sinodom.ElevatorCloud.EntityFrameworkCore;

namespace Sinodom.ElevatorCloud.Migrations.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly ElevatorCloudDbContext _context;

        public InitialHostDbBuilder(ElevatorCloudDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
            new DefaultDictsCreator(this._context).Create();

            _context.SaveChanges();
        }
    }
}
