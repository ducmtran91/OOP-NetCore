using OOP.DesignPatterns;
var accountTest = new AccountTest();
await accountTest.Main();

var duckSimulator = new DuckSimulator();

duckSimulator.Main();

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/", () => "Hello World!");

app.Run();

public class DIContainer
{
    // Chứa các module & interface
    private static readonly Dictionary<Type, object> RegisteredModules = new Dictionary<Type, object>();

    public static void SetModule<TInterface, TModule>()
    {
        SetModule(typeof(TInterface), typeof(TModule));
    }

    public static T GetModule<T>()
    {
        return (T)GetModule(typeof(T));
    }

    private static void SetModule(Type interfaceType, Type moduleType)
    {
        // check module đã implement interface chưa
        if(!interfaceType.IsAssignableFrom(moduleType))
        {
            throw new Exception("Wrong Module type");
        }

        // Find constructor first
        var firstConstructor = moduleType.GetConstructors()[0];
        object module = null;
        // Nếu như k có parameters
        if (!firstConstructor.GetParameters().Any())
        {
            module = firstConstructor.Invoke(null);
        } else
        {
            // Lấy các tham số của constructor
            var constructorParamenters = firstConstructor.GetParameters(); // IDatabase, ILogger
            var moduleDependencies = new List<object>();
            foreach(var parameter in constructorParamenters)
            {
                var dependency = GetModule(parameter.ParameterType);
                moduleDependencies.Add(dependency);
            }
            // Inject các dependency vào constructor của module
            module = firstConstructor.Invoke(moduleDependencies.ToArray());
        }
        // Lưu trữ interface và module tương ứng
        RegisteredModules.Add(interfaceType, module);
    }

    private static object GetModule(Type interfaceType)
    {
        if(RegisteredModules.ContainsKey(interfaceType))
        {
            return RegisteredModules[interfaceType];
        }
        throw new Exception("Module not register");
    }
}