namespace OOP.DesignPatterns
{
    public class SimulatorUDuck
    {
    }

    public interface Quackable
    {
        public void quack();
    }

    // concrete duck
    public class MallardDuck : Quackable
    {
        public void quack()
        {
            Console.WriteLine("Quack");
        }
    }

    public class RedheadDuck: Quackable
    {
        public void quack()
        {
            Console.WriteLine("Quack");
        }
    }

    public class DuckCall: Quackable
    {
        public void quack()
        {
            Console.WriteLine("kwack");
        }
    }

    // Khi vịt đã implement, nhưng ngỗng không thể
    public class Goose
    {
        public void honk()
        {
            Console.WriteLine("Honk");
        }
    }

    // Muốn có ngỗng xen kẽ với vịt (Sử dụng bộ chuyển đổi goose Adapter)
    public class GooseAdapter : Quackable
    {
        private readonly Goose _goose;
        public GooseAdapter(Goose goose)
        {
            _goose = goose;
        }

        // Khi quack() được invoke, this invoke will delegate(ủy thác) sang method honk() of goose object
        public void quack()
        {
            _goose.honk();
        }
    }

    // Decorator Làm cho các nhà nghiên cứu vui vẻ và đếm tiếng quack
    // DECORATE OBJECT ĐỂ TRANG TRÍ CÁC HÀNH VI
    // Giống như bạn mua thêm toping chè (Thêm sữa, thêm )
    public class QuackCounterDecorator : Quackable
    {
        Quackable _duck;
        static int numberOfQuacks;

        public QuackCounterDecorator(Quackable duck)
        {
            _duck = duck;
        }

        public void quack()
        {
            _duck.quack();
            numberOfQuacks++;
        }

        public static int getQuacks()
        {
            return numberOfQuacks;
        }
    }

    // Tại sao chúng ta không đưa việc tạo những con vịt ra một nơi khác? nói cách khác, hãy tách công việc tạo và trang trí vịt ra và đóng gói nó.
    // Cần 1 nhà máy sản xuất vịt ( chúng tôi cần kiểm soát chất lượng để đảm bảo rằng vịt của chúng tôi sẽ được bọc)
    // Nhà máy là sản xuất 1 bộ các sản phẩm includes các type duck khác nhau (use Abstract Factory pattern)
    // (Vì vậy, làm thế nào để bạn đảm bảo mỗi nhượng quyền đang sử dụng các thành phần chất lượng hay không? Bạn sẽ xây dựng một factory sản xuất chúng và chuyển chúng đến nhượng quyền của bạn!)

    // 1 trình giả lập Simulator
    public class DuckSimulator
    {
        public async void Main()
        {
            DuckSimulator simulator = new DuckSimulator();
            simulator.simulate();
        }

        void simulate()
        {
            Quackable mallarDuck = new MallardDuck();
            Quackable redheadDuck = new RedheadDuck();
            Quackable duckCall = new DuckCall();
            Quackable gooseDuck = new GooseAdapter(new Goose());
            simulate(mallarDuck);
            simulate(redheadDuck);
            simulate(duckCall);
            simulate(gooseDuck);
            Console.WriteLine("Duck Simulator");

            Quackable mallarDuckDecorator = new QuackCounterDecorator(new MallardDuck());
            simulate(mallarDuckDecorator);
            Console.WriteLine($"The ducks quacked {QuackCounterDecorator.getQuacks()} times");
        }

        void simulate(Quackable duck)
        {
            duck.quack();
        }
    }
}
