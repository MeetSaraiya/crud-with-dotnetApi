using Microsoft.EntityFrameworkCore;
namespace crud_with_dotnetApi.Data
{
    public class EmployeeRepositery
    {
    private readonly AppDbContext _appDbContext;
        public EmployeeRepositery(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;           
        }
        public async Task AddEmployeeAsync(Employee employee)
        {
            await _appDbContext.Set<Employee>().AddAsync(employee);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await _appDbContext.Employees.ToListAsync();
        }

        public async Task<Employee> GetSingleEmployeeAsync(int id)
        {
            return await _appDbContext.Employees.FindAsync(id);

        }

        public async Task UpdateEmployeeAsync(int id,Employee model)
        {
            Employee emp;
            emp  = await _appDbContext.Employees.FindAsync(id);
            if(emp == null)
            {
                throw new Exception("Can't Fetch Employee");
            }
            emp.Salary= model.Salary;
            emp.Age = model.Age;
            emp.Phone = model.Phone;
            emp.Name = model.Name;
            _appDbContext.SaveChanges();
        }

        public async Task DeleteEmployee(int id)
        {
            Employee emp;
            emp = await _appDbContext.Employees.FindAsync(id);
            if (emp == null)
            {
                throw new Exception("Can't Fetch Employee");
            }
             _appDbContext.Employees.Remove(emp);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Employee>GetEmployeeByName(string name)
        {
            return await _appDbContext.Employees.Where(x => x.Name == name).FirstOrDefaultAsync();
        }
    }
}
