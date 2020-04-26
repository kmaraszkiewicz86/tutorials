package examples.union_find;

import examples.std.StdIn;
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

        System.out.println("Choose number p");

        var p = StdIn.readInt();

        System.out.println("Choose number q");

        var q = StdIn.readInt();

        Instant start = Instant.now();
        if (!uf.connected(p, q))
        {
            System.out.println(p + "!=" + q);

            System.out.println("Create new connection between p ang q");

            this.uf.union(p, q);

            System.out.println("Is union correctly match p and q => " + uf.connected(p, q));
        } else {
            System.out.println(p + "==" + q);
        }

        Instant end = Instant.now();
        Duration timeElapsed = Duration.between(start, end);
        System.out.println("Time taken: "+ timeElapsed.toMillis() +" milliseconds");

    }

    @Override
    protected void finalize() throws Throwable {
        this.uf = null;
    }
}