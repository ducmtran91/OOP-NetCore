using System.Collections;
using System;
using System.Collections.Generic;

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

    public class RedheadDuck : Quackable
    {
        public void quack()
        {
            Console.WriteLine("Redhead Quack");
        }
    }

    public class DuckCall : Quackable
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
    public abstract class AbstractDuckFactory {
        public abstract Quackable createMallardDuck();
        public abstract Quackable createRedheadDuck();
        public abstract Quackable createDuckCall();
    }

    // A Factory to create product
    public class DuckFactory: AbstractDuckFactory
    {
        public override Quackable createMallardDuck()
        {
            return new MallardDuck();
        }
        public override Quackable createRedheadDuck()
        {
            return new RedheadDuck();
        }
        public override Quackable createDuckCall()
        {
            return new DuckCall();
        }
    }

    public class CountingDuckFactory: AbstractDuckFactory {
        public override Quackable createMallardDuck()
        {
            return new QuackCounterDecorator(new MallardDuck());
        }
        public override Quackable createRedheadDuck()
        {
            return new QuackCounterDecorator(new RedheadDuck());
        }
        public override Quackable createDuckCall()
        {
            return new QuackCounterDecorator(new DuckCall());
        }
    }

    // Composite Pattern
    // Flock cụm, đám đông
    public class Flock: Quackable
    {
        ArrayList quackers = new ArrayList();

        public void add(Quackable quacker)
        {
            quackers.Add(quacker);
        }

        public void quack()
        {
        }
    }

    // 1 trình giả lập Simulator
    public class DuckSimulator
    {
        public async void Main()
        {
            DuckSimulator simulator = new DuckSimulator();
            simulator.simulate();

            simulator.simulateFactory(new CountingDuckFactory());
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

        void simulateFactory(AbstractDuckFactory abstractDuckFactory)
        {
            Quackable mallarDuck = abstractDuckFactory.createDuckCall();
            Quackable redheadDuck = abstractDuckFactory.createMallardDuck();
            Quackable duckCall = abstractDuckFactory.createRedheadDuck();
            Quackable gooseDuck = new GooseAdapter(new Goose());
            simulate(mallarDuck);
            simulate(redheadDuck);
            simulate(duckCall);
            simulate(gooseDuck);
            Console.WriteLine($"The ducks quacked {QuackCounterDecorator.getQuacks()} times");
        }

        void simulate(Quackable duck)
        {
            duck.quack();
        }
    }
}
