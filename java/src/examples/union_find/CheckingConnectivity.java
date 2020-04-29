package examples.union_find;

import examples.std.StdIn;
import examples.std.StdOut;

import java.time.Duration;
import java.time.Instant;

public class CheckingConnectivity
{
    private UF uf;

    public CheckingConnectivity(UF uf) {
        this.uf = uf;
    }

    public void doWork()
    {
        System.out.println("Checking algorithm with:");
        System.out.println(uf.getClass().getTypeName());

        while (true) {

            System.out.print("Choose number p: ");

            var p = StdIn.readInt();

            System.out.print("Choose number q: ");

            var q = StdIn.readInt();

            if (!uf.connected(p, q))
            {
                System.out.println(p + "!=" + q);

                System.out.println("Create new connection between p ang q");

                this.uf.union(p, q);

                System.out.println("Is union correctly match p and q => " + uf.connected(p, q));
            } else {
                System.out.println(p + "==" + q);
            }

            System.out.println("Continue? (y/n): ");
            String shouldContinue = StdIn.readString();

            System.out.println(shouldContinue.toLowerCase());
            System.out.println(shouldContinue.toLowerCase() == "y");

            if (shouldContinue.toLowerCase().equals("y")) {
                break;
            }
        }
    }

    @Override
    protected void finalize() throws Throwable {
        this.uf = null;
    }
}